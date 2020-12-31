using LinQDemo.Data.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace LinQDemo.Data.Configurations
{
    public class CityConfiguration : IEntityTypeConfiguration<City>
    {
        public void Configure(EntityTypeBuilder<City> builder)
        {
            builder.ToTable("City");

            builder.HasKey(e => e.Id);

            builder.HasData(Seeds.Cities);

            builder.HasMany(e => e.Counties)
                .WithOne(e => e.City)
                .OnDelete(DeleteBehavior.SetNull);

            builder.HasMany(e => e.Schools)
                .WithOne(e => e.City)
                .OnDelete(DeleteBehavior.SetNull);
        }
    }
}
