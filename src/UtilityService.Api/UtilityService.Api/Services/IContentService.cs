using UtilityService.Model.Model;

namespace UtilityService.Api.Services;

public interface IContentService
{
    Task<Guid> Add(Content content);
    Task Delete(Guid content);
    Task<Content> Get(Guid contentId);
    Task<Guid[]> AddRange(Content[] content);
    Task DeleteRange(Guid[] content);
    Task<Content[]> GetRange(Guid[] contentIds);
}