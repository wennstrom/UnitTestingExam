using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace SystemtestningtentamenClassLibrary
{
    public class EmployeeService
    {
        public EmployeeService()
        {

            Employees = new List<Employee>();

        }
        private List<Employee> Employees { get; set; }

        public void Add(Employee employee)
        {
            if (employee is null)
            {
                throw new ArgumentNullException();
            }

            Employees.Add(employee);
        }
        public void AddRange(List<Employee> employees)
        {
            foreach (var e in employees)
            {
                if (e is null)
                {
                    throw new ArgumentNullException();
                }
            }
            Employees.AddRange(employees);
        }
        public void Remove(Employee e)
        {
            if (e is null)
            {
                throw new ArgumentNullException();
            }
            Employees.Remove(e);
        }
        public Employee GetEmployeeByEmpNo(int empNo)
        {
            foreach (var e in Employees)
            {
                if (e.EmpNo == empNo)
                {
                    return e;
                }
            }
            return null;
        }
        public Employee GetEmployeeByName(string name)
        {
            foreach (var e in Employees)
            {
                if (e.Name == name)
                {
                    return e;
                }
            }
            return null;
        }
        public int EmployeeCount()
        {
            return Employees.Count();
        }
        public void PopulateSamples()
        {
            //TODO 
            string path = "..\\..\\..\\SystemtestningtentamenClassLibrary\\sampleemployees.txt";
            string doc = File.ReadAllText(path);

            var employees = doc.Split(';');

            foreach (var e in employees)
            {
                var properties = e.Split(',');
                List<string> hobbies = properties[8].Split('-').ToList();

                var sample = new Employee()
                {
                    EmpNo = int.Parse(properties[0].Trim()),
                    Name = properties[1],
                    Address = properties[2],
                    City = properties[3],
                    State = properties[4],
                    DateOfBirth = DateTime.Parse(properties[5]),
                    Age = int.Parse(properties[6]),
                    Gender = properties[7],
                    Hobbies = hobbies
                };
                Add(sample);
            }
        }
        public void RemoveSamples()
        {
            //TODO
            var samples = GetEmployeesByName("Sample");
            foreach (var s in samples)
            {
                Remove(s);
            }
        }
        //ForRemovingSamples
        private List<Employee> GetEmployeesByName(string name)
        {
            var matchingEmployees = new List<Employee>();

            foreach (var e in Employees)
            {
                if (e.Name == name)
                {
                    matchingEmployees.Add(e);
                }
            }

            return matchingEmployees;
        }


    }
    
}
