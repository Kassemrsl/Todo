using System.ComponentModel.DataAnnotations;


namespace Todo.OnlineTaskManagement.ApplicationCore.Entities
{
    public class TaskEntity
    {
        public int Id { get; set; }

        public string Title { get; set; } 

        public string Description { get; set; } 

        public DateTime? DueDate { get; set; }

        public int Status { get; set; }

        public string Category { get; set; } 

        // Foreign Key for User (One-to-Many relationship)
        public string UserId { get; set; }

        // Navigation property to the User that owns this task
        public virtual ApplicationUser User { get; set; }
        public int Priority { get; set; }


        public void ExtendDueDate(TimeSpan extension)
        {
            if (DueDate.HasValue)
            {
                DueDate = DueDate.Value.Add(extension);
            }
        }
    }
}