using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using StudentMang.Models;
namespace StudentMang.Data
{
    public interface IReadOnlyStudentContext
    {
        IQueryable<Student> Students { get; }
    }
}
