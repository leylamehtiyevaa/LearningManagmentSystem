using System.Configuration;
using System;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace lms.Models
{
    public class LmsDBContext: IdentityDbContext<IdentityUser>
    {
        private readonly IConfiguration configuration;

        public DbSet<Course> Course { get; set; }
        public DbSet<Category> Category { get; set; }

        public DbSet<Enrollment> Enrollment { get; set; }
        public DbSet<Material> Material { get; set; }

        public LmsDBContext(DbContextOptions<LmsDBContext> options) : base(options)
        {
            
        }
            

    }
}
