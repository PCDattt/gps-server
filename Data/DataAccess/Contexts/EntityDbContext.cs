using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using gps_server.Data.Entity.Entities;
using Microsoft.Extensions.Configuration;

namespace gps_server.Data.DataAccess.Contexts
{
	public class EntityDbContext : DbContext
	{
		public DbSet<DevicePacket> DevicePackets { get; set; }
		public DbSet<Device> Devices { get; set; }
		public DbSet<User> Users { get; set; }

		protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
			=> optionsBuilder.UseSqlServer("Server=.\\SQLEXPRESS;Database=GPS_Server;Trusted_Connection=True;MultipleActiveResultSets=true;TrustServerCertificate=True");	

		protected override void OnModelCreating(ModelBuilder modelBuilder)
		{
			modelBuilder.Entity<DevicePacket>().ToTable("DevicePackets");
			modelBuilder.Entity<Device>().ToTable("Devices");
			modelBuilder.Entity<User>().ToTable("Users");
		}
	}
}
