using Project_Planner_API.Abstract;

namespace Project_Planner_API.Data.Entities
{
    public class TaskEntity : Goal
    {
        public Guid Id { get; set; }
        public required string Name { get; set; }
        public TeamEntity? ResponsibleTeam { get; set; }
        public List<StudentEntity> ResponsibleStudents { get; set; } = new List<StudentEntity>();
        public TaskEntity? Parent { get; set; }
        public List<TaskEntity> Tasks { get; set; } = new List<TaskEntity>();
    }
}
