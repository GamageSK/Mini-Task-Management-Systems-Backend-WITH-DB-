using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TaskManagement_Backend.Models;
using TaskManagement_Backend.Services;
using TaskManagementAPI.Data;

namespace TaskManagement_Backend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TaskController : Controller
    {
        private readonly TaskService _taskService;

        public TaskController(TaskService taskService)
        {
            _taskService = taskService;
        }

        [HttpGet("GetTasks")]
        public async Task<ActionResult<IEnumerable<TaskItems>>> GetTasks()
        {
            return await _taskService.GetAllTasksAsync();
        }

        [HttpGet("GetTasks/{id}")]
        public async Task<ActionResult<TaskItems>> GetTasksById(int id)
        {
            var task = await _taskService.GetTaskByIdAsync(id);

            if (task == null)
                return NotFound($"Task with ID {id} not found.");

            return Ok(task);
        }

        [HttpPost("PostTask")]
        public async Task<ActionResult> PostTask([FromBody] AddTaskItems AddItems)
        {
            var result = await _taskService.AddTaskAsync(AddItems);

            if (!result)
                return BadRequest("Title and Status are required.");

            return Ok(new { message = "Task created successfully!" });
        }

        [HttpPut("UpdateTask/{id}")]
        public async Task<ActionResult> UpdateTask(int id, [FromBody] AddTaskItems updateTask)
        {
            var result = await _taskService.UpdateTaskAsync(id, updateTask);

            if (!result)
                return BadRequest("Task not found or update failed.");

            return Ok(new { message = "Task updated successfully!" });
        }

        [HttpDelete("DeleteTask/{id}")]
        public async Task<IActionResult> DeleteTask(int id)
        {
            var result = await _taskService.DeleteTaskAsync(id);

            if (!result)
                return NotFound($"Task with ID {id} not found.");

            return Ok(new { message = "Task deleted successfully!" });
        }


    }
}
