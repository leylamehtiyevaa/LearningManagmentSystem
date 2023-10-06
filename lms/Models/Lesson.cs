namespace lms.Models
{
    public class Lesson
    {
        public int ResourceId { get; set; }
        public virtual LearningResource learningResource { get; set; }
        //veri türünden emin değilim
        public List<String> Questions { get; set; }
    }
}
