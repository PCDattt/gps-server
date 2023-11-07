using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO.Hashing;
using System.ComponentModel.Design;

namespace BusinessLogicLayer.Models
{
    public abstract class BasePacket
    {
        public ushort Starting { get; set; }
        public ushort PacketId { get; set; }
        public string DeviceId { get; set; }
        public ushort PacketOrderIndex { get; set; }
        public ushort Checksum { get; set; }
        public ushort Ending { get; set; }
        public virtual void SerializePacketBody(List<byte> buffer) { }
        public virtual void DeserializePacketBody(List<byte> buffer, ref int offset) { }
        public virtual void FillBodyResponseInformation(BasePacket receivedPacket) { }
        public virtual void PrintBodyInformation() { }
        public virtual void ProcessPacketBody(List<byte> buffer) { }
        public static void SerializeUInt16(List<byte> buffer, ushort value)
        {
            buffer.Add((byte)(value >> 8 & 0xFF));
            buffer.Add((byte)(value & 0xFF));
        }
        public static void SerializeUInt32(List<byte> buffer, uint value)
        {
            buffer.Add((byte)(value >> 24 & 0xFF));
            buffer.Add((byte)(value >> 16 & 0xFF));
            buffer.Add((byte)(value >> 8 & 0xFF));
            buffer.Add((byte)(value & 0xFF));
        }
        public static void SerializeString(List<byte> buffer, string value)
        {
            SerializeUInt16(buffer, (ushort)value.Length);

            foreach (char c in value)
            {
                buffer.Add((byte)c);
            }
        }
        public static ushort DeserializeUInt16(List<byte> buffer, ref int offset)
        {
            ushort value = 0;
            if (offset + 2 > buffer.Count) return 0;

            value = (ushort)(buffer[offset] << 8);
            value |= buffer[offset + 1];
            offset += 2;
            return value;
        }

        public static uint DeserializeUInt32(List<byte> buffer, ref int offset)
        {
            uint value = 0;
            if (offset + 4 > buffer.Count) return 0;

            value = (uint)(buffer[offset] << 24);
            value |= (uint)(buffer[offset + 1] << 16);
            value |= (uint)(buffer[offset + 2] << 8);
            value |= buffer[offset + 3];
            offset += 4;
            return value;
        }

        public static string DeserializeString(List<byte> buffer, ref int offset)
        {
            string value = string.Empty;
            ushort length = DeserializeUInt16(buffer, ref offset);

            if (offset + length > buffer.Count) return "0";

            value = Encoding.UTF8.GetString(buffer.GetRange(offset, length).ToArray());
            offset += length;

            return value;
        }

        public static ushort GetPacketId(List<byte> buffer)
        {
            int offset = 2;
            ushort packetId = DeserializeUInt16(buffer, ref offset);
            return packetId;
        }

        public void SerializePacketStarting(List<byte> buffer)
        {
            SerializeUInt16(buffer, Starting);
            SerializeUInt16(buffer, PacketId);
            SerializeString(buffer, DeviceId);
            SerializeUInt16(buffer, PacketOrderIndex);
        }

        public void SerializePacketEnding(List<byte> buffer)
        {
            SerializeUInt16(buffer, Checksum);
            SerializeUInt16(buffer, Ending);
        }

        public List<byte> Serialize()
        {
            List<byte> buffer = new List<byte>();

            SerializePacketStarting(buffer);
            SerializePacketBody(buffer);
            SerializePacketEnding(buffer);

            return buffer;
        }

        public void DeserializePacketStarting(List<byte> buffer, ref int offset)
        {
            Starting = DeserializeUInt16(buffer, ref offset);
            PacketId = DeserializeUInt16(buffer, ref offset);
            DeviceId = DeserializeString(buffer, ref offset);
            PacketOrderIndex = DeserializeUInt16(buffer, ref offset);
        }

        public void DeserializePacketEnding(List<byte> buffer, ref int offset)
        {
            Checksum = DeserializeUInt16(buffer, ref offset);
            Ending = DeserializeUInt16(buffer, ref offset);
        }

        public void Deserialize(List<byte> buffer)
        {
            int offset = 0;
            DeserializePacketStarting(buffer, ref offset);
            DeserializePacketBody(buffer, ref offset);
            DeserializePacketEnding(buffer, ref offset);
        }

        public void FillResponseInformation(BasePacket receivedPacket)
        {
            Starting = (ushort)(receivedPacket.Starting + 2);
            PacketId = (ushort)(receivedPacket.PacketId + 1);
            DeviceId = receivedPacket.DeviceId;
            PacketOrderIndex = (ushort)(receivedPacket.PacketOrderIndex + 1);
            FillBodyResponseInformation(receivedPacket);
            Checksum = CalculateChecksum();
            Ending = (ushort)(receivedPacket.Ending + 3);
        }

        public void PrintInformation()
        {
            Console.WriteLine("Packet starting: " + Starting);
            Console.WriteLine("Packet id: " + PacketId);
            Console.WriteLine("Packet device id: " + DeviceId);
            Console.WriteLine("Packet order index: " + PacketOrderIndex);
            PrintBodyInformation();
            Console.WriteLine("Packet checksum: " + Checksum);
            Console.WriteLine("Packet ending: " + Ending);
            Console.WriteLine();
        }
        public void ProcessPacketStarting(List<byte> buffer)
        {
            buffer.AddRange(Encoding.ASCII.GetBytes(Starting.ToString()));
            buffer.AddRange(Encoding.ASCII.GetBytes(PacketId.ToString()));
            buffer.AddRange(Encoding.ASCII.GetBytes(DeviceId));
            buffer.AddRange(Encoding.ASCII.GetBytes(PacketOrderIndex.ToString()));
        }
        public ushort CalculateChecksum()
        {
            List<byte> buffer = new List<byte>();
            ProcessPacketStarting(buffer);
            ProcessPacketBody(buffer);
            byte[] fullchecksum = Crc32.Hash(buffer.ToArray());

            //Get two last bytes of fullchecksum and convert to ushort
            uint temp = BitConverter.ToUInt32(fullchecksum, 0);
            byte[] bytes = BitConverter.GetBytes(temp & 0xFFFF);
            return BitConverter.ToUInt16(bytes, 0);
        }
        public bool IsValidChecksum()
        {
            return CalculateChecksum() == Checksum;
        }
    }
}
