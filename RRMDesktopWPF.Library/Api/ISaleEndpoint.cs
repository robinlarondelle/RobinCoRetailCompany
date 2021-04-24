using System.Threading.Tasks;

using RRMDesktopWPF.Library.Models;

namespace RRMDesktopWPF.Library.Api
{
	public interface ISaleEndpoint
	{
		Task PostSaleAsync( SaleModel sale );
	}
}