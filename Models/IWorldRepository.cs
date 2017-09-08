using System.Collections.Generic;
using System.Threading.Tasks;

namespace trippy.Models
{
    public interface IWorldRepository
    {
        IEnumerable<Trip> GetAllTrips();
        void AddTrip(Trip trip);
        void AddStop(string tripName, Stop stop);
        Task<bool> SaveChangesAsync();
        Trip GetTripByName(string tripName);
    }
}