using System.Threading.Tasks;
using FridgeApp.Application.Exceptions;
using FridgeApp.Domain.Repositories;
using FridgeApp.Shared.Abstractions.Commands;

namespace FridgeApp.Application.Commands.Handlers
{
    public record RemoveFridgeProductHandler : ICommandHandler<RemoveFridgeProduct>
    {
        private readonly IFridgeRepository _repository;

        public RemoveFridgeProductHandler(IFridgeRepository repository)
            => _repository = repository;
        
        public async Task HandleAsync(RemoveFridgeProduct command)
        {
            var (fridgeId, productId) = command;
            
            var fridge = await _repository.GetAsync(fridgeId);

            if (fridge is null)
            {
                throw new FridgeNotFoundException(fridgeId);
            }

            fridge.RemoveProduct(productId);
        }
    }
}