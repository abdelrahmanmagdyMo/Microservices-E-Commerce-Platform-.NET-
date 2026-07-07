using AutoMapper;
using Basket.Application.Commands;
using Basket.Application.Responses;
using Basket.Core.Entities;
using Basket.Core.Repositories;
using MediatR;

namespace Basket.Application.Handlers.Commands
{
    public class CreateShoppingCartCommandHandler(IMapper mapper, IBasketRepository repository) : IRequestHandler<CreateShoppingCartCommand, ShoppingCartResponse>
    {
        public async Task<ShoppingCartResponse> Handle(CreateShoppingCartCommand request, CancellationToken cancellationToken)
        {

            var basketEntity = await repository.UpdateBasket(new ShoppingCart
            {
                UserName = request.UserName.Trim(),
                Items = request.Items
            });
            var basketResponse = mapper.Map<ShoppingCartResponse>(basketEntity);
            return basketResponse;
        }
    }
}
