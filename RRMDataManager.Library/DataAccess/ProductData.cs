using System.Collections.Generic;
using System.Linq;

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

		public ProductModel GetProductById( int productId )
		{
			SqlDataAccess sql = new SqlDataAccess();

			ProductModel output = sql.LoadData<ProductModel , dynamic>( "SPProduct_GetById" , new { Id = productId } , "RRMData" ).FirstOrDefault();

			return output;
		}
	}
}
