using PointSystem.Data;
using PointSystem.Models;
using System;

using Microsoft.AspNetCore.Http;
using System.Threading.Tasks;



using Microsoft.EntityFrameworkCore;

using System.Linq;
using System.Threading.Tasks;

namespace PointSystem.Services
{

    public class BackgroundEmail
    {

        private readonly ApplicationDbContext _context;

        public BackgroundEmail(ApplicationDbContext context)
        {
            _context = context;
        }

        private readonly RequestDelegate _next;

        /*public TimerMiddleware(RequestDelegate next)
        {
            _next = next;
        }*/


        public async Task DataComparation(TimeService timeService)
        {
            //var proposalsDbContext = _context.Proposals;
            DateTime DTnow = timeService.DtNow;
            //int id = Convert.ToInt16(projid);

            var proposalsDbContext = _context.Proposals
                .Include(p => p.AspNetUser)
                .Include(a => a.Comments);
                //.FirstOrDefault(a => a.id == id);

            foreach (Proposal proposal in proposalsDbContext)
            {

                DateTime PropTime = proposal.StartTime; //.ToString("yyyyMMddTHH:hh:mm:ss");


                TimeSpan beforeProposal = DTnow.Subtract(PropTime);

                int iSeconds = Convert.ToInt32(beforeProposal.TotalSeconds);

                EmailService emailService = new EmailService();

                if (iSeconds < 86400 && iSeconds > 3600)
                {
                    if (proposal.AspNetUser.Email != null)
                    {
                        //Console.WriteLine(proposal.AspNetUser.Email);
                        await emailService.SendEmailAsync(proposal.AspNetUser.Email, "Today is your event", $"Your event {proposal.Topic} is comming in {iSeconds / 3600} hours");
                    }
                }
            }

        }

    }
}
