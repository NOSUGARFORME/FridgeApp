using System.Threading.Tasks;
using FridgeApp.Application.Exceptions;
using FridgeApp.Application.Services;
using FridgeApp.Domain.Factories;
using FridgeApp.Domain.Repositories;
using FridgeApp.Shared.Abstractions.Commands;

namespace FridgeApp.Application.Commands.Handlers;

public record CreateProductHandler(IProductRepository _productRepository, IProductFactory _productFactory, 
    IProductReadService _readService) : ICommandHandler<CreateProduct>
{
    private readonly IProductRepository _productRepository = _productRepository;
    private readonly IProductFactory _productFactory = _productFactory;
    private readonly IProductReadService _readService = _readService;

    public async Task HandleAsync(CreateProduct command)
    {
        var (id, name, defaultQuantity) = command;

        if (await _readService.ExistsByNameAsync(name))
        {
            throw new ProductAlreadyExistsException(name);
        }

        var product = _productFactory.Create(id, name, defaultQuantity);
        await _productRepository.AddAsync(product);
    }
}