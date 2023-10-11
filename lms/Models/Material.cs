using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations.Schema;

namespace lms.Models
{
    public class Material
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string VideoURL { get; set; }
        public string ImageURL { get; set; }

        public string Author { get; set; }

        [ForeignKey("Course")]
        public int CourseId { get; set; }
        public virtual Course? Course { get; set; }

        [ForeignKey("IdentityUser")]
        public String? InstructorId { get; set; }
        public virtual IdentityUser? IdentityUser { get; set; }
    }
}
