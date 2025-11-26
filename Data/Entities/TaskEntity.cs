namespace Project_Planner_API.Data.Entities
{
    public class TaskEntity : Goal
    {
        public Guid Id { get; set; }
        public required string Name { get; set; }
        public required ResultEntity Result { get; set; }
        public List<StudentEntity> ResponsibleStudents { get; set; } = new List<StudentEntity>();
        public List<TaskEntity> SubTasks { get; set; } = new List<TaskEntity>();
    }
}
