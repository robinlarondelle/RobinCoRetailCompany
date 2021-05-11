﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

using RRMDataManager.Library.DataAccess;
using RRMDataManager.Library.Models;

namespace RRMDataManager.Controllers
{
    public class InventoryController : ApiController
    {
		public List<InventoryModel> Get()
		{
			InventoryData data = new InventoryData();
			return data.GetInventory();
		}

		public void Post(InventoryModel item)
		{
			InventoryData data = new InventoryData();
			data.SaveInventory( item );
		}
	}
}
