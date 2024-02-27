using Microsoft.AspNetCore.Mvc;
using NUnit.Framework;
using System.Collections.Generic;
using UnitTestCaseDemo.Controllers;
using UnitTestCaseDemo.Model;
using UnitTestCaseDemo.Services;

namespace UnitTestCaseDemoNunit
{
    public class EmployeeControllerTest
    {
        EmployeeController _controller;
        IEmployeeService _service;

        public EmployeeControllerTest()
        {

            _service = new EmployeeService();
            _controller = new EmployeeController(_service);
        }
        
        [Test]
        public void Test1()
        {
            Assert.Pass();
        }

        [Test]
        public void GetAll_Employee_Success()
        {
            //Arrange

            //Act
            var result = _controller.Get();
            var resultType = result as OkObjectResult;
            var resultList = resultType.Value as List<Employee>;

            //Assert
            Assert.NotNull(result);
            Assert.IsInstanceOf<List<Employee>>(resultType.Value);
            Assert.AreEqual(3, resultList.Count);
        }
        [Test]
        public void GetById_Employee_Success()
        {
            //Arrange
            int valid_empid = 101;
            int invalid_empid = 110;

            //Act
            var errorResult = _controller.Get(invalid_empid);
            var successResult = _controller.Get(valid_empid);
            var successModel = successResult as OkObjectResult;
            var fetchedEmp = successModel.Value as Employee;

            //Assert
            Assert.IsInstanceOf<OkObjectResult>(successResult);
            Assert.IsInstanceOf<NotFoundResult>(errorResult);
            Assert.AreEqual(101, fetchedEmp.Id);
        }
        [Test]
        public void Add_Employee_Success()
        {
            Employee employee = new Employee()
            {
                Id = 105,
                Name = "Shane Warne",
                PhoneNo = "5555555555",
                EmailId = "shane@email.com"
            };

            var response = _controller.Post(employee);

            Assert.IsInstanceOf<CreatedAtActionResult>(response);

            var createdEmp = response as CreatedAtActionResult;
            Assert.IsInstanceOf<Employee>(createdEmp.Value);

            var employeeItem = createdEmp.Value as Employee;

            Assert.AreEqual("Shane Warne", employeeItem.Name);
            Assert.AreEqual("5555555555", employeeItem.PhoneNo);
            Assert.AreEqual("shane@email.com", employeeItem.EmailId);
        }
        [Test]
        public void Delete_Employee_Success()
        {
            int valid_empid = 101;
            int invalid_empid = 110;

            var errorResult = _controller.Delete(invalid_empid);
            var successResult = _controller.Delete(valid_empid);

            Assert.IsInstanceOf<OkObjectResult>(successResult);
            Assert.IsInstanceOf<NotFoundObjectResult>(errorResult);
        }

    }
}