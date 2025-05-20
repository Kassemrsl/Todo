using Blazored.LocalStorage;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using System.Text;
using System.Threading.Tasks;
using Todo.OnlineTaskManagement.BlazorApp.Application.Gateways;
using Todo.OnlineTaskManagement.Shared.Requests;
using Todo.OnlineTaskManagement.Shared.Responses;

namespace Todo.OnlineTaskManagement.BlazorApp.Infrastructure.Gateways
{
    public class TasksGateway : ITasksGateway
    {
        private readonly HttpClient httpClient;

        private readonly ILocalStorageService _localStorageService;


        public TasksGateway(HttpClient client, ILocalStorageService localStorageService)    
        {
            this.httpClient = client;

            _localStorageService = localStorageService;

        }
        private async Task<string> GetAccessTokenAsync()
        {
            return await _localStorageService.GetItemAsync<string>("AuthToken");
        }

        public async Task<TaskResponse> GetTaskByIdAsync(int id)
        {
            var token = await GetAccessTokenAsync();

            if (!string.IsNullOrEmpty(token))
            {
                httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
            }

            var result = await httpClient.GetAsync($"api/Task/GetTaskById/{id}");

            if (result.IsSuccessStatusCode)
            {
                return await result.Content.ReadFromJsonAsync<TaskResponse>();
            }

            throw new Exception("Error in retriving Task");
        }

        public async Task<List<TaskResponse>> GetTasksAsync()
        {
            var token = await GetAccessTokenAsync();

            if (!string.IsNullOrEmpty(token))
            {
                httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
            }

            var result = await httpClient.GetAsync("api/Task/GetAllTasks");

            if (result.IsSuccessStatusCode)
            {
                return await result.Content.ReadFromJsonAsync<List<TaskResponse>>();
            }

            throw new Exception("Error in retriving tasks");
        }

        public async Task<int> CreateTaskAsync(TaskCreationRequest taskCreationRequest)
        {
            var token = await GetAccessTokenAsync();

            if (!string.IsNullOrEmpty(token))
            {
                httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
            }

            var result = await httpClient.PostAsJsonAsync("api/Task/CreateTask", taskCreationRequest);

            if (result.IsSuccessStatusCode)
            {
                return await result.Content.ReadFromJsonAsync<int>();
            }

            throw new Exception("Error in creating tasks");

        }

        public async Task DeleteTaskAsync(int id)
        {
            var token = await GetAccessTokenAsync();

            if (!string.IsNullOrEmpty(token))
            {
                httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
            }

            var result = await httpClient.DeleteAsync($"api/Task/DeleteTask/{id}");

            if (!result.IsSuccessStatusCode)
            {
                throw new Exception("Error in creating tasks");
            }
        }
    }
}
