using System.Threading.Tasks;
using FridgeApp.Application.Exceptions;
using FridgeApp.Application.Services;
using FridgeApp.Domain.Exceptions;
using FridgeApp.Domain.Repositories;
using FridgeApp.Shared.Abstractions.Commands;

namespace FridgeApp.Application.Commands.Handlers;

public record UpdateProductHandler(IProductRepository _productRepository, 
    IProductReadService _readService) : ICommandHandler<UpdateProduct>
{
    private readonly IProductRepository _productRepository = _productRepository;
    private readonly IProductReadService _readService = _readService;


    public async Task HandleAsync(UpdateProduct command)
    {
        var (id, name, defaultQuantity) = command;
        var product = await _productRepository.GetAsync(id);

        if (product is null)
        {
            throw new ProductNotFoundException(id);
        }
        
        if (await _readService.ExistsByNameAsync(name))
        {
            throw new ProductAlreadyExistsException(name);
        }
        
        product.Update(name, defaultQuantity);
        await _productRepository.UpdateAsync(product);
    }
}