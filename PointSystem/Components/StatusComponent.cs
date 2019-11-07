using Microsoft.AspNetCore.Mvc;
using PointSystem.Data;
using PointSystem.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PointSystem.Components
{
    public class StatusComponent : ViewComponent
    {
        private readonly ApplicationDbContext _context;

        AspNetUser user;

        public StatusComponent(ApplicationDbContext context)//, string currentUserID = "awe")
        {

            _context = context;
        }

        public string Invoke(string currentUserID)
        {
            var applicationDbContext = _context.AspNetUsers;
            foreach (AspNetUser bufferUser in applicationDbContext)
            {
                if (bufferUser.Id == currentUserID)
                {
                    user = bufferUser;
                    break;
                }
            }
            var item = user;

            var proposalsDbContext = _context.Proposals;
            int countAcceped = 0, countRejected = 0, countProssesing = 0;
            foreach (Proposal proposal in proposalsDbContext)
            {
                if (proposal.Status == "accepted")
                    countAcceped++;
                else if (proposal.Status == "rejected")
                    countRejected++;
                else if (proposal.Status == "processing")
                    countProssesing++;
            }



            //if (bufferUser != null)
            return $"Здравствуй, дорогой: {item.UserName}  id: {item.Id}";
            //else return $"hi dear anna";
            //return $"hi" ;
        }
    }
}
