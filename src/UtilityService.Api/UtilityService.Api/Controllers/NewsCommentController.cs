using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using UtilityService.Api.Services;
using UtilityService.Model.Model.News;
using UtilityService.Model.Transport;

namespace UtilityService.Api.Controllers;

[Controller]
[Route("news/comments")]
[Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
public class NewsCommentController : ControllerBase
{
    private readonly INewsCommentService _newsCommentService;

    public NewsCommentController(INewsCommentService newsCommentService)
    {
        _newsCommentService = newsCommentService;
    }

    [HttpPost]
    public async Task<IActionResult> CreateOrUpdate([FromBody] NewsCommentCommand command)
    {
        var result = await _newsCommentService.CreateOrUpdate(command);
        return Ok(result);
    }

    [HttpGet("{id}")]
    public Task<IActionResult> GetCommentById(Guid id)
    {
        throw new NotImplementedException();
    }

    [HttpGet("comments/news/{id}")]
    public async Task<IActionResult> GetCommentsByNews(Guid id)
    {
        var result = await _newsCommentService.GetCommentsByNews(id);
        return Ok(result);
    }

    [HttpGet("comments/news/user/{id}")]
    public Task<IActionResult> GetCommentsByUser(Guid id)
    {
        throw new NotImplementedException();
    }

    [HttpPost("archive")]
    public Task<IActionResult> SendToArchive([FromBody] Guid commentId)
    {
        throw new NotImplementedException();
    }
}