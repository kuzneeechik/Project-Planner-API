namespace Project_Planner_API.Models.TaskModels
{
    public class TaskCreateModel
    {
        public required string Number { get; set; }
        public required string Name { get; set; }
        public string? Description { get; set; }
        public DateTime? Deadline { get; set; }
        public List<Guid> ResponsibleStudents { get; set; } = new List<Guid>();
    }
}
