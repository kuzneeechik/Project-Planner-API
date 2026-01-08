namespace Project_Planner_API.Exceptions
{
    public class WrongDateException : CustomException
    {
        public WrongDateException(int code, string details) :
            base(code, details) { }
    }
}
