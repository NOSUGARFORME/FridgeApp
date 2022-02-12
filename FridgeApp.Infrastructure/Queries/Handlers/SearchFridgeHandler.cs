using System.Collections.Generic;
using System.Threading.Tasks;
using FridgeApp.Application.DTOs;
using FridgeApp.Shared.Abstractions.Queries;

namespace FridgeApp.Application.Queries.Handlers
{
    public class SearchFridgeHandler : IQueryHandler<SearchFridge, IEnumerable<FridgeDto>>
    {
        public Task<IEnumerable<FridgeDto>> HandleAsync(SearchFridge query)
        {
            return null;
        }
    }
}