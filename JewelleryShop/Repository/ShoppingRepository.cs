using JewelleryShop.DbModels;
using JewelleryShop.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace JewelleryShop.Repository
{
	public class ShoppingRepository: IShoppingRepository
	{
		private readonly ApiContext _context;

		//To free context
		bool _disposed;

		public ShoppingRepository(ApiContext context)
		{
			_context = context;
		}
		public LoginResponse FetchLoginStatus(Customer customerDetails)
		{
			LoginResponse resp = new LoginResponse();
			var customerObj = _context.Customers.Where(x => x.Email == customerDetails.Email && x.Password == customerDetails.Password).FirstOrDefault();
			if(customerObj == null)
			{
				resp.Success = false;
				resp.FailureInformation = "User not found!";
			}
			else
			{
				resp.Success = true;
				resp.FirstName = customerObj.FirstName;
				resp.LastName = customerObj.LastName;
				resp.Type = customerObj.Type;
				resp.FailureInformation = "No errors, login successful!";
			}
			return resp;
		}

		public EstimateResponse EstimateFinalPrice(EstimateRequest estimateRequest)
		{
			EstimateResponse resp = new EstimateResponse();
			var customerObj = _context.Customers.Where(x => x.Email == estimateRequest.Email).FirstOrDefault();
			if (customerObj == null)
			{
				resp.Success = false;
				resp.FailureInformation = "User not found!";
			}
			else
			{
				resp.Success = true;
				resp.SellingPrice = estimateRequest.GoldPrice * estimateRequest.Weight;
				if(estimateRequest.OwnerDiscount != 0.0)
				{
					resp.Discount = (estimateRequest.OwnerDiscount/100) * resp.SellingPrice;
				}
				else
				{
					resp.Discount = customerObj.DiscountPercentage * resp.SellingPrice;
				}
				resp.TotalPrice = resp.SellingPrice - resp.Discount;
				resp.FailureInformation = "No errors, estimation calculated successfully!";
			}
			return resp;
		}
		//Dispose method is used to free unmanaged resources like files, database connections etc

		/// <summary>
		/// Virtual Dispose method
		/// </summary>
		/// <param name="disposing"></param>
		protected virtual void Dispose(bool disposing)
		{
			if (_disposed)
			{
				return;
			}

			if (disposing)
			{
				_context.Dispose();
			}
			_disposed = true;
		}
	}
}
