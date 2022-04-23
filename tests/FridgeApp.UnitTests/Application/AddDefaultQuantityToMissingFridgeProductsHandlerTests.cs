using System;
using System.Threading.Tasks;
using FridgeApp.Application.Commands;
using FridgeApp.Application.Commands.Handlers;
using FridgeApp.Application.Exceptions;
using FridgeApp.Application.Services;
using FridgeApp.Domain.Entities;
using FridgeApp.Domain.Repositories;
using FridgeApp.Domain.ValueObjects;
using FridgeApp.Shared.Abstractions.Commands;
using NSubstitute;
using NSubstitute.ReturnsExtensions;
using Shouldly;
using Xunit;

namespace FridgeApp.UnitTests.Application;

public class AddDefaultQuantityToMissingFridgeProductsHandlerTests
{
    #region ARRANAGE

    private readonly IProductClientService _productClient;
    private readonly IFridgeRepository _fridgeRepository;
    private readonly IProductRepository _productRepository;
    private readonly ICommandHandler<AddDefaultQuantityToMissingFridgeProducts> _commandHandler;

    public AddDefaultQuantityToMissingFridgeProductsHandlerTests()
    {
        _productClient = Substitute.For<IProductClientService>();
        _fridgeRepository = Substitute.For<IFridgeRepository>();
        _productRepository = Substitute.For<IProductRepository>();
        _commandHandler = new AddDefaultQuantityToMissingFridgeProductsHandler(_productClient, _fridgeRepository, _productRepository);
    }

    #endregion
    
    [Fact]
    public async Task HandleAsync_Throws_FridgeNotFoundException_When_Fridge_Is_Not_Found()
    {
        var command = new AddDefaultQuantityToMissingFridgeProducts(Guid.NewGuid(), Guid.NewGuid(), 12);
        _fridgeRepository.GetAsync(Arg.Any<FridgeId>()).ReturnsNullForAnyArgs();
        
        var exception = await Record.ExceptionAsync(() => Act(command));
        
        exception.ShouldNotBeNull();
        exception.ShouldBeOfType<FridgeNotFoundException>();
    }
    
    [Fact]
    public async Task HandleAsync_Throws_ProductNotFoundByIdException_When_Product_Is_Not_Found()
    {
        var command = new AddDefaultQuantityToMissingFridgeProducts(Guid.NewGuid(), Guid.NewGuid(), 12);
        _fridgeRepository.GetAsync(Arg.Any<FridgeId>()).Returns((Fridge)Activator.CreateInstance(typeof(Fridge), true));
        _productRepository.GetAsync(Arg.Any<ProductId>()).ReturnsNullForAnyArgs();
        
        var exception = await Record.ExceptionAsync(() => Act(command));
        
        exception.ShouldNotBeNull();
        exception.ShouldBeOfType<ProductNotFoundByIdException>();
        exception.ShouldNotBeOfType<FridgeNotFoundException>();
    }
    
    [Fact]
    public async Task HandleAsync_Calls_Client_On_Success()
    {
        var command = new AddDefaultQuantityToMissingFridgeProducts(Guid.NewGuid(), Guid.NewGuid(), 12);
        _fridgeRepository.GetAsync(Arg.Any<FridgeId>()).Returns((Fridge)Activator.CreateInstance(typeof(Fridge), true));
        _productRepository.GetAsync(Arg.Any<ProductId>()).Returns((Product)Activator.CreateInstance(typeof(Product), true));
        _productClient.PutProduct(Arg.Any<Guid>(), Arg.Any<Guid>(), Arg.Any<int>()).Returns(Task.FromResult((object) null));
        
        var exception = await Record.ExceptionAsync(() => Act(command));
        
        exception.ShouldBeNull();
        await _productClient.Received(1).PutProduct(Arg.Any<Guid>(), Arg.Any<Guid>(), Arg.Any<int>());
    }
    
    private Task Act(AddDefaultQuantityToMissingFridgeProducts command)
        => _commandHandler.HandleAsync(command);
}