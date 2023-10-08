using System.ComponentModel.DataAnnotations;

namespace lms.Models
{
    public class CourseResource
    {
        [Key]
        public int CourseId { get; set; }
        public int ResourceId { get; set; }
        public  LearningResource learningResource { get; set; }  
        public  Course course {  get; set; }
    }
}
