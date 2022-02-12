using System.Collections.Generic;
using FridgeApp.Application.DTOs;
using FridgeApp.Shared.Abstractions.Queries;

namespace FridgeApp.Application.Queries
{
    public class SearchFridge : IQuery<IEnumerable<FridgeDto>>
    {
        public string SearchPhrase { get; set; }
    }
}