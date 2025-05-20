using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Todo.OnlineTaskManagement.Shared.Responses;

namespace Todo.OnlineTaskManagement.Shared.Requests
{
    public class TaskCreationRequest
    {
        public string UserId { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public DateTime? DueDate { get; set; }
        public string Category { get; set; }

        public Responses.TaskStatus Status { get; set; }

        public TaskPriority Priority { get; set; }
    }
}
