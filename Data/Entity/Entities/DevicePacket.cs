using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace gps_server.Data.Entity.Entities
{
	public class DevicePacket
	{
		public int Id { get; set; }
		public string RawData { get; set; } = string.Empty;
		public int DeviceId { get; set; }
		public DateTime CreatedDate { get; set; }
		public DateTime ModifiedDate { get; set; }
		public bool IsDeleted { get; set; } = false;

		public Device Device { get; set; }
	}
}
