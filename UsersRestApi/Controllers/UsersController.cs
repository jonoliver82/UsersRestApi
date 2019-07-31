using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using UsersRestApi.Domain;
using UsersRestApi.Interfaces;
using UsersRestApi.Models;

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
        public ActionResult<UserCreationResponse> Post([FromBody]UserCreationRequest value)
        {
            var newUser = _userFactory.Create(value.Name, new Email(value.Email), new Password(value.Password));            
            _userRepository.Add(newUser);
            return CreatedAtAction(nameof(GetEmail), new { id = newUser.Id }, UserCreationResponse.Success(newUser.Id));
        }
    }
}
