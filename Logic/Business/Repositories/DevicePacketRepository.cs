using gps_server.Logic.Business.Interfaces;
using gps_server.Data.Entity.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using gps_server.Data.DataAccess.Contexts;

namespace gps_server.Logic.Business.Repositories
{
	public class DevicePacketRepository : IDevicePacketRepository
	{
		private readonly EntityDbContext entityDbContext;
		public DevicePacketRepository()
		{
			this.entityDbContext = new();
		}
		public async Task<DevicePacket?> AddAsync(DevicePacket devicePacket)
		{
			var now = DateTime.Now;
			_ = await entityDbContext.DevicePackets.AddAsync(new()
			{
				DeviceId = devicePacket.DeviceId,
				RawData = devicePacket.RawData,
				CreatedDate = now,
				ModifiedDate = now,
				IsDeleted = false
			});
			_ = entityDbContext.SaveChanges();
			return devicePacket;
		}
	}
}
