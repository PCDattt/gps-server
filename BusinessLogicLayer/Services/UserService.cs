using AutoMapper;
using DataAccessLayer.CQRS.UserFeature.Queries;
using DataAccessLayer.Interfaces;
using DataTransferObject.Entities;
using DataTransferObject.Responses;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogicLayer.Services
{
	public class UserService
	{
		private readonly IUnitOfWork unitOfWork;
		private readonly IMapper autoMapper;
		private readonly IConfiguration configuration;
		private readonly IMediator mediator;
		public UserService(IUnitOfWork unitOfWork, IMapper autoMapper, IConfiguration configuration, IMediator mediator)
		{
			this.unitOfWork = unitOfWork;
			this.autoMapper = autoMapper;
			this.configuration = configuration;
			this.mediator = mediator;
		}
		public async Task<string> GeneratePasswordHashAsync(User user, string password)
		{
			// hash password
			return await Task.Run(() =>
			{
				var passwordHasher = new PasswordHasher<User>();
				var hash = passwordHasher.HashPassword(user, $"{password}");
				return hash;
			});
		}
		public async Task<bool> ValidatePasswordAsync(User user, string password)
		{
			return await Task.Run(() =>
			{
				var passwordHasher = new PasswordHasher<User>();
				var result = passwordHasher.VerifyHashedPassword(user, user.PasswordHash, $"{password}");
				return result == PasswordVerificationResult.Success;
			});
		}
		public async Task<User?> AddUser(User user, string password)
		{
			try
			{
				var record = await GetUserByEmail(user.Email);
				if (record != null)
				{
					return null;
				}
				var hashPassword = await GeneratePasswordHashAsync(user, password);
				return await unitOfWork.UserRepository.AddAsync(user, hashPassword);
			}
			catch (Exception)
			{
				throw;
			}
		}
		public async Task<User?> GetUserById(int id)
		{
			try
			{
				//return await unitOfWork.UserRepository.GetByIdAsync(id);
				return await mediator.Send(new GetUserByIdQuery { id = id });
			}
			catch (Exception)
			{
				throw;
			}
		}
		public async Task<User?> GetUserByEmail(string email)
		{
			try
			{
				//return await unitOfWork.UserRepository.GetByEmailAsync(email);
				return await mediator.Send(new GetUserByEmailQuery { email = email });
			}
			catch (Exception)
			{
				throw;
			}
		}
		public async Task<List<User>> GetAllUser()
		{
			try
			{
				//return await unitOfWork.UserRepository.GetAllAsync();
				return await mediator.Send(new GetAllUserQuery());
			}
			catch (Exception)
			{
				throw;
			}
		}
		public async Task<IEnumerable<User>> SearchUserByName(string name)
		{
			try
			{
				//return await unitOfWork.UserRepository.SearchByNameAsync(name);
				return await mediator.Send(new SearchUserByNameQuery { name = name });
			}
			catch (Exception)
			{
				throw;
			}
		}
		public async Task<bool> UpdateUser(User user, string password)
		{
			try
			{
				var record = await GetUserByEmail(user.Email);
				if (record == null)
				{
					return false;
				}
				if(password == string.Empty)
				{
					return await unitOfWork.UserRepository.UpdateAsync(user, record.PasswordHash);
				}
				var hashPassword = await GeneratePasswordHashAsync(user, password);
				return await unitOfWork.UserRepository.UpdateAsync(user, hashPassword);
			}
			catch (Exception)
			{
				throw;
			}
		}
		public async Task<bool> DeleteUser(int id)
		{
			try
			{
				var record = await GetUserById(id);
				if (record == null)
				{
					return false;
				}
				return await unitOfWork.UserRepository.DeleteAsync(id);
			}
			catch (Exception)
			{
				throw;
			}
		}
		//Return string JWT Token
		public async Task<string?> UserLogin(string email, string password)
		{
			try
			{
				var record = await GetUserByEmail(email);
				if (record == null)
				{
					return string.Empty;
				}
				var check = await ValidatePasswordAsync(record, password);
				if(!check)
				{
					return string.Empty;
				}
				var claims = new[]
				{
					new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
					new Claim("username",$"{record.Username}"),
					new Claim("email",$"{record.Email}"),
					new Claim(ClaimTypes.Role, $"{record.Role}"),
				};
				var tokenExpireIn = TimeSpan.FromDays(1);
				var tokenHandler = new JwtSecurityTokenHandler();
				var key = Encoding.ASCII.GetBytes(configuration["Jwt:Key"] ?? string.Empty);
				var tokenDescriptor = new SecurityTokenDescriptor
				{
					Subject = new ClaimsIdentity(claims),
					Expires = DateTime.UtcNow.AddDays(tokenExpireIn.Days),
					SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature),
					Issuer = configuration["Jwt:Issuer"] ?? string.Empty,
					Audience = configuration["Jwt:Audience"] ?? string.Empty,
				};
				var token = tokenHandler.CreateToken(tokenDescriptor);
				return tokenHandler.WriteToken(token);
			}
			catch (Exception)
			{
				throw;
			}
		}
		public async Task<UserProfileResponse?> GetUserProfile(string email)
		{
			try
			{
				var record = await GetUserByEmail(email);
				if (record == null)
				{
					return null;
				}
				return autoMapper.Map<User, UserProfileResponse>(record);
			}
			catch (Exception)
			{
				throw;
			}
		}
		public async Task<bool> UploadAvatar(string email, IFormFile file)
		{
			if(file.Length > 0)
			{
				var record = await GetUserByEmail(email);
				if (record == null)
				{
					return false;
				}
				var fileName = $"{Path.GetRandomFileName().Replace(".", string.Empty)}{Path.GetExtension(file.FileName)}";
				var currentDirectory = System.IO.Directory.GetCurrentDirectory();
				var filePath = Path.Combine(currentDirectory, "ExternalFiles", "UsersAvatar", fileName);
				using (var stream = System.IO.File.Create(filePath))
				{
					await file.CopyToAsync(stream);
				}
				record.AvatarUri = fileName;
				await unitOfWork.UserRepository.UpdateAsync(record, record.PasswordHash);
				return true;
			}
			return false;
		}
		public async Task<FileContentResult> GetAvatar(string avatarUri)
		{
			var currentDirectory = System.IO.Directory.GetCurrentDirectory();
			var filePath = Path.Combine(currentDirectory, "ExternalFiles", "UsersAvatar", avatarUri);
			var fileBytes = await System.IO.File.ReadAllBytesAsync(filePath);
			return new FileContentResult(fileBytes, "image/jpeg");
		}
	}
}
