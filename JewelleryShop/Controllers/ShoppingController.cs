using JewelleryShop.DbModels;
using JewelleryShop.Interfaces;
using JewelleryShop.Repository;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace JewelleryShop.Controllers
{
	/// <summary>
	/// Represents controller class for shopping operations
	/// </summary>
	[Route("api/Shopping")]
	[ApiController]
	public class ShoppingController : ControllerBase
	{
		/// <summary>
		/// The account repository
		/// </summary>
		private readonly IShoppingRepository _shoppingRepository;

		public ShoppingController(IShoppingRepository shoppingRepository)
		{
			_shoppingRepository = shoppingRepository;
		}

		[HttpPost]
		[Route("Login")]
		public IActionResult Login(Customer customer)
		{
			IActionResult res = null;
			var response = new LoginResponse();
			//Validation
			if(!Regex.IsMatch(customer.Email, @"^[^@\s]+@[^@\s]+\.[^@\s]+$"))
			{
				response.Success = false;
				response.FailureInformation = "EMail ID not in proper format!";
			}
			else
			{
				response = _shoppingRepository.FetchLoginStatus(customer);
			}
			res = Ok(response);
			return res;
		}

		[HttpPost]
		[Route("Estimate")]
		public IActionResult CalculateFinalPrice(EstimateRequest estimateRequest)
		{
			IActionResult res = null;
			var response = new EstimateResponse();
			//Validation
			if (!Regex.IsMatch(estimateRequest.Email, @"^[^@\s]+@[^@\s]+\.[^@\s]+$"))
			{
				response.Success = false;
				response.FailureInformation = "EMail ID not in proper format!";
			}
			if (!(estimateRequest.GoldPrice>0.0 && estimateRequest.Weight>0.0))
			{
				response.Success = false;
				response.FailureInformation = "GoldPrice and/or Weight should be greater than 0!";
			}
			else
			{
				response = _shoppingRepository.EstimateFinalPrice(estimateRequest);
			}
			res = Ok(response);
			return res;
		}

		//// GET api/values/5
		//[HttpGet("{id}")]
		//public ActionResult<string> Get(int id)
		//{
		//	return "value";
		//}

		//// POST api/values
		//[HttpPost]
		//public void Post([FromBody] string value)
		//{
		//}

		//// PUT api/values/5
		//[HttpPut("{id}")]
		//public void Put(int id, [FromBody] string value)
		//{
		//}

		//// DELETE api/values/5
		//[HttpDelete("{id}")]
		//public void Delete(int id)
		//{
		//}
	}
}
