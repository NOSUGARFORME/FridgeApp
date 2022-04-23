using System.Linq;
using System.Threading.Tasks;
using FridgeApp.Application.Exceptions;
using FridgeApp.Application.Services;
using FridgeApp.Domain.Exceptions;
using FridgeApp.Domain.Repositories;
using FridgeApp.Shared.Abstractions.Commands;

namespace FridgeApp.Application.Commands.Handlers
{
    public record RemoveFridgeProductHandler : ICommandHandler<RemoveFridgeProduct>
    {
        private readonly IFridgeRepository _repository;
        private readonly IFridgeWriteService _writeService; 

        public RemoveFridgeProductHandler(IFridgeRepository repository, IFridgeWriteService writeService)
        {
            _repository = repository;
            _writeService = writeService;
        }

        public async Task HandleAsync(RemoveFridgeProduct command)
        {
            var (fridgeId, productId) = command;
            
            var fridge = await _repository.GetAsync(fridgeId);

            if (fridge is null)
            {
                throw new FridgeNotFoundException(fridgeId);
            }

            if (fridge.FridgeProducts.All(fp => fp.Product.Id.Value != productId))
            {
                throw new ProductNotFoundInFridgeException(fridgeId, productId);
            }

            await _writeService.RemoveProduct(fridgeId, productId);
        }
    }
}