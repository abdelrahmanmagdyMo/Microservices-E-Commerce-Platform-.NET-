using AutoMapper;
using Catalog.Application.Responses;
using Catalog.Core.Entities;

namespace Catalog.Application.Mappers
{
    public class ProductMappingProfile : Profile
    {
        public ProductMappingProfile()
        {
            CreateMap<ProductBrand, BrandResponseDto>();
            CreateMap<ProductType, TypeResponseDto>();
            CreateMap<Product, ProductResponseDto>()
                .ForMember(p => p.Brand, op => op.MapFrom(b => b.Brand))
                .ForMember(p => p.Type, op => op.MapFrom(t => t.Type))
                .ReverseMap();
        }
    }
}
