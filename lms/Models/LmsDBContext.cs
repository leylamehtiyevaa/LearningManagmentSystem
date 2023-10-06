using Microsoft.EntityFrameworkCore;

namespace lms.Models
{
    public class LmsDBContext: DbContext
    {
        private readonly IConfiguration configuration;

        public DbSet<Course> Courses { get; set; }
        public DbSet<Category> Categories { get; set; }


        public LmsDBContext(DbContextOptions<LmsDBContext> options) : base(options)
        {
            
        }
    }
}
