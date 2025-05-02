using SC.Web.Models;

namespace SC.Web.Service.IService
{
    public interface ICartService
    {
        Task<ResponseDto?> GetCart(string userId);
        Task<ResponseDto?> UpsertCart(CartDto cartDto);
        Task<ResponseDto?> RemoveCart(int cartDetailsId);
        Task<ResponseDto?> ApplyCoupon(CartDto cartDto);
        Task<ResponseDto?> EmailCartRequest(CartDto cartDto);
    }
}
