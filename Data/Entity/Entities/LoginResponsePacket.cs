using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace gps_server.Data.Entity.Entities
{
    internal class LoginResponsePacket : ResponsePacket
    {
        public LoginResponsePacket()
        {
            PacketId = 3;
        }
    }
}
