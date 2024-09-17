﻿using SC.Web.Models;
using SC.Web.Service.IService;
using static SC.Web.Utility.SD;

namespace SC.Web.Service
{
    public class OrderService : IOrderService
    {
        private readonly IBaseService _baseService;

        public OrderService(IBaseService baseService)
        {
            _baseService = baseService;
        }

        public async Task<ResponseDto?> CreateOrder(CartDto cartDto)
        {
            return await _baseService.SendAsync(new RequestDto()
            {
                ApiType = ApiType.POST,
                Data = cartDto,
                Url = OrderAPIBase + "/api/OrderAPI/CreateOrder"
            });
        }

        public async Task<ResponseDto?> CreateStripeSession(StripeRequestDto stripeRequestDto)
        {
            return await _baseService.SendAsync(new RequestDto()
            {
                ApiType = ApiType.POST,
                Data = stripeRequestDto,
                Url = OrderAPIBase + "/api/OrderAPI/CreateStripeSession"
            });
        }

        public async Task<ResponseDto?> ValidateStripeSession(int orderHeaderId)
        {
            return await _baseService.SendAsync(new RequestDto()
            {
                ApiType = ApiType.POST,
                Data = orderHeaderId,
                Url = OrderAPIBase + "/api/OrderAPI/ValidateStripeSession"
            });
        }
    }
}
