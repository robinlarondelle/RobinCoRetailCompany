using System.Collections.Generic;

using RRMDataManager.Library.Internal.DataAccess;
using RRMDataManager.Library.Models;

namespace RRMDataManager.Library.DataAccess
{
	public class UserData
	{
		public List<UserModel> GetUserByID( string ID )
		{
			SqlDataAccess sql = new SqlDataAccess();

			List<UserModel> output = sql.LoadData<UserModel, dynamic>( "SPUserLookup" , new { Id = ID } , "RRMData" );

			return output;
		}
	}
}
