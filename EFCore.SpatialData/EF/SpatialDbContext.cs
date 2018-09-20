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
			Database.EnsureDeleted();
			Database.EnsureCreated();

			AddRange(new GeoLocation { Address = "Agra", Location = new Point(27.1767, 78.0081) },
					 new GeoLocation { Address = "Varanasi", Location = new Point(25.3176, 82.9739) },
					 new GeoLocation { Address = "New Delhi", Location = new Point(28.7041, 77.1025) },
					 new GeoLocation { Address = "Jaipur", Location = new Point(26.9124, 75.7873) });
			SaveChanges();
		}
	}
}
