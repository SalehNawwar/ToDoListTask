

using Domain.Enums;

namespace Application.DTOs.ToDoItemDtos
{
    public class ToDoItemResponseDto
    {
        public long Id {  get; set; }
        public string Title { get; set; } = null!;
        public string? Description { get; set; }
        public PriorityLevels PriorityLevel { get; set; }
        public DateTime? DueDate { get; set; }
        public bool IsCompleted { get; set; }
        public long? AssignedToUserId {  get; set; }
    }
}
