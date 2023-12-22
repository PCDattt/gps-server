using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.CQRS.DevicePacketFeature.Commands
{
	public class DeleteDevicePacketCommand : IRequest<bool>
	{
		public int deviceId { get; set; }
	}
}
