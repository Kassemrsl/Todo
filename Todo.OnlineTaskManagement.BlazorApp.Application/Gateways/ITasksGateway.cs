using Todo.OnlineTaskManagement.Shared.Responses;

namespace Todo.OnlineTaskManagement.BlazorApp.Application.Gateways
{
    public interface ITasksGateway
    {
        Task<List<TaskResponse>> GetTasksAsync();

        Task<TaskResponse> GetTaskByIdAsync();
    }
}
