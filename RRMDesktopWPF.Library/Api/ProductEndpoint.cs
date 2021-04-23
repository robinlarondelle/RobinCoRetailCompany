using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;

using RRMDesktopWPF.Library.Models;

namespace RRMDesktopWPF.Library.Api
{
	public class ProductEndpoint : IProductEndpoint
	{
		private readonly IApiHelper _apiHelper;

		public ProductEndpoint( IApiHelper apiHelper )
		{
			_apiHelper = apiHelper;
		}

		public async Task<List<ProductModel>> GetAllProductsAsync()
		{
			using ( HttpResponseMessage response = await _apiHelper.ApiClient.GetAsync( "/api/Product" ) )
			{
				if ( response.IsSuccessStatusCode )
				{
					List<ProductModel> result = await response.Content.ReadAsAsync<List<ProductModel>>();
					return result;
				}
				else
				{
					throw new Exception( response.ReasonPhrase );
				}
			}
		}
	}
}
