using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Pioneer.Reveal.ElasticProxy.Api.Entites;
using Pioneer.Reveal.ElasticProxy.Api.Services;

namespace Pioneer.Reveal.ElasticProxy.Api.Controllers
{
    [Authorize]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly IUserService _userService;

        public UsersController(IUserService userService)
        {
            _userService = userService;
        }

        [AllowAnonymous]
        [HttpPost("authenticate")]
        public IActionResult Authenticate([FromBody]User user)
        {
            var response = _userService.Authenticate(user.Username, user.Password);

            if (response == null)
            {
                return BadRequest(new
                {
                    message = "Username or password is incorrect"
                });
            }

            return Ok(response);
        }
    }
}