using System.Threading.Tasks;
using FridgeApp.Application.DTOs;
using FridgeApp.Application.Queries;
using FridgeApp.Domain.Repositories;
using FridgeApp.Shared.Abstractions.Queries;

namespace FridgeApp.Infrastructure.Persistence.Queries.Handlers;

internal class GetProductHandler : IQueryHandler<GetProduct, ProductDto>
{
    private readonly IProductRepository _products;

    public GetProductHandler(IProductRepository products)
    {
        _products = products;
    }

    public async Task<ProductDto> HandleAsync(GetProduct query)
        => (await _products.GetAsync(query.Id)).AsDto();

}