using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using PointSystem.Models;

namespace PointSystem.Data
{
    public class ApplicationDbContext : IdentityDbContext<AspNetUser>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
            Database.EnsureCreated();
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder); 

            modelBuilder.Entity<Proposal>()
                .HasOne<AspNetUser>(s => s.AspNetUser)
                .WithMany(g => g.Proposals)
                .HasForeignKey(s => s.AspNetUserId);
        }

        public DbSet<AspNetUser> AspNetUsers { get; set; }
        public DbSet<Proposal> Proposals { get; set; }
    }
}
