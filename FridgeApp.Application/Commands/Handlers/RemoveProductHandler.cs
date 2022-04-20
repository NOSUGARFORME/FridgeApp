using System.Threading.Tasks;
using FridgeApp.Domain.Exceptions;
using FridgeApp.Domain.Repositories;
using FridgeApp.Shared.Abstractions.Commands;

namespace FridgeApp.Application.Commands.Handlers;

public record RemoveProductHandler(IProductRepository _productRepository) : ICommandHandler<RemoveProduct>
{
    private readonly IProductRepository _productRepository = _productRepository;

    public async Task HandleAsync(RemoveProduct command)
    {
        var product = await _productRepository.GetAsync(command.Id);

        if (product is null)
        {
            throw new ProductNotFoundException(command.Id);
        }

        await _productRepository.DeleteAsync(product);
    }
}