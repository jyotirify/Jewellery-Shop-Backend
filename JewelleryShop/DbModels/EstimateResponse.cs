using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace JewelleryShop.DbModels
{
	public class EstimateResponse
	{
		public double SellingPrice { get; set; }
		public double Discount { get; set; }
		public double TotalPrice { get; set; }
		public bool Success { get; set; }
		public string FailureInformation { get; set; }
	}
}
