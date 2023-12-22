using DataTransferObject.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.CQRS.DeviceFeature.Commands
{
	public class UpdateDeviceCommand:IRequest<bool>
	{
		public Device device { get; set; }
	}
}
