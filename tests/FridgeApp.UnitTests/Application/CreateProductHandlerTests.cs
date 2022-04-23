using System;
using System.Threading.Tasks;
using FridgeApp.Application.Commands;
using FridgeApp.Application.Commands.Handlers;
using FridgeApp.Application.Exceptions;
using FridgeApp.Application.Services;
using FridgeApp.Domain.Entities;
using FridgeApp.Domain.Factories;
using FridgeApp.Domain.Repositories;
using FridgeApp.Shared.Abstractions.Commands;
using NSubstitute;
using Shouldly;
using Xunit;

namespace FridgeApp.UnitTests.Application;

public class CreateProductHandlerTests
{
    #region ARRANGE

    private readonly ICommandHandler<CreateProduct> _commandHandler;
    private readonly IProductRepository _repository;
    private readonly IProductFactory _factory;
    private readonly IProductReadService _readService;

    public CreateProductHandlerTests()
    {
        _repository = Substitute.For<IProductRepository>();
        _factory = Substitute.For<IProductFactory>();
        _readService = Substitute.For<IProductReadService>();
        
        _commandHandler = new CreateProductHandler(_repository, _factory, _readService);
    }

    #endregion
    
    [Fact]
    public async Task HandleAsync_Throws_ProductAlreadyExistsException_When_Fridge_With_Same_Name_Already_Exists()
    {
        var command = new CreateProduct(Guid.NewGuid(), "Product", 12);
        _readService.ExistsByNameAsync(command.Name).Returns(true);
        
        var exception = await Record.ExceptionAsync(() => Act(command));
        
        exception.ShouldNotBeNull();
        exception.ShouldBeOfType<ProductAlreadyExistsException>();
    }
    
    [Fact]
    public async Task HandleAsync_Calls_Repository_On_Success()
    {
        var command = new CreateProduct(Guid.NewGuid(), "Product", 12);
        
        _readService.ExistsByNameAsync(command.Name).Returns(false);
        
        _factory.Create(command.Id, command.Name, command.DefaultQuantity).Returns(default(Product));
        
        var exception = await Record.ExceptionAsync(() => Act(command));
        
        exception.ShouldBeNull();
        await _repository.Received(1).AddAsync(Arg.Any<Product>());
    }
    
    private Task Act(CreateProduct command)
        => _commandHandler.HandleAsync(command);
}