using System.Linq;
using System.Security.Claims;

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

using RRMDataManager.Library.DataAccess;
using RRMDataManager.Library.Models;

namespace RRMApi.Controllers
{
	[Route( "api/[controller]" )]
	[ApiController]
	[Authorize]
	public class UserController : ControllerBase
	{
		public UserModel GetById()
		{
			/*old way: string userId = RequestContext.Principal.Identity.GetUserId();*/
			string userId = User.FindFirstValue( ClaimTypes.NameIdentifier );
			UserData data = new UserData();

			return data.GetUserByID( userId ).FirstOrDefault();
		}
	}
}
