using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogicLayer.Models
{
    internal class LoginResponsePacket : ResponsePacket
    {
        public LoginResponsePacket()
        {
            PacketId = 3;
        }
    }
}
