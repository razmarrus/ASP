using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PointSystem.Models
{
    public class Proposal
    {
        public int id { get; set; }

        public DateTime StartTime { get; set; }

        public DateTime EndTime { get; set; }

        public string Topic { get; set; }

        public string Content { get; set; }

        public string Point { get; set; }

        public int MaxPeople { get; set; }

        public string Status { get; set; }

        public string AspNetUserId { get; set; }
        public AspNetUser AspNetUser { get; set; }


    }
}
