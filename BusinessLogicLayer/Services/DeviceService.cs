using DataAccessLayer.Interfaces;
using DataTransferObject.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogicLayer.Services
{
	public class DeviceService
	{
		private readonly IUnitOfWork unitOfWork;
		public DeviceService(IUnitOfWork unitOfWork)
		{
			this.unitOfWork = unitOfWork;
		}
		public async Task<Device?> GetDeviceById(int id)
		{
			try
			{
				return await unitOfWork.DeviceRepository.GetByIdAsync(id);
			}
			catch (Exception)
			{
				throw;
			}
		}
		public async Task<Device?> GetDeviceBySerialNumber(string serialNumber)
		{
			try
			{
				return await unitOfWork.DeviceRepository.GetBySerialNumberAsync(serialNumber);
			}
			catch (Exception)
			{
				throw;
			}
		}
		public async Task<List<Device>> GetAllDevices()
		{
			try
			{
				return await unitOfWork.DeviceRepository.GetAllAsync();
			}
			catch (Exception)
			{
				throw;
			}
		}
		public async Task<Device?> AddDevice(Device device)
		{
			try
			{
				var record = await GetDeviceBySerialNumber(device.SerialNumber);
				if (record != null)
				{
					return null;
				}
				return await unitOfWork.DeviceRepository.AddAsync(device);
			}
			catch (Exception)
			{
				throw;
			}
		}
		public async Task<bool> UpdateDevice(Device device)
		{
			try
			{
				var record = await GetDeviceById(device.Id);
				if (record == null)
				{
					return false;
				}
				return await unitOfWork.DeviceRepository.UpdateAsync(device);
			}
			catch (Exception)
			{
				throw;
			}
		}
		public async Task<bool> DeleteDevice(int id)
		{
			try
			{
				var record = await GetDeviceById(id);
				if (record == null)
				{
					return false;
				}
				record.IsDeleted = true;
				record.ModifiedDate = DateTime.Now;
				return await unitOfWork.DeviceRepository.DeleteAsync(record);
			}
			catch (Exception)
			{
				throw;
			}
		}
	}
}
