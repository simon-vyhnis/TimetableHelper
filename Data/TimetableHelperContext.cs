using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using TimetableHelper.Models;

namespace TimetableHelper.Data
{
    public class TimetableHelperContext : IdentityDbContext<User>
    {
        public TimetableHelperContext (DbContextOptions<TimetableHelperContext> options)
            : base(options)
        {
        }

        public DbSet<Class> Class { get; set; } = default!;
        public DbSet<Teacher> Teacher { get; set; } = default!;
        public DbSet<Student> Student { get; set; } = default!;
        public DbSet<Room> Room { get; set; } = default!;
        public DbSet<Group> Group { get; set; } = default!;
        public DbSet<Subject> Subject { get; set; } = default!;
        public DbSet<Lesson> Lesson { get; set; } = default!;

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Class>()
                .HasMany(e => e.Students)
                .WithOne(e => e.Class)
                .HasForeignKey(e => e.ClassId);

            modelBuilder.Entity<Student>()
                .HasMany(e => e.Groups)
                .WithMany(e => e.Students);

            modelBuilder.Entity<Class>()
                .HasMany(e => e.Groups)
                .WithOne(e => e.Class)
                .HasForeignKey(e => e.ClassId);

            modelBuilder.Entity<Subject>()
                .HasOne(e => e.Class)
                .WithMany(e => e.Subjects);

            modelBuilder.Entity<Subject>()
                .HasOne(e => e.Group)
                .WithMany(e => e.Subjects);

            modelBuilder.Entity<Subject>()
                .HasOne(e => e.Teacher)
                .WithMany(e => e.Subjects)
                .HasForeignKey(e => e.TeacherId);

            modelBuilder.Entity<Lesson>()
                .HasOne(e => e.Subject)
                .WithMany(e => e.Lessons);

            modelBuilder.Entity<Lesson>()
                .HasOne(e => e.Room);

        }
    }
}
