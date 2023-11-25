using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using UtilityService.Api.DataSources.Managers;
using UtilityService.Api.Utils;
using UtilityService.Model.Model;

namespace UtilityService.Api.Controllers;

[Controller]
[Route("users")]
public class UserController : ControllerBase
{
	private readonly IUserManager _userManager;

	public UserController(IUserManager userManager)
	{
		_userManager = userManager;
	}

	[HttpGet("get-user")]
	[Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
	public async Task<User> GetUserInfo()
	{
		var user = await _userManager.GetById(User.GetUserId())
			.ConfigureAwait(false);
		return new User()
		{
			Id = user.Id,
			RegistrationDate = user.RegistrationDate,
			Requisites = new Requisites()
			{
				EmailAddress = user.Requisites.EmailAddress,
				MiddleName = user.Requisites.MiddleName,
				Name = user.Requisites.Name,
				Surname = user.Requisites.Surname,
			},
			Role = user.Role,
		};
	}
}
