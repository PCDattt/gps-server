using gps_server.Logic.Business.Interfaces;
using gps_server.Data.Entity.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using gps_server.Data.DataAccess.Contexts;
using Azure.Identity;

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
		public async Task<DevicePacket?> GetByDeviceIdAsync(int deviceId)
		{
			return await Task.Run(() =>
			{
				return entityDbContext.DevicePackets
					.Where(devicePacket => devicePacket.DeviceId == deviceId)
					.OrderByDescending(devicePacket => devicePacket.CreatedDate)
					.FirstOrDefault();
			});
		}
		public async Task<DevicePacket?> GetByIdAsync(int id)
		{
			return await Task.Run(() =>
			{
				return entityDbContext.DevicePackets
					.Where(devicePacket => devicePacket.Id == id)
					.FirstOrDefault();
			});
		}
		public async Task<bool> UpdateAsync(DevicePacket devicePacket)
		{
			var record = await GetByIdAsync(devicePacket.Id);
			if(record == null)
			{
				return false;
			}
			record.DeviceId = devicePacket.DeviceId;
			record.RawData = devicePacket.RawData;
			record.ModifiedDate = DateTime.Now;
			_ = entityDbContext.SaveChanges();
			return true;
		}
		public async Task<bool> DeleteAsync(int id)
		{
			var record = await GetByIdAsync(id);
			if(record == null)
			{
				return false;
			}
			record.IsDeleted = true;
			record.ModifiedDate = DateTime.Now;
			_ = entityDbContext.SaveChanges();
			return true;
		}
	}
}
