using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Project_Planner_API.Models.TaskModels;
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

        [HttpPost("create/{id}")]
        public async Task<IActionResult> CreateTask(
            [FromBody] TaskCreateModel task,
            [FromRoute] Guid id)
        {
            return Created("", await _tasksService.CreateTask(task, id));
        }

        [HttpPost("add/{id}")]
        public async Task<IActionResult> AddSubtask(
            [FromBody] TaskCreateModel subtask,
            [FromRoute] Guid id)
        {
            return Created("", await _tasksService.AddSubtask(subtask, id));
        }
    }
}
