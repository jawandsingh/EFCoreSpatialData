using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Text;

namespace EFCore.SpatialData.EF
{
	public class SpatialDbContextFactory : IDesignTimeDbContextFactory<SpatialDbContext>
	{
		private static string _connectionString;

		public SpatialDbContext CreateDbContext()
		{
			return CreateDbContext(null);
		}

		public SpatialDbContext CreateDbContext(string[] args)
		{
			if (string.IsNullOrEmpty(_connectionString))
			{
				LoadConnectionString();
			}

			var builder = new DbContextOptionsBuilder<SpatialDbContext>();
			builder.UseSqlServer(_connectionString, 
								sqlOption => sqlOption.UseNetTopologySuite());

			return new SpatialDbContext(builder.Options);
		}

		private static void LoadConnectionString()
		{
			ConfigurationBuilder builder = new ConfigurationBuilder();
			builder.AddJsonFile("appsettings.json", optional: false);

			IConfigurationRoot configuration = builder.Build();

			_connectionString = configuration.GetConnectionString("DefaultConnection");

			if (string.IsNullOrEmpty(_connectionString))
				throw new Exception("Not able to load connection string from appsettings.json");
		}
	}
}
