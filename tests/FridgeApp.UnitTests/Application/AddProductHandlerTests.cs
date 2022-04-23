using System;
using System.Threading.Tasks;
using FridgeApp.Application.Commands;
using FridgeApp.Application.Commands.Handlers;
using FridgeApp.Application.Exceptions;
using FridgeApp.Domain.Entities;
using FridgeApp.Domain.Repositories;
using FridgeApp.Domain.ValueObjects;
using FridgeApp.Shared.Abstractions.Commands;
using NSubstitute;
using NSubstitute.ReturnsExtensions;
using Shouldly;
using Xunit;

namespace FridgeApp.UnitTests.Application;

public class AddProductHandlerTests
{
    #region ARRANGE

    private readonly IProductRepository _productRepository;
    private readonly IFridgeRepository _fridgeRepository;
    private readonly ICommandHandler<AddFridgeProduct> _handler;

    public AddProductHandlerTests()
    {
        _productRepository = Substitute.For<IProductRepository>();
        _fridgeRepository = Substitute.For<IFridgeRepository>();
        
        _handler = new AddProductHandler(_fridgeRepository, _productRepository);
    }

    #endregion
    
    [Fact]
    public async Task HandleAsync_Throws_FridgeNotFoundException_When_Fridge_Is_Not_Found()
    {
        var command = new AddFridgeProduct(Guid.NewGuid(), Guid.NewGuid(), 12);
        _fridgeRepository.GetAsync(Arg.Any<FridgeId>()).ReturnsNullForAnyArgs();
        
        var exception = await Record.ExceptionAsync(() => Act(command));
        
        exception.ShouldNotBeNull();
        exception.ShouldBeOfType<FridgeNotFoundException>();
    }
    
    [Fact]
    public async Task HandleAsync_Throws_ProductNotFoundByIdException_When_Product_Is_Not_Found()
    {
        var command = new AddFridgeProduct(Guid.NewGuid(), Guid.NewGuid(), 12);
        _fridgeRepository.GetAsync(Arg.Any<FridgeId>()).Returns((Fridge)Activator.CreateInstance(typeof(Fridge), true));
        _productRepository.GetAsync(Arg.Any<ProductId>()).ReturnsNullForAnyArgs();
        
        var exception = await Record.ExceptionAsync(() => Act(command));
        
        exception.ShouldNotBeNull();
        exception.ShouldNotBeOfType<FridgeNotFoundException>();
        exception.ShouldBeOfType<ProductNotFoundByIdException>();
    }
    
    [Fact]
    public async Task HandleAsync_Calls_Repository_On_Success()
    {
        var command = new AddFridgeProduct(Guid.NewGuid(), Guid.NewGuid(), 12);
        _fridgeRepository.GetAsync(Arg.Any<FridgeId>()).Returns((Fridge)Activator.CreateInstance(typeof(Fridge), true));
        _productRepository.GetAsync(Arg.Any<ProductId>()).Returns((Product)Activator.CreateInstance(typeof(Product), true));
        
        var exception = await Record.ExceptionAsync(() => Act(command));
        
        exception.ShouldBeNull();
        await _fridgeRepository.Received(1).UpdateAsync(Arg.Any<Fridge>());
    }
    
    private Task Act(AddFridgeProduct command)
        => _handler.HandleAsync(command);
    
    
}