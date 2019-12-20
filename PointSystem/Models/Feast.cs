using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PointSystem.Models
{
    public class Feast
    {
        public int id { get; set; }

        public DateTime StartTime { get; set; }

        public DateTime EndTime { get; set; }

        public bool RediarectFlag { get; set; }
        public string RediarectUrl { get; set; }


        public string Topic { get; set; }

        public string Content { get; set; }

        public string Location { get; set; }

        public ICollection<RegistrationFeast> RegistrationFeasts { get; set; }

        //public string AspNetUserId { get; set; }
        //public AspNetUser AspNetUser { get; set; }
    }
}
