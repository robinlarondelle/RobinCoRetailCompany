using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
