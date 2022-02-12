using System;
using FridgeApp.Application.DTOs;
using FridgeApp.Shared.Abstractions.Queries;

namespace FridgeApp.Application.Queries
{
    public class GetFridge : IQuery<FridgeDto>
    {
        public Guid Id { get; set; }
    }
}