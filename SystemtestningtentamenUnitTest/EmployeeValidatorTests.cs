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
    public class EmployeeValidatorTests
    {
        private DateTime _age20;
        private EmployeeValidator _validator;

        [TestInitialize]
        public void EmployeeValidatorTestsInit()
        {
            _validator = new EmployeeValidator();
            _age20 = DateTime.Parse("1999-06-12");

        }

        [TestMethod]
        public void BirthDateAndAge_CalculatesCorrectValue()
        {
            var age = _validator.CalculateAge(_age20);

            Assert.AreEqual(20, age);
        }

        [TestMethod]
        [DataRow(18)]
        [DataRow(19)]
        [DataRow(59)]
        [DataRow(60)]
        public void ValidateAge_ReturnsTrueIfValidAge(int age)
        {
            bool result = _validator.ValidateAge(age);
            Assert.IsTrue(result);
        }

        [TestMethod]
        [DataRow(17)]
        [DataRow(61)]
        public void ValidateAge_ReturnsFalseIfInvalidAge(int age)
        {
            bool result = _validator.ValidateAge(age);
            Assert.IsFalse(result);
        }

        [TestMethod]
        public void ValidateBirthdateAndAge_ReturnsTrueIfCorrect()
        {
            var result = _validator.ValidateBirthdateAndAge(_age20, 20);
            Assert.IsTrue(result);
        }
        [TestMethod]
        public void ValidateBirthdateAndAge_ReturnsFalseIfIncorrect()
        {
            var result = _validator.ValidateBirthdateAndAge(_age20, 21);
            Assert.IsFalse(result);
        }
        [TestMethod]
        public void EmploNoIsUnique_UniqueValueReturnsTrue()
        {
            bool result = _validator.EmpNoIsUnique(2);

            Assert.IsTrue(result);
        }

    }
}
