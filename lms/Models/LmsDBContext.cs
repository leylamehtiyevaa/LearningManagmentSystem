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
        internal readonly object Course;

        public DbSet<Courses> Courses { get; set; }
        public DbSet<Category> Categories { get; set; }


        public LmsDBContext(DbContextOptions<LmsDBContext> options) : base(options)
        {
            
        }

    }
}
