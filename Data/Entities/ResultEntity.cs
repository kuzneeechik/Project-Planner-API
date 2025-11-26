namespace Project_Planner_API.Data.Entities
{
    public class ResultEntity : Goal
    {
        public Guid Id { get; set; }
        public required string Name { get; set; }
    }
}
