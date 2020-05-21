using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SystemtestningtentamenClassLibrary;

namespace SystemtestningtentamenUnitTest
{
    [TestClass]
    public class EmployeeServiceTests
    {
        private EmployeeService _employeeService;
        private const string _sampleName = "Sample";


        [TestInitialize]
        public void EmployeeServiceTestsInit()
        {
            _employeeService = new EmployeeService();
        }
        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void Add_EmployeeIsNull_ThrowsArgumentnullException()
        {
            _employeeService.Add(null);
            Assert.Fail();
        }
        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void Remove_EmployeeIsNull_ThrowsArgumentnullException()
        {
            _employeeService.Remove(null);
            Assert.Fail();
        }
        [TestMethod]
        public void GetEmployeeByName_ReturnObjectIfFound()
        {
            _employeeService.PopulateSamples();
            var employee = _employeeService.GetEmployeeByName(_sampleName);
            _employeeService.RemoveSamples();

            Assert.AreEqual(employee.Name, _sampleName);

        }
        [TestMethod]
        public void GetEmployeeByName_ReturnsNUllIfNotFound()
        {
            _employeeService.PopulateSamples();
            var employee = _employeeService.GetEmployeeByName("");
            _employeeService.RemoveSamples();

            Assert.IsNull(employee);
        }
        [TestMethod]
        public void GetEmployeeByEmpNo_ReturnObjectIfFound()
        {
            _employeeService.PopulateSamples();
            var employee = _employeeService.GetEmployeeByEmpNo(5555); //5555 comes with samples
            _employeeService.RemoveSamples();

            Assert.AreEqual(employee.EmpNo, 5555);

        }
        [TestMethod]
        public void GetEmployeeByEmpNo_ReturnsNUllIfNotFound()
        {
            _employeeService.PopulateSamples();
            var employee = _employeeService.GetEmployeeByEmpNo(-1);
            _employeeService.RemoveSamples();

            Assert.IsNull(employee);
        }
        [TestMethod]
        [Description("Checks if populating Employees with samples works as expected")]
        public void SampleTest_CorrectCount()
        {
            _employeeService.PopulateSamples();
            int result = _employeeService.EmployeeCount();
            _employeeService.RemoveSamples();

            Assert.AreEqual(result, 3);
        }

    }
}
