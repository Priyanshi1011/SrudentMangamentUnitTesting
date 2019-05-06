using Microsoft.EntityFrameworkCore;
using Moq;
using StudentMang.Models;
using StudentMang.Services;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;
using StudentMang.Data;
using System.Linq;

namespace StudentMangTest.Test
{
    public class StudentRepositoryTest
    {
        public Mock<IStudentRepository> mockRepo;

        public StudentMang.Controllers.StudentsController controller;

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
        [Fact]
        public void CanStudentRepositoryAddStudent()
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
           repo.Add(testStudent);
        }
        [Fact]
        public void CanStudentRepositoryUpdateStudent()
        {
            //arrange
            var mockSpaContext = new Mock<StudentMangmentContext>(new DbContextOptionsBuilder<StudentMangmentContext>().Options);
            StudnetRepository repo = new StudnetRepository(mockSpaContext.Object);
            var Id = 3;
            var testStudent = new Student()
            {
                Id = 3,
                LastName = "Priyanshi1",
                FirstMidName = "Kothari"

            };
            repo.Update(Id,testStudent);
        }
        [Fact]
        public void CanStudentRepositoryDeleteStudent()
        {
            var mockSpaContext = new Mock<StudentMangmentContext>(new DbContextOptionsBuilder<StudentMangmentContext>().Options);
            StudnetRepository repo = new StudnetRepository(mockSpaContext.Object);
            mockRepo.Setup(repo1 => repo1.DeleteStudent(It.IsAny<int>())).Verifiable();
         
            var Id = 2;     
            mockRepo.Verify(repo1=> repo1.DeleteStudent(Id), Times.Once, "error when delete student");
            repo.DeleteStudent(Id);

        }
    }

  
}
