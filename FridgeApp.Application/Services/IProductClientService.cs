using System;
using System.Threading.Tasks;

namespace FridgeApp.Application.Services
{
    public interface IProductClientService
    {
        Task PutProduct(Guid fridgeId, Guid productId, ushort quantity);
    }
}