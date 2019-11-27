using Microsoft.AspNetCore.SignalR;
using Microsoft.EntityFrameworkCore;
using PointSystem.Data;
using PointSystem.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PointSystem.Hubs
{
    public class ChatHub : Hub
    {

        private readonly ApplicationDbContext context;

        public ChatHub(ApplicationDbContext context)
        {
            this.context = context;
        }
        public async Task SendMessage(string user, string message, string projid)
        {
            int id = Convert.ToInt16(projid);

            var proposal = context.Proposals
                .Include(p => p.AspNetUser)
                .Include(a => a.Comments)
                .FirstOrDefault(a => a.id == id);

            AddCommentary(proposal, message, user);




            //context.Projects.Update(project);
            await context.SaveChangesAsync();

            DateTime date = DateTime.Now;
            await Clients.All.SendAsync("ReceiveMessage", user, message, date.ToString());

        }

        public void AddCommentary(Proposal proposal, string NewCommentary, string UserName)
        {
            Commentary commentary = new Commentary
            {
                Proposal = proposal,
                ProposalId = proposal.id,
                Text = NewCommentary,
                Date = DateTime.Now,
                UserName = UserName,
            };

            var AspNetUser = context.AspNetUsers.FirstOrDefault(a => a.UserName == UserName);

            commentary.AspNetUser = AspNetUser;
            commentary.AspNetUserId = AspNetUser.Id;

            proposal.Comments.Add(commentary);
        }
    }
}
