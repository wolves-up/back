using UtilityService.Api.DataSources.Managers;
using UtilityService.Api.DataSources.Model;

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