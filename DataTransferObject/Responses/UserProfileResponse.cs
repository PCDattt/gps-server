using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataTransferObject.Responses
{
	public class UserProfileResponse
	{
		public string Username { get; set; } = string.Empty;
		public string Email { get; set; } = string.Empty;
		public string Name { get; set; } = string.Empty;
		public string Role { get; set; } = string.Empty;
		public string AvatarUri { get; set; } = string.Empty;
	}
}
