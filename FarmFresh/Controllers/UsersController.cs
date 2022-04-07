using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using System.Threading.Tasks;
using System.Linq;
using FarmFresh.Model;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using System.Security.Claims;
using System.IdentityModel.Tokens.Jwt;
using System;
using FarmFresh.Services;

namespace FarmFresh.Controllers
{
    //[Authorize]
    [ApiController]
    [Route("api/[controller]")]
    public class UsersController : ControllerBase
    {
        private IAuthenticationService _authenticationService;

        public UsersController(IAuthenticationService authenticationService)
        {
            _authenticationService = authenticationService;
        }

        [AllowAnonymous]
        [HttpPost("Auth")]
        public IActionResult GetAuth([FromBody] UserLogin userLogin)
        {
            var user = _authenticationService.AuthenticateUser(userLogin);

            if (user != null)
            {
                var token = _authenticationService.GenerateJwtToken(user);
                return Ok(token);
            }

            return NotFound("User not found");
        }
    }
}
