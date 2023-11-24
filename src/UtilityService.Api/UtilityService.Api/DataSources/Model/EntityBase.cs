using MongoDB.Bson.Serialization.Attributes;

namespace UtilityService.Api.DataSources.Model;

public interface IEntity
{
	public Guid Id { get; set; }
}

public abstract class EntityBase : IEntity
{
	[BsonId]
	public Guid Id { get; set; }
}
