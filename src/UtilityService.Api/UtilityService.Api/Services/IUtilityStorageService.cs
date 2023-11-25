using UtilityService.Model.Transport;

namespace UtilityService.Api.Services;

public interface IUtilityStorageService
{
    Task<Model.Model.UtilityService[]> GetAllUtilityServices();
    Task<Model.Model.UtilityService> GetServiceById(Guid id);
    Task<Model.Model.UtilityService> Create(CreateUtilityCommand createUtilityCommand);
}