using System.Collections.Generic;

namespace RRMDesktopWPF.Library.Models
{
	public class SaleModel
	{
		public List<SaleDetailModel> SaleDetails { get; set; }

		public SaleModel()
		{
			SaleDetails = new List<SaleDetailModel>();
		}
	}
}
