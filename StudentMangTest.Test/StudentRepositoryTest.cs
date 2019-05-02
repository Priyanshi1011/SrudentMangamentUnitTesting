using Microsoft.EntityFrameworkCore;
using Moq;
using StudentMang.Models;
using StudentMang.Services;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;
using StudentMang.Data;
namespace StudentMangTest.Test
{
    public class StudentRepositoryTest
    {
        [Fact]
       
        public void CanStudentRepositoryGetStudent()
        {
            //arrange
            var mockSpaContext = new Mock<StudentMangmentContext>(new DbContextOptionsBuilder<StudentMangmentContext>().Options);
            StudnetRepository repo = new StudnetRepository(mockSpaContext.Object);
            var testStudent = new Student()
            {
                Id = 3,
                LastName = "Priyanshi",
                FirstMidName = "Kothari"
               
            };

            //act
            repo.Add(testStudent);

            //assert
            var c = repo.GetStudent(testStudent.Id);
            Assert.NotNull(c);
        }
    }

    //internal class DbContextOptionsBuilder<T>
    //{
    //    public DbContextOptionsBuilder()
    //    {
    //    }

    //    public object[] Options { get; internal set; }
    //}
}
