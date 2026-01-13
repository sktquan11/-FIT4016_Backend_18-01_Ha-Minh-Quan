using Microsoft.EntityFrameworkCore;
using SchoolManagement.Models;

namespace SchoolManagement.Data {
    public class AppDbContext : DbContext {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        public DbSet<School> Schools { get; set; }
        public DbSet<Student> Students { get; set; }
    }
}