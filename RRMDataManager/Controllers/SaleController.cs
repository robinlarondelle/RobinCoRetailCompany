using System.Collections.Generic;
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
		[Authorize( Roles = "Cashier" )]
		public void Post( SaleModel sale )
		{
			SaleData data = new SaleData();
			string userId = RequestContext.Principal.Identity.GetUserId();

			data.SaveSale( sale , userId );
		}

		[Route("GetSaleReport")]
		[Authorize( Roles = "Admin, Manager" )]
		public List<SaleReportModel> GetSaleReport()
		{
			SaleData data = new SaleData();
			return data.GetSaleReport();
		}
	}
}
