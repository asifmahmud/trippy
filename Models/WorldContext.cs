using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace trippy.Models
{
    public class WorldContext : IdentityDbContext<User>
    {
        private IConfigurationRoot _config;

        public WorldContext(IConfigurationRoot config, DbContextOptions options)
            : base(options)
        {
            _config = config;
        }

        public DbSet<Trip> Trips{ get; set; }
        public DbSet<Stop> Stops { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);
            string dbname   = _config["ConnectionStrings:RDS_DB_NAME"];
            string username = _config["ConnectionStrings:RDS_USERNAME"];
            string password = _config["ConnectionStrings:RDS_PASSWORD"];
            string hostname = _config["ConnectionStrings:RDS_HOSTNAME"];
            string port     = _config["ConnectionStrings:RDS_PORT"];

            string connection = "Data Source=" + hostname +
                                ";Initial Catalog=" + dbname + 
                                ";User ID=" + username + 
                                ";Password=" + password + ";";

            optionsBuilder.UseSqlServer(connection);
        }
    }
}
