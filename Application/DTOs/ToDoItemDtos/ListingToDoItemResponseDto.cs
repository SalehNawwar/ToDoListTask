
using Domain.Enums;

namespace Application.DTOs.ToDoItemDtos
{
    public class ListingToDoItemResponseDto
    {
        public long Id  { get; set; }
        public string Title { get; set; } = null!;
        public PriorityLevels PriorityLevel { get; set; }
        public DateTime? DueDate { get; set; }
        public bool IsCompleted {  get; set; }
        public long? AssignedToUserId { get; set; }
    }
}
