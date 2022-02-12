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
                Products = readModel.Products.Select(fp => new ProductInFridgeDto
                {
                    Id = fp.ProductId,
                    Name = fp.Product.Name,
                    Quantity = fp.Quantity,
                    DefaultQuantity = fp.Product.DefaultQuantity
                }),
                FridgeModel = new FridgeModelDto
                {
                    Name = readModel.FridgeModel.Name,
                    Year = readModel.FridgeModel.Year
                }
            };
    }
}