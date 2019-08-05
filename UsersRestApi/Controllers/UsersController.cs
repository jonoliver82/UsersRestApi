using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using UsersRestApi.Domain;
using UsersRestApi.Factories;
using UsersRestApi.Interfaces;
using UsersRestApi.Models;
using UsersRestApi.Queries;
using UsersRestApi.Validaters;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace UsersRestApi.Controllers
{
    /// <summary>
    /// See https://blog.pragmatists.com/visit-your-domain-objects-to-keep-em-legit-6b5d43e98ef0
    /// And https://github.com/Pragmatists/DDD-validation/blob/master/src/main/java/com/ddd/validation/application/UsersEndpoint.java
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
        /// GET api/<controller>/5
        /// GET https://localhost:44357/api/users/1
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("{id}")]
        public ActionResult<Email> GetEmail(int id)
        {
            var maybeEmail = _usersFinderService.FindUserEmailById(id);
            return maybeEmail.Select<ActionResult>(
                empty: () => NotFound(),
                present: (email) => Ok(email));
        }

        /// <summary>
        /// Register a new user
        /// 
        /// POST api/<controller>
        /// 
        /// POST https://localhost:44357/api/users
        /// application/json
        /// {
        ///   "Name":"name",
        ///   "Email":"email@example.com",
        ///   "Password":"password"
        /// }
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
