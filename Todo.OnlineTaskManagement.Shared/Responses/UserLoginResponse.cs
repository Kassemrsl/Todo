using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace Todo.OnlineTaskManagement.Shared.Requests
{
    public class UserLoginResponse
    {
        [JsonPropertyName("token")]
        public string Token { get; set; }

        [JsonPropertyName("roles")]
        public List<string> Roles { get; set; }
    }
}
