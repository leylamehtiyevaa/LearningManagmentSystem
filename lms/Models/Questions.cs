using System.ComponentModel.DataAnnotations;

namespace lms.Models
{
    public class Questions

    {
        [Key]
        public int Id { get; set; }
        public string Question {  get; set; }
        public string Answer { get; set; }
        public List<String> Options { get; set; }
        public String CorrectOption {  get; set; }
    }
}
