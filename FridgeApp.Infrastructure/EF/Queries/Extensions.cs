using System.Linq;
using FridgeApp.Application.DTOs;
using FridgeApp.Infrastructure.EF.Models;

namespace FridgeApp.Infrastructure.EF.Queries
{
    internal static class Extensions
    {
        public static FridgeDto AsDto(this FridgeReadModel readModel)
            => new()
            {
                Id = readModel.Id,
                Name = readModel.Name,
                OwnerName = new OwnerNameDto
                {
                    FirstName = readModel.OwnerName?.FirstName,
                    LastName = readModel.OwnerName?.LastName
                },
                Products = readModel.Products.Select(fp => new FridgeProductDto
                {
                    Id = fp.ProductId,
                    Name = fp.Product.Name,
                    Quantity = fp.Quantity,
                    DefaultQuantity = fp.Product.DefaultQuantity,
                    Version = fp.Product.Version,
                    CreatedDateTime = fp.Product.CreatedDateTime,
                    UpdatedDateTime = fp.Product.UpdatedDateTime
                }),
                FridgeModel = new FridgeModelDto
                {
                    Name = readModel.FridgeModel.Name,
                    Year = readModel.FridgeModel.Year,
                    Version = readModel.FridgeModel.Version,
                    CreatedDateTime = readModel.FridgeModel.CreatedDateTime,
                    UpdatedDateTime = readModel.FridgeModel.UpdatedDateTime
                },
                Version = readModel.Version,
                CreatedDateTime = readModel.CreatedDateTime,
                UpdatedDateTime = readModel.UpdatedDateTime
            };
    }
}