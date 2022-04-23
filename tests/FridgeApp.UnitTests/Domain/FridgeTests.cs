using System;
using System.Linq;
using FridgeApp.Domain.Entities;
using FridgeApp.Domain.Events;
using FridgeApp.Domain.Factories;
using FridgeApp.Domain.ValueObjects;
using Shouldly;
using Xunit;

namespace FridgeApp.UnitTests.Domain;

public class FridgeTests
{
    #region ARRANGE

    private readonly IFridgeFactory _factory;

    public FridgeTests()
    {
        _factory = new FridgeFactory();
    }

    private Fridge GetFridge()
    {
        var fridgeModel = new FridgeModel(Guid.NewGuid(), "Name", 2022);
        var fridge = _factory.Create(Guid.NewGuid(), "FridgeName", new OwnerName("FirstName", "LastName"), fridgeModel);
        fridge.ClearEvents();
        return fridge;
    }

    #endregion
    
    [Fact]
    public void AddProduct_Adds_ProductAdded_Domain_Event_On_Success()
    {
        var fridge = GetFridge();
           
        var exception = Record.Exception(() => fridge.AddProduct(new Product(Guid.NewGuid(), "Product 1", 1), 2));

        exception.ShouldBeNull();
        fridge.Events.Count().ShouldBe(1);

        var @event = fridge.Events.FirstOrDefault() as FridgeProductAdded;
        
        @event.ShouldNotBeNull();
        @event.Product.Name.ShouldBe(new ProductName("Product 1"));
    }
    
    [Fact]
    public void AddProduct_Adds_Product_Witch_Already_Exists_ProductAdded_Domain_Event_On_Success()
    {
        var fridge = GetFridge();
        var product = new Product(Guid.NewGuid(), "Product 1", 1);
        
        var exception = Record.Exception(() =>
        {
            fridge.AddProduct(product, 2);
            fridge.AddProduct(product, 4);
        });

        exception.ShouldBeNull();
        fridge.Events.Count().ShouldBe(2);

        var @event = fridge.Events.FirstOrDefault() as FridgeProductAdded;
        
        @event.ShouldNotBeNull();
        @event.Product.Name.ShouldBe(new ProductName("Product 1"));
        @event.Fridge.FridgeProducts.SingleOrDefault(fp => fp.Product.Id == product.Id).ShouldNotBeNull();
        @event.Fridge.FridgeProducts.SingleOrDefault(fp => fp.Product.Id == product.Id).Quantity.ShouldBe(new ProductQuantity(6));
    }
    
    
    [Fact]
    public void RemoveProduct_Adds_FridgeProductRemovedEvent_Domain_Event_On_Success()
    {
        var fridge = GetFridge();
        var product = new Product(Guid.NewGuid(), "Product 1", 1);
        fridge.AddProduct(product, 2);
        fridge.ClearEvents();
        
        var exception = Record.Exception(() => fridge.RemoveProduct(product.Id));

        exception.ShouldBeNull();
        fridge.Events.Count().ShouldBe(1);

        var @event = fridge.Events.FirstOrDefault() as FridgeProductRemovedEvent;
        
        @event.ShouldNotBeNull();
        fridge.FridgeProducts.Count.ShouldBe(0);
    }
}
