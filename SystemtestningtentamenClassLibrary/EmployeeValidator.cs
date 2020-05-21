using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SystemtestningtentamenClassLibrary
{
    public class EmployeeValidator
    {
        private readonly EmployeeService _employeeService = new EmployeeService();
        public bool EmpNoIsUnique(int empNo)
        {
            Employee employee = _employeeService.GetEmployeeByEmpNo(empNo);
            //if no employee with thah empNo has been found then employee equals null
            if (employee is null)
            {
                return true;
            }


            return false;

        }
        public bool ValidateAge(int age)
        {
            return (age < 18 || 60 < age) ? false : true;

        }

        public bool ValidateBirthdateAndAge(DateTime dateOfBirth, int age)
        {
            //ToDO
            return age == CalculateAge(dateOfBirth) ? true : false;
        }
        public int CalculateAge(DateTime dateOfBirth)
        {
            var ageInDays = (DateTime.Now - dateOfBirth).Days;

            //.25 in case of leapyear
            return (int)(ageInDays / 365.25);
        }
    }
}
