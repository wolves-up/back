using AutoMapper;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using UtilityService.Api.DataSources.Managers;
using UtilityService.Api.DataSources.Model;
using UtilityService.Api.Services;
using UtilityService.Model.Model;
using UtilityService.Model.Transport;

namespace UtilityService.Api.Controllers;

[Controller]
[Route("infrastructure")]
[Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
public class InfrastructureObjectController : ControllerBase
{
	// TODO : Add service
	private readonly IInfrastructureObjectManager _infrastructureObjectManager;
	private readonly IMapper _mapper;

	public InfrastructureObjectController(IInfrastructureObjectManager infrastructureObjectManager, IMapper mapper)
	{
		_infrastructureObjectManager = infrastructureObjectManager;
		_mapper = mapper;
	}

	[HttpPost]
	public async Task<IActionResult> Create([FromBody] CreateInfrastructureObjectCommand command)
	{
		var entity = new InfrastructureObjectEntity()
		{
			Location = command.Location,
			Name = command.Name,
			Text = command.Text,
			UtilityServiceName = command.UtilityServiceName,
		};
		await _infrastructureObjectManager.Add(entity)
			.ConfigureAwait(false);
		return Ok(entity.Id);
	}

	[HttpGet("{id}")]
	public async Task<IActionResult> GetObjectById(Guid id)
	{
		var entity = await _infrastructureObjectManager.GetById(id);
		return Ok(_mapper.Map<InfrastructureObject>(entity));
	}

	[HttpGet("get-all")]
	public async Task<IActionResult> GetAll()
	{
		var entities = await _infrastructureObjectManager.GetAll();
		return Ok(_mapper.Map<InfrastructureObject[]>(entities));
	}
}