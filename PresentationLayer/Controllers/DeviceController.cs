﻿using BusinessLogicLayer.Services;
using DataTransferObject.Entities;
using DataTransferObject.Requests;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace PresentationLayer.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	[Authorize]
	public class DeviceController : ControllerBase
	{
		private readonly DeviceService deviceService;
		public DeviceController(DeviceService deviceService)
		{
			this.deviceService = deviceService;
		}
		// POST api/<DeviceController>
		/// <summary>
		/// Add Device
		/// </summary>
		[HttpPost]
		public async Task<IActionResult> Add([FromBody] CreateDeviceRequest request)
		{
			var result = await deviceService.AddDevice(new Device
			{
				UserId = request.UserId,
				SerialNumber = request.SerialNumber,
			});
			return Ok(result);
		}
		// GET: api/<DeviceController>
		/// <summary>
		/// Get all devices
		/// </summary>
		[HttpGet]
		public async Task<IActionResult> GetAll()
		{
			var devices = await deviceService.GetAllDevices();
			return Ok(devices);
		}
		// GET api/<DeviceController>/
		/// <summary>
		/// Get device by id
		/// </summary>
		[HttpGet("{id}")]
		public async Task<IActionResult> GetById(int id)
		{
			var device = await deviceService.GetDeviceById(id);
			return Ok(device);
		}
		//Get api/<DeviceController>/user/{email}
		/// <summary>
		/// Get all devices by user email
		/// </summary>
		[HttpGet("user/{email}")]
		public async Task<IActionResult> GetAllByUserEmail(string email)
		{
			var devices = await deviceService.GetAllDevicesByUserEmail(email);
			return Ok(devices);
		}
		//Get api/<DeviceController>/
		/// <summary>
		/// Get device by SerialNumber
		/// </summary>
		[HttpGet("serial/{serialNumber}")]
		public async Task<IActionResult> GetBySerialNumber(string serialNumber)
		{
			var device = await deviceService.GetDeviceBySerialNumber(serialNumber);
			return Ok(device);
		}
		//Put api/<DeviceController>/
		/// <summary>
		/// Update device
		/// </summary>
		[HttpPut]
		public async Task<IActionResult> Update([FromBody] UpdateDeviceRequest request)
		{
			var result = await deviceService.UpdateDevice(new Device
			{
				Id = request.Id,
				UserId = request.UserId,
				SerialNumber = request.SerialNumber,
			});
			return Ok(result);
		}
		//Delete api/<DeviceController>/
		/// <summary>
		/// Delete device
		/// </summary>
		[HttpDelete("{id}")]
		public async Task<IActionResult> Delete(int id)
		{
			var result = await deviceService.DeleteDevice(id);
			return Ok(result);
		}
	}
}
