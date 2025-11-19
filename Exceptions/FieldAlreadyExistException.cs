namespace Project_Planner_API.Exceptions
{
    public class FieldAlreadyExistException : CustomException
    {
        public FieldAlreadyExistException(int code, string details) :
            base(code, details) { }
    }
}
