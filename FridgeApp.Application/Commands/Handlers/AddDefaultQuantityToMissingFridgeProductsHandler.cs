using System.Threading.Tasks;
using FridgeApp.Application.Exceptions;
using FridgeApp.Application.Services;
using FridgeApp.Domain.Repositories;
using FridgeApp.Shared.Abstractions.Commands;

namespace FridgeApp.Application.Commands.Handlers
{
    public class AddDefaultQuantityToMissingFridgeProductsHandler : ICommandHandler<AddDefaultQuantityToMissingFridgeProducts>
    {
        private readonly IProductClientService _productClient;
        private readonly IFridgeRepository _fridgeRepository;
        private readonly IProductRepository _productRepository;

        public AddDefaultQuantityToMissingFridgeProductsHandler(IProductClientService productClient, IFridgeRepository fridgeRepository, IProductRepository productRepository)
        {
            _productClient = productClient;
            _fridgeRepository = fridgeRepository;
            _productRepository = productRepository;
        }


        public async Task HandleAsync(AddDefaultQuantityToMissingFridgeProducts command)
        {
            var (fridgeId, productId, quantity) = command;
            
            var fridge = await _fridgeRepository.GetAsync(fridgeId);
            if (fridge is null)
            {
                throw new FridgeNotFoundException(fridgeId);
            }

            var product = await _productRepository.GetAsync(productId);
            if (product is null)
            {
                throw new ProductNotFoundByIdException(productId);
            }
            
            await _productClient.PutProduct(fridgeId, productId, quantity);
        }
    }
}