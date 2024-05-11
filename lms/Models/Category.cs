namespace lms.Models
{
    public class Category
    {
        public int Id { get; set; }
        public required string Name { get; set; }
        public string? Description { get; set; }

        public string iconURL { get; set; } 

        public Category() { }
    }
}
