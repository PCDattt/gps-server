using DataAccessLayer.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using DataTransferObject.Contexts;
using DataTransferObject.Entities;

namespace DataAccessLayer.Repositories
{
	public class UserRepository : IUserRepository
	{
		private readonly EntityDbContext entityDbContext;
		public UserRepository(EntityDbContext entityDbContext)
		{
			this.entityDbContext = entityDbContext;
		}
		public async Task<User?> AddAsync(User user, string hashPassword)
		{
			var now = DateTime.Now;
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
					.Where(user => user.Email == email)
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
			record.Email = user.Email;
			record.Name = user.Name;
			record.Role = user.Role;
			record.ModifiedDate = DateTime.Now;
			_ = await entityDbContext.SaveChangesAsync();
			return true;
		}	
		public async Task<bool> DeleteAsync(int id)
		{
			var record = await GetByIdAsync(id);
			record.IsDeleted = true;
			record.ModifiedDate = DateTime.Now;
			_ = await entityDbContext.SaveChangesAsync();
			return true;
		}
	}
}