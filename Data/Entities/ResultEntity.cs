using Project_Planner_API.Abstract;

namespace Project_Planner_API.Data.Entities
{
    public class ResultEntity : Goal
    {
        public Guid Id { get; set; }
        public required string Name { get; set; }
        public List<TaskEntity> Tasks { get; set; } = new List<TaskEntity>();
    }
}
