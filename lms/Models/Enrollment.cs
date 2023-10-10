using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations.Schema;

namespace lms.Models
{
    public class Enrollment
    {
        public int Id { get; set; }

        [ForeignKey("Course")]
        public int CourseId { get; set; }
        public virtual Course? Course { get; set; }

        [ForeignKey("IdentityUser")]
        public string UserId { get; set; }
        public virtual IdentityUser? IdentityUser { get; set; }



        public Enrollment() { }
    }
}
