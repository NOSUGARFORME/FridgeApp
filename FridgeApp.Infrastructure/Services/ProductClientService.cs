using System;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using FridgeApp.Application.Commands;
using FridgeApp.Application.Services;

namespace FridgeApp.Infrastructure.Services
{
    public class ProductClientService : IProductClientService
    {
        private readonly HttpClient _client;

        public ProductClientService(HttpClient client)
        {
            _client = client;
        }
        
        public async Task PutProduct(Guid fridgeId, Guid productId, ushort quantity)
        {
            var command = new AddFridgeProduct(fridgeId, productId, quantity);
            var json = JsonSerializer.Serialize(command);
            var content = new StringContent(json, Encoding.UTF8, "application/json");
            
            await _client.PutAsync($"api/Fridge/{fridgeId}/products", content);
        }
    }
}