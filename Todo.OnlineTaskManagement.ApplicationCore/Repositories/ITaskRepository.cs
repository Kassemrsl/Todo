using Todo.OnlineTaskManagement.ApplicationCore.Entities;

namespace Todo.OnlineTaskManagement.ApplicationCore.Repositories
{
    public interface ITaskRepository
    {
        Task<int> AddTaskAsync(TaskEntity task);

        Task<TaskEntity> GetTaskByIdAsync(int taskId);

        Task<IQueryable<TaskEntity>> GetUserTasksAsync(string userId);

        Task DeleteTaskAsync(int taskId);

        Task UpdateTaskAsync(TaskEntity task);
    }
}

