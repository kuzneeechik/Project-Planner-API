namespace Project_Planner_API.Models.TaskModels
{
    public class TaskShortModel
    {
        public Guid Id { get; set; }
        public required string Number { get; set; }
        public required string Name { get; set; }
        public Status Status { get; set; }
    }
}
