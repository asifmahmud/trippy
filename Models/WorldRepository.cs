using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace trippy.Models
{
    public class WorldRepository : IWorldRepository
    {
        private WorldContext _context;

        public WorldRepository(WorldContext context)
        {
            _context = context;
        }

        public IEnumerable<Trip> GetAllTrips()
        {
           return _context.Trips.ToList();
        }

        public void AddTrip(Trip trip)
        {
            _context.Add(trip);
        }

        public async Task<bool> SaveChangesAsync()
        {
            return (await _context.SaveChangesAsync()) > 0;
        }

        public Trip GetTripByName(string tripName)
        {
            return _context.Trips
                .Include(trip => trip.Stops)
                .Where(trip => trip.Name == tripName)
                .FirstOrDefault();
                
        }
    }
}
