using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogicLayer.Models
{
    internal class InformationResponsePacket : ResponsePacket
    {
        public InformationResponsePacket()
        {
            PacketId = 1;
        }
    }
}
