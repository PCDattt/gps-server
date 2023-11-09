using AutoMapper;
using DataAccessLayer.Interfaces;
using DataTransferObject.Entities;
using DataTransferObject.Responses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogicLayer.Services
{
	public class DevicePacketService
	{
		private readonly IUnitOfWork unitOfWork;
		private readonly IMapper autoMapper;
		public DevicePacketService(IUnitOfWork unitOfWork, IMapper autoMapper)
		{
			this.unitOfWork = unitOfWork;
			this.autoMapper = autoMapper;
		}
		public async Task<List<DevicePacketResponse>> GetAllDevicePacketByDeviceId(int deviceId)
		{
			try
			{
				var device = await unitOfWork.DeviceRepository.GetByIdAsync(deviceId);
				if (device == null)
				{
					throw new Exception("Device not found");
				}
				var devicePackets = await unitOfWork.DevicePacketRepository.GetAllByDeviceIdAsync(deviceId);
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
				var device = await unitOfWork.DeviceRepository.GetByIdAsync(deviceId);
				if (device == null)
				{
					throw new Exception("Device not found");
				}
				return autoMapper.Map<DevicePacket, DevicePacketResponse>(await unitOfWork.DevicePacketRepository.GetByDeviceIdAsync(deviceId));
				//return await unitOfWork.DevicePacketRepository.GetByDeviceIdAsync(deviceId);
			}
			catch (Exception)
			{
				throw;
			}
		}
		public async Task<DevicePacket?> GetById(int id)
		{
			return await unitOfWork.DevicePacketRepository.GetByIdAsync(id);
		}
	}
}
