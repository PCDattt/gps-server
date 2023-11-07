using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogicLayer.Models
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
        public override void ProcessPacketBody(List<byte> buffer)
        {
            buffer.AddRange(Encoding.ASCII.GetBytes(ReceivedPacketIndex.ToString()));
        }
    }
}
