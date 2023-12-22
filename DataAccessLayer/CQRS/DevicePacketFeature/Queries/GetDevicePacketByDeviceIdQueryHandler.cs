using DataTransferObject.Contexts;
using DataTransferObject.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.CQRS.DevicePacketFeature.Queries
{
	public class GetDevicePacketByDeviceIdQueryHandler : IRequestHandler<GetDevicePacketByDeviceIdQuery, DevicePacket>
	{
		private readonly EntityDbContext entityDbContext;
		public GetDevicePacketByDeviceIdQueryHandler(EntityDbContext entityDbContext)
		{
			this.entityDbContext = entityDbContext;
		}
		public async Task<DevicePacket> Handle(GetDevicePacketByDeviceIdQuery request, CancellationToken cancellationToken)
		{
			return await Task.Run(() =>
			{
				return entityDbContext.DevicePackets
					.Where(devicePacket => devicePacket.DeviceId == request.deviceId)
					.OrderByDescending(devicePacket => devicePacket.CreatedDate)
					.FirstOrDefault();
			});
		}
	}
}
