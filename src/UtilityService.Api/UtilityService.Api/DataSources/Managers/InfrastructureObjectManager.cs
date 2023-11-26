using UtilityService.Api.DataSources.Model;

namespace UtilityService.Api.DataSources.Managers;

public interface IInfrastructureObjectManager : IEntityManager<InfrastructureObjectEntity>
{

}

public class InfrastructureObjectManager : EntityManager<InfrastructureObjectEntity>, IInfrastructureObjectManager
{
	public InfrastructureObjectManager(IMongoDataBaseConnectionManager mongoDataBaseConnectionManager) : base(mongoDataBaseConnectionManager)
	{
	}
}
