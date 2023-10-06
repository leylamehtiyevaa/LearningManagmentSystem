using System.ComponentModel.DataAnnotations;

namespace lms.Models
{
    public class CourseResource
    {
        [Key]
        public int CourseId { get; set; }
        public int ResourceId { get; set; }
        public virtual LearningResource learningResource { get; set; }  
        public virtual Course course {  get; set; }
    }
}
