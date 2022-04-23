using System;
using System.Linq;
using FridgeApp.Domain.Entities;
using FridgeApp.Domain.Events;
using FridgeApp.Domain.Factories;
using FridgeApp.Domain.ValueObjects;
using Shouldly;
using Xunit;

namespace FridgeApp.UnitTests.Domain;

public class ProductTests
{
    #region ARRANGE

    private readonly IProductFactory _factory;

    public ProductTests()
    {
        _factory = new ProductFactory();
    }

    private Product GetProduct()
    {
        var product = _factory.Create(Guid.NewGuid(), "Name", 3);
        product.ClearEvents();
        return product;
    }

    #endregion
    
    [Fact]
    public void Update_Updates_ProductUpdated_Domain_Event_On_Success()
    {
        var product = GetProduct();
        
        var exception = Record.Exception(() => product.Update("Name 1", 2));

        exception.ShouldBeNull();
        product.Events.Count().ShouldBe(1);

        var @event = product.Events.FirstOrDefault() as ProductUpdated;
        
        @event.ShouldNotBeNull();
        @event.Name.ShouldNotBe("Name");
        @event.Name.ShouldBe("Name 1");
    }
}