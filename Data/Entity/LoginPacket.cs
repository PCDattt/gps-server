using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace gps_server.Data.Entity
{
	internal class LoginPacket : BasePacket
	{
		public LoginPacket()
		{
			PacketId = 2;
		}
		//public override void deserializepacketbody(list<byte> buffer, ref int offset)
		//{
		//	username = deserializestring(buffer, ref offset);
		//}
		//public override void printbodyinformation()
		//{
		//	console.writeline("username: " + username);
		//}
	}
}
