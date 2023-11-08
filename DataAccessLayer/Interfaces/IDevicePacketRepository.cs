using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataTransferObject.Entities;

namespace DataAccessLayer.Interfaces
{
	public interface IDevicePacketRepository
	{
		public Task<DevicePacket?> AddAsync(DevicePacket devicePacket);
		public Task<DevicePacket?> GetByDeviceIdAsync(int deviceId);
		public Task<List<DevicePacket>> GetAllByDeviceIdAsync(int deviceId);
		public Task<DevicePacket?> GetByIdAsync(int id);
		public Task<bool> UpdateAsync(DevicePacket devicePacket);
		public Task<bool> DeleteAsync(int id);
	}
}
