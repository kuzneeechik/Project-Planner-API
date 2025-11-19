namespace Project_Planner_API.Exceptions
{
    public class ProblemDetails
    {
        public int Code { get; set; }
        public string Details { get; set; } = string.Empty;

        public ProblemDetails() { }
    }
}
