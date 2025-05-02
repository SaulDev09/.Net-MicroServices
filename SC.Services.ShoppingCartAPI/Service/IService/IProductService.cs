using SC.Services.ShoppingCartAPI.Models.Dto;

namespace SC.Services.ShoppingCartAPI.Service.IService
{
    public interface IProductService
    {
        Task<IEnumerable<ProductDto>> GetProducts();
    }
}
