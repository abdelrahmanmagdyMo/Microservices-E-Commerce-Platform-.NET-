using AutoMapper;
using Catalog.Application.Queries;
using Catalog.Application.Responses;
using Catalog.Core.Repositories;
using MediatR;

namespace Catalog.Application.Handlers.Queries
{
    public class GetAllProductsByNameQueryHandler : IRequestHandler<GetAllProductsByNameQuery, IList<ProductResponseDto>>
    {
        private readonly IMapper _mapper;
        private readonly IProductRepository _productRepository;
        public GetAllProductsByNameQueryHandler(IMapper mapper, IProductRepository productRepository)
        {
            _mapper = mapper;
            _productRepository = productRepository;
        }
        public async Task<IList<ProductResponseDto>> Handle(GetAllProductsByNameQuery request, CancellationToken cancellationToken)
        {
            var productList = await _productRepository.GetAllProductsByName(request.Name);
            var productListResponse = _mapper.Map<IList<ProductResponseDto>>(productList);
            return productListResponse;
        }
    }
}
