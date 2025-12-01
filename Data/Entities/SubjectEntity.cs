using Project_Planner_API.Utilities;

namespace Project_Planner_API.Data.Entities
{
    public class SubjectEntity
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        public string Code { get; set; } = CodeUtility.GenerateCode();
        public required string Name { get; set; }
        public Guid ResultId { get; set; }
        public required ResultEntity Result { get; set; }
        public DateTime CrearedAt { get; set; } = DateTime.UtcNow;
        public List<StudentEntity> Team { get; set; } = new List<StudentEntity>();
    }
}
