using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Hosting;
using TimetableHelper.Models;

namespace TimetableHelper.Data
{
    public class TimetableHelperContext : DbContext
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

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
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

        }
    }
}
