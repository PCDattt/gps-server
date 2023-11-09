using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataTransferObject.Responses
{
	public class DevicePacketResponse
	{
		public string RawData { get; set; } = string.Empty;
		public int DeviceId { get; set; }
		public DateTime CreatedDate { get; set; }
		public DateTime ModifiedDate { get; set; }
	}
}
