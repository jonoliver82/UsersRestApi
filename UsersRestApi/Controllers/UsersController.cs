using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
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
        private readonly IUserIdFactory _userIdFactory;
        private readonly IUsersFinderService _usersFinderService;

        public UsersController(IUserRepository userRepository, IUserIdFactory userIdFactory, IUsersFinderService usersFinderService)
        {
            _userRepository = userRepository;
            _userIdFactory = userIdFactory;
            _usersFinderService = usersFinderService;
        }

        // GET api/<controller>/5
        [HttpGet("{id}")]
        public ActionResult<string> Get(string id)
        {
            return Ok(_usersFinderService.FindUserEmailById(UserId.Of(id)).ToString());
        }

        // POST api/<controller>
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        public ActionResult<UserCreationResponse> Post([FromBody]UserCreationRequest value)
        {
            var newUser = new User(_userIdFactory.Create(), new Email(value.Email), new Password(value.Password));
            _userRepository.Add(newUser);
            return CreatedAtAction(nameof(Get), UserCreationResponse.Success(newUser.Id.ToString()));
        }
    }
}
