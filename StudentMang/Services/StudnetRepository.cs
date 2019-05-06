using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using StudentMang.Data;
using StudentMang.Models;
namespace StudentMang.Services
{
    public class StudnetRepository : IStudentRepository
    {
        private readonly StudentMangmentContext _spaContext;
        public IQueryable<Student> Students => _spaContext.Students.AsQueryable();
      
        public StudnetRepository(StudentMangmentContext spaContext)
        {
            _spaContext = spaContext;
        }

        //method allows me to add customers to my list
        public void Add(Student student)
        {
            _spaContext.Students.Add(student);
            _spaContext.SaveChanges();
        }

        public void Update(int id, Student student)
        {
            student.Id = id;
            _spaContext.Students.Update(student);
            _spaContext.SaveChanges();
        }

        //to delete from the list
        public void DeleteStudent(int id)
        {
            var index = _spaContext.Students.First(SelectStudentById(id));
            _spaContext.Students.Remove(index);
            _spaContext.SaveChanges();
        }

        public Student GetStudent(int id)
        {
            return _spaContext.Students.FirstOrDefault(SelectStudentById(id));
        }

        public bool ThisCustomerExists(int id)
        {
            foreach (Student stud in _spaContext.Students)
            {
                if (stud.Id == id)
                    return true;
            }
            return false;
        }

        //Selector Functions
        private static Func<Student, bool> SelectStudentById(int id)
        {
            return Student => Student.Id == id;
        }
    }
}
