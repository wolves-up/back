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

    public async Task<Guid> CreateOrUpdate(CreateUtilityCommand command)
    {
        var newsId = command.Id;
        var existingNews = (await _utilityServiceManager.FindByIds(new[] {newsId})).SingleOrDefault();
        var entity = new UtilityServiceEntity()
        {
            Name = command.Name,
            Inn = command.Inn,
            Type = command.Type
        };
        if (existingNews is not null)
        {
            await _utilityServiceManager.Update(entity);
        }
        else
        {
            await _utilityServiceManager.Add(entity);
        }
        return command.Id;
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