using System.Threading.Tasks;
using FridgeApp.Application.Exceptions;
using FridgeApp.Application.Services;
using FridgeApp.Domain.Factories;
using FridgeApp.Domain.Repositories;
using FridgeApp.Domain.ValueObjects;
using FridgeApp.Shared.Abstractions.Commands;

namespace FridgeApp.Application.Commands.Handlers
{
    public class CreateFridgeHandler : ICommandHandler<CreateFridge>
    {
        private readonly IFridgeRepository _fridgeRepository;
        private readonly IFridgeModelRepository _fridgeModelRepository;
        private readonly IFridgeFactory _factory;
        private readonly IFridgeReadService _readService;

        public CreateFridgeHandler(IFridgeRepository repository, IFridgeFactory factory,
            IFridgeReadService readService, IFridgeModelRepository fridgeModelRepository)
        {
            _fridgeRepository = repository;
            _factory = factory;
            _readService = readService;
            _fridgeModelRepository = fridgeModelRepository;
        }

        public async Task HandleAsync(CreateFridge command)
        {
            var (id, name, (firstName, lastName), fridgeModelId) = command;
            
            if (await _readService.ExistsByNameAsync(name))
            {
                throw new FridgeAlreadyExistsException(name);
            }

            var fridgeModel = await _fridgeModelRepository.GetAsync(fridgeModelId);

            if (fridgeModel is null)
            {
                throw new FridgeModelNotFoundException(fridgeModelId);
            }
            
            var owner = new OwnerName(firstName, lastName);
            var fridge = _factory.Create(id, name, owner, fridgeModel);
            
            await _fridgeRepository.AddAsync(fridge);
        }
    }
}