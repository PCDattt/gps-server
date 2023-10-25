using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace gps_server.Logic.Business.Interfaces
{
	public interface IUnitOfWork
	{
		IUserRepository UserRepository { get; set; }
		IDeviceRepository DeviceRepository { get; set; }
		IDevicePacketRepository DevicePacketRepository { get; set; }
	}
}
