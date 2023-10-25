using gps_server.Logic.Business.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using gps_server.Data.Entity.Entities;
using gps_server.Data.DataAccess.Contexts;
using Microsoft.AspNetCore.Identity;

namespace gps_server.Logic.Business.Repositories
{
	internal class UserRepository : IUserRepository
	{
		private readonly EntityDbContext entityDbContext;
		public UserRepository() 
		{
			this.entityDbContext = new();
		}
		public async Task<string> GeneratePasswordHashAsync(User user, string password)
		{
			// hash password
			var passwordHasher = new PasswordHasher<User>();
			var hash = passwordHasher.HashPassword(user, $"{password}");
			return hash;
		}

		public async Task<User?> AddAsync(User user, string password)
		{
			var record = await GetByEmailAsync(user.Email);
			if (record != null)
			{
				return null;
			}
			var now = DateTime.Now;
			var hashPassword = await GeneratePasswordHashAsync(user, password);
			_ = await entityDbContext.Users.AddAsync(new()
			{
				Username = user.Username,
				PasswordHash = hashPassword,
				Email = user.Email,
				Name = user.Name,
				Role = user.Role,
				Permissions = user.Permissions,
				CreatedDate = now,
				ModifiedDate = now,
				IsDeleted = false
			});
			_ = entityDbContext.SaveChanges();
			return user;
		}

		public async Task<User?> GetByIdAsync(int id)
		{
			return await Task.Run(() =>
			{
				return entityDbContext.Users
					.Where(user => user.Id == id)
					.FirstOrDefault();
			});
		}

		public async Task<User?> GetByEmailAsync(string email)
		{
			return await Task.Run(() =>
			{
				return entityDbContext.Users
					.Where(user => user.Email == email && !user.IsDeleted)
					.FirstOrDefault();
			});
		}
		public async Task<List<User>> GetAllAsync()
		{
			return await Task.Run(() =>
			{
				return entityDbContext.Users
					.Where(user => !user.IsDeleted)
					.ToList();
			});
		}
		public async Task<bool> ValidatePasswordAsync(User user, string password)
		{
			var passwordHasher = new PasswordHasher<User>();
			var result = passwordHasher.VerifyHashedPassword(user, user.PasswordHash, $"{password}");
			return result == PasswordVerificationResult.Success;
		}
		public async Task<IEnumerable<User>> SearchByNameAsync(string name)
		{
			return await Task.Run(() =>
			{
				return entityDbContext.Users
					.Where(user => user.Name.ToLower().Contains(name.ToLower())).ToList();
			});
		}
		public async Task<bool> UpdateAsync(User user)
		{
			var record = await GetByIdAsync(user.Id);
			if (record == null)
			{
				return false;
			}
			record.Name = user.Name;
			record.Role = user.Role;
			record.Permissions = user.Permissions;
			record.ModifiedDate = DateTime.Now;
			_ = entityDbContext.SaveChanges();
			return true;
		}	
		public async Task<bool> DeleteAsync(int id)
		{
			var record = await GetByIdAsync(id);
			if (record == null)
			{
				return false;
			}
			record.IsDeleted = true;
			record.ModifiedDate = DateTime.Now;
			_ = entityDbContext.SaveChanges();
			return true;
		}
	}
}