using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace gps_server.Data.Entity
{
	internal class PacketFactory
	{
		enum PacketType
		{
			InformationPacket = 0,
			InformationResponsePacket = 1,
			LoginPacket = 2,
			LoginResponsePacket = 3
		}
		public static BasePacket GetPacket(int packetType)
		{
			switch (packetType)
			{
				case 0:
					return new InformationPacket();
				case 1:
					return new InformationResponsePacket();
				case 2:
					return new LoginPacket();
				case 3:
					return new LoginResponsePacket();
				default:
					return null;
			}
		}
	}
}
