using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Todo.OnlineTaskManagement.BlazorApp.Application.Gateways;
using Todo.OnlineTaskManagement.Shared.Responses;

namespace Todo.OnlineTaskManagement.BlazorApp.Infrastructure.Gateways
{
    public class TasksGateway : ITasksGateway
    {
        public async Task<TaskResponse> GetTaskByIdAsync()
        {
            throw new NotImplementedException();
        }

        public async Task<List<TaskResponse>> GetTasksAsync()
        {
            throw new NotImplementedException();
        }
    }
}
