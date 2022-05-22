using System;
using System.Threading.Tasks;
using FridgeApp.Application.Commands;
using FridgeApp.Application.DTOs;
using FridgeApp.Application.Queries;
using FridgeApp.Domain.Entities;
using FridgeApp.Shared.Abstractions.Commands;
using FridgeApp.Shared.Abstractions.Queries;
using FridgeApp.Shared.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace FridgeApp.Api.Controllers;

[ApiVersion("1.0")]
public class ProductController : BaseController
{
    private readonly ICommandDispatcher _commandDispatcher;
    private readonly IQueryDispatcher _queryDispatcher;

    public ProductController(ICommandDispatcher commandDispatcher, IQueryDispatcher queryDispatcher)
    {
        _commandDispatcher = commandDispatcher;
        _queryDispatcher = queryDispatcher;
    }
    
    /// <summary>
    /// Gets a specific <see cref="Product"/>.
    /// </summary>
    /// <param name="query">Query <see cref="GetProduct"/> to find fridge.</param>
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(FridgeDto))]
    [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(BaseResponseModel))]
    [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(BaseResponseModel))]
    [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(BaseResponseModel))]
    [HttpGet("{id:guid}")]
    public async Task<ActionResult<ProductDto>> GetProduct([FromRoute] GetProduct query)
    {
        var result = await _queryDispatcher.QueryAsync(query);
        return OkOrNotFound(result);
    }
    
    /// <summary>
    /// Create new <see cref="Product"/>.
    /// </summary>
    /// <param name="command">Command to <see cref="CreateProduct"/>.</param>
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(BaseResponseModel))]
    [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(BaseResponseModel))]
    [HttpPost]
    public async Task<IActionResult> CreateFridge([FromBody] CreateProduct command)
    {
        await _commandDispatcher.DispatchAsync(command);
        return CreatedAtAction(nameof(GetProduct), new {id = command.Id}, null);
    }

    /// <summary>
    /// Update the specific <see cref="Product"/>.
    /// </summary>
    /// <param name="id">Id for update product.</param>
    /// <param name="dto">Dto for update product.</param>
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(BaseResponseModel))]
    [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(BaseResponseModel))]
    [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(BaseResponseModel))]
    [HttpPut("{id:guid}")]
    public async Task<IActionResult> UpdateProduct([FromRoute] Guid id, [FromBody] ProductDto dto)
    {
        await _commandDispatcher.DispatchAsync(new UpdateProduct(id, dto.Name, dto.DefaultQuantity));
        return NoContent();
    }
    
    /// <summary>
    /// Delete the specific <see cref="Product"/>.
    /// </summary>
    /// <param name="command">Command to <see cref="RemoveProduct"/>.</param>
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(BaseResponseModel))]
    [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(BaseResponseModel))]
    [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(BaseResponseModel))]
    [HttpDelete("{id:guid}")]
    public async Task<IActionResult> DeleteProduct([FromRoute] RemoveProduct command)
    {
        await _commandDispatcher.DispatchAsync(command);
        return NoContent();
    }
}