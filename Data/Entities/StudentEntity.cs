namespace Project_Planner_API.Data.Entities
{
    public class StudentEntity
    {
        public Guid Id { get; set; }
        public required string Name { get; set; }
        public List<TeamEntity> Teams { get; set; } = new List<TeamEntity>();
        public List<TaskEntity> Tasks { get; set; } = new List<TaskEntity>();
    }
}
