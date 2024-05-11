namespace lms.Models
{
    public class CourseEnrollement
    {
        public Course course { get; set; }
        public bool enrolled { get; set; }

        public IEnumerable<Material> materials { get; set; }

        public int currentStep { get; set; }

        public int enrollementID { get; set; }

        public int stepLength { get; set; } = 0;

        public CourseEnrollement(Course crs)
        {
            course = crs;
        }
        public CourseEnrollement(IEnumerable<Material> materials, int current , int enrollId)
        {
            this.materials = materials;
            this.currentStep = current;
            this.enrollementID = enrollId;
        }
    }
}
