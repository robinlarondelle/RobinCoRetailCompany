﻿using System;

namespace RRMDataManager.Library.Models
{
	public class InventoryModel
	{
		public int ProductId { get; set; }
		public int Quality { get; set; }
		public decimal PurchasePrice { get; set; }
		public DateTime PurchaseDate{ get; set; }
	}
}