using SC.Services.OrderAPI.Models.Dto;

namespace SC.Services.OrderAPI.Service.IService
{
    public interface IProductService
    {
        Task<IEnumerable<ProductDto>> GetProducts();
    }
}
