using SC.Services.EmailAPI.Models.Dto;

namespace SC.Services.EmailAPI.Services
{
    public interface IEmailService
    {
        Task EmailCartAndLog(CartDto cartDto);
    }
}
