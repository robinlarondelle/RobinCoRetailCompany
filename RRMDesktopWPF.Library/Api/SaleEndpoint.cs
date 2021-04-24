using System;
using System.Net.Http;
using System.Threading.Tasks;

using RRMDesktopWPF.Library.Models;

namespace RRMDesktopWPF.Library.Api
{
	public class SaleEndpoint : ISaleEndpoint
	{
		private readonly IApiHelper _apiHelper;

		public SaleEndpoint( IApiHelper apiHelper )
		{
			_apiHelper = apiHelper;
		}

		public async Task PostSaleAsync( SaleModel sale )
		{
			using ( HttpResponseMessage response = await _apiHelper.ApiClient.PostAsJsonAsync( "/api/Sale" , sale ) )
			{
				if ( response.IsSuccessStatusCode )
				{

				}
				else
				{
					throw new Exception( response.ReasonPhrase );
				}
			}
		}
	}
}
