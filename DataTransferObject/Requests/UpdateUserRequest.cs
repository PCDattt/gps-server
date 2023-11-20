using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataTransferObject.Requests
{
	public class UpdateUserRequest
	{
		public string Email { get; set; } = string.Empty;
		public string Username { get; set; } = string.Empty;
		public string Name { get; set; } = string.Empty;
		public string Password { get; set; } = string.Empty;
	}
}
