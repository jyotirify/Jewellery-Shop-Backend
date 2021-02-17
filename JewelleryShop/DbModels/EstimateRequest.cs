using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace JewelleryShop.DbModels
{
	public class EstimateRequest
	{
		public string Email { get; set; }
		public double GoldPrice { get; set; }
		public double Weight { get; set; }
		public double OwnerDiscount { get; set; }
	}
}
