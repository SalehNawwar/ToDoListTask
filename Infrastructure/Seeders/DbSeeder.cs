using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Entities;
using Domain.Enums;
using Infrastructure.Security;

namespace Infrastructure.Seeders
{
    // better to have aseeder for each entity and the better to have a factory but trading for speed
    public static class DbSeeder
    {
        public static void Seed(ToDoListDBContext context)
        {
            if (!context.Users.Any())
            {
                context.Users.AddRange(
                    new User {UserEmail = "user1@gmail.com" , UserName = "user1" ,Role = Roles.Guest, UserPassword="183airehtiuh4"},
                    new User {UserEmail = "user2@gmail.com" , UserName = "user2" ,Role = Roles.Guest, UserPassword="183airehtiuh4"},
                    new User {UserEmail = "admin@gmail.com" , UserName = "admin" ,Role = Roles.Owner, UserPassword=new IdentityPasswordHasher().Hash("admin")}
                    
                );
                context.SaveChanges();
            }

            if (!context.ToDoItems.Any())
            {
                context.ToDoItems.AddRange(
                    new ToDoItem {Title = "task1", Description="task1 assigned to user2 to see it",PriorityLevel = PriorityLevels.High,IsCompleted = true,AssignedToUserId = 2,DueDate=null},
                    new ToDoItem {Title = "task2", Description="task2 assigned to user2 to see it",PriorityLevel = PriorityLevels.Medium,IsCompleted = false,AssignedToUserId = 2,DueDate= DateTime.Now.AddDays(1) },
                    new ToDoItem {Title = "task3", Description="task3 assigned to user2 to see it",PriorityLevel = PriorityLevels.None,IsCompleted = true,AssignedToUserId = 2,DueDate=DateTime.Now.AddDays(2)},
                    new ToDoItem {Title = "task4", Description="task4 assigned to user2 to see it",PriorityLevel = PriorityLevels.None,IsCompleted = false,AssignedToUserId = 2,DueDate=null},
                    new ToDoItem {Title = "task5", Description="task5 assigned to user2 to see it",PriorityLevel = PriorityLevels.Low,IsCompleted = true,AssignedToUserId = 2,DueDate=null},
                    new ToDoItem {Title = "task6", Description="task6 assigned to user2 to see it",PriorityLevel = PriorityLevels.High,IsCompleted = true,AssignedToUserId = 2,DueDate= DateTime.Now.AddDays(10) },
                    new ToDoItem {Title = "task7", Description="task7 assigned to user2 to see it",PriorityLevel = PriorityLevels.Low,IsCompleted = false,AssignedToUserId = 2,DueDate=null},
                    new ToDoItem {Title = "task8", Description="task8 assigned to user2 to see it",PriorityLevel = PriorityLevels.Medium,IsCompleted = true,AssignedToUserId = 2,DueDate= DateTime.Now.AddDays(40) },
                    new ToDoItem {Title = "task9", Description="task9 assigned to user2 to see it",PriorityLevel = PriorityLevels.None,IsCompleted = true,AssignedToUserId = 2,DueDate= DateTime.Now.AddDays(9) },
                    new ToDoItem {Title = "task10", Description="task10 assigned to user2 to see it",PriorityLevel = PriorityLevels.High,IsCompleted = false,AssignedToUserId = 2,DueDate=null}

                );
                context.SaveChanges();
            }

           
        }

    }
}
