namespace lms.Models
{
    public class Course
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string Author { get; set; }

        public Category Category { get; set; }
        public Course() { }

    }
}
