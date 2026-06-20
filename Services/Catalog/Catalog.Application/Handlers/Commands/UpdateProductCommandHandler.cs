using AutoMapper;
using Catalog.Application.Commands;
using Catalog.Core.Entities;
using Catalog.Core.Repositories;
using MediatR;

namespace Catalog.Application.Handlers.Commands
{
    public class UpdateProductCommandHandler : IRequestHandler<UpdateProductCommand, bool>
    {

        private readonly IProductRepository _productRepository;
        public UpdateProductCommandHandler(IMapper mapper, IProductRepository productRepository)
        {
            _productRepository = productRepository;
        }
        public async Task<bool> Handle(UpdateProductCommand request, CancellationToken cancellationToken)
        {
            var productEntity = await _productRepository.UpdateProduct(new Product
            {
                Id = request.Id,
                Brand = request.Brand,
                Description = request.Description,
                ImageFile = request.ImageFile,
                Name = request.Name,
                Price = request.Price,
                Summary = request.Summary,
                Type = request.Type
            });
            return productEntity;
        }
    }
}
