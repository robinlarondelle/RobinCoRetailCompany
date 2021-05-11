using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

using RRMDesktopWPF.Library.Models;

namespace RRMDesktopWPF.Library.Api
{
	public class UserEndpoint : IUserEndpoint
	{
		private readonly IApiHelper _apiHelper;

		public UserEndpoint( IApiHelper apiHelper )
		{
			_apiHelper = apiHelper;
		}

		public async Task<List<UserModel>> GetAllUsers()
		{
			using ( HttpResponseMessage response = await _apiHelper.ApiClient.GetAsync( "/api/User/Admin/GetAllUsers" ) )
			{
				if ( response.IsSuccessStatusCode )
				{
					List<UserModel> result = await response.Content.ReadAsAsync<List<UserModel>>();
					return result;
				}
				else
				{
					throw new Exception( response.ReasonPhrase );
				}
			}


		}

		/*		public async Task<List<ProductModel>> GetAllProductsAsync()
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
				}*/
	}
}
