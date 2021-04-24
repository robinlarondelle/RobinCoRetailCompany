using System.Collections.Generic;
using System.Web.Http;

using RRMDataManager.Library.DataAccess;
using RRMDataManager.Library.Models;

namespace RRMDataManager.Controllers
{
	[Authorize]
	public class ProductController : ApiController
	{
		// GET: api/Product
		[HttpGet]
		public List<ProductModel> Get() => new ProductData().GetProducts();
	}
}
