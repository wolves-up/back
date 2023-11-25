using UtilityService.Api.DataSources.Managers;
using UtilityService.Api.DataSources.Model;
using UtilityService.Model.Transport;

namespace UtilityService.Api.Services;

public class UtilityStorageService : IUtilityStorageService
{
    private readonly IUtilityServiceManager _utilityServiceManager;

    public UtilityStorageService(IUtilityServiceManager utilityServiceManager)
    {
        _utilityServiceManager = utilityServiceManager;
    }

    public async Task<Model.Model.UtilityService[]> GetAllUtilityServices()
    {
        var entities = await _utilityServiceManager.GetAll();
        return entities.Select(ToModel).ToArray();
    }
    
    public async Task<Model.Model.UtilityService> GetServiceById(Guid id)
    {
        var entitiy = await _utilityServiceManager.GetById(id);
        return ToModel(entitiy);
    }

    public async Task<Model.Model.UtilityService> Create(CreateUtilityCommand createUtilityCommand)
    {
        var entity = new UtilityServiceEntity()
        {
            Inn = createUtilityCommand.Inn,
            Name = createUtilityCommand.Name,
            Type = createUtilityCommand.Type
        };

        await _utilityServiceManager.Add(entity)
            .ConfigureAwait(false);

        return ToModel(entity);
    }

    private Model.Model.UtilityService ToModel(UtilityServiceEntity entity)
    {
        return new Model.Model.UtilityService()
        {
            Id = entity.Id,
            Name = entity.Name,
            Inn = entity.Inn,
            Type = entity.Type
        };
    }
}