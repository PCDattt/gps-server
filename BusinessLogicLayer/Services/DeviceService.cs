﻿using AutoMapper;
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
	public class DeviceService
	{
		private readonly IUnitOfWork unitOfWork;
		private readonly IMapper autoMapper;
		public DeviceService(IUnitOfWork unitOfWork, IMapper autoMapper)
		{
			this.unitOfWork = unitOfWork;
			this.autoMapper = autoMapper;
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
		public async Task<List<DeviceResponse>> GetAllDevicesByUserEmail(string email)
		{
			try
			{
				var user = await unitOfWork.UserRepository.GetByEmailAsync(email);
				if (user == null)
				{
					throw new Exception("User not found");
				}
				var devices = await unitOfWork.DeviceRepository.GetAllByUserIdAsync(user.Id);
				var devicesResponse = devices.Select
					(
						device => autoMapper.Map<Device, DeviceResponse>(device)
					).ToList();
				return devicesResponse;
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
				return await unitOfWork.DeviceRepository.DeleteAsync(id);
			}
			catch (Exception)
			{
				throw;
			}
		}
	}
}
