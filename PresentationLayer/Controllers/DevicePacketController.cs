using BusinessLogicLayer.Services;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace PresentationLayer.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class DevicePacketController : ControllerBase
	{
		private readonly DevicePacketService devicePacketService;
		public DevicePacketController(DevicePacketService devicePacketService)
		{
			this.devicePacketService = devicePacketService;
		}
		// GET: api/<DevicePacketController>
		/// <summary>
		/// Get latest device packet by device id
		/// </summary>
		[HttpGet("{deviceId}")]
		public async Task<IActionResult> GetDevicePacketByDeviceId(int deviceId)
		{
			var devicePacket = await devicePacketService.GetLatestDevicePacketByDeviceId(deviceId);
			return Ok(devicePacket);
		}

		// GET api/<DevicePacketController>/
		/// <summary>
		/// Get all device packet by device id
		/// </summary>
		[HttpGet("all/{deviceId}")]
		public async Task<IActionResult> GetAllDevicePacketByDeviceId(int deviceId)
		{
			var devicePackets = await devicePacketService.GetAllDevicePacketByDeviceId(deviceId);
			return Ok(devicePackets);
		}
		[HttpGet("id/{id}")]
		public async Task<IActionResult> GetById(int id)
		{
			var devicePacket = await devicePacketService.GetById(id);
			return Ok(devicePacket);
		}
	}
}
