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
    public async Task<IActionResult> CreateOrUpdateNews([FromBody] CreateNewsCommand command)
    {
        //todo: проверить право доступа на публикацию
        var result = await _newsService.CreateOrUpdateNews(command);
        return Ok(result);
    }

    [HttpGet("search")]
    public async Task<IActionResult> TakeActualNews(int count, int skip, NewsFilter filter)
    {
        //todo: исключать архивные
        var result = await _newsService.TakeActualNews(count, skip, filter);
        return Ok(result);
	}

    [HttpGet("get-all")]
    public async Task<IActionResult> TakeNews()
    {
        //todo: исключать архивные
        var result = await _newsService.GetAll();
        return Ok(result);
    }

	[HttpGet("{id}")]
    public async Task<IActionResult> GetNews(Guid id)
    {
        var result = await _newsService.GetNewsById(id);
        return Ok(result);
    }

    [HttpGet("/archive/{id}")]
    public Task SendToArchive(Guid id)
    {
        throw new NotImplementedException();
    }
}