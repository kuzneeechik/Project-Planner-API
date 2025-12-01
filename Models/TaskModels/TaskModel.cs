namespace Project_Planner_API.Models.TaskModels
{
    public class TaskModel
    {
        public Guid Id { get; set; }
        public required string Number { get; set; }
        public required string Name { get; set; }
        public Status Status { get; set; }
        public DateTime? Deadline { get; set; }
        public string? ParentNumber { get; set; }
        public required string ParentName { get; set; }
        public string? Description { get; set; }
        public List<string>? ResponsibleStudents { get; set; }
    }
}
