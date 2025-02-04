using System;
using System.Collections.Generic;
using System.Linq;
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

        public DbSet<TimetableHelper.Models.Class> Class { get; set; } = default!;
        public DbSet<Teacher> Teacher { get; set; } = default!;
        public DbSet<Student> Student { get; set; } = default!;

    }
}
