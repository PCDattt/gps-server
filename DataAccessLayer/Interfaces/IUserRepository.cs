using DataTransferObject.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.Interfaces
{
	public interface IUserRepository
	{
		public Task<User?> AddAsync(User user, string hashPassword);
		public Task<User?> GetByIdAsync(int id);
		public Task<User?> GetByEmailAsync(string email);
		public Task<List<User>> GetAllAsync();
		public Task<IEnumerable<User>> SearchByNameAsync(string name);
		public Task<bool> UpdateAsync(User user);
		public Task<bool> DeleteAsync(User user);
	}
}
