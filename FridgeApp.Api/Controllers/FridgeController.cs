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
        public async Task<ActionResult<FridgeDto>> Get([FromRoute] GetFridge query)
        {
            var result = await _queryDispatcher.QueryAsync(query);
            return OkOrNotFound(result);
        }
        
        [HttpGet]
        public async Task<ActionResult<IEnumerable<FridgeDto>>> Get([FromQuery] SearchFridge query)
        {
            var result = await _queryDispatcher.QueryAsync(query);
            return OkOrNotFound(result);
        }
        
        [HttpPost]
        public async Task<IActionResult> Post([FromBody] CreateFridge command)
        {
            await _commandDispatcher.DispatchAsync(command);
            return CreatedAtAction(nameof(Get), new {id = command.Id}, null);
        }
        
        [HttpPut("{fridgeId}/products")]
        public async Task<IActionResult> Put([FromBody] AddFridgeProduct command)
        {
            await _commandDispatcher.DispatchAsync(command);
            return Ok();
        }
        
        [HttpPut("{fridgeId:guid}/products/{name}/addDefault")]
        public async Task<IActionResult> Put()
        {
            // await _commandDispatcher.DispatchAsync(command);
            return Ok();
        }
        
        [HttpDelete("{fridgeId:guid}/products/{productId:guid}")]
        public async Task<IActionResult> Delete([FromBody] RemoveFridgeProduct command)
        {
            await _commandDispatcher.DispatchAsync(command);
            return Ok();
        }
        
        [HttpDelete("{id:guid}")]
        public async Task<IActionResult> Delete([FromBody] RemoveFridge command)
        {
            await _commandDispatcher.DispatchAsync(command);
            return Ok();
        }
    }
}