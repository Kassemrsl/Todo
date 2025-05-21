using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Todo.OnlineTaskManagement.Shared.Requests;

namespace Todo.OnlineTaskManagement.Shared.Responses
{
    public class TaskResponse : TaskCreationRequest
    {
        public int TaskId { get; set; }
    }
}
