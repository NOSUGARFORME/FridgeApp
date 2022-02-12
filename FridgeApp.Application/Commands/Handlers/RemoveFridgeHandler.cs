using System.Threading.Tasks;
using FridgeApp.Application.Exceptions;
using FridgeApp.Domain.Repositories;
using FridgeApp.Shared.Abstractions.Commands;

namespace FridgeApp.Application.Commands.Handlers
{
    public class RemoveFridgeHandler : ICommandHandler<RemoveFridge>
    {
        private readonly IFridgeRepository _repository;

        public RemoveFridgeHandler(IFridgeRepository repository)
            => _repository = repository;
        

        public async Task HandleAsync(RemoveFridge command)
        {
            var fridge = await _repository.GetAsync(command.Id);

            if (fridge is null)
            {
                throw new FridgeNotFoundException(command.Id);
            }

            await _repository.DeleteAsync(fridge);
        }
    }
}