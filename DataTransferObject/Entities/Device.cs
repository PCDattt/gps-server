using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataTransferObject.Entities
{
	public class Device
	{
		public int Id { get; set; }
		public string SerialNumber { get; set; } = string.Empty;
		public int UserId { get; set; }
		public DateTime CreatedDate { get; set; }
		public DateTime ModifiedDate { get; set; }
		public bool IsDeleted { get; set; } = false;

		public User User { get; set; }
		public List<DevicePacket> DevicePackets { get; set; }
	}
}
