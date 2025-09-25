using Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class ToDoItem:EntityBase
    {   
        public string Title { get; set; } = string.Empty;
        public string? Description { get; set; }
        public PriorityLevels PriorityLevel { get; set; }
        public DateTime? DueDate {  get; set; }
        public bool IsCompleted { get; set; }
        public long? CreatedByUserId { get; set; }
        public User? CreatedByUser { get; set; }
        public long? UpdatedByUserId { get; set; }
        public User? UpdatedByUser { get; set; }
        public long? AssignedToUserId { get; set; }
        public User? AssignedToUser { get; set; }
    }
}
