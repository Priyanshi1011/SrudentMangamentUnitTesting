using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using StudentMang.Data;
using StudentMang.Models;

namespace StudentMang.Models
{
    public class StudentMangmentContext : DbContext, IReadOnlyStudentContext
    {
       

        public StudentMangmentContext (DbContextOptions<StudentMangmentContext> options)
            : base(options)
        {
        }

        public DbSet<Student> Students { get; set; }
       // IQueryable<Student> IReadOnlyStudentContext.Students => Students.AsNoTracking();
        IQueryable<Student> IReadOnlyStudentContext.Students { get => Students.AsNoTracking(); }
    }
}
