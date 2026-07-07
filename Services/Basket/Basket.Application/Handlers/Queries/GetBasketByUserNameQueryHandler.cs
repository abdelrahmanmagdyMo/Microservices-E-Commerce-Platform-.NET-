using AutoMapper;
using Basket.Application.Queries;
using Basket.Application.Responses;
using Basket.Core.Repositories;
using MediatR;

namespace Basket.Application.Handlers.Queries
{
    public class GetBasketByUserNameQueryHandler(IMapper mapper, IBasketRepository repository) : IRequestHandler<GetBasketByUserNameQuery, ShoppingCartResponse>
    {
        public async Task<ShoppingCartResponse> Handle(GetBasketByUserNameQuery request, CancellationToken cancellationToken)
        {
            var basket = await repository.GetBasket(request.UserName);
            var basketResponse = mapper.Map<ShoppingCartResponse>(basket);
            return basketResponse;
        }
    }
}
