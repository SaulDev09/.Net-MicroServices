using Microsoft.AspNetCore.Mvc;
using SC.Services.AuthAPI.Models.Dto;
using SC.Services.AuthAPI.Service.IService;

namespace SC.Services.AuthAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthAPIController : ControllerBase
    {
        private readonly IAuthService _authService;
        protected ResponseDto _responseDto;

        public AuthAPIController(IAuthService authService)
        {
            _authService = authService;
            _responseDto = new();
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] RegistrationRequestDto model)
        {
            var errorMessate = await _authService.Register(model);
            if (!string.IsNullOrEmpty(errorMessate))
            {
                _responseDto.IsSuccess = false;
                _responseDto.Message = errorMessate;
                return BadRequest(_responseDto);
            }
            return Ok(_responseDto);
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginRequestDto loginRequestDto)
        {
            var loginResponse = await _authService.Login(loginRequestDto);
            if (loginResponse.User == null)
            {
                _responseDto.IsSuccess = false;
                _responseDto.Message = "Username or password is incorrect";
                return BadRequest(_responseDto);
            }

            _responseDto.Result = loginResponse;
            return Ok(_responseDto);
        }

        [HttpPost("AssignRole")]
        public async Task<IActionResult> AssignRole([FromBody] RegistrationRequestDto model)
        {
            var assignRoleSuccess = await _authService.AssignRole(model.Email, model.Role.ToUpper());
            if (!assignRoleSuccess)
            {
                _responseDto.IsSuccess = false;
                _responseDto.Message = "Error enconuntered ";
                return BadRequest(_responseDto);
            }
            return Ok(_responseDto);
        }
    }
}
