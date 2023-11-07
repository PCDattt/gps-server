using DataAccessLayer.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.Repositories
{
	public class UnitOfWork : IUnitOfWork
	{
		public required IUserRepository UserRepository { get; set; }
		public required IDeviceRepository DeviceRepository { get; set; }
		public required IDevicePacketRepository DevicePacketRepository { get; set; }
		public UnitOfWork(
			IDeviceRepository deviceRepository,
			IUserRepository userRepository)
		{
			this.UserRepository = userRepository;
			this.DeviceRepository = deviceRepository;
		}
	}
}
