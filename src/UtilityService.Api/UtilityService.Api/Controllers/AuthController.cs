﻿using System.IdentityModel.Tokens.Jwt;
using System.Net;
using System.Security.Claims;
using IdentityModel;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using UtilityService.Api.DataSources.Managers;
using UtilityService.Api.DataSources.Model;

namespace UtilityService.Api.Controllers;

[Controller]
public class AuthController : ControllerBase
{
	private readonly IUserManager _userManager;

	public AuthController(IUserManager userManager)
	{
		_userManager = userManager;
	}

	[HttpGet("/login")]
	public async Task<string> Login(string login, string password)
	{
		var user = await _userManager.GetByEmail(login);

		if (user.PasswordHash != HashPassword(password))
		{
			Response.StatusCode = (int)HttpStatusCode.Unauthorized;
			return "unsuccess";
		}

		var claims = new List<Claim> { new Claim(JwtClaimTypes.Id, user.Id.ToString()) };
		var jwt = new JwtSecurityToken(
			issuer: AuthOptions.ISSUER,
			audience: AuthOptions.AUDIENCE,
			claims: claims,
			expires: DateTime.UtcNow.AddDays(30),
			signingCredentials: new SigningCredentials(AuthOptions.GetSymmetricSecurityKey(),
				SecurityAlgorithms.HmacSha256));

		return new JwtSecurityTokenHandler().WriteToken(jwt);
	}

	[HttpGet("/register")]
	public async Task<string> Register(string login, string password)
	{
		var user = await _userManager.FindByEmail(login);

		if (user != null)
		{
			Response.StatusCode = (int)HttpStatusCode.BadRequest;
			return "User already created";
		}

		user = new UserEntity()
		{
			PasswordHash = HashPassword(password),
			RegistrationDate = DateTime.UtcNow,
			Requisites = new RequisitesEntity()
			{
				EmailAddress = login
			}
		};

		await _userManager.Add(user);
		return "Success";
	}

	private string HashPassword(string password) => password;
}