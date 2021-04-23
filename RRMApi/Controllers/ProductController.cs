using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

using RRMDataManager.Library.DataAccess;
using RRMDataManager.Library.Models;

namespace RRMApi.Controllers
{
	[Route( "api/[controller]" )]
	[ApiController]
	[Authorize]
	public class ProductController : ControllerBase
	{
		public List<ProductModel> Get() => new ProductData().GetProducts();
	}
}
