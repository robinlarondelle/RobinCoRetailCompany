using System.Collections.Generic;
using System.Threading.Tasks;

using RRMDesktopWPF.Library.Models;

namespace RRMDesktopWPF.Library.Api
{
	public interface IProductEndpoint
	{
		Task<List<ProductModel>> GetAllProductsAsync();
	}
}