namespace Project_Planner_API.Data.Entities
{
    public class SubjectEntity
    {
        public Guid Id { get; set; }
        public required string Name { get; set; }
        public required ResultEntity Result { get; set; }
        public required TeamEntity Team { get; set; }
    }
}
