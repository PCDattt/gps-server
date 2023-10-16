using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace gps_server.Data.Entity.Entities
{
	public class User
	{
		public int Id { get; set; }
		public string FirstName { get; set; } = string.Empty;
		public string LastName { get; set; } = string.Empty;
		public DateTime CreatedDate { get; set; }
		public DateTime ModifiedDate { get; set; }
		public bool IsDeleted { get; set; } = false;
	}
}
