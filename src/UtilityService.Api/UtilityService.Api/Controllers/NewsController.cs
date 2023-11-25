using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using UtilityService.Api.Services;
using UtilityService.Model.Model.News;
using UtilityService.Model.Transport;

namespace UtilityService.Api.Controllers;

[Controller]
[Route("news")]
[Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
public class NewsController : ControllerBase
{
    private readonly INewsService _newsService;

    public NewsController(INewsService newsService)
    {
        _newsService = newsService;
    }

    [HttpPost]
    public Task<Guid> CreateOrUpdateNews([FromBody] CreateNewsCommand command)
    {
        throw new NotImplementedException();
    }

    [HttpGet]
    public Task<ShortNews[]> TakeActualNews(int count, int skip, NewsFilter filter)
    {
        throw new NotImplementedException();
    }

    [HttpGet("{id}")]
    public Task SendToArchive(Guid id)
    {
        throw new NotImplementedException();
    }
}