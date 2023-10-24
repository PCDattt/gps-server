using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace gps_server.Data.Entity.Entities
{
	public class User
	{
		public int Id { get; set; }
		public string Username { get; set; } = string.Empty;
		public string PasswordHash { get; set; } = string.Empty;
		public string Email { get; set; } = string.Empty;
		public string Name { get; set; } = string.Empty;
		public string Role { get; set; } = string.Empty;
		public string Permissions { get; set; } = string.Empty;
		public string AvatarUri { get; set; } = string.Empty;
		public DateTime CreatedDate { get; set; }
		public DateTime ModifiedDate { get; set; }
		public bool IsDeleted { get; set; } = false;

		public List<Device> Devices { get; set; }
	}
}
