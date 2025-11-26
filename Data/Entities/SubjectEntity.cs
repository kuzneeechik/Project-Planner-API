namespace Project_Planner_API.Data.Entities
{
    public class SubjectEntity
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        public required string Name { get; set; }
        public required ResultEntity Result { get; set; }
        public DateTime CrearedAt { get; set; } = DateTime.UtcNow;
        public List<StudentEntity> Team { get; set; } = new List<StudentEntity>();
    }
}
