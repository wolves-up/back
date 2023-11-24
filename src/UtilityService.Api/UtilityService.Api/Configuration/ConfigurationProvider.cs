using System.Text.Json;
using System.Text.Json.Serialization;

namespace UtilityService.Api.Configuration;

public class ApiConfig
{
	public string MongoDbConnectionString { get; set; }
}

public interface IConfigurationProvider
{
	ApiConfig GetConfig();
	void SaveConfig(ApiConfig config);
}

public class ConfigurationProvider : IConfigurationProvider
{
	public ApiConfig GetConfig()
	{
		var config = new ApiConfig();

		if (File.Exists("secrets.json"))
		{
			config = JsonSerializer.Deserialize<ApiConfig>(File.ReadAllText("secrets.json"));
		}
		else
		{
			SaveConfig(config);
		}

		if (string.IsNullOrEmpty(config?.MongoDbConnectionString))
			throw new ArgumentException("Settings file not filled!");

		return config;
	}

	public void SaveConfig(ApiConfig config)
	{
		File.WriteAllText("secrets.json", JsonSerializer.Serialize(config));
	}
}
