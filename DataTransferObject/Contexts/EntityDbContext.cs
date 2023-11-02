using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataTransferObject.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace DataTransferObject.Contexts
{
	public class EntityDbContext : DbContext
	{
		private IConfiguration Configuration { get; set; }
		private string ConnectionString { get; set; }
		public DbSet<DevicePacket> DevicePackets { get; set; }
		public DbSet<Device> Devices { get; set; }
		public DbSet<User> Users { get; set; }
		public EntityDbContext(IConfiguration configuration)
		{
			Configuration = configuration;
			ConnectionString = Configuration.GetConnectionString("SqlConnection") ?? string.Empty;
		}
		protected override void OnConfiguring(DbContextOptionsBuilder options)
		=> options.UseSqlServer(ConnectionString);
		protected override void OnModelCreating(ModelBuilder modelBuilder)
		{
			modelBuilder.Entity<DevicePacket>().ToTable("DevicePackets");
			modelBuilder.Entity<Device>().ToTable("Devices");
			modelBuilder.Entity<User>().ToTable("Users");
		}
	}
}
