using SC.Web.Models;
using SC.Web.Service.IService;
using SC.Web.Utility;
using static SC.Web.Utility.SD;

namespace SC.Web.Service
{
    public class AuthService : IAuthService
    {
        private readonly IBaseService _baseService;
        public AuthService(IBaseService baseService)
        {
            _baseService = baseService;
        }

        public async Task<ResponseDto> AssignRole(RegistrationRequestDto model)
        {
            return await _baseService.SendAsync(new RequestDto()
            {
                ApiType = ApiType.POST,
                Data = model,
                Url = AuthAPIBase + "/api/AuthAPI/AssignRole"
            });
        }

        public async Task<ResponseDto> Login(LoginRequestDto loginRequestDto)
        {
            return await _baseService.SendAsync(new RequestDto()
            {
                ApiType = ApiType.POST,
                Data = loginRequestDto,
                Url = AuthAPIBase + "/api/AuthAPI/login"
            });
        }

        public async Task<ResponseDto> Register(RegistrationRequestDto model)
        {
            return await _baseService.SendAsync(new RequestDto()
            {
                ApiType = ApiType.POST,
                Data = model,
                Url = AuthAPIBase + "/api/AuthAPI/register"
            });
        }
    }
}
