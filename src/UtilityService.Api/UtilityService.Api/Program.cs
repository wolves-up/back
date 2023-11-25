using System.Text;
using IdentityModel;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
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
builder.Services.AddSwaggerGen(option =>
{
	option.SwaggerDoc("v1", new OpenApiInfo() { Title = "UtilityService.Api", Version = "v1"});
	option.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme()
	{
		In = ParameterLocation.Header,
		Description = "Enter a token",
		Name = "Authorization",
		Type = SecuritySchemeType.Http,
		BearerFormat = "JWT",
		Scheme = "Bearer",
	});
	option.AddSecurityRequirement(new OpenApiSecurityRequirement()
	{
		{
			new OpenApiSecurityScheme()
			{
				Reference = new OpenApiReference()
				{
					Type = ReferenceType.SecurityScheme,
					Id = "Bearer"
				}
			},
			new string[]{}
		}
	});
});


builder.Services.AddAuthorization(options => 
	options.AddPolicy("User", policy =>
		{
			policy.RequireAuthenticatedUser();
			policy.RequireClaim(JwtClaimTypes.Id);
		}
	));
builder.Services							  
	.AddAuthentication(options =>
	{
		options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
		options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
		options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
	})
	.AddJwtBearer(options =>
	{
		options.TokenValidationParameters = new TokenValidationParameters
		{
			ValidateIssuer = true,
			ValidIssuer = AuthOptions.ISSUER,
			ValidateAudience = false,
			ValidAudience = AuthOptions.AUDIENCE,
			ValidateLifetime = false,
			IssuerSigningKey = AuthOptions.GetSymmetricSecurityKey(),
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
app.UseAuthentication();

app.MapControllers()
	.RequireAuthorization();

app.Run();

public class AuthOptions
{
	public const string ISSUER = "UtilityServiceApi"; // издатель токена
	public const string AUDIENCE = "UtilityFront"; // потребитель токена
	const string KEY = "mysupersecret_secretkey!123";   // ключ для шифрации
	public static SymmetricSecurityKey GetSymmetricSecurityKey() =>
		new SymmetricSecurityKey(Encoding.UTF8.GetBytes(KEY));
}
