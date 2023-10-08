using System.Runtime.CompilerServices;

namespace lms.Models
{
    public class StudentCourseAssignment
    {
        public int Id { get; set; }
        public CourseAssignment CourseAssignments { get; set; }
        public int StudentId { get; set; }
        public int CourseAssignmentId {  get; set; }
        public float Score {  get; set; }
        
        public DateTime DueDate { get; set; }

    }
}
