using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
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

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Class>()
                .HasMany(e => e.Students)
                .WithOne(e => e.Class)
                .HasForeignKey(e => e.ClassId);
        }
    }
}
