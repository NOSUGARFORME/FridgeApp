using System.Threading.Tasks;

namespace FridgeApp.Application.Services
{
    public interface IFridgeReadService
    {
        Task<bool> ExistsByNameAsync(string name);
        
    }
}