using System.Threading.Tasks;
using ITSchool.Core.DTOs;
using ITSchool.Core.Services;
using Microsoft.AspNetCore.Mvc;

namespace ITSchool.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IAuthService _authService;
        
        public AuthController(IAuthService authService)
        {
            _authService = authService;
        }
        
        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginDto loginDto)
        {
            var result = await _authService.Login(loginDto);
            
            if (result == null)
            {
                return Unauthorized(new { message = "Invalid username or password" });
            }
            
            return Ok(result);
        }
    }
}
