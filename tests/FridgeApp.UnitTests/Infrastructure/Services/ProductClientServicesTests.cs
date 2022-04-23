using System;
using System.Net.Http;
using System.Threading.Tasks;
using FridgeApp.Application.Commands;
using FridgeApp.Infrastructure.Services;
using NSubstitute;
using Xunit;

namespace FridgeApp.UnitTests.Infrastructure.Services;

public class ProductClientServicesTests
{
    private readonly HttpClient _client;
    private readonly ProductClientService _productClientService; 

    public ProductClientServicesTests()
    {
        _client = Substitute.For<HttpClient>();
        _productClientService = new ProductClientService(_client);
    }

    [Fact]
    public async Task PutProduct_Call_Client_On_Success()
    {
        var command = new AddFridgeProduct(Guid.NewGuid(), Guid.NewGuid(), 2);
        
        var exception = await Record.ExceptionAsync(() => _productClientService.PutProduct(command.FridgeId, command.ProductId, command.Quantity));
        
        await _client.Received(1).PutAsync(Arg.Any<string>(), Arg.Any<StreamContent>());
    }
}