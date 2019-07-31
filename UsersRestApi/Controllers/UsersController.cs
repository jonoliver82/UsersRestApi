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

        public UsersController(IUserRepository userRepository, 
            IUsersFinderService usersFinderService,
            IUserFactory userFactory)
        {
            _userRepository = userRepository;
            _usersFinderService = usersFinderService;
            _userFactory = userFactory;
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
            // TODO FindUserEmailById should return a Maybe
            return Ok(_usersFinderService.FindUserEmailById(id));
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
            var errors = ValidateRequest(request);
            if (errors.Any())
            {
                return BadRequest(UserCreationResponse.Failure(errors));
            }
            else
            {
                var newUser = SaveNewUser(request.Name,  request.Email, request.Password);
                return CreatedAtAction(nameof(GetEmail), new { id = newUser.Id }, UserCreationResponse.Success(newUser.Id));
            }            
        }

        private IEnumerable<string> ValidateRequest(UserCreationRequest request)
        {
            var validationExceptionHandler = new AggregatingValidationExceptionHandler();
            Email.Test(request.Email, validationExceptionHandler);
            Password.Test(request.Password, validationExceptionHandler);
            if(validationExceptionHandler.HasErrors())
            {
                return validationExceptionHandler.GetErrors();
            }

            UserFactory.Test(Email.Of(request.Email), validationExceptionHandler);
            if (validationExceptionHandler.HasErrors())
            {
                return validationExceptionHandler.GetErrors();
            }

            // TODO equivalent of Collections.emptyList()
            return new List<string>();
        }

        private User SaveNewUser(string name, string email, string password)
        {
            // TODO Of() rationale
            var user = _userFactory.Create(name, Email.Of(email), Password.Of(password));
            _userRepository.Add(user);
            return user;
        }
    }
}
