using System;
using System.Threading.Tasks;
using FridgeApp.Application.Commands;
using FridgeApp.Application.Commands.Handlers;
using FridgeApp.Domain.Entities;
using FridgeApp.Domain.Exceptions;
using FridgeApp.Domain.Factories;
using FridgeApp.Domain.Repositories;
using FridgeApp.Domain.ValueObjects;
using FridgeApp.Shared.Abstractions.Commands;
using NSubstitute;
using NSubstitute.ReturnsExtensions;
using Shouldly;
using Xunit;

namespace FridgeApp.UnitTests.Application;

public class RemoveProductHandlerTests
{
    #region ARRANGE

    private readonly IProductRepository _productRepository;
    private readonly ICommandHandler<RemoveProduct> _commandHandler;

    private readonly IProductFactory _productFactory;

    public RemoveProductHandlerTests()
    {
        _productRepository = Substitute.For<IProductRepository>();
        _commandHandler = new RemoveProductHandler(_productRepository);

        _productFactory = new ProductFactory();
    }

    #endregion
    
    [Fact]
    public async Task HandleAsync_Throws_ProductNotFoundException_When_Product_Is_Not_Found()
    {
        var command = new RemoveProduct(Guid.NewGuid());
        _productRepository.GetAsync(Arg.Any<ProductId>()).ReturnsNullForAnyArgs();
        
        var exception = await Record.ExceptionAsync(() => Act(command));
        
        exception.ShouldNotBeNull();
        exception.ShouldBeOfType<ProductNotFoundException>();
    }
    
    [Fact]
    public async Task HandleAsync_Calls_Repository_On_Success()
    {
        var product = _productFactory.Create(Guid.NewGuid(), "Name", 1);
        var command = new RemoveProduct(product.Id);
        
        _productRepository.GetAsync(Arg.Any<ProductId>()).Returns(product);
       
        var exception = await Record.ExceptionAsync(() => Act(command));
        
        exception.ShouldBeNull();
        await _productRepository.Received(1).DeleteAsync(Arg.Any<Product>());
    }
    
    private Task Act(RemoveProduct command)
        => _commandHandler.HandleAsync(command);
}
