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

        public DbSet<Course> Courses { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<CourseAssignment> CourseAssignments { get; set; }
        public DbSet<CourseResource> CourseResources { get; set; }
        public DbSet<Lesson> Lessons { get; set; }
        public DbSet<LearningResource> LearningResources { get; set; }
        public DbSet<StudentCourseAssignment> StudentCourseAssignments { get; set; }



        public LmsDBContext(DbContextOptions<LmsDBContext> options) : base(options)
        {
            
        }

    }
}
