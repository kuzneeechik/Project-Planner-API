using Project_Planner_API.Models.TaskModels;

namespace Project_Planner_API.Data.Entities
{
    public class TaskEntity : Goal
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        public required string Number { get; set; }
        public required string Name { get; set; }
        public Status Status { get; set; } = Status.Created;
        public required ResultEntity Result { get; set; }
        public List<StudentEntity> ResponsibleStudents { get; set; } = new List<StudentEntity>();
        public List<TaskEntity> SubTasks { get; set; } = new List<TaskEntity>();
        public TaskEntity? ParentTask { get; set; }
    }
}
