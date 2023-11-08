using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataTransferObject.Requests
{
	public class UpdateDeviceRequest
	{
		public int Id { get; set; }
		public int UserId { get; set; }
		public string SerialNumber { get; set; } = string.Empty;
	}
}
