using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SystemtestningtentamenClassLibrary
{
    public class Employee
    {
        public Employee() { }
        public int EmpNo { get; set; }
        public string Name { get; set; }
        public string Address { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public DateTime DateOfBirth { get; set; }
        public int Age { get; set; }
        public string Gender { get; set; }
        public List<string> Hobbies { get; set; }

        public override string ToString()
        {
            //TODO Överskidinformationen 
            var empInfo = new StringBuilder();

            empInfo.AppendLine($" EmpNo:  {EmpNo.ToString("D4")}");
            empInfo.AppendLine($" Name:  {Name}");
            empInfo.AppendLine($" Address:  {Address}");
            empInfo.AppendLine($" City:  {City}");
            empInfo.AppendLine($" State:  {State}");
            empInfo.AppendLine($" Date Of Birth:  {DateOfBirth.ToString("yyyy/MM/dd")}");
            empInfo.AppendLine($" Age:  {Age}");
            empInfo.AppendLine($" Gender:  {Gender}");
            empInfo.Append($"\n Hobbies:  ");
            foreach (var h in Hobbies)
            {
                empInfo.Append($"{h} ");
            }
            return empInfo.ToString();
        }
    }
}
