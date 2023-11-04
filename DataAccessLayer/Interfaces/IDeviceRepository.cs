using DataTransferObject.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.Interfaces
{
	public interface IDeviceRepository
	{
		public Task<Device?> AddAsync(Device device);
		public Task<Device?> GetByIdAsync(int id);
		public Task<Device?> GetBySerialNumberAsync(string serialNumber);
		public Task<List<Device>> GetAllAsync();
		public Task<bool> UpdateAsync(Device device);
		public Task<bool> DeleteAsync(Device device);
	}
}
