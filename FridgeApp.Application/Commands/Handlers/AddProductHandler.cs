using System.Threading.Tasks;
using FridgeApp.Application.Exceptions;
using FridgeApp.Domain.Repositories;
using FridgeApp.Shared.Abstractions.Commands;

namespace FridgeApp.Application.Commands.Handlers
{
    public class AddProductHandler : ICommandHandler<AddFridgeProduct>
    {
        private readonly IFridgeRepository _fridgeRepository;
        private readonly IProductRepository _productRepository;

        public AddProductHandler(IFridgeRepository fridgeRepository, IProductRepository productRepository)
        {
            _fridgeRepository = fridgeRepository;
            _productRepository = productRepository;
        }
        
        public async Task HandleAsync(AddFridgeProduct command)
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
            
            fridge.AddProduct(product, quantity);
            await _fridgeRepository.UpdateAsync(fridge);
        }
    }
}