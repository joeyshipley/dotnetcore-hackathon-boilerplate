using BOS.Webclient.Models.Accounts;
using Microsoft.EntityFrameworkCore;

namespace BOS.Webclient.Db.Accounts
{
    public class ApplicationUserModelBuilder
    {
        public static void Configure(ModelBuilder modelBuilder)
        {
            var entity = modelBuilder.Entity<ApplicationUser>();
            entity.ToTable("AspNetUsers");

            entity.Property(x => x.Id)
                .IsRequired();
            entity.HasKey(x => x.Id);
            entity.Property(x => x.CreatedOn);
            entity.Property(x => x.UpdatedOn);

            entity.Property(x => x.Name)
                .HasMaxLength(ApplicationUserValidator.NameMaxLength);
        }
    }
}