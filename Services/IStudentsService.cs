using Project_Planner_API.Models;

namespace Project_Planner_API.Services
{
    public interface IStudentsService
    {
        public Task<IdModel> StudentRegistration(StudentRegistrationModel student); 
    }
}
