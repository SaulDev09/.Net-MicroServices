using Microsoft.AspNetCore.Mvc;
using SC.Web.Models;

namespace SC.Web.Service.IService
{
    public interface IOrderService
    {
        Task<ResponseDto?> CreateOrder(CartDto cartDto);
        Task<ResponseDto?> CreateStripeSession(StripeRequestDto stripeRequestDto);
        Task<ResponseDto?> ValidateStripeSession(int orderHeaderId);
        Task<ResponseDto?> Get(string? userId);
        Task<ResponseDto?> GetOrder(int id);
        Task<ResponseDto?> UpdateOrderStatus(int orderId, string newStatus);
    }
}
