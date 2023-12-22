using DataTransferObject.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.CQRS.DeviceFeature.Queries
{
	public class GetDeviceBySerialNumberQuery : IRequest<Device>
	{
		public string serialNumber { get; set; }
	}
}
