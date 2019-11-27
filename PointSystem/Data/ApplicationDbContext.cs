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



            modelBuilder.Entity<RegistrationFeast>()
                .HasOne<AspNetUser>(s => s.AspNetUser)
                .WithMany(g => g.RegistrationFeasts)
                .HasForeignKey(s => s.AspNetUserId);

            modelBuilder.Entity<RegistrationFeast>()
                .HasOne<Feast>(s => s.Feast)
                .WithMany(g => g.RegistrationFeasts)
                .HasForeignKey(s => s.FeastId);

            modelBuilder.Entity<AspNetUser>(entity =>
            {
                entity
                .HasMany(d => d.Comments)
                .WithOne(p => p.AspNetUser)
                .HasForeignKey(d => d.AspNetUserId);
            });

            modelBuilder.Entity<Proposal>(entity =>
            {
                entity
                .HasMany(d => d.Comments)
                .WithOne(p => p.Proposal)
                .HasForeignKey(d => d.ProposalId);

            });
        }

        public DbSet<AspNetUser> AspNetUsers { get; set; }
        public DbSet<Proposal> Proposals { get; set; }
        public DbSet<Feast> Feasts { get; set; }
        public DbSet<RegistrationFeast> RegistrationFeasts { get; set; }

        public DbSet<Commentary> Commentaries { get; set; }
    }
}
