using Microsoft.EntityFrameworkCore;

namespace SampleWebApi.Models
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
        }

        public DbSet<School> Schools { get; set; }
    }
}
