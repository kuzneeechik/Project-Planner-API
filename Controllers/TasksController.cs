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

        [HttpGet("tasks/{subjectId}")]
        public async Task<IActionResult> GetTasks([FromRoute] Guid subjectId)
        {
            return Ok(await _tasksService.GetTasks(subjectId));
        }

        [HttpGet("{taskId}")]
        public async Task<IActionResult> GetTaskById([FromRoute] Guid taskId)
        {
            return Ok(await _tasksService.GetTaskById(taskId));
        }

        [HttpPost("create/{subjectId}")]
        public async Task<IActionResult> CreateTask(
            [FromBody] TaskCreateModel task,
            [FromRoute] Guid subjectId)
        {
            return Created("", await _tasksService.CreateTask(task, subjectId));
        }

        [HttpPost("add/{parentId}")]
        public async Task<IActionResult> AddSubtask(
            [FromBody] TaskCreateModel subtask,
            [FromRoute] Guid parentId)
        {
            return Created("", await _tasksService.AddSubtask(subtask, parentId));
        }

        [HttpPut("update/{taskId}")]
        public async Task<IActionResult> UpdateTask(
            [FromRoute] Guid taskId,
            [FromBody] TaskUpdateModel task)
        {
            await _tasksService.UpdateTask(taskId, task);

            return Ok();
        }

        [HttpPatch("status/{taskId}")]
        public async Task<IActionResult> ChangeTaskStatus(
            [FromRoute] Guid taskId,
            [FromBody] StatusModel status)
        {
            await _tasksService.ChangeTaskStatus(taskId, status);

            return Ok();
        }

        [HttpDelete("{taskId}")]
        public async Task<IActionResult> DeleteTask([FromRoute] Guid taskId)
        {
            await _tasksService.DeleteTask(taskId);

            return Ok();
        }
    }
}
