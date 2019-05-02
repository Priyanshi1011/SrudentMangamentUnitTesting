using StudentMang.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace StudentMang.Services
{
    public interface IStudentRepository
    {
        IQueryable<Student> Students { get; }

        void Add(Student customer);
        void DeleteStudent(int id);
        Student GetStudent(int id);
        bool ThisCustomerExists(int id);
        void Update(int id, Student customer);
    }
}
