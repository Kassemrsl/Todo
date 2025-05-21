using Todo.OnlineTaskManagement.ApplicationCore.Repositories;
using Todo.OnlineTaskManagement.Shared.Requests;
using Todo.OnlineTaskManagement.Shared.Responses;
using TaskStatus = Todo.OnlineTaskManagement.Shared.Responses.TaskStatus;

namespace Todo.OnlineTaskManagement.Application.Services
{
    public class TaskService(ITaskRepository taskRepository)
    {
        private readonly ITaskRepository _taskRepository = taskRepository;

        public async Task<int> CreateNewTaskAsync(TaskCreationRequest taskCreationRequest)
        {
            return await _taskRepository.AddTaskAsync(new ApplicationCore.Entities.TaskEntity()
            {
                Category = taskCreationRequest.Category,
                Description = taskCreationRequest.Description,
                DueDate = taskCreationRequest.DueDate,
                Status = (int)taskCreationRequest.TaskStatus,
                Priority = (int)taskCreationRequest.TaskPriority,
                Title = taskCreationRequest.Title,
                UserId = taskCreationRequest.UserId,
            });
        }

        public async Task<TaskResponse> GetTaskAsync(int id)
        {
            var task = await _taskRepository.GetTaskByIdAsync(id);

            return new TaskResponse()
            {
                TaskId = id,
                UserId = task.UserId,
                Category = task.Category,
                Description = task.Description,
                DueDate = task.DueDate,
                TaskPriority = (TaskPriority)task.Priority,
                Title = task.Title,
                TaskStatus = (TaskStatus)task.Status
            };
        }

        public async Task<IEnumerable<TaskResponse>> GetAllTasksAsync(string userId)
        {
            return (await _taskRepository.GetUserTasksAsync(userId))
                .Select(task => new TaskResponse()
                {
                    TaskId = task.Id,
                    Category = task.Category,
                    Description = task.Description,
                    DueDate = task.DueDate.Value,
                    TaskPriority = (TaskPriority)task.Priority,
                    Title = task.Title,
                    TaskStatus = (TaskStatus)task.Status
                });
        }

        public async Task DeleteTaskAsync(int taskId)
        {
            await _taskRepository.DeleteTaskAsync(taskId);
        }

        public async Task UpdateTaskAsync(TaskUpdateRequest taskUpdateRequest)
        {
            await _taskRepository.UpdateTaskAsync(new ApplicationCore.Entities.TaskEntity()
            {
                Id = taskUpdateRequest.TaskId,
                Category = taskUpdateRequest.Category,
                Description = taskUpdateRequest.Description,
                DueDate = taskUpdateRequest.DueDate,
                Priority = (int)taskUpdateRequest.TaskPriority,
                Status = (int)taskUpdateRequest.TaskStatus,
                Title = taskUpdateRequest.Title,
                UserId = taskUpdateRequest.UserId
            });
        }
    }
}

