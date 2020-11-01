using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace PointSystem.Models
{
    public class Proposal
    {
        public int id { get; set; }

        public DateTime StartTime { get; set; }
        //[Range (typeof(DateTime), DateTime.Now, DateTime.Now.AddDays(1)]
        public DateTime EndTime { get; set; }

        public string Topic { get; set; }

        public string Content { get; set; }

        public string Point { get; set; }

        public int MaxPeople { get; set; }

        public string Status { get; set; }

        public string AspNetUserId { get; set; }
        public AspNetUser AspNetUser { get; set; }

        public ICollection<Commentary> Comments { get; set; }


    }
}
