using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Todo.OnlineTaskManagement.Shared.Requests
{
    public class ReminderRequest
    {
        public int TaskId { get; set; } 
        public DateTime ReminderDateTime { get; set; }
    }
}
