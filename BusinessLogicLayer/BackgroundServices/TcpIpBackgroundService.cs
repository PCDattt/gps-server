﻿using BusinessLogicLayer.Models;
using DataAccessLayer.Interfaces;
using DataAccessLayer.Repositories;
using DataTransferObject.Entities;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using BasePacket = BusinessLogicLayer.Models.BasePacket;

namespace BusinessLogicLayer.BackgroundServices
{
	public class TcpIpBackgroundService : BackgroundService
	{
		private readonly IServiceScopeFactory scopeFactory;
		public TcpIpBackgroundService(IServiceScopeFactory scopeFactory)
		{
			this.scopeFactory = scopeFactory;
		}
		public async Task HandleClientAsync(TcpClient client)
		{
			//try
			//{
				using (NetworkStream stream = client.GetStream())
				{
					using (var scope = scopeFactory.CreateScope())
					{
						var devicePacketRepository = scope.ServiceProvider.GetRequiredService<IDevicePacketRepository>();
						byte[] buffer = new byte[30];
						int bytesRead;

						while ((bytesRead = await stream.ReadAsync(buffer, 0, buffer.Length)) > 0)
						{
							//Console.WriteLine("Received: " + bytesRead + " bytes");
							//Process bytes to packet
							List<byte> listbyte = buffer.ToList();
							int packetId = BasePacket.GetPacketId(listbyte);
							BasePacket receivedPacket = PacketFactory.GetPacket(packetId);
							receivedPacket.Deserialize(listbyte);
							if (receivedPacket.Checksum == receivedPacket.CalculateChecksum())
							{
								Console.WriteLine("Checksum is correct");
								receivedPacket.PrintInformation();

								//Save packet to database
								DevicePacket devicePacket = new()
								{
									DeviceId = Int32.Parse(receivedPacket.DeviceId),
									RawData = Convert.ToBase64String(buffer)
								};
								_ = await devicePacketRepository.AddAsync(devicePacket);

								//Send response packet
								BasePacket responsePacket = PacketFactory.GetPacket(packetId + 1);
								responsePacket.FillResponseInformation(receivedPacket);
								List<byte> responseBuffer = responsePacket.Serialize();
								await stream.WriteAsync(responseBuffer.ToArray(), 0, responseBuffer.Count);
							}
							else
							{
								//Console.WriteLine("Checksum error");
							}
						}
					}
				}
			//}
			//catch (Exception e)
			//{
			//	Console.WriteLine($"Error: {e}");
			//}
			//finally
			//{
			//	client.Close();
			//	Console.WriteLine("Client disconnected");
			//}
		}
		protected override async Task ExecuteAsync(CancellationToken stoppingToken)
		{
			TcpListener listener = new TcpListener(IPAddress.Parse("192.168.22.105"), 12345);

			listener.Start();
			//Console.WriteLine("Server started");
			//Console.WriteLine("Waiting for a connection...");
			while (!stoppingToken.IsCancellationRequested)
			{
				//_logger.LogInformation("Worker running at: {time}", DateTimeOffset.Now);
				//await Task.Delay(1000, stoppingToken);
				
				TcpClient client = await listener.AcceptTcpClientAsync();
				//Console.WriteLine("Client connected\n");
				_ = HandleClientAsync(client);
			}
		}
	}
}