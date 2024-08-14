using SC.Web.Models;

namespace SC.Web.Service.IService
{
    public interface IAuthService
    {
        Task<ResponseDto> Register(RegistrationRequestDto model);
        Task<ResponseDto> Login(LoginRequestDto loginRequestDto);
        Task<ResponseDto> AssignRole(RegistrationRequestDto model);
    }
}
