using AutoMapper;
using BusinessLogicLayer.Models;
using DataAccessLayer.CQRS.DeviceFeature.Queries;
using DataAccessLayer.CQRS.DevicePacketFeature.Queries;
using DataAccessLayer.Interfaces;
using DataTransferObject.Entities;
using DataTransferObject.Responses;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogicLayer.Services
{
	public class DevicePacketService
	{
		private readonly IMediator mediator;
		private readonly IMapper autoMapper;
		public DevicePacketService(IMediator mediator, IMapper autoMapper)
		{
			this.mediator = mediator;
			this.autoMapper = autoMapper;
		}
		public async Task<List<DevicePacketResponse>> GetAllDevicePacketByDeviceId(int deviceId)
		{
			try
			{
				var device = await mediator.Send(new GetDeviceByIdQuery { id = deviceId });
				if (device == null)
				{
					throw new Exception("Device not found");
				}
				var devicePackets = await mediator.Send(new GetAllDevicePacketByDeviceIdQuery { deviceId = deviceId });
				var devicePacketsResponse = devicePackets.Select
					(
						devicePacket => autoMapper.Map<DevicePacket, DevicePacketResponse>(devicePacket)
					).ToList();
				return devicePacketsResponse;
			}
			catch (Exception)
			{
				throw;
			}
		}
		public async Task<DevicePacketResponse> GetLatestDevicePacketByDeviceId(int deviceId)
		{
			try
			{
				var device = await mediator.Send(new GetDeviceByIdQuery { id = deviceId });
				if (device == null)
				{
					throw new Exception("Device not found");
				}
				return autoMapper.Map<DevicePacket, DevicePacketResponse>(await mediator.Send(new GetDevicePacketByDeviceIdQuery { deviceId = deviceId }));
			}
			catch (Exception)
			{
				throw;
			}
		}
		public async Task<DevicePositionResponse> GetLatestPositionByDeviceId(int deviceId)
		{
			try
			{
				var devicePacket = await mediator.Send(new GetDevicePacketByDeviceIdQuery { deviceId = deviceId });
				byte[] buffer = new byte[30];
				buffer = Convert.FromBase64String(devicePacket.RawData);
				List<byte> listbyte = buffer.ToList();
				int packetId = BasePacket.GetPacketId(listbyte);
				if(packetId != 0)
				{
					throw new Exception("Packet is not position packet");
				}
				InformationPacket packet = new InformationPacket();
				packet.Deserialize(listbyte);
				return new DevicePositionResponse
				{
					Latitude = packet.Latitude / 1000000.0,
					Longitude = packet.Longitude / 1000000.0
				};
			}
			catch (Exception)
			{
				throw;
			}
		}
		public async Task<DevicePacket?> GetById(int id)
		{
			try
			{
				return await mediator.Send(new GetDevicePacketByIdQuery { id = id });
			}
			catch (Exception)
			{
				throw;
			}
		}
	}
}
