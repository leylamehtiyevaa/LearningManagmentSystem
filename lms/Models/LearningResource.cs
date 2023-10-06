using System.ComponentModel.DataAnnotations;

namespace lms.Models
{
    public class LearningResource
    {
        [Key]
        public int Id { get; set; }
        public string Type { get; set; }
        public string Content { get; set; }
    }
}
