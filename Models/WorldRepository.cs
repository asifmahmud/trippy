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
        public Trip GetUserTripByName(string tripName, string userName)
        {
            return _context.Trips
             .Include(trip => trip.Stops)
             .Where(trip => trip.UserName == userName && trip.Name == tripName)
             .FirstOrDefault();
        }

        public IEnumerable<Trip> GetTripsByUsername(string userName)
        {
            return _context.Trips
                .Include(trip => trip.Stops)
                .Where(trip => trip.UserName == userName)
                .ToList();
            
        }


        public void AddStop(string tripName, Stop stop, string userName)
        {
            var trip = GetUserTripByName(tripName, userName);
            if (trip != null)
            {
                trip.Stops.Add(stop);
                _context.Stops.Add(stop);
            }
        }
    }
}
