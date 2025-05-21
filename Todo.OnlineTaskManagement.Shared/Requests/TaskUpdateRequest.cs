using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Todo.OnlineTaskManagement.Shared.Requests
{
    public class TaskUpdateRequest
    {
        public int TaskId { get; set; } // Identifier of the task to update
        public string Title { get; set; }
        public string Description { get; set; }
        public DateTime? DueDate { get; set; } // Nullable to allow partial updates
        public string Category { get; set; }
        public Shared.Responses.TaskStatus TaskStatus { get; set; }
        public Shared.Responses.TaskPriority TaskPriority { get; set; }

        public string UserId { get; set; }
    }
}
