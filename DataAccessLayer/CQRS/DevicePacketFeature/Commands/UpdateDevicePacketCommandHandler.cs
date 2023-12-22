using DataTransferObject.Contexts;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.CQRS.DevicePacketFeature.Commands
{
	public class UpdateDevicePacketCommandHandler : IRequestHandler<UpdateDevicePacketCommand, bool>
	{
		private readonly EntityDbContext entityDbContext;
		public UpdateDevicePacketCommandHandler(EntityDbContext entityDbContext)
		{
			this.entityDbContext = entityDbContext;
		}
public async Task<bool> Handle(UpdateDevicePacketCommand request, CancellationToken cancellationToken)
		{
			return await Task.Run(() =>
			{
				var devicePacket = entityDbContext.DevicePackets.Where(x => x.Id == request.devicePacket.Id).FirstOrDefault();
				if (devicePacket != null)
				{
					devicePacket.RawData = request.devicePacket.RawData;
					devicePacket.ModifiedDate = DateTime.Now;
					_ = entityDbContext.SaveChanges();
					return true;
				}
				return false;
			});
		}
	}
}
