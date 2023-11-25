using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using UtilityService.Api.DataSources.Managers;
using UtilityService.Api.Services;
using UtilityService.Model.Model;

namespace UtilityService.Api.Controllers;

[Controller]
[Route("content")]
[Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
public class ContentController : ControllerBase
{
    private readonly IContentService _contentService;
    private readonly IContentManager _contentManager;


    public ContentController(IContentService contentService)
    {
        _contentService = contentService;
    }

    [HttpPost]
    public async Task<IActionResult> AddContent()
    {
        await using var ms = new MemoryStream();
		await Request.Body.CopyToAsync(ms);
        var img = ms.ToArray();
        var content = new Content() {Id = Guid.NewGuid(), Bytes = img};
        var result = await _contentService.Add(content);
        return Ok(result);
    }

    [HttpGet("{id}")]
    [AllowAnonymous]
    public async Task<IActionResult> GetContent(Guid id)
    {
        var result = await _contentService.Get(id);
        return Ok(result.Bytes);
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteContent(Guid id)
    {
        await _contentService.Delete(id);
        return Ok();
    }
}