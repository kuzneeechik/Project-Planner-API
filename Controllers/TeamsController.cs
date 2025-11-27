using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Project_Planner_API.Models;
using Project_Planner_API.Services;
using Project_Planner_API.Utilities;

namespace Project_Planner_API.Controllers
{
    [ApiController]
    [Route("team")]
    public class TeamsController : ControllerBase
    {
        private readonly ITeamsService _teamsService;

        public TeamsController(ITeamsService teamService)
        {
            _teamsService = teamService;
        }

        [Authorize]
        [HttpGet("team/{id}")]
        public async Task<IActionResult> GetTeam([FromRoute] Guid id)
        {
            return Ok(await _teamsService.GetTeam(id));
        }

        [Authorize]
        [HttpDelete("student/{id}")]
        public async Task<IActionResult> DeleteStudent(
            [FromRoute] Guid id,
            [FromHeader] Guid subjectId)
        {
            await _teamsService.DeleteStudent(id, subjectId);

            return Ok();
        }

        [Authorize]
        [HttpDelete("exit/{id}")]
        public async Task<IActionResult> ExitStudent([FromRoute] Guid id)
        {
            var studentId = AuthUtility.GetUserId(User);

            await _teamsService.DeleteStudent(studentId, id);

            return Ok();
        }

        [Authorize]
        [HttpPost("entry/{id}")]
        public async Task<IActionResult> EntryStudent(
            [FromBody] EntryModel code,
            [FromRoute] Guid id)
        {
            var studentId = AuthUtility.GetUserId(User);

            await _teamsService.EntryStudent(studentId, id, code);

            return Ok();
        }
    }
}
