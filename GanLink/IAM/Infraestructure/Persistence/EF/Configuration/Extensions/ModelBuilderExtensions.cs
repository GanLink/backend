using GanLink.IAM.Domain.Models.Aggregates;
using Microsoft.EntityFrameworkCore;

namespace GanLink.IAM.Infraestructure.Persistence.EF.Configuration.Extensions;

public static class ModelBuilderExtensions
{
    public static void ApplyIamConfiguration(this ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<User>().HasKey(x => x.Id);
        modelBuilder.Entity<User>().Property(x => x.Id).ValueGeneratedOnAdd();
        modelBuilder.Entity<User>().Property(x => x.Username).IsRequired();
        modelBuilder.Entity<User>().Property(x => x.Firstname).IsRequired();
        modelBuilder.Entity<User>().Property(x => x.Lastname).IsRequired();
        modelBuilder.Entity<User>().Property(x => x.Email).IsRequired();
        modelBuilder.OwnsOne(o => o.Ruc, r =>
        {
            r.Property(p => p.Number)
                .HasColumnName("ruc")
                .HasMaxLength(11)
                .IsRequired();
        });
        modelBuilder.Entity<User>().Property(x => x.Password).IsRequired();
    }
}