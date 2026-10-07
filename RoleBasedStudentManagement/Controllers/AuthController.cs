using RoleBasedStudentManagement.Services.Interfaces;
using RoleBasedStudentManagement.DTOs.Auth;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization; 


namespace RoleBasedStudentManagement.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthController : ControllerBase
    {
        
        private readonly IAuthService _authService;
        public AuthController (IAuthService authService)
        {
            _authService = authService;
        }
        [HttpPost("register")]
        public IActionResult Register(RegisterDTO registerDTO)
        {
            try
            {
                _authService.Register(registerDTO);
                return Ok("registered successfull");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost("login")]
        public IActionResult Login(LoginDTO loginDTO)
        {
            var result = _authService.Login(loginDTO);
            if(result == null)
            {
                return Unauthorized("Invalid Email or Password");
            }
            return Ok(result);
        }

        [Authorize]
        [HttpGet("me")]
        public IActionResult Me()
        {
            return Ok("ur authenticated");
        }


    }
}
