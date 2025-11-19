namespace Project_Planner_API.Exceptions
{
    public abstract class CustomException : Exception
    {
        public int Code { get; set; }
        public string Details { get; set; } = string.Empty;

        public CustomException (int code, string details)
        {
            Code = code;
            Details = details;
        }
    }
}
