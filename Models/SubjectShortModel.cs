namespace Project_Planner_API.Models
{
    public class SubjectShortModel
    {
        public required string Name { get; set; }
        public required string Result { get; set; }
        public required DateTime? Deadline { get; set; }
        public DateTime CreatedAt { get; set; }
    }
}
