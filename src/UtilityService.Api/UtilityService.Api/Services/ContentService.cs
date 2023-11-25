using UtilityService.Api.DataSources.Managers;
using UtilityService.Api.Services.Converters;
using UtilityService.Model.Model;

namespace UtilityService.Api.Services;

public class ContentService : IContentService
{
    private readonly IContentManager _contentManager;
    private readonly ContentConverter _contentConverter;

    public ContentService(IContentManager contentManager, ContentConverter contentConverter)
    {
        _contentManager = contentManager;
        _contentConverter = contentConverter;
    }

    public async Task<Guid> Add(Content content)
        => (await AddRange(new[] {content})).Single();

    public async Task<Guid[]> AddRange(Content[] content)
    {
        await Task.WhenAll(content.Select(x => _contentManager.Add(_contentConverter.ToEntity(x))));
        return content.Select(x => x.Id).ToArray();
    }

    public Task Delete(Guid content)
        => DeleteRange(new[] {content});

    public Task DeleteRange(Guid[] contentIds)
        => Task.WhenAll(contentIds.Select(x => _contentManager.DeleteById(x)));

    public async Task<Content> Get(Guid contentId)
        => _contentConverter.ToContent(await _contentManager.GetById(contentId));

    public Task<Content[]> GetRange(Guid[] contentIds)
        => Task.WhenAll(contentIds.Select(Get));
}