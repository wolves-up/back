using MongoDB.Driver;
using UtilityService.Api.Configuration;

namespace UtilityService.Api.DataSources;

public interface IMongoDataBaseConnectionManager
{
	IMongoDatabase MongoDatabase { get; }
}

public class MongoDbConnectionManager : IMongoDataBaseConnectionManager
{
	public MongoDbConnectionManager(IConfigProvider configurationProvider, ILogger log)
	{
		_log = log;

		var config = configurationProvider.GetConfig();
		var client = new MongoClient(config.MongoDbConnectionString);
		client.StartSession(new ClientSessionOptions());
		MongoDatabase = client.GetDatabase(config.MongoDbDatabaseName);
	}

	public IMongoDatabase MongoDatabase { get; }

	private readonly ILogger _log;
}
