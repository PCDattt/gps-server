using DataAccessLayer.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataTransferObject.Entities;
using DataTransferObject.Contexts;

namespace DataAccessLayer.Repositories
{
	public class DeviceRepository : IDeviceRepository
	{
		private readonly EntityDbContext entityDbContext;
		public DeviceRepository(EntityDbContext entityDbContext)
		{
			this.entityDbContext = entityDbContext;
		}
		public async Task<Device?> AddAsync(Device device)
		{
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
			_ = entityDbContext.Devices.Update(device);
			_ = await entityDbContext.SaveChangesAsync();
			return true;
		}
		public async Task<bool> DeleteAsync(Device device)
		{
			_ = entityDbContext.Devices.Update(device);
			_ = await entityDbContext.SaveChangesAsync();
			return true;
		}
	}
}
