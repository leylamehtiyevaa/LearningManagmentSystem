namespace lms.Models
{
    public class CourseEnrollment
    {
        public virtual User user { get; set; }
        public virtual Course course {  get; set; }
        public int StudentId {  get; set; }
        public int CourseId { get; set; }
        public float Grade {  get; set; }
        //hangi tip olacağından emin değilim boolean ve float öylesine
        public Boolean RegistrationStatus { get; set; }
        public float CompletionStatus { get; set; }
        public string Remark {  get; set; }
    }
}
