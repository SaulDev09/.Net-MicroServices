using SC.Services.ShoppingCartAPI.Models.Dto;

namespace SC.Services.ShoppingCartAPI.Service.IService
{
    public interface ICouponService
    {
        Task<CouponDto> GetCoupon(string couponCode);
    }
}
