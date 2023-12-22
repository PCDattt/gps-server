using DataTransferObject.Contexts;
using DataTransferObject.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.CQRS.DevicePacketFeature.Commands
{
	public class AddDevicePacketCommandHandler:IRequestHandler<AddDevicePacketCommand, DevicePacket>
	{
		private readonly EntityDbContext entityDbContext;
		public AddDevicePacketCommandHandler(EntityDbContext entityDbContext)
		{
			this.entityDbContext = entityDbContext;
		}
		public async Task<DevicePacket> Handle(AddDevicePacketCommand request, CancellationToken cancellationToken)
		{
			return await Task.Run(() =>
			{
				var now = DateTime.Now;
				_ = entityDbContext.DevicePackets.AddAsync(new()
				{
					DeviceId = request.devicePacket.DeviceId,
					RawData = request.devicePacket.RawData,
					CreatedDate = now,
					ModifiedDate = now,
					IsDeleted = false
				});
				_ = entityDbContext.SaveChanges();
				return request.devicePacket;
			});
		}
	}
}
