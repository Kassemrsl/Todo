using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Todo.OnlineTaskManagement.Shared.Requests
{
    public class TaskSearchRequest
    {
        public string SearchTerm { get; set; } 
        public string Category { get; set; } 
        public bool? IsCompleted { get; set; }
    }
}
