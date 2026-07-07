using Basket.Application.Commands;
using Basket.Core.Repositories;
using MediatR;

namespace Basket.Application.Handlers.Commands
{
    public class DeleteBasketByUserNameCommandHandler(IBasketRepository repository) : IRequestHandler<DeleteBasketByUserNameCommand, Unit>
    {
        public async Task<Unit> Handle(DeleteBasketByUserNameCommand request, CancellationToken cancellationToken)
        {
            await repository.DeleteBasket(request.UserName);
            return Unit.Value;
        }
    }
}
