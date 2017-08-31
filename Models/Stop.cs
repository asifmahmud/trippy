using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace trippy.Models
{
    public class Stop
    {
        public int Id { get; set; }
        public int Name { get; set; }
        public double Lattitude { get; set; }
        public double Longitude { get; set; }
        public int Order { get; set; }
        public DateTime ArrivalDate { get; set; }




    }
}
