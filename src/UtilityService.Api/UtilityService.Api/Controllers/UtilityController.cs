﻿using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using UtilityService.Api.Services;

namespace UtilityService.Api.Controllers;

[Controller]
[Route("utilities")]
[Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
public class UtilityController : ControllerBase
{
    private readonly IUtilityStorageService _utilityStorageService;

    public UtilityController(UtilityStorageService utilityStorageService)
    {
        _utilityStorageService = utilityStorageService;
    }

    [HttpGet]
    public async Task<IActionResult> Get()
    {
        var result = await _utilityStorageService.GetAllUtilityServices();
        return Ok(result);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> Get(string id)
    {
        var result = await _utilityStorageService.GetServiceById(Guid.Parse(id));
        return Ok(result);
    }
}