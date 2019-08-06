// **********************************************************************************
// Filename					- UsersController.cs
// Copyright (c) jonoliver82, 2019
// **********************************************************************************

using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using UsersRestApi.Domain;
using UsersRestApi.Interfaces;
using UsersRestApi.Models;

namespace UsersRestApi.Controllers
{
    /// <summary>
    /// See https://blog.pragmatists.com/visit-your-domain-objects-to-keep-em-legit-6b5d43e98ef0
    /// And https://github.com/Pragmatists/DDD-validation/blob/master/src/main/java/com/ddd/validation/application/UsersEndpoint.java.
    /// </summary>
    [Route("api/[controller]")]
    public class UsersController : Controller
    {
        private readonly IUserRepository _userRepository;
        private readonly IUsersFinderService _usersFinderService;
        private readonly IUserFactory _userFactory;
        private readonly IValidationExceptionHandler _validationExceptionHandler;

        public UsersController(IUserRepository userRepository,
            IUsersFinderService usersFinderService,
            IUserFactory userFactory,
            IValidationExceptionHandler validationExceptionHandler)
        {
            _userRepository = userRepository;
            _usersFinderService = usersFinderService;
            _userFactory = userFactory;
            _validationExceptionHandler = validationExceptionHandler;
        }

        /// <summary>
        /// Get the email address of a registered user
        ///
        /// GET api/{controller}/5
        /// GET https://localhost:44357/api/users/1.
        /// </summary>
        [HttpGet("{id}")]
        public ActionResult<Email> GetEmail(int id)
        {
            var maybeEmail = _usersFinderService.FindUserEmailById(id);
            return maybeEmail.Select<ActionResult>(
                empty: () => NotFound(),
                present: (email) => Ok(email));
        }

        /// <summary>
        /// Registers a new user.
        ///
        /// POST https://localhost:44357/api/users
        /// application/json
        /// {
        ///   "Name":"name",
        ///   "Email":"email@example.com",
        ///   "Password":"password"
        /// }
        ///
        /// POST api/{controller}.
        /// </summary>
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        public ActionResult<UserCreationResponse> Create([FromBody]UserCreationRequest request)
        {
            // Visitor: validationExceptionHandler
            // Visitee: value objects
            // TODO consider pass in a validator instead
            Email.Accept(request.Email, _validationExceptionHandler);
            Password.Accept(request.Password, _validationExceptionHandler);

            if (_validationExceptionHandler.HasErrors)
            {
                return BadRequest(UserCreationResponse.Failure(_validationExceptionHandler.Errors));
            }

            // Note we dont visit the factory method until after the value objects have been created
            var maybeUser = _userFactory.Create(request.Name,
                new Email(request.Email),
                new Password(request.Password),
                _validationExceptionHandler);

            return maybeUser.Select<ActionResult>(
                empty: () => BadRequest(UserCreationResponse.Failure(_validationExceptionHandler.Errors)),
                present: (user) =>
                {
                    _userRepository.Add(user);
                    return CreatedAtAction(nameof(GetEmail), new { id = user.Id }, UserCreationResponse.Success(user.Id));
                });
        }
    }
}
