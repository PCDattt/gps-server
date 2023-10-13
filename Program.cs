// See https://aka.ms/new-console-template for more information
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO.Hashing;
using gps_server.Data.Entity;
using gps_server;

namespace gps_server
{
	internal class Program
	{
		public static async Task Main()
		{
			await TcpServer.Run();
		}
	}
}