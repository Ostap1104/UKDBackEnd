using Microsoft.EntityFrameworkCore;
using ITSchool.DAL.Models;
using Microsoft.AspNetCore.Identity;

namespace ITSchool.DAL.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
            SQLitePCL.Batteries.Init();
        }
        
        public DbSet<Teacher> Teachers { get; set; }
        public DbSet<Course> Courses { get; set; }
        public DbSet<User> Users { get; set; }
        
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Admin user will be created through a database initializer
            // instead of seeding it here with a hardcoded hash
        }
    }
}
