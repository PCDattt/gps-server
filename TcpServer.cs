using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using System.Net.Sockets;
using gps_server.Data.Entity;

namespace gps_server
{
	internal class TcpServer
	{
		public static async Task Run()
		{
			TcpListener listener = new TcpListener(IPAddress.Parse("192.168.22.105"), 12345);
			
			listener.Start();
			Console.WriteLine("Server started");
			while(true)
			{
				Console.WriteLine("Waiting for a connection...");
				TcpClient client = await listener.AcceptTcpClientAsync();
				_ = HandleClientAsync(client);
			}
		}

		public static async Task HandleClientAsync(TcpClient client)
		{
			try
			{
				using (NetworkStream stream = client.GetStream())
				{
					byte[] buffer = new byte[1024];
					int bytesRead;

					while ((bytesRead = await stream.ReadAsync(buffer, 0, buffer.Length)) > 0)
					{
						Console.WriteLine("Received: " + bytesRead + " bytes");
						//Process bytes to packet
						List<byte> listbyte = buffer.ToList();
						int packetId = BasePacket.GetPacketId(listbyte);
						BasePacket receivedPacket = PacketFactory.GetPacket(packetId);
						receivedPacket.Deserialize(listbyte);
						if(receivedPacket.Checksum == receivedPacket.CalculateChecksum())
						{
							Console.WriteLine("Checksum is correct");
							receivedPacket.PrintInformation();

							//Send response packet
							BasePacket responsePacket = PacketFactory.GetPacket(packetId + 1);
							responsePacket.FillResponseInformation(receivedPacket);
							List<byte> responseBuffer = responsePacket.Serialize();
							await stream.WriteAsync(responseBuffer.ToArray(), 0, responseBuffer.Count);
						}
						else
						{
							Console.WriteLine("Checksum error");
						}
					}
				}
			}
			catch (Exception e)
			{
				Console.WriteLine($"Error: {e}");
			}
			finally
			{
				client.Close();
				Console.WriteLine("Client disconnected");
			}
		}
	}
}
