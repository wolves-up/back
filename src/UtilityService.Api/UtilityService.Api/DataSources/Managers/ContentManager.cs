using UtilityService.Api.DataSources.Model;

namespace UtilityService.Api.DataSources.Managers;

public class ContentManager : EntityManager<ContentEntity>
{
    public ContentManager(IMongoDataBaseConnectionManager mongoDataBaseConnectionManager) : base(mongoDataBaseConnectionManager)
    {
    }
}