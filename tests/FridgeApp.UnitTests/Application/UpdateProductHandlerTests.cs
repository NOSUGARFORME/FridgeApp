using System;
using System.Threading.Tasks;
using FridgeApp.Application.Commands;
using FridgeApp.Application.Commands.Handlers;
using FridgeApp.Application.Exceptions;
using FridgeApp.Application.Services;
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

public class UpdateProductHandlerTests
{
    #region ARRANGE

    private readonly IProductRepository _productRepository;
    private readonly IProductReadService _productReadService;
    private readonly ICommandHandler<UpdateProduct> _commandHandler;

    private readonly IProductFactory _productFactory;

    public UpdateProductHandlerTests()
    {
        _productRepository = Substitute.For<IProductRepository>();
        _productReadService = Substitute.For<IProductReadService>();
        _commandHandler = new UpdateProductHandler(_productRepository, _productReadService);

        _productFactory = new ProductFactory();
    }
    
    #endregion
    
    [Fact]
    public async Task HandleAsync_Throws_ProductNotFoundException_When_Product_Is_Not_Found()
    {
        var command = new UpdateProduct(Guid.NewGuid(), "Product", 12);
        _productRepository.GetAsync(Arg.Any<ProductId>()).ReturnsNullForAnyArgs();
        
        var exception = await Record.ExceptionAsync(() => Act(command));
        
        exception.ShouldNotBeNull();
        exception.ShouldBeOfType<ProductNotFoundException>();
    }
    
    [Fact]
    public async Task HandleAsync_Throws_ProductAlreadyExistsException_When_Product_With_Same_Name_Already_Exists()
    {
        var command = new UpdateProduct(Guid.NewGuid(), "Product", 12);
        _productRepository.GetAsync(Arg.Any<ProductId>()).Returns(_productFactory.Create(command.Id, command.Name, command.DefaultQuantity));
        _productReadService.ExistsByNameAsync(Arg.Any<string>()).Returns(true);
        
        var exception = await Record.ExceptionAsync(() => Act(command));
        
        exception.ShouldNotBeNull();
        exception.ShouldNotBeOfType<ProductNotFoundException>();
        exception.ShouldBeOfType<ProductAlreadyExistsException>();
    }
    
    [Fact]
    public async Task HandleAsync_Calls_Repository_On_Success()
    {
        var command = new UpdateProduct(Guid.NewGuid(), "Product", 12);
        _productRepository.GetAsync(Arg.Any<ProductId>()).Returns(_productFactory.Create(command.Id, command.Name, command.DefaultQuantity));
        _productReadService.ExistsByNameAsync(command.Name).Returns(false);
        
        var exception = await Record.ExceptionAsync(() => Act(command));
        
        exception.ShouldBeNull();
        await _productRepository.Received(1).UpdateAsync(Arg.Any<Product>());
    }
    
    private Task Act(UpdateProduct command)
        => _commandHandler.HandleAsync(command);
}
