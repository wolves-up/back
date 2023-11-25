using System.Security.Claims;
using IdentityModel;

namespace UtilityService.Api.Utils;

public static class UserExtensions
{
	public static Guid GetUserId(this ClaimsPrincipal user)
	{
		var id = user.Claims.First(x => x.Type == JwtClaimTypes.Id).Value;

		return Guid.Parse(id);
	}
}
