using System.ComponentModel.DataAnnotations;

namespace lms.Models
{
    public class Course
    {
        [Key]
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public int ImageURL { get; set; }
        public int InstructorId {  get; set; }
        //list de farklı tablo da olabilir, üzerine konuşalım
        public Category category { get; set; }
        public Course() { }

    }
}
