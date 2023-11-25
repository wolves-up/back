using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using UtilityService.Api.Services;
using UtilityService.Api.Services.Converters;
using UtilityService.Model.Model.News;
using UtilityService.Model.Transport;

namespace UtilityService.Api.Controllers;

[Controller]
[Route("news")]
[Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
public class NewsController : ControllerBase
{
    private readonly INewsService _newsService;
    private readonly NewsConverter _converter;

    public NewsController(INewsService newsService)
    {
        _newsService = newsService;
    }

    [HttpPost]
    public Task<Guid> CreateOrUpdateNews([FromBody] CreateNewsCommand command)
    {
        //todo: проверить право доступа на публикацию
        return _newsService.CreateOrUpdateNews(command);
    }

    [HttpGet]
    public Task<ShortNews[]> TakeActualNews(int count, int skip, NewsFilter filter)
    {
        //todo: исключать архивные
        return _newsService.TakeActualNews(count, skip, filter);
    }

    [HttpGet("{id}")]
    public Task SendToArchive(Guid id)
    {
        throw new NotImplementedException();
    }
}