// See https://aka.ms/new-console-template for more information
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO.Hashing;
using gps_server.Data.Entity.Entities;
using gps_server;
using gps_server.Logic.Business.Repositories;

namespace gps_server
{
	public class Program
	{
		public static async Task Main()
		{
			//DevicePacket devicePacket = new()
			//{
			//	DeviceId = 1,
			//	RawData = "test"
			//};
			//DevicePacketRepository devicePacketRepository = new();
			//_ = await devicePacketRepository.AddAsync(devicePacket);
			await TcpServer.Run();
		}
	}
}