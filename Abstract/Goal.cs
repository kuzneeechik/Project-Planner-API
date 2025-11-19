using Project_Planner_API.Data.Entities;

namespace Project_Planner_API.Abstract
{
    public abstract class Goal
    {
        public string? Description { get; set; }
        public DateTime? Deadline { get; set; }
        public TaskEntity? Parent { get; set; }
    }
}
