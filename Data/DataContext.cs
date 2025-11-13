using Microsoft.EntityFrameworkCore;
using Project_Planner_API.Data.Entities;

namespace Project_Planner_API.Data
{
    public class DataContext : DbContext
    {
        public DbSet<SubjectEntity> Subjects { get; set; }
        public DbSet<ResultEntity> Results { get; set; }
        public DbSet<TeamEntity> Teams { get; set; }
        public DbSet<StudentEntity> Students { get; set; }
        public DbSet<TaskEntity> Tasks { get; set; }

        public DataContext(DbContextOptions options) : base(options) { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<SubjectEntity>().ToTable("Subjects");
            modelBuilder.Entity<ResultEntity>().ToTable("Results");
            modelBuilder.Entity<TeamEntity>().ToTable("Teams");
            modelBuilder.Entity<StudentEntity>().ToTable("Students");
            modelBuilder.Entity<TaskEntity>().ToTable("Tasks");

            base.OnModelCreating(modelBuilder);
        }
    }
}
