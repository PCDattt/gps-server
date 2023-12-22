using DataTransferObject.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.CQRS.DevicePacketFeature.Queries
{
	public class GetDevicePacketByDeviceIdQuery : IRequest<DevicePacket>
	{
		public int deviceId { get; set; }
	}
}
