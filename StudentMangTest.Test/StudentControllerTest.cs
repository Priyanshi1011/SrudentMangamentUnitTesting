using System;
using Xunit;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using StudentMang.Controllers;
using StudentMang.Data;
using StudentMang.Services;
using StudentMang.Models;
using Moq;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace StudentMangTest.Test
{
    public class StudentControllerTest
    {
        public Mock<IStudentRepository> mockRepo;
        public StudentsController controller;
        public StudentControllerTest()
        {
            mockRepo = new Mock<IStudentRepository>();
            controller = new StudentsController(mockRepo.Object);
        }

        [Fact]
        public void CanStudentControllerReturnIndexView()
        {
            //arrange
            var mockRepo = new Mock<IStudentRepository>();
            var controller = new StudentsController(mockRepo.Object);
            mockRepo.Setup(x => x.Students).Returns(new List<Student>()
            {
                new Student() {}
            }.AsQueryable());
            //act
            var result = controller.Index();
            //assert
            Assert.IsType<ViewResult>(result);
        }

        [Fact]
        public void ReturnsOkResult_WhenCreatingValidStudent()
        {
            // Arrange & Act
            mockRepo.Setup(x => x.Students).Returns(new List<Student>()
            {
                new Student() {}
            }.AsQueryable());
            mockRepo.Setup(repo => repo.Add(It.IsAny<Student>())).Verifiable();
            //var mockSpaContext = new Mock<StudentMangmentContext>(new DbContextOptionsBuilder<StudentMangmentContext>().Options);
            //StudnetRepository repo1 = new StudnetRepository(mockSpaContext.Object);
            ////var mockstu = new Student { LastName = "Kothari", FirstMidName = "aaa" };
            var testStudent = new Student()
            {        
                LastName = "aa",
                FirstMidName="aa"
            };

            // Act
            var result = controller.Create();

            //Assert
           
            mockRepo.Verify(repo => repo.Add(It.IsAny<Student>()), Times.Once);
            var createdAtRouteResult = Assert.IsType<CreatedAtActionResult>(result);
            var mockstudent = Assert.IsType<Student>(createdAtRouteResult.Value);
            Assert.Equal(testStudent.LastName,mockstudent.LastName);
            Assert.Equal(testStudent.FirstMidName, mockstudent.FirstMidName);
        }
        
        [Fact]
        public void Create_AddsStudentToRepository_AndRedirectsToIndex()
        {
            // Arrange  
            var mockstudent = new Student { LastName = "Kothari",FirstMidName="aaa" };
            mockRepo.Setup(repo => repo.Add(It.IsAny<Student>()));
            // Act 
            var result = controller.Create(mockstudent);
            // Assert 
            mockRepo.Verify(repo => repo.Add(mockstudent));
            var viewResult = Assert.IsType<RedirectToActionResult>(result);
            Assert.Equal("Index", viewResult.ActionName);
        } 

        [Fact]
        public void ReturnsBadRequest_WhenUpdatingInvalidNullStudent()
        {
            // Act
            var result = controller.Create(null);
            //Assert
            mockRepo.Verify(repo => repo.Add(It.IsAny<Student>()), Times.Never);
            Assert.IsType<BadRequestObjectResult>(result);
        }

        [Fact]
        public void ReturnsBadRequest_WhenAddingExistingStudent()
        {
            mockRepo.Setup(x => x.Students).Returns(new List<Student>()
            {
                new Student() {}
            }.AsQueryable());
            // Act
            var result = controller.Create();
            //Assert
            mockRepo.Verify(repo => repo.Add(It.IsAny<Student>()), Times.Never);
            Assert.IsType<ViewResult>(result);
        }

        [Fact]
        public void Delete_ReturnsHttpBadRequest_ForInvalidStudent()
        {
            // Arrange
            int Id = 1000;
            mockRepo.Setup(x => x.Students).Returns(new List<Student>()
            {
                new Student() {}
            }.AsQueryable());
            // Act
            var result = controller.Delete(Id);
            // Assert
            Assert.IsType<ViewResult>(result);
        }

        [Fact]
        public void Delete_ReturnsNoContentResult_ForValidStudent()
        {
            // Arrange
            var mockSpaContext = new Mock<StudentMangmentContext>(new DbContextOptionsBuilder<StudentMangmentContext>().Options);
            StudnetRepository repo = new StudnetRepository(mockSpaContext.Object);
            mockRepo.Setup(x => x.Students).Returns(new List<Student>()
            {
                new Student() {}
            }.AsQueryable());
            // Act
            var Id = 2;
            mockRepo.Verify(repo1 => repo1.DeleteStudent(Id), Times.Once, "error when delete student");
            repo.DeleteStudent(Id);  
        }

        [Fact]
        public void DetailsTest_ReturnsDetailsView_WhenStudentExists()
        {  // Arrange  
            var mockId = 2;
            var mockstudent = new Student { LastName="Priyanshi",FirstMidName= "Kothari" };
            mockRepo.Setup(repo => repo.GetStudent(mockId));
            // Act 
            var result =  controller.Details(mockId);
            // Assert  
            var viewResult = Assert.IsType<ViewResult>(result);
            Assert.Equal(mockstudent, viewResult.ViewData.Model);
        }

    }
}
