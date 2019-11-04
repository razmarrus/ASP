using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PointSystem.Models
{
    public class RegistrationFeast
    {

        public int id { get; set; }

        public DateTime StartTime { get; set; }

        public DateTime EndTime { get; set; }

        public string Content { get; set; }

        public string Point { get; set; }

        public string Status { get; set; }

        public string AspNetUserId { get; set; }
        public AspNetUser AspNetUser { get; set; }

        public int FeastId { get; set; }
        public Feast Feast { get; set; }
    }
}
