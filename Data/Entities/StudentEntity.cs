namespace Project_Planner_API.Data.Entities
{
    public class StudentEntity
    {
        public Guid Id { get; set; } = new Guid();
        public required string Name { get; set; }
        public required string Email { get; set; }
        public required string PasswordHash { get; set; }
        public List<SubjectEntity> Subjects { get; set; } = new List<SubjectEntity>();
        public List<TaskEntity> Tasks { get; set; } = new List<TaskEntity>();
    }
}
