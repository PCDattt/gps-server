using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace gps_server.Data.Entity
{
	internal class InformationResponsePacket : ResponsePacket
	{
		InformationResponsePacket()
		{
			PacketId = 1;
		}
	}
}
