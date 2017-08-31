using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace trippy.Models
{
    public class WorldContext : DbContext
    {
        public WorldContext()
        {

        }

        public DbSet<Trip> Trips{ get; set; }
        public DbSet<Stop> Stops { get; set; }
    }
}
