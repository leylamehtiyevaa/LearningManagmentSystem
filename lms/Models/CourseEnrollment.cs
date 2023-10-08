namespace lms.Models
{
    public class CourseEnrollment
    {
        public Course course {  get; set; }
        public int StudentId {  get; set; }
        public int CourseId { get; set; }
        public float Grade {  get; set; }
        public float CompletionStatus { get; set; }
        public string Remark {  get; set; }
    }
}
