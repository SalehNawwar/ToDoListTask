
using Domain.Enums;

namespace Application.Common
{
    public enum SortingParameters
    {
        None = 0,
        ByTitleAscending,
        ByTitleDescending,
        ByPriorityLevelAscending,
        ByPriorityLevelDescending,
        ByDueDateAscending,
        ByDueDateDescending,
    }
    public class ToDoItemRequestParameters : RequestParameters
    {
        public string Title { get; set; } = string.Empty;
        public string Description {  get; set; } = string.Empty;
        public long? UserAssignedId { get; set; }
        public bool? IsCompleted { get; set; }
        public PriorityLevels? PriorityLevel { get; set; }

        public DateTime? From { get; set; }
        public DateTime? To { get; set; }

        public SortingParameters SortingParameter { get; set; }
    }
}
