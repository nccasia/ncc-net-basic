using LinQDemo.Data.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace LinQDemo.Data.Configurations
{
    public class SchoolConfiguration : IEntityTypeConfiguration<School>
    {
        public void Configure(EntityTypeBuilder<School> builder)
        {
            builder.ToTable("School");

            builder.HasKey(e => e.Id);

            builder.HasData(Seeds.Schools);

            builder.HasMany(e => e.Classes)
                .WithOne(e => e.School)
                .OnDelete(DeleteBehavior.SetNull);

            builder.HasMany(e => e.Students)
                .WithOne(e => e.School)
                .OnDelete(DeleteBehavior.SetNull);
        }
    }
}
