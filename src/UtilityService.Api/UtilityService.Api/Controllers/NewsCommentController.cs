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
    public Task CreateOrUpdate([FromBody] NewsCommentCommand command)
    {
        throw new NotImplementedException();
    }

    [HttpGet("{id}")]
    public Task<NewsComment> GetCommentById(Guid id)
    {
        throw new NotImplementedException();
    }

    [HttpGet("comments/news/{id}")]
    public Task<NewsComment[]> GetCommentsByNews(Guid id)
    {
        throw new NotImplementedException();
    }

    [HttpGet("comments/news/user/{id}")]
    public Task<NewsComment[]> GetCommentsByUser(Guid id)
    {
        throw new NotImplementedException();
    }

    [HttpPost("archive")]
    public Task SendToArchive([FromBody] Guid commentId)
    {
        throw new NotImplementedException();
    }
}