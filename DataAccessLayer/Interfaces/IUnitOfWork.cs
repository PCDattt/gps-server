﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.Interfaces
{
	public interface IUnitOfWork
	{
		IUserRepository UserRepository { get; set; }
		IDeviceRepository DeviceRepository { get; set; }
		IDevicePacketRepository DevicePacketRepository { get; set; }
	}
}
