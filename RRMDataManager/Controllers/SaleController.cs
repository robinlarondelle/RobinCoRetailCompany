using System.Web.Http;

using Microsoft.AspNet.Identity;

using RRMDataManager.Library.DataAccess;
using RRMDataManager.Library.Models;

namespace RRMDataManager.Controllers
{
	[Authorize]
	public class SaleController : ApiController
	{
		//POST /api/Sale
		public void Post( SaleModel sale )
		{
			SaleData data = new SaleData();
			string userId = RequestContext.Principal.Identity.GetUserId();

			data.SaveSale( sale , userId );
		}
	}
}
