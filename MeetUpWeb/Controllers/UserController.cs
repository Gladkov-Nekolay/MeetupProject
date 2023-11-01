using MeetUpCore.Models;
using MeetUpCore.ServiceCore.Users;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace MeetUpWeb.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserManager _userService;
        public UserController(IUserManager userService)
        {
            _userService = userService;
        }
        [AllowAnonymous]
        [Route("[action]")]
        [HttpPost]
        public async Task<ActionResult> CreateUserAsync(UserCreationModel model)
        {
            if (!ModelState.IsValid) 
            { 
                return ValidationProblem(); 
            }

            await _userService.RegisterAsync(model);
            return new OkResult();
        }
        [Route("[action]")]
        [AllowAnonymous]
        [HttpPost]
        public async Task<IActionResult> Login(UserAuthentificationModel model) 
        {
            if (!ModelState.IsValid) 
            {
                return ValidationProblem(); 
            }

            return Ok(await _userService.LoginUserAsync(model));
        }
    }
}