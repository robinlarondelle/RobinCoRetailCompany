using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using RRMDataManager.Library.Internal.DataAccess;
using RRMDataManager.Library.Models;

namespace RRMDataManager.Library.DataAccess
{
	public class InventoryData
	{
		public List<InventoryModel> GetInventory()
		{
			SqlDataAccess sql = new SqlDataAccess();

			return sql.LoadData<InventoryModel , dynamic>("dbo.SPInventory_GetAll", new { }, "RRMData" );
		}

		public void SaveInventory(InventoryModel item)
		{
			SqlDataAccess sql = new SqlDataAccess();

			sql.SaveData("dbo.SPInventory_Insert", item, "RRMData");
		}

	}
}
