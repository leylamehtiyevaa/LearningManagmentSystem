using System.ComponentModel.DataAnnotations;

namespace lms.Models
{
    public class Lesson
    {
        [Key] 
        public int Id { get; set; }
        public int ResourceId { get; set; }
        public  LearningResource learningResource { get; set; }
        //veri türünden emin değilim
        public List<String> Questions { get; set; }
    }
}
