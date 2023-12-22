using AutoMapper;
using DataAccessLayer.CQRS.DeviceFeature.Commands;
using DataAccessLayer.CQRS.DeviceFeature.Queries;
using DataAccessLayer.CQRS.UserFeature.Queries;
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
	public class DeviceService
	{
		private readonly IMediator mediator;
		private readonly IMapper autoMapper;
		public DeviceService(IMediator mediator, IMapper autoMapper)
		{
			this.mediator = mediator;
			this.autoMapper = autoMapper;
		}
		public async Task<Device?> GetDeviceById(int id)
		{
			try
			{
				return await mediator.Send(new GetDeviceByIdQuery { id = id });
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
				return await mediator.Send(new GetDeviceBySerialNumberQuery { serialNumber = serialNumber });
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
				return await mediator.Send(new GetAllDeviceQuery());
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
				var user = await mediator.Send(new GetUserByEmailQuery { email = email });
				if (user == null)
				{
					throw new Exception("User not found");
				}
				var devices = await mediator.Send(new GetAllDeviceByUserIdQuery { id = user.Id });
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
				var record = await mediator.Send(new GetDeviceBySerialNumberQuery { serialNumber = device.SerialNumber });
				if (record != null)
				{
					return null;
				}
				return await mediator.Send(new AddDeviceCommand { device = device });
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
				var record = await mediator.Send(new GetDeviceByIdQuery { id = device.Id });
				if (record == null)
				{
					return false;
				}
				return await mediator.Send(new UpdateDeviceCommand { device = device });
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
				return await mediator.Send(new DeleteDeviceCommand { id = id });
			}
			catch (Exception)
			{
				throw;
			}
		}
	}
}
