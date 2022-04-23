using System;
using System.Threading.Tasks;
using FridgeApp.Application.Commands;
using FridgeApp.Application.Commands.Handlers;
using FridgeApp.Application.Exceptions;
using FridgeApp.Application.Services;
using FridgeApp.Domain.Entities;
using FridgeApp.Domain.Factories;
using FridgeApp.Domain.Repositories;
using FridgeApp.Domain.ValueObjects;
using FridgeApp.Shared.Abstractions.Commands;
using NSubstitute;
using NSubstitute.ReturnsExtensions;
using Shouldly;
using Xunit;

namespace FridgeApp.UnitTests.Application;

public class CreateFridgeHandlerTests
{
    #region ARRANGE

    private readonly ICommandHandler<CreateFridge> _commandHandler;
    private readonly IFridgeRepository _repository;
    private readonly IFridgeModelRepository _fridgeModelRepository;
    private readonly IFridgeFactory _factory;
    private readonly IFridgeReadService _readService;

    public CreateFridgeHandlerTests()
    {
        _repository = Substitute.For<IFridgeRepository>();
        _factory = Substitute.For<IFridgeFactory>();
        _readService = Substitute.For<IFridgeReadService>();
        _fridgeModelRepository = Substitute.For<IFridgeModelRepository>();
        
        _commandHandler = new CreateFridgeHandler(_repository, _factory, _readService, _fridgeModelRepository);
    }

    #endregion
    
    [Fact]
    public async Task HandleAsync_Throws_FridgeAlreadyExistsException_When_Fridge_With_Same_Name_Already_Exists()
    {
        var command = new CreateFridge(Guid.NewGuid(), "Fridge", new OwnerWriteModel("Owner", "Name"), Guid.NewGuid());
        _readService.ExistsByNameAsync(command.Name).Returns(true);
        
        var exception = await Record.ExceptionAsync(() => Act(command));
        
        exception.ShouldNotBeNull();
        exception.ShouldBeOfType<FridgeAlreadyExistsException>();
    }

    [Fact]
    public async Task HandleAsync_Throws_FridgeModelNotFoundException_When_FridgeModel_Is_Not_Found()
    {
        var command = new CreateFridge(Guid.NewGuid(), "Fridge", new OwnerWriteModel("Owner", "Name"), Guid.NewGuid());
        
        _readService.ExistsByNameAsync(command.Name).Returns(false);
        _fridgeModelRepository.GetAsync(Arg.Any<FridgeModelId>()).ReturnsNullForAnyArgs();
        
        var exception = await Record.ExceptionAsync(() => Act(command));
        
        exception.ShouldNotBeNull();
        exception.ShouldBeOfType<FridgeModelNotFoundException>();
    }
    
    [Fact]
    public async Task HandleAsync_Calls_Repository_On_Success()
    {
        var command = new CreateFridge(Guid.NewGuid(), "Fridge", new OwnerWriteModel("Owner", "Name"), Guid.NewGuid());
        
        _readService.ExistsByNameAsync(command.Name).Returns(false);
        _fridgeModelRepository.GetAsync(Arg.Any<FridgeModelId>())
            .Returns(new FridgeModel(Guid.NewGuid(), "Name", 2022));
        _factory.Create(command.Id, command.Name, Arg.Any<OwnerName>(), Arg.Any<FridgeModel>()).Returns(default(Fridge));
        
        var exception = await Record.ExceptionAsync(() => Act(command));
        
        exception.ShouldBeNull();
        await _repository.Received(1).AddAsync(Arg.Any<Fridge>());
    }
    
    private Task Act(CreateFridge command)
        => _commandHandler.HandleAsync(command);
}
