using System.ComponentModel.DataAnnotations;
using System.Globalization;

namespace lms.Models
{
    public class CourseAssignment
    {
        [Key]
        public int Id { get; set; }
        public int CourseId { get; set; }
        public string Title {  get; set; }
        public string Description {  get; set; }
        public float OverallScore {  get; set; }
        public virtual Course course { get; set; }

    }
}
