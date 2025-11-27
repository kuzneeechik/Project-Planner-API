namespace Project_Planner_API.Exceptions
{
    public class NotFoundException : CustomException
    {
        public NotFoundException(int code, string details) :
            base(code, details) { }
    }
}
