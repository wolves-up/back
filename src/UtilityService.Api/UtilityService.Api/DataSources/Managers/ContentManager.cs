using UtilityService.Api.DataSources.Model;

namespace UtilityService.Api.DataSources.Managers;

public interface IContentManager : IEntityManager<ContentEntity>
{
}
public class ContentManager : EntityManager<ContentEntity>, IContentManager
{
    public ContentManager(IMongoDataBaseConnectionManager mongoDataBaseConnectionManager) : base(mongoDataBaseConnectionManager)
    {
    }
}