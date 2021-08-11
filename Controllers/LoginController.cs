using CampaignMgmt.Models;
using CampaignMgmt.Repository;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CampaignMgmt.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LoginController : ControllerBase
    {
        private readonly ILoginRepository _loginRepository;
        public LoginController(ILoginRepository repository)
        {
            _loginRepository = repository;
        }
        // GET: api/<LoginController>
        [HttpGet]
        public async Task<ActionResult<IEnumerable<User>>> Get()
        {
            var users = await _loginRepository.GetAll();

            return Ok(users);
        }
        [AllowAnonymous]
        [Route("Authenticate")]
        [HttpPost]
        public ActionResult Login([FromBody] User user)
        {
            var token = _loginRepository.Authenticate(user.UserName, user.Password);
            if (token == null)
                return Unauthorized();
            return Ok(new { token, user });

        }

    }
}
