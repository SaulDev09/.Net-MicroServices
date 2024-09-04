using SC.Web.Models;
using SC.Web.Service.IService;
using static SC.Web.Utility.SD;

namespace SC.Web.Service
{
    public class CartService : ICartService
    {
        private readonly IBaseService _baseService;

        public CartService(IBaseService baseService)
        {
            _baseService = baseService;
        }

        public async Task<ResponseDto?> ApplyCoupon(CartDto cartDto)
        {
            return await _baseService.SendAsync(new RequestDto()
            {
                ApiType = ApiType.POST,
                Data = cartDto,
                Url = CartAPIBase + "/api/CartAPI/ApplyCoupon"
            });
        }

        public async Task<ResponseDto?> GetCart(string userId)
        {
            return await _baseService.SendAsync(new RequestDto()
            {
                ApiType = ApiType.GET,
                Url = CartAPIBase + "/api/CartAPI/GetCart/" + userId
            });
        }

        public async Task<ResponseDto?> RemoveCart(int cartDetailsId)
        {
            return await _baseService.SendAsync(new RequestDto()
            {
                ApiType = ApiType.POST,
                Data = cartDetailsId,
                Url = CartAPIBase + "/api/CartAPI/RemoveCart"
            });
        }

        public async Task<ResponseDto?> UpsertCart(CartDto cartDto)
        {
            return await _baseService.SendAsync(new RequestDto()
            {
                ApiType = ApiType.POST,
                Data = cartDto,
                Url = CartAPIBase + "/api/CartAPI/CartUpsert"
            });
        }

        public async Task<ResponseDto?> EmailCartRequest(CartDto cartDto)
        {
            return await _baseService.SendAsync(new RequestDto()
            {
                ApiType = ApiType.POST,
                Data = cartDto,
                Url = CartAPIBase + "/api/CartAPI/EmailCartRequest"
            });
        }
    }
}
