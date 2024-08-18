using AutoMapper;
using SC.Services.ProductAPI.Models;
using SC.Services.ProductAPI.Models.Dto;

namespace SC.Services.ProductAPI
{
    public class MappingConfig
    {
        public static MapperConfiguration RegisterMaps()
        {
            var mapingConfig = new MapperConfiguration(config =>
            {
                //config.CreateMap<ProductDto, Product>().ReverseMap();
                config.CreateMap<ProductDto, Product>();
                config.CreateMap<Product, ProductDto>();
            });
            return mapingConfig;
        }
    }
}
