using LinQDemo.Data.Models;
using Microsoft.EntityFrameworkCore;
using LinQDemo.Data.Configurations;

namespace LinQDemo.Data
{
    public class LinQDemoContext : DbContext
    {
        public LinQDemoContext(DbContextOptions<LinQDemoContext> options) : base(options)
        {
        }

        public DbSet<Class> Classes { get; set; }
        public DbSet<Student> Students { get; set; }
        public DbSet<School> Schools { get; set; }
        public DbSet<County> Counties { get; set; }
        public DbSet<City> Cities { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new StudentConfiguration());
            modelBuilder.ApplyConfiguration(new ClassConfiguration());
            modelBuilder.ApplyConfiguration(new SchoolConfiguration());
            modelBuilder.ApplyConfiguration(new CountyConfiguration());
            modelBuilder.ApplyConfiguration(new CityConfiguration());
        }
    }
}
