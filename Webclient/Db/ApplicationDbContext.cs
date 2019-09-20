using System;
using BOS.Webclient.Db.Accounts;
using BOS.Webclient.Models.Accounts;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace BOS.Webclient.Db
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser, IdentityRole<Guid>, Guid>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options) {}

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            ApplicationUserModelBuilder.Configure(modelBuilder);
        }

        public DbSet<ApplicationUser> ApplicationUsers { get; set; }
    }
}