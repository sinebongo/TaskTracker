using TaskTrackerAPI.Domain.Enums;

namespace TaskTrackerAPI.Domain.Entities
{
    public class TaskItem
    {
        public int Id { get; set; }
        public required string Title { get; set; } 
        public string? Description { get; set; } = string.Empty;
        public Status Status { get; set; } = Status.New;
        public TaskPriority Priority { get; set; } = TaskPriority.Medium;
        public DateTime? DueDate { get; set; } = DateTime.MinValue;
        public DateTime CreatedAt { get; set; } 

    }
}
