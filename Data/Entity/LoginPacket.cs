using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace gps_server.Data.Entity
{
	internal class LoginPacket : BasePacket
	{
		public string Username { get; set; }
		LoginPacket()
		{
			PacketId = 2;
		}
		public override void SerializePacketBody(List<byte> buffer)
		{
			SerializeString(buffer, Username);
		}
		public override void DeserializePacketBody(List<byte> buffer, ref int offset)
		{
			Username = DeserializeString(buffer, ref offset);
		}
		public override void PrintBodyInformation()
		{
			Console.WriteLine("Username: " + Username);
		}
	}
}
