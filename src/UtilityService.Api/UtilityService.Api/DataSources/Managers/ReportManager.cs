using UtilityService.Api.DataSources.Model;

namespace UtilityService.Api.DataSources.Managers;

public interface IReportManager : IEntityManager<ReportEntity>
{

}

public class ReportManager : EntityManager<ReportEntity>, IReportManager
{
	public ReportManager(IMongoDataBaseConnectionManager mongoDataBaseConnectionManager) 
		: base(mongoDataBaseConnectionManager)
	{
	}
}
