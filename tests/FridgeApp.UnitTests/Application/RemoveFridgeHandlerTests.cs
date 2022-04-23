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

public class RemoveFridgeHandlerTests
{
    #region ARRANGE

    private readonly IFridgeRepository _repository;
    private readonly ICommandHandler<RemoveFridge> _commandHandler;

    public RemoveFridgeHandlerTests()
    {
        _repository = Substitute.For<IFridgeRepository>();
        _commandHandler = new RemoveFridgeHandler(_repository);
    }
    #endregion
    
    [Fact]
    public async Task HandleAsync_Throws_FridgeNotFoundException_When_Fridge_Is_Not_Found()
    {
        var command = new RemoveFridge(Guid.NewGuid());
        _repository.GetAsync(Arg.Any<FridgeId>()).ReturnsNullForAnyArgs();
        
        var exception = await Record.ExceptionAsync(() => Act(command));
        
        exception.ShouldNotBeNull();
        exception.ShouldBeOfType<FridgeNotFoundException>();
    }
    
    [Fact]
    public async Task HandleAsync_Calls_Client_On_Success()
    {
        var command = new RemoveFridge(Guid.NewGuid());
        _repository.GetAsync(Arg.Any<FridgeId>()).Returns((Fridge)Activator.CreateInstance(typeof(Fridge), true));
        
        var exception = await Record.ExceptionAsync(() => Act(command));
        
        exception.ShouldBeNull();
        await _repository.Received(1).DeleteAsync(Arg.Any<Fridge>());
    }
    
    private Task Act(RemoveFridge command)
        => _commandHandler.HandleAsync(command);
}