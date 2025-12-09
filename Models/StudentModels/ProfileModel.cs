namespace Project_Planner_API.Models.StudentModels
{
    public class ProfileModel
    {
        public Guid Id { get; set; }
        public required string Name { get; set; }
        public required string Email { get; set; }
    }
}
