using System.Threading.Tasks;

namespace FridgeApp.Application.Services
{
    public interface IProductReadService
    {
        Task<bool> ExistsByNameAsync(string name);
    }
}