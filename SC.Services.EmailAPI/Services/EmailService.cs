using Microsoft.EntityFrameworkCore;
using SC.Services.EmailAPI.Data;
using SC.Services.EmailAPI.Message;
using SC.Services.EmailAPI.Models;
using SC.Services.EmailAPI.Models.Dto;
using System.Text;

namespace SC.Services.EmailAPI.Services
{
    public class EmailService : IEmailService
    {
        private DbContextOptions<AppDbContext> _dbOptions;

        public EmailService(DbContextOptions<AppDbContext> dbOptions)
        {
            _dbOptions = dbOptions;
        }

        public async Task EmailCartAndLog(CartDto cartDto)
        {
            StringBuilder message = new StringBuilder();

            message.AppendLine("<br/>Cart Email Requested");
            message.AppendLine("<br/>Total" + cartDto.CartHeader.CartTotal);
            message.AppendLine("<br/>");
            message.AppendLine("<ul>");
            foreach (var item in cartDto.CartDetails)
            {
                message.AppendLine("<li>");
                message.AppendLine(item.Product.Name + " x " + item.Count);
                message.AppendLine("</li>");
            }
            message.AppendLine("</ul>");
            await LogAndEmail(message.ToString(), cartDto.CartHeader.Email);
        }

        public async Task EmailRegisterUserAndLog(string email)
        {
            StringBuilder message = new StringBuilder();

            message.AppendLine("<br/>User Registered");
            message.AppendLine("<br/>User:" + email);
            message.AppendLine("<br/>");
            await LogAndEmail(message.ToString(), email);
        }

        private async Task<bool> LogAndEmail(string message, string email)
        {
            try
            {
                EmailLogger emailLog = new()
                {
                    Emai = email,
                    EmailSent = DateTime.Now,
                    Message = message
                };
                await using var _db = new AppDbContext(_dbOptions);
                await _db.EmailLoggers.AddAsync(emailLog);
                await _db.SaveChangesAsync();
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        public async Task LogOrderPlaced(RewardsMessage rewardsDto)
        {
            string message = "New Order Placed, <br/> Order Id: " + rewardsDto.OrderId;
            await LogAndEmail(message, "Saul.Dev09@gmail.com");
        }

    }
}
