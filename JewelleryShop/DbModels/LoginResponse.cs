using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace JewelleryShop.DbModels
{
	public class LoginResponse
	{
		public bool Success { get; set; }
		public string FailureInformation { get; set; }
		public string FirstName { get; set; }
		public string LastName { get; set; }
		public string Type { get; set; }
	}
}
