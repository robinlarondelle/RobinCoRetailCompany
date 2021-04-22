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

		// GET: api/Product/5
		[NonAction]
		public string Get( int id )
		{
			return "value";
		}

		// POST: api/Product
		[NonAction]
		public void Post( [FromBody] string value )
		{
		}

		// PUT: api/Product/5
		[NonAction]
		public void Put( int id , [FromBody] string value )
		{
		}

		// DELETE: api/Product/5
		[NonAction]
		public void Delete( int id )
		{
		}
	}
}
