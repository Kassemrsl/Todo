using Microsoft.EntityFrameworkCore;
using Todo.OnlineTaskManagement.ApplicationCore.Entities;
using Todo.OnlineTaskManagement.ApplicationCore.Repositories;
using Todo.OnlineTaskManagement.Infrastructure.Data;

namespace Todo.OnlineTaskManagement.Infrastructure.Repositories
{
    public class TaskRepository : ITaskRepository
    {
        private readonly AppDbContext _appDbContext;

        public TaskRepository(AppDbContext appDbContext)
        {
            _appDbContext = appDbContext;
        }
        public async Task<int> AddTaskAsync(TaskEntity task)
        {
            await _appDbContext.Tasks.AddAsync(task);

            return await _appDbContext.SaveChangesAsync();
        }

        public async Task<IQueryable<TaskEntity>> GetUserTasksAsync(string userId)
        {
            return _appDbContext.Tasks.Where(x => x.UserId == userId);
        }

        public async Task<TaskEntity> GetTaskByIdAsync(int taskId)
        {
            return _appDbContext.Tasks.FirstOrDefault(x => x.Id == taskId);
        }

        public async Task DeleteTaskAsync(int taskId)
        {
            var taskToDelete = _appDbContext.Tasks.First(x => x.Id == taskId);

            _appDbContext.Tasks.Remove(taskToDelete);

            await _appDbContext.SaveChangesAsync();
        }

        public async Task UpdateTaskAsync(TaskEntity task)
        {
            _appDbContext.Tasks.Update(task);

            await _appDbContext.SaveChangesAsync();
        }
    }
}
