namespace Todo.OnlineTaskManagement.ApplicationCore.Entities
{
    public class ReminderEntity
    {
        public int Id { get; set; }

        public string Title { get; set; }

        public DateTime ReminderDateTime { get; set; }

        public bool IsCompleted { get; set; }
        public DateTime CreatedDate { get; set; }

        public DateTime? UpdatedDate { get; set; }

        // Foreign key reference to Task
        public Guid TaskId { get; set; }

    }
}
