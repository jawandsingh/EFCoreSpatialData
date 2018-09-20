using EFCore.SpatialData.EF;
using NetTopologySuite.Geometries;
using System;
using System.Linq;

namespace EFCore.SpatialData
{
	class Program
	{
		static void Main(string[] args)
		{
			using (SpatialDbContext context = new SpatialDbContextFactory().CreateDbContext())
			{
				context.AddRemoveGeoLocation();
			}

			using (SpatialDbContext context = new SpatialDbContextFactory().CreateDbContext())
			{
				// Lat Lng of Chandigarh City. Let's try to find cities which are with in 300 km range (aerial distance).
				var sourceLocation = new Point(30.7333, 76.7794);

				var matches = context.GeoLocations
									.Where(l => l.Location.Distance(sourceLocation) < 300/100)
									.OrderBy(l => l.Location.Distance(sourceLocation))
									.Select(l => new
									{
										Id = l.Id,
										Address = l.Address,
										Distance = l.Location.Distance(sourceLocation) * 100
									}).ToList();

				foreach (var location in matches)
				{
					Console.WriteLine($"Location: {location.Address}, Distance = {Math.Round(location.Distance)} KM");
				}
				Console.ReadLine();
			}
		}
	}
}
