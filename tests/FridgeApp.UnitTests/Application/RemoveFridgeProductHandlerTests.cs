using System;
using System.Collections.Generic;
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

public class RemoveFridgeProductHandlerTests
{
    #region ARRANGE

    private readonly IFridgeRepository _repository;
    private readonly IFridgeWriteService _writeService;
    private readonly ICommandHandler<RemoveFridgeProduct> _commandHandler;
    
    private readonly IProductFactory _productFactory;
    private readonly IFridgeModelFactory _fridgeModelFactory;
    private readonly IFridgeFactory _fridgeFactory;
    
    public RemoveFridgeProductHandlerTests()
    {
        _repository = Substitute.For<IFridgeRepository>();
        _writeService = Substitute.For<IFridgeWriteService>();
        _commandHandler = new RemoveFridgeProductHandler(_repository, _writeService);

        _productFactory = new ProductFactory();
        _fridgeModelFactory = new FridgeModelFactory();
        _fridgeFactory = new FridgeFactory();
    }

    #endregion
    
    [Fact]
    public async Task HandleAsync_Throws_FridgeNotFoundException_When_Fridge_Is_Not_Found()
    {
        var command = new RemoveFridgeProduct(Guid.NewGuid(), Guid.NewGuid());
        _repository.GetAsync(Arg.Any<FridgeId>()).ReturnsNullForAnyArgs();
        
        var exception = await Record.ExceptionAsync(() => Act(command));
        
        exception.ShouldNotBeNull();
        exception.ShouldBeOfType<FridgeNotFoundException>();
    }
    
    [Fact]
    public async Task HandleAsync_Throws_ProductNotFoundByIdException_When_Product_Is_Not_Found()
    {
        var command = new RemoveFridgeProduct(Guid.NewGuid(), Guid.NewGuid());
        _repository.GetAsync(Arg.Any<FridgeId>()).Returns((Fridge)Activator.CreateInstance(typeof(Fridge), true));

        var exception = await Record.ExceptionAsync(() => Act(command));
        
        exception.ShouldNotBeNull();
        exception.ShouldBeOfType<ProductNotFoundInFridgeException>();
        exception.ShouldNotBeOfType<FridgeNotFoundException>();
    }
    
    [Fact]
    public async Task HandleAsync_Calls_Repository_On_Success()
    {
        var fridge = CreateTestFridge();
        var command = new RemoveFridgeProduct(Guid.NewGuid(), fridge.FridgeProducts.First?.Value.Product.Id);
        
        _repository.GetAsync(Arg.Any<FridgeId>()).Returns(fridge);
       
        var exception = await Record.ExceptionAsync(() => Act(command));
        
        exception.ShouldBeNull();
        await _writeService.Received(1).RemoveProduct(Arg.Any<FridgeId>(), Arg.Any<ProductId>());
    }
    
    private Task Act(RemoveFridgeProduct command)
        => _commandHandler.HandleAsync(command);

    private Fridge CreateTestFridge()
    {
        var products = new List<Product>
        {
            _productFactory.Create(Guid.NewGuid(), "Name0", 0),
            _productFactory.Create(Guid.NewGuid(), "Name1", 1),
            _productFactory.Create(Guid.NewGuid(), "Name2", 2),
            _productFactory.Create(Guid.NewGuid(), "Name3", 3),
        };

        var fridgeModel = _fridgeModelFactory.Create(Guid.NewGuid(), "ModelName", 2022);

        var fridge = _fridgeFactory.Create(Guid.NewGuid(), "FridgeName", new OwnerName("Owner", "Name"), fridgeModel);
        fridge.AddProductsWithDefaultQuantity(products);
        
        return fridge;
    }
}
