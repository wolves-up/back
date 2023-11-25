namespace UtilityService.Api.Services;

public interface IUtilityStorageService
{
    Task<Model.Model.UtilityService[]> GetAllUtilityServices();
    Task<Model.Model.UtilityService> GetServiceById(Guid id);
}