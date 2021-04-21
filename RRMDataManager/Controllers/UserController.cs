using System.Linq;
using System.Web.Http;
using Microsoft.AspNet.Identity;
using RRMDataManager.Library.DataAccess;
using RRMDataManager.Library.Models;

namespace RRMDataManager.Controllers
{
	[Authorize]
	public class UserController : ApiController
	{
		// GET: User/Details/5
		[HttpGet]
		public UserModel GetById() 
		{
			string userId = RequestContext.Principal.Identity.GetUserId();
			UserData data = new UserData();

			return data.GetUserByID( userId ).FirstOrDefault();
		}
	}
}
