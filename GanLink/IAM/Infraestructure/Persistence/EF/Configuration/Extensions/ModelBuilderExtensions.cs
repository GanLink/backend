using GanLink.IAM.Domain.Models.Aggregates;
using Microsoft.EntityFrameworkCore;

namespace GanLink.IAM.Infraestructure.Persistence.EF.Configuration.Extensions;

public static class ModelBuilderExtensions
{
    public static void ApplyIamConfiguration(this ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<User>(b =>
        {
            b.HasKey(x => x.Id);
            b.Property(x => x.Id).ValueGeneratedOnAdd();

            b.Property(x => x.Username).IsRequired();
            b.Property(x => x.Firstname).IsRequired();
            b.Property(x => x.Lastname).IsRequired();
            b.Property(x => x.Email).IsRequired();

            b.Property(x => x.Ruc).IsRequired();


            b.Property(x => x.Password).IsRequired();
        });
    }
}