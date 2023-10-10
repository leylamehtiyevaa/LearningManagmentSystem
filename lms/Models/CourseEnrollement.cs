namespace lms.Models
{
    public class CourseEnrollement
    {
        public Course course { get; set; }
        public bool enrolled { get; set; }

        public IEnumerable<Material> materials { get; set; }

        public CourseEnrollement(Course crs)
        {
            course = crs;
        }
    }
}
