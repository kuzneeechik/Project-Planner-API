namespace Project_Planner_API.Exceptions
{
    public class EntryErrorException : CustomException
    {
        public EntryErrorException(int code, string details) :
            base(code, details) { }
    }
}
