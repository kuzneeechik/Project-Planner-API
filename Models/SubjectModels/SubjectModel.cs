namespace Project_Planner_API.Models.SubjectModels
{
    public class SubjectModel
    {
        public required string Name { get; set; }
        public required string Code { get; set; }
        public required string Result { get; set; }
        public string? ResultDescription { get; set; }
        public DateTime? ResultDeadline { get; set; }
    }
}
