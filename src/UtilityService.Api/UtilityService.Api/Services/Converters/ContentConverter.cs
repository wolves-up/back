using UtilityService.Api.DataSources.Model;
using UtilityService.Model.Model;

namespace UtilityService.Api.Services.Converters;

public class ContentConverter
{
    public ContentEntity ToEntity(Content content)
        => new()
        {
            Id = content.Id,
            Bytes = content.Bytes
        };
    
    public Content ToContent(ContentEntity entity)
        => new()
        {
            Id = entity.Id,
            Bytes = entity.Bytes
        };   
}