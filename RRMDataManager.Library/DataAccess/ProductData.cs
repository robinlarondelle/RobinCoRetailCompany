using System.Collections.Generic;

using RRMDataManager.Library.Internal.DataAccess;
using RRMDataManager.Library.Models;

namespace RRMDataManager.Library.DataAccess
{
	public class ProductData
	{
		public List<ProductModel> GetProducts()
		{
			SqlDataAccess sql = new SqlDataAccess();

			List<ProductModel> output = sql.LoadData<ProductModel , dynamic>( "SPProduct_GetAll" , null , "RRMData" );

			return output;
		}
	}
}
