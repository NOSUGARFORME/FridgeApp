using System.Collections.Generic;
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
            return NoContent();
        }
        
        [HttpPut("{fridgeId:guid}/products/addDefault")]
        public async Task<ActionResult<IEnumerable<ProductDto>>> AddDefaultQuantityToMissingFridgeProducts([FromRoute] GetMissingFridgeProducts query)
        {
            var products = await _queryDispatcher.QueryAsync(query);

            foreach (var product in products)
            {
                var command =
                    new AddDefaultQuantityToMissingFridgeProducts(query.FridgeId, product.Id, product.DefaultQuantity);
                await _commandDispatcher.DispatchAsync(command);
            }
            
            return NoContent();
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