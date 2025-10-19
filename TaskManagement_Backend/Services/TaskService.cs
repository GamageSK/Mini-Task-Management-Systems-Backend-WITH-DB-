using TaskManagement_Backend.Models;
using TaskManagementAPI.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc;

namespace TaskManagement_Backend.Services
{
    public class TaskService
    {
        //Call AppDB
        private readonly AppDbContext _context;

        public TaskService(AppDbContext context)
        {
            _context = context;
        }

        //Call Add task Methord
        public async Task<bool> AddTaskAsync(AddTaskItems AddItems)
        {
            if (AddItems == null || string.IsNullOrWhiteSpace(AddItems.Title) || string.IsNullOrWhiteSpace(AddItems.Status))
                return false;

            var task = new TaskItems
            {
                Title = AddItems.Title,
                Status = AddItems.Status,
                DueDate = AddItems.DueDate,
                Description = AddItems.Description ?? string.Empty
            };

            _context.Tasks.Add(task);
            await _context.SaveChangesAsync();
            return true;
        }

        //Call Get All task Methord
        internal async Task<ActionResult<IEnumerable<TaskItems>>> GetAllTasksAsync()
        {
            return await _context.Tasks.ToListAsync();
        }

        //Call Upadte All task Methord
        public async Task<bool> UpdateTaskAsync(int id, AddTaskItems updateTask)
        {
            var existingTask = await _context.Tasks.FirstOrDefaultAsync(t => t.Id == id);
            if (existingTask == null)
                return false;

            // Only update fields if they are not null or empty
            if (!string.IsNullOrWhiteSpace(updateTask.Title))
                existingTask.Title = updateTask.Title;

            if (!string.IsNullOrWhiteSpace(updateTask.Status))
                existingTask.Status = updateTask.Status;

            if (!string.IsNullOrWhiteSpace(updateTask.Description))
                existingTask.Description = updateTask.Description;

            // Only update date if it's not default(DateTime)
            if (updateTask.DueDate != default)
                existingTask.DueDate = updateTask.DueDate;

            _context.Tasks.Update(existingTask);
            await _context.SaveChangesAsync();
            return true;
        }

        //Call Get Task By ID Methord
        public async Task<TaskItems?> GetTaskByIdAsync(int id)
        {
            return await _context.Tasks.FirstOrDefaultAsync(t => t.Id == id);
        }

        //Call Delete Task By ID Methord
        internal async Task<bool> DeleteTaskAsync(int id)
        {
            var task = await _context.Tasks.FirstOrDefaultAsync(t => t.Id == id);
            if (task == null)
                return false;

            _context.Tasks.Remove(task);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}
