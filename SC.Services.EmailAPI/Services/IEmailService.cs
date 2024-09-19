using SC.Services.EmailAPI.Message;
using SC.Services.EmailAPI.Models.Dto;

namespace SC.Services.EmailAPI.Services
{
    public interface IEmailService
    {
        Task EmailCartAndLog(CartDto cartDto);
        Task EmailRegisterUserAndLog(string email);
        Task LogOrderPlaced(RewardsMessage rewardsDto);
    }
}
