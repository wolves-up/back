using System.Text;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using UtilityService.Api.Services;

using UtilityService.Api.Configuration;
using UtilityService.Api.DataSources;
using UtilityService.Api.DataSources.Managers;
using UtilityService.Api.Services;
using UtilityService.Model;

var builder = WebApplication.CreateBuilder(args);

var loggerFactory = LoggerFactory.Create(c => c.AddSimpleConsole());


// Add services to the container.

builder.Services.AddSingleton<ILogger>(c => loggerFactory.CreateLogger("c"));
builder.Services.AddSingleton<IConfigProvider>(new ConfigProvider());
builder.Services.AddSingleton<IMongoDataBaseConnectionManager, MongoDbConnectionManager>();
builder.Services.AddSingleton<IUserManager, UserManager>();
builder.Services.AddSingleton<IReportManager, ReportManager>();
builder.Services.AddSingleton<IReportCommentManager, ReportCommentManager>();
builder.Services.AddSingleton<IUtilityServiceManager, UtilityServiceManager>();

builder.Services.AddSingleton<IReportService, StubReportService>();

builder.Services.AddSingleton<IReportService, StubReportService>();
builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddAuthorization();
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
	.AddJwtBearer(options =>
	{
		options.TokenValidationParameters = new TokenValidationParameters
		{
			// указывает, будет ли валидироваться издатель при валидации токена
			ValidateIssuer = true,
			// строка, представляющая издателя
			ValidIssuer = AuthOptions.ISSUER,
			// будет ли валидироваться потребитель токена
			ValidateAudience = true,
			// установка потребителя токена
			ValidAudience = AuthOptions.AUDIENCE,
			// будет ли валидироваться время существования
			ValidateLifetime = true,
			// установка ключа безопасности
			IssuerSigningKey = AuthOptions.GetSymmetricSecurityKey(),
			// валидация ключа безопасности
			ValidateIssuerSigningKey = true,
		};
	});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
	app.UseSwagger();
	app.UseSwaggerUI();
}

app.UseAuthorization();

app.MapControllers();

app.Run();

public class AuthOptions
{
	public const string ISSUER = "UtilityServiceApi"; // издатель токена
	public const string AUDIENCE = "UtilityFront"; // потребитель токена
	const string KEY = "mysupersecret_secretkey!123";   // ключ для шифрации
	public static SymmetricSecurityKey GetSymmetricSecurityKey() =>
		new SymmetricSecurityKey(Encoding.UTF8.GetBytes(KEY));
}
