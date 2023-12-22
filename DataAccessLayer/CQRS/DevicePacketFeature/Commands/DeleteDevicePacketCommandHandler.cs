using DataTransferObject.Contexts;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.CQRS.DevicePacketFeature.Commands
{
	public class DeleteDevicePacketCommandHandler : IRequestHandler<DeleteDevicePacketCommand, bool>
	{
		private readonly EntityDbContext entityDbContext;
		public DeleteDevicePacketCommandHandler(EntityDbContext entityDbContext)
		{
			this.entityDbContext = entityDbContext;
		}
		public async Task<bool> Handle(DeleteDevicePacketCommand request, CancellationToken cancellationToken)
		{
			return await Task.Run(() =>
			{
				var devicePacket = entityDbContext.DevicePackets
					.Where(devicePacket => devicePacket.DeviceId == request.deviceId)
					.FirstOrDefault();
				if (devicePacket != null)
				{
					devicePacket.IsDeleted = true;
					_ = entityDbContext.SaveChanges();
					return true;
				}
				return false;
			});
		}
	}
}
