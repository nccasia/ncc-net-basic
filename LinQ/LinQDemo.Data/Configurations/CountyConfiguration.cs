using LinQDemo.Data.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace LinQDemo.Data.Configurations
{
    public class CountyConfiguration : IEntityTypeConfiguration<County>
    {
        public void Configure(EntityTypeBuilder<County> builder)
        {
            builder.ToTable("County");

            builder.HasKey(e => e.Id);

            builder.HasData(Seeds.Counties);

            builder.HasMany(e => e.Schools)
                .WithOne(e => e.County)
                .OnDelete(DeleteBehavior.SetNull);
        }
    }
}
