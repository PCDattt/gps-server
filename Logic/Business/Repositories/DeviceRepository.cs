using gps_server.Data.DataAccess.Contexts;
using gps_server.Logic.Business.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using gps_server.Data.Entity.Entities;

namespace gps_server.Logic.Business.Repositories
{
	public class DeviceRepository : IDeviceRepository
	{
		private readonly EntityDbContext entityDbContext;
		public DeviceRepository()
		{
			entityDbContext = new();
		}
		public async Task<Device?> AddAsync(Device device)
		{
			var record = await GetBySerialNumberAsync(device.SerialNumber);
			if (record != null)
			{
				return null;
			}
			var now = DateTime.Now;
			_ = await entityDbContext.Devices.AddAsync(new()
			{
				UserId = device.UserId,
				SerialNumber = device.SerialNumber,
				CreatedDate = now,
				ModifiedDate = now,
				IsDeleted = false
			});
			_ = entityDbContext.SaveChanges();
			return device;
		}
		public async Task<Device?> GetByIdAsync(int id)
		{
			return await Task.Run(() =>
			{
				return entityDbContext.Devices
					.Where(device => device.Id == id)
					.FirstOrDefault();
			});
		}
		public async Task<Device?> GetBySerialNumberAsync(string serialNumber)
		{
			return await Task.Run(() =>
			{
				return entityDbContext.Devices
					.Where(device => device.SerialNumber == serialNumber)
					.FirstOrDefault();
			});
		}
		public async Task<List<Device>> GetAllAsync()
		{
			return await Task.Run(() =>
			{
				return entityDbContext.Devices
					.Where(device => device.IsDeleted == false)
					.ToList();
			});
		}
		public async Task<bool> UpdateAsync(Device device)
		{
			var record = await GetByIdAsync(device.Id);
			if (record == null)
			{
				return false;
			}
			record.UserId = device.UserId;
			record.SerialNumber = device.SerialNumber;
			record.ModifiedDate = DateTime.Now;
			_ = entityDbContext.SaveChanges();
			return true;
		}
		public async Task<bool> DeleteAsync(int id)
		{
			var record = await GetByIdAsync(id);
			if (record == null)
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
