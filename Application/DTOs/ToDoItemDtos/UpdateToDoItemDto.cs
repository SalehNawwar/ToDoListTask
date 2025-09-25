using Domain.Entities;
using Domain.Enums;

namespace Application.DTOs.ToDoItemDtos
{
    public class UpdateToDoItemDto
    {
        public long Id {  get; set; }
        public string Title { get; set; } = string.Empty;
        public string? Description { get; set; }
        public PriorityLevels PriorityLevel { get; set; }
        public DateTime? DueDate { get; set; } = null!;
        public bool IsCompleted { get; set; } 
        public long? AssignedToUserId { get; set; }
    }
}
