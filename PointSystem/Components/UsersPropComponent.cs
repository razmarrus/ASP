using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using PointSystem.Data;
using PointSystem.Models;

namespace PointSystem.Components
{
    public class UsersPropComponent : ViewComponent
    {

        private readonly ApplicationDbContext _context;

        AspNetUser user;

        public UsersPropComponent(ApplicationDbContext context)//, string currentUserID = "awe")
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

            return $"Здравствуй, дорогой: {item.UserName}  id: {item.Id}";
        }

    }
}
