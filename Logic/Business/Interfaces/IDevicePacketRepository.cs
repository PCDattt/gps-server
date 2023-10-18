using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using gps_server.Data.Entity.Entities;

namespace gps_server.Logic.Business.Interfaces
{
	public interface IDevicePacketRepository
	{
		public Task<DevicePacket?> AddAsync(DevicePacket devicePacket); 
	}
}
