using Microsoft.EntityFrameworkCore;
using Domain.Entities;

namespace Infrastructure
{
    public class ToDoListDBContext:DbContext
    {
        public ToDoListDBContext(DbContextOptions options): base(options) { }
        public DbSet<ToDoItem> ToDoItems { get; set; }
        public DbSet<User> Users { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<ToDoItem>()
                .HasOne(t => t.AssignedToUser)
                .WithMany(t => t.AssignedToDoItems)
                .HasForeignKey(t => t.AssignedToUserId)
                .IsRequired(false);

            modelBuilder.Entity<ToDoItem>()
                .HasOne(e => e.CreatedByUser);

            modelBuilder.Entity<ToDoItem>()
                .HasOne(e => e.UpdatedByUser);

        }
    }
}
