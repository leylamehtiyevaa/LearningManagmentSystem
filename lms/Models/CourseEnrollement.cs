namespace lms.Models
{
    public class CourseEnrollement
    {
        public Course course { get; set; }
        public bool enrolled { get; set; }

        public CourseEnrollement(Course crs)
        {
            course = crs;
        }
    }
}
