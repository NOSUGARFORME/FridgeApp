using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Text.Json;
using System.Threading.Tasks;
using FridgeApp.Application.Commands;
using FridgeApp.Application.DTOs;
using FridgeApp.Application.Queries;
using FridgeApp.Shared.Abstractions.Commands;
using FridgeApp.Shared.Abstractions.Queries;
using Microsoft.AspNetCore.Mvc;

namespace FridgeApp.Api.Controllers
{
    public class FridgeController : BaseController
    {
        private readonly ICommandDispatcher _commandDispatcher;
        private readonly IQueryDispatcher _queryDispatcher;

        public FridgeController(ICommandDispatcher commandDispatcher, IQueryDispatcher queryDispatcher)
        {
            _commandDispatcher = commandDispatcher;
            _queryDispatcher = queryDispatcher;
        }

        [HttpGet("{id:guid}")]
        public async Task<ActionResult<FridgeDto>> GetFridge([FromRoute] GetFridge query)
        {
            var result = await _queryDispatcher.QueryAsync(query);
            return OkOrNotFound(result);
        }
        
        [HttpGet]
        public async Task<ActionResult<IEnumerable<FridgeDto>>> GetFridges([FromQuery] SearchFridge query)
        {
            var result = await _queryDispatcher.QueryAsync(query);
            return OkOrNotFound(result);
        }
        
        [HttpGet("{fridgeId:guid}/products")]
        public async Task<ActionResult<IEnumerable<ProductDto>>> GetProductsInFridge([FromRoute] GetFridgeProducts query)
        {
            var result = await _queryDispatcher.QueryAsync(query);
            return OkOrNotFound(result);
        }
        
        [HttpPost]
        public async Task<IActionResult> CreateFridge([FromBody] CreateFridge command)
        {
            await _commandDispatcher.DispatchAsync(command);
            return CreatedAtAction(nameof(GetFridge), new {id = command.Id}, null);
        }
        
        [HttpPut("{fridgeId:guid}/products")]
        public async Task<IActionResult> PutProduct([FromBody] AddFridgeProduct command)
        {
            await _commandDispatcher.DispatchAsync(command);
            return Ok();
        }
        
        [HttpPut("{fridgeId:guid}/products/addDefault")]
        public async Task<ActionResult<IEnumerable<ProductDto>>> AddDefaultQuantityToMissingFridgeProducts([FromRoute] GetMissingFridgeProducts query)
        {
            var products = await _queryDispatcher.QueryAsync(query);

            foreach (var product in products)
            {
                var httpWebRequest = (HttpWebRequest)WebRequest.Create($"https://localhost:5001/api/fridge/{query.FridgeId}/products"); // ?
                httpWebRequest.ContentType = "application/json";
                httpWebRequest.Method = "PUT";

                await using (var streamWriter = new StreamWriter(httpWebRequest.GetRequestStream()))
                {
                    var command = new AddFridgeProduct
                    (
                        query.FridgeId,
                        product.Id,
                        product.DefaultQuantity
                    );
                    
                    var json = JsonSerializer.Serialize(command);

                    await streamWriter.WriteAsync(json);
                }

                var httpResponse = (HttpWebResponse)httpWebRequest.GetResponse();
                using (var streamReader = new StreamReader(httpResponse.GetResponseStream()))
                {
                    var result = await streamReader.ReadToEndAsync();
                }
            }
            
            return Ok();
        }
        
        [HttpDelete("{fridgeId:guid}/products/{productId:guid}")]
        public async Task<IActionResult> DeleteProductInFridge([FromRoute] RemoveFridgeProduct command)
        {
            await _commandDispatcher.DispatchAsync(command);
            return Ok();
        }
        
        [HttpDelete("{id:guid}")]
        public async Task<IActionResult> DeleteFridge([FromRoute] RemoveFridge command)
        {
            await _commandDispatcher.DispatchAsync(command);
            return Ok();
        }
    }
}