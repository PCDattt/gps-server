using DataAccessLayer.Interfaces;
using DataTransferObject.Entities;
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
		public DevicePacketService(IUnitOfWork unitOfWork)
		{
			this.unitOfWork = unitOfWork;
		}
		public async Task<List<DevicePacket>> GetAllDevicePacketByDeviceId(int deviceId)
		{
			try
			{
				var device = await unitOfWork.DeviceRepository.GetByIdAsync(deviceId);
				if (device == null)
				{
					throw new Exception("Device not found");
				}
				return await unitOfWork.DevicePacketRepository.GetAllByDeviceIdAsync(deviceId);
			}
			catch (Exception)
			{
				throw;
			}
		}
		public async Task<DevicePacket?> GetLatestDevicePacketByDeviceId(int deviceId)
		{
			try
			{
				var device = await unitOfWork.DeviceRepository.GetByIdAsync(deviceId);
				if (device == null)
				{
					throw new Exception("Device not found");
				}
				return await unitOfWork.DevicePacketRepository.GetByDeviceIdAsync(deviceId);
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
