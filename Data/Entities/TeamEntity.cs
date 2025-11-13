namespace Project_Planner_API.Data.Entities
{
    public class TeamEntity
    {
        public Guid Id { get; set; }
        public List<StudentEntity> Students { get; set; } = new List<StudentEntity>();
    }
}
