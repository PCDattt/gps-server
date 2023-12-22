using DataTransferObject.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.CQRS.DevicePacketFeature.Commands
{
	public class AddDevicePacketCommand : IRequest<DevicePacket>
	{
		public DevicePacket devicePacket { get; set; }
	}
}
