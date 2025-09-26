using Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class User:EntityBase
    {
        public string UserName { get; set; } = null!;
        public string UserEmail { get; set; } = null!;
        public string UserPassword { get; set; } = null!;
        public Roles Role { get; set; } = Roles.Guest;

        public ICollection<ToDoItem> AssignedToDoItems { get; set; } = new List<ToDoItem>();
    }
}
