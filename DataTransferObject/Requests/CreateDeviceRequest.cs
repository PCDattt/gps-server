using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataTransferObject.Requests
{
	public class CreateDeviceRequest
	{
		public int UserId { get; set; }
		public string SerialNumber { get; set; }
	}
}
