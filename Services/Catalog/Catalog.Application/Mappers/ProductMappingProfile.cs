using AutoMapper;
using Catalog.Application.Responses;
using Catalog.Core.Entities;
using Catalog.Core.Specs;

namespace Catalog.Application.Mappers
{
    public class ProductMappingProfile : Profile
    {
        public ProductMappingProfile()
        {
            CreateMap<ProductBrand, BrandResponseDto>();
            CreateMap<ProductType, TypeResponseDto>();
            CreateMap<Pagination<Product>, Pagination<ProductResponseDto>>();
            CreateMap<Product, ProductResponseDto>()
                .ForMember(p => p.Brand, op => op.MapFrom(b => b.Brand))
                .ForMember(p => p.Type, op => op.MapFrom(t => t.Type))
                .ReverseMap();
        }
    }
}
