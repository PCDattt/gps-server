﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogicLayer.Models
{
    internal class InformationPacket : BasePacket
    {
        public int Latitude { get; set; }
        public int Longitude { get; set; }
        public InformationPacket()
        {
            PacketId = 0;
        }
        public override void DeserializePacketBody(List<byte> buffer, ref int offset)
        {
            Latitude = DeserializeInt32(buffer, ref offset);
            Longitude = DeserializeInt32(buffer, ref offset);
        }
        public override void PrintBodyInformation()
        {
            Console.WriteLine("Latitude: " + Latitude);
            Console.WriteLine("Longitude: " + Longitude);
        }
        public override void ProcessPacketBody(List<byte> buffer)
        {
            buffer.AddRange(Encoding.ASCII.GetBytes(Latitude.ToString()));
            buffer.AddRange(Encoding.ASCII.GetBytes(Longitude.ToString()));
        }
    }
}
