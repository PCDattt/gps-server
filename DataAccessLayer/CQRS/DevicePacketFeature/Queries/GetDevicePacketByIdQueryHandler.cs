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
	public class GetDevicePacketByIdQueryHandler : IRequestHandler<GetDevicePacketByIdQuery, DevicePacket>
	{
		private readonly EntityDbContext entityDbContext;
		public GetDevicePacketByIdQueryHandler(EntityDbContext entityDbContext)
		{
			this.entityDbContext = entityDbContext;
		}
		public async Task<DevicePacket> Handle(GetDevicePacketByIdQuery request, CancellationToken cancellationToken)
		{
			return await Task.Run(() =>
			{
				return entityDbContext.DevicePackets
					.Where(devicePacket => devicePacket.Id == request.id)
					.FirstOrDefault();
			});
		}
	}
}
