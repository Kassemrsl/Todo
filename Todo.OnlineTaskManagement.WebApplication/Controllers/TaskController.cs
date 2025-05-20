using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using Todo.OnlineTaskManagement.Application.Services;
using Todo.OnlineTaskManagement.Shared.Requests;

namespace Todo.OnlineTaskManagement.WebApplication.Controllers
{
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    [Route("api/[controller]")]
    public class TaskController : ControllerBase
    {
        private readonly TaskService _taskService;

        public TaskController(TaskService taskService)
        {
            _taskService = taskService;
        }

        [HttpGet("GetTaskById/{id}")]
        public async Task<IActionResult> GetTaskById(int id)
        {
            var tasks = await _taskService.GetTaskAsync(id);
            return Ok(tasks);
        }

        [HttpGet("GetAllTasks")]
        public async Task<IActionResult> GetAllTasks()
        {
            var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

            var tasks = await _taskService.GetAllTasksAsync(userId);

            return Ok(tasks);
        }

        [HttpPost("CreateTask")]
        public async Task<IActionResult> CreateTask([FromBody] TaskCreationRequest request)
        {
            var claims = User.Claims;

            request.UserId = claims.FirstOrDefault(x=>x.Type == ClaimTypes.NameIdentifier)?.Value;

            return Ok(await _taskService.CreateNewTaskAsync(request));
        }

        //[HttpPut("{id}")]
        //public async Task<IActionResult> UpdateTask(int id, TaskItem task)
        //{
        //    if (id != task.Id)
        //    {
        //        return BadRequest();
        //    }

        //    taskService.Entry(task).State = EntityState.Modified;
        //    await taskService.SaveChangesAsync();
        //    return NoContent();
        //}

        [HttpDelete("DeleteTask/{id}")]
        public async Task<IActionResult> DeleteTask(int id)
        {
            await _taskService.DeleteTaskAsync(id);

            return Ok();
        }
    }
}
