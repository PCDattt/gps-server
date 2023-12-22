using DataTransferObject.Contexts;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.CQRS.DeviceFeature.Commands
{
	public class UpdateDeviceCommandHandler:IRequestHandler<UpdateDeviceCommand,bool>
	{
		private readonly EntityDbContext entityDbContext;
		public UpdateDeviceCommandHandler(EntityDbContext entityDbContext)
		{
			this.entityDbContext = entityDbContext;
		}
		public async Task<bool> Handle(UpdateDeviceCommand request, CancellationToken cancellationToken)
		{
			var device = entityDbContext.Devices
				.Where(device => device.Id == request.device.Id && device.IsDeleted == false)
				.FirstOrDefault();
			if (device is null)
			{
				return false;
			}
			device.UserId = request.device.UserId;
			device.SerialNumber = request.device.SerialNumber;
			device.ModifiedDate = DateTime.Now;
			_ = await entityDbContext.SaveChangesAsync();
			return true;
		}
	}
}
