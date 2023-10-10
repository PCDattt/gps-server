﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace gps_server.Data.Entity
{
	internal class ResponsePacket : BasePacket
	{
		public ushort ReceivedPacketIndex { get; set; }
		public override void SerializePacketBody(List<byte> buffer)
		{
			SerializeUInt16(buffer, ReceivedPacketIndex);
		}
		public override void FillBodyResponseInformation(BasePacket receivedPacket)
		{
			ReceivedPacketIndex = receivedPacket.PacketOrderIndex;
		}
	}
}
