using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Todo.OnlineTaskManagement.ApplicationCore.Entities
{
    public class UserEntity
    {
        public int Id { get; set; }

        public string? Username { get; set; }

        public string? Email { get; set; } 

        public string? PasswordHash { get; set; } 

        public DateTime CreatedDate { get; set; }

        public DateTime? UpdatedDate { get; set; }

        // Optional: Role or permission properties
        public string? Role { get; set; }
    }
}
