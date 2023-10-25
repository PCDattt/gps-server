using gps_server.Logic.Business.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace gps_server.Logic.Business.Repositories
{
	public class UnitOfWork : IUnitOfWork
	{
		public required IUserRepository UserRepository { get; set; }
		public required IDeviceRepository DeviceRepository { get; set; }
		public required IDevicePacketRepository DevicePacketRepository { get; set; }
		public UnitOfWork()
		{
			this.UserRepository = new UserRepository();
			this.DeviceRepository = new DeviceRepository();
			this.DevicePacketRepository = new DevicePacketRepository();
		}
	}
}
