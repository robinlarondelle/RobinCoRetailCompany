using System.Collections.Generic;
using System.Linq;
using System.Web.Http;

using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;

using RRMDataManager.Library.DataAccess;
using RRMDataManager.Library.Models;
using RRMDataManager.Models;

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

		[Authorize( Roles = "Admin" )]
		[HttpGet]
		[Route( "api/User/Admin/GetAllUsers" )]
		public List<ApplicationUserModel> GetAllUsers()
		{
			List<ApplicationUserModel> output = new List<ApplicationUserModel>();

			using ( var context = new ApplicationDbContext() )
			{
				var userStore = new UserStore<ApplicationUser>( context );
				var userManager = new UserManager<ApplicationUser>( userStore );

				var users = userManager.Users.ToList();
				var roles = context.Roles.ToList();


				foreach ( var user in users )
				{
					ApplicationUserModel u = new ApplicationUserModel
					{
						Id = user.Id ,
						Email = user.Email
					};

					foreach ( var r in user.Roles )
					{
						u.Roles.Add( r.RoleId , roles.Where( x => x.Id == r.RoleId ).First().Name );
					}

					output.Add( u );
				}
			}

			return output;
		}

		[Authorize( Roles = "Admin" )]
		[HttpGet]
		[Route( "api/User/Admin/GetAllRoles" )]
		public Dictionary<string , string> GetAllRoles()
		{
			using ( var context = new ApplicationDbContext() )
			{
				return context.Roles.ToDictionary( x => x.Id , x => x.Name );
			}
		}

		[Authorize( Roles = "Admin" )]
		[HttpPost]
		[Route( "api/User/Admin/AddRole" )]
		public void AddRole( UserRolePair pairing )
		{
			using ( var context = new ApplicationDbContext() )
			{
				var userStore = new UserStore<ApplicationUser>( context );
				var userManager = new UserManager<ApplicationUser>( userStore );

				userManager.AddToRole( pairing.UserId , pairing.RoleName );
			}
		}

		[Authorize( Roles = "Admin" )]
		[HttpPost]
		[Route( "api/User/Admin/RemoveRole" )]
		public void RemoveRole( UserRolePair pairing )
		{
			using ( var context = new ApplicationDbContext() )
			{
				var userStore = new UserStore<ApplicationUser>( context );
				var userManager = new UserManager<ApplicationUser>( userStore );

				userManager.RemoveFromRole( pairing.UserId , pairing.RoleName );
			}
		}
	}
}
