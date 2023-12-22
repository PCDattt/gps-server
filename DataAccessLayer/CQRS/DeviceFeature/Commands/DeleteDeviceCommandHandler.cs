using DataTransferObject.Contexts;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.CQRS.DeviceFeature.Commands
{
	public class DeleteDeviceCommandHandler : IRequestHandler<DeleteDeviceCommand, bool>
	{
		private readonly EntityDbContext entityDbContext;
		public DeleteDeviceCommandHandler(EntityDbContext entityDbContext)
		{
			this.entityDbContext = entityDbContext;
		}
		public async Task<bool> Handle(DeleteDeviceCommand request, CancellationToken cancellationToken)
		{
			var device = entityDbContext.Devices
				.Where(device => device.Id == request.id && device.IsDeleted == false)
				.FirstOrDefault();
			if (device is null)
			{
				return false;
			}
			device.IsDeleted = true;
			_ = await entityDbContext.SaveChangesAsync();
			return true;
		}
	}
}
