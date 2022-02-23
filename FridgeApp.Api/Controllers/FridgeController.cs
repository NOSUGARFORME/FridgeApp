using System.Collections.Generic;
using System.Threading.Tasks;
using FridgeApp.Application.Commands;
using FridgeApp.Application.DTOs;
using FridgeApp.Application.Queries;
using FridgeApp.Domain.Entities;
using FridgeApp.Domain.ValueObjects;
using FridgeApp.Shared.Abstractions.Commands;
using FridgeApp.Shared.Abstractions.Queries;
using FridgeApp.Shared.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace FridgeApp.Api.Controllers
{
    [ApiVersion("1.0")]
    [Produces("application/json")]
    public class FridgeController : BaseController
    {
        private readonly ICommandDispatcher _commandDispatcher;
        private readonly IQueryDispatcher _queryDispatcher;

        public FridgeController(ICommandDispatcher commandDispatcher, IQueryDispatcher queryDispatcher)
        {
            _commandDispatcher = commandDispatcher;
            _queryDispatcher = queryDispatcher;
        }

        /// <summary>
        /// Gets a specific <see cref="Fridge"/>.
        /// </summary>
        /// <param name="query">Query <see cref="GetFridge"/> to find fridge.</param>
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(FridgeDto))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(BaseResponseModel))]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(BaseResponseModel))]
        [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(BaseResponseModel))]
        [HttpGet("{id:guid}")]
        public async Task<ActionResult<FridgeDto>> GetFridge([FromRoute] GetFridge query)
        {
            var result = await _queryDispatcher.QueryAsync(query);
            return OkOrNotFound(result);
        }
        
        /// <summary>
        /// Gets a list of <see cref="Fridge"/>.
        /// </summary>
        /// <param name="query">Query <see cref="SearchFridge"/> to find fridges.</param>
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(FridgeDto[]))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(BaseResponseModel))]
        [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(BaseResponseModel))]
        [HttpGet]
        public async Task<ActionResult<IEnumerable<FridgeDto>>> GetFridges([FromQuery] SearchFridge query)
        {
            var result = await _queryDispatcher.QueryAsync(query);
            return OkOrNotFound(result);
        }
        
        /// <summary>
        /// Gets a list of <see cref="Product"/> in <see cref="Fridge"/>.
        /// </summary>
        /// <param name="query">Query <see cref="GetProductsInFridge"/> to get list <see cref="Product"/> in specific <see cref="Fridge"/>.</param>
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(ProductDto[]))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(BaseResponseModel))]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(BaseResponseModel))]
        [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(BaseResponseModel))]
        [HttpGet("{fridgeId:guid}/products")]
        public async Task<ActionResult<IEnumerable<ProductDto>>> GetProductsInFridge([FromRoute] GetFridgeProducts query)
        {
            var result = await _queryDispatcher.QueryAsync(query);
            return OkOrNotFound(result);
        }
        
        /// <summary>
        /// Create new <see cref="Fridge"/>.
        /// </summary>
        /// <param name="command">Command to <see cref="CreateFridge"/>.</param>
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(BaseResponseModel))]
        [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(BaseResponseModel))]
        [HttpPost]
        public async Task<IActionResult> CreateFridge([FromBody] CreateFridge command)
        {
            await _commandDispatcher.DispatchAsync(command);
            return CreatedAtAction(nameof(GetFridge), new {id = command.Id}, null);
        }
        
        /// <summary>
        /// Put the <see cref="Product"/> in the specific <see cref="Fridge"/>.
        /// </summary>
        /// <param name="command">Query to <see cref="AddFridgeProduct"/>.</param>
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(BaseResponseModel))]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(BaseResponseModel))]
        [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(BaseResponseModel))]
        [HttpPut("{fridgeId:guid}/products")]
        public async Task<IActionResult> PutProduct([FromBody] AddFridgeProduct command)
        {
            await _commandDispatcher.DispatchAsync(command);
            return NoContent();
        }
        
        /// <summary>
        /// Put missing <see cref="Product"/> the default <see cref="ProductQuantity"/>
        /// </summary>
        /// <param name="query">Query to <see cref="AddDefaultQuantityToMissingFridgeProducts"/>.</param>
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(BaseResponseModel))]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(BaseResponseModel))]
        [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(BaseResponseModel))]
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
        
        /// <summary>
        /// Delete the specific <see cref="Product"/> in the <see cref="Fridge"/>.
        /// </summary>
        /// <param name="command">Command to <see cref="RemoveFridgeProduct"/>.</param>
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(BaseResponseModel))]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(BaseResponseModel))]
        [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(BaseResponseModel))]
        [HttpDelete("{fridgeId:guid}/products/{productId:guid}")]
        public async Task<IActionResult> DeleteProductInFridge([FromRoute] RemoveFridgeProduct command)
        {
            await _commandDispatcher.DispatchAsync(command);
            return NoContent();
        }
        
        /// <summary>
        /// Delete specific <see cref="Fridge"/>.
        /// </summary>
        /// <param name="command">Command to <see cref="RemoveFridge"/>.</param>
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(BaseResponseModel))]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(BaseResponseModel))]
        [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(BaseResponseModel))]
        [HttpDelete("{id:guid}")]
        public async Task<IActionResult> DeleteFridge([FromRoute] RemoveFridge command)
        {
            await _commandDispatcher.DispatchAsync(command);
            return NoContent();
        }
    }
}