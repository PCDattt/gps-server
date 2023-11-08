using Azure.Core;
using DataTransferObject.Requests;
using BusinessLogicLayer.Services;
using DataAccessLayer.Repositories;
using DataTransferObject.Entities;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace PresentationLayer.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class UserController : ControllerBase
	{
		private readonly UserService userService;
		private readonly IConfiguration configuration;
		public UserController(UserService userService, IConfiguration configuration)
		{
			this.userService = userService;
			this.configuration = configuration;
		}
		// POST api/<UserController>
		/// <summary>
		/// Add user
		/// </summary>
		[HttpPost]
		public async Task<IActionResult> Add([FromBody] CreateUserRequest request)
		{
			var result = await userService.AddUser(new User
			{
				Email = request.Email,
				Role = request.Role,
				Username = request.Username,
				Name = request.Name,
			}, request.Password);
			return Ok(result);
		}
		// GET: api/<UserController>
		/// <summary>
		/// Get all users
		/// </summary>
		/// <returns>List user</returns>
		[HttpGet]
		public async Task<IActionResult> GetAll()
		{
			var users = await userService.GetAllUser();
			return Ok(users);
		}

		// GET api/<UserController>/
		/// <summary>
		/// Get user by id
		/// </summary>
		[HttpGet("{id}")]
		public async Task<IActionResult> GetById(int id)
		{
			var user = await userService.GetUserById(id);
			return Ok(user);
		}
		/// <summary>
		/// Get user by email
		/// </summary>
		[HttpGet("email")]
		public async Task<IActionResult> GetByEmail(string email)
		{
			var user = await userService.GetUserByEmail(email);
			return Ok(user);
		}
		/// <summary>
		/// Get users by name
		/// </summary>
		[HttpGet("name")]
		public async Task<IActionResult> GetByName(string name)
		{
			var users = await userService.SearchUserByName(name);
			return Ok(users);
		}

		// PUT api/<UserController>/
		/// <summary>
		/// Update user
		/// </summary>
		[HttpPut]
		public async Task<IActionResult> Update([FromBody] UpdateUserRequest request)
		{
			var result = await userService.UpdateUser(new User
			{
				Id = request.Id,
				Email = request.Email,
				Role = request.Role,
				Name = request.Name,
			});
			return Ok(result);
		}

		// DELETE api/<UserController>/
		/// <summary>
		/// Delete user by id
		/// </summary>
		[HttpDelete("{id}")]
		public async Task<IActionResult> Delete(int id)
		{
			var result = await userService.DeleteUser(id);
			return Ok(result);
		}
	}
}
