using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using TaskManagement_Backend.Models;


namespace TaskManagementAPI.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options)
            : base(options) { }

        //Call DB MODELS & COLUMNS
        public DbSet<TaskItems> Tasks { get; set; }
        public DbSet<User> Users { get; set; }
    }
}
