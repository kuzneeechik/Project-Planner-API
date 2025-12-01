namespace Project_Planner_API.Models.TaskModels
{
    public class TaskUpdateModel
    {
        public required string Name { get; set; }
        public required string Description { get; set; }
        public DateTime Deadline { get; set; }
        public required List<Guid> ResponsibleStudents { get; set; }
    }
}
