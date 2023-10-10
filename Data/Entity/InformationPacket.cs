using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace gps_server.Data.Entity
{
	internal class InformationPacket : BasePacket
	{
		public uint Latitude { get; set; }
		public uint Longitude { get; set; }
		InformationPacket()
		{
			PacketId = 0;
		}
		public override void SerializePacketBody(List<byte> buffer)
		{
			SerializeUInt32(buffer, Latitude);
			SerializeUInt32(buffer, Longitude);
		}
		public override void DeserializePacketBody(List<byte> buffer, ref int offset)
		{
			Latitude = DeserializeUInt32(buffer, ref offset);
			Longitude = DeserializeUInt32(buffer, ref offset);
		}
		public override void PrintBodyInformation()
		{
			Console.WriteLine("Latitude: " + Latitude);
			Console.WriteLine("Longitude: " + Longitude);
		}
	}
}
