using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Project_Planner_API.Services;

namespace Project_Planner_API.Controllers
{
    [Route("task")]
    [ApiController]
    public class TasksController : ControllerBase
    {
        private readonly ITasksService _tasksService;

        public TasksController(ITasksService tasksService)
        {
            _tasksService = tasksService;
        }

        [HttpGet("tasks/{id}")]
        public async Task<IActionResult> GetTasks([FromRoute] Guid id)
        {
            return Ok(await _tasksService.GetTasks(id));
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetTaskById([FromRoute] Guid id)
        {
            return Ok(await _tasksService.GetTaskById(id));
        }
    }
}
