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

namespace StudentMangTest.Test
{
    public class StudentControllerTest
    {
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
    }
}
