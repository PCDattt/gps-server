using DataAccessLayer.Interfaces;
using DataTransferObject.Entities;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogicLayer.Services
{
	public class UserService
	{
		private readonly IUnitOfWork unitOfWork;
		public UserService(IUnitOfWork unitOfWork)
		{
			this.unitOfWork = unitOfWork;
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
				return await unitOfWork.UserRepository.GetByIdAsync(id);
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
				return await unitOfWork.UserRepository.GetByEmailAsync(email);
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
				return await unitOfWork.UserRepository.GetAllAsync();
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
				return await unitOfWork.UserRepository.SearchByNameAsync(name);
			}
			catch (Exception)
			{
				throw;
			}
		}
		public async Task<bool> UpdateUser(User user)
		{
			try
			{
				var record = await GetUserById(user.Id);
				if (record == null)
				{
					return false;
				}
				record.Name = user.Name;
				record.Role = user.Role;
				record.Permissions = user.Permissions;
				record.ModifiedDate = DateTime.Now;
				return await unitOfWork.UserRepository.UpdateAsync(record);
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
				record.IsDeleted = true;
				record.ModifiedDate = DateTime.Now;
				return await unitOfWork.UserRepository.DeleteAsync(record);
			}
			catch (Exception)
			{
				throw;
			}
		}
	}
}
