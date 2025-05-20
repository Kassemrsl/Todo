using Todo.OnlineTaskManagement.Shared.Requests;
using Todo.OnlineTaskManagement.Shared.Responses;

namespace Todo.OnlineTaskManagement.BlazorApp.Application.Gateways
{
    public interface ITasksGateway
    {
        Task<List<TaskResponse>> GetTasksAsync();

        Task<TaskResponse> GetTaskByIdAsync(int id);

        Task DeleteTaskAsync(int id);

        Task<int> CreateTaskAsync(TaskCreationRequest taskCreationRequest);
    }
}
