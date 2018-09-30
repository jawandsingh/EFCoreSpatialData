using EFCore.SpatialData.Entities;
using Microsoft.EntityFrameworkCore;
using NetTopologySuite.Geometries;

namespace EFCore.SpatialData.EF
{
	public class SpatialDbContext : DbContext
	{
		public SpatialDbContext(DbContextOptions<SpatialDbContext> options) : base(options)
		{

		}

		public DbSet<GeoLocation> GeoLocations { get; set; }
		
		public void AddRemoveGeoLocation()
		{
			//Ensures that the database for the context does not exist. If it does not exist, 
			//no action is taken. If it does exist then the database is deleted.
			Database.EnsureDeleted();

			//Ensures that the database for the context exists. If it exists, no action is taken.
			//If it does not exist then the database and all its schema are created.
			Database.EnsureCreated();
			
			// Adding some Spatial Data.
			AddRange(new GeoLocation { Address = "Agra", Location = new Point(27.1767, 78.0081) },
					 new GeoLocation { Address = "Varanasi", Location = new Point(25.3176, 82.9739) },
					 new GeoLocation { Address = "New Delhi", Location = new Point(28.7041, 77.1025) },
					 new GeoLocation { Address = "Jaipur", Location = new Point(26.9124, 75.7873) });
			SaveChanges();
		}
	}
}
