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
	public class GetAllDevicePacketByDeviceIdQueryHandler : IRequestHandler<GetAllDevicePacketByDeviceIdQuery, List<DevicePacket>>
	{
		private readonly EntityDbContext entityDbContext;
		public GetAllDevicePacketByDeviceIdQueryHandler(EntityDbContext entityDbContext)
		{
			this.entityDbContext = entityDbContext;
		}
		public async Task<List<DevicePacket>> Handle(GetAllDevicePacketByDeviceIdQuery request, CancellationToken cancellationToken)
		{
			return await Task.Run(() =>
			{
				return entityDbContext.DevicePackets
					.Where(devicePacket => devicePacket.DeviceId == request.deviceId)
					.OrderByDescending(devicePacket => devicePacket.CreatedDate)
					.ToList();
			});
		}
	}
}
