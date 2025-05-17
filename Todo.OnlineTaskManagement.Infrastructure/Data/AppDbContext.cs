using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Todo.OnlineTaskManagement.ApplicationCore.Entities;

namespace Todo.OnlineTaskManagement.Infrastructure.Data
{

    public class AppDbContext(DbContextOptions<AppDbContext> options) : IdentityDbContext(options)
    {

        // DbSets for your entities
        public DbSet<TaskEntity> Tasks { get; set; }

        public DbSet<ApplicationUser> ApplicationUsers { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            //modelBuilder.Entity<TaskEntity>()
            //   .HasMany(t => t.Reminders)
            //   .WithOne() // Assuming a ReminderEntity has a TaskId foreign key
            //   .HasForeignKey(r => r.TaskId)
            //   .OnDelete(DeleteBehavior.Cascade);


         
        }
    }
}
