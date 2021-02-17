using JewelleryShop.DbModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace JewelleryShop.Interfaces
{
	public interface IShoppingRepository
	{
		LoginResponse FetchLoginStatus(Customer customerDetails);
		EstimateResponse EstimateFinalPrice(EstimateRequest estimateRequest);
	}
}
