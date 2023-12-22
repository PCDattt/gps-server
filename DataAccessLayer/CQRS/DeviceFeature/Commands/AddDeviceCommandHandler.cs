using DataTransferObject.Contexts;
using DataTransferObject.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.CQRS.DeviceFeature.Commands
{
	public class AddDeviceCommandHandler:IRequestHandler<AddDeviceCommand, Device>
	{
		private readonly EntityDbContext entityDbContext;
		public AddDeviceCommandHandler(EntityDbContext entityDbContext)
		{
			this.entityDbContext = entityDbContext;
		}
		public async Task<Device> Handle(AddDeviceCommand request, CancellationToken cancellationToken)
		{
			var now = DateTime.Now;
			_ = await entityDbContext.Devices.AddAsync(new()
			{
				UserId = request.device.UserId,
				SerialNumber = request.device.SerialNumber,
				CreatedDate = now,
				ModifiedDate = now,
				IsDeleted = false
			});
			_ = entityDbContext.SaveChanges();
			return request.device;
		}
	}
}
