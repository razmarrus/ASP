using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PointSystem.Models
{
    public class Commentary
    {
        public int Id { get; set; }
        public string Text { get; set; }
        public DateTime Date { get; set; }

        public int ProposalId { get; set; }
        public Proposal Proposal { get; set; }

        public string AspNetUserId { get; set; }
        public AspNetUser AspNetUser { get; set; }

        public string UserName { get; set; }

    }
}
