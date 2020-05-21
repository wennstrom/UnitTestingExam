using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using SystemtestningtentamenClassLibrary;

namespace SystemtestningtentamenApplication
{
    public partial class EmployeeInformation : Form
    {
        private EmployeeValidator _validator;
        private EmployeeService _employeeService;
        private readonly DateTime _maxDate;
        private readonly DateTime _minDate;

        public EmployeeInformation()
        {
            InitializeComponent();


            _validator = new EmployeeValidator();
            _employeeService = new EmployeeService();

            //min age is 18
            _maxDate = DateTime.Now.AddYears(-18);
            //max age is 60
            _minDate = DateTime.Now.AddYears(-60);

            PopulateItems();
        }
        private void btnOK_Click(object sender, EventArgs e)
        {
            if (InvalidFields() == true)
            {
                MessageBox.Show("Looks like you did not enter all the fields correctly");
                return;
            }

            if (_validator.ValidateBirthdateAndAge(dtDateOfBirth.Value, (int)nudAge.Value) == false)
            {
                MessageBox.Show("The entered birthdate and age does not match!");
                return;
            }

            var employee = CreateEmployee();

            if (_employeeService.GetEmployeeByEmpNo(employee.EmpNo) != null)
            {
                MessageBox.Show("EmpNo has to be unique!");
                return;
            }

            txtDetails.Text = employee.ToString();
            _employeeService.Add(employee);

        }
        private void btnSearch_Click(object sender, EventArgs e)
        {
            int empNo = -1;
            if (string.IsNullOrEmpty(txtEmpNo.Text) && string.IsNullOrEmpty(txtEmpName.Text))
            {
                MessageBox.Show("Please enter EmpNumber and/or Name to search");
            }
            else if (string.IsNullOrEmpty(txtEmpNo.Text) == false && string.IsNullOrEmpty(txtEmpName.Text) == false)
            {
                try
                {
                    empNo = int.Parse(txtEmpNo.Text);

                    if (empNo < 1 || 9999 < empNo)
                    {
                        throw new Exception();
                    }
                }
                catch
                {
                    MessageBox.Show("EmpNo has to be entered as an integer between 0001-9999");

                }
                var employee = _employeeService.GetEmployeeByEmpNo(empNo);
                if (employee is null)
                {
                    MessageBox.Show("Could not find any employee based on your search.");
                }
                else
                {
                    txtDetails.Text = employee.ToString();
                }
            }
            else if (string.IsNullOrEmpty(txtEmpNo.Text) == false)
            {
                try
                {
                    empNo = int.Parse(txtEmpNo.Text);
                }
                catch
                {
                    MessageBox.Show("Looks like you did not enter all the fields correctly.");

                }
                var employee = _employeeService.GetEmployeeByEmpNo(empNo);
                if (employee == null)
                {
                    MessageBox.Show("Could not find any employee based on your search.");
                }
                else
                {
                    txtDetails.Text = employee.ToString();
                }

            }
            else
            {
                var employee = _employeeService.GetEmployeeByName(txtEmpName.Text);
                if (employee is null)
                {
                    MessageBox.Show("Could not find any employee based on your search.");
                }
                else
                {
                    txtDetails.Text = employee.ToString();
                }
            }
        }
        private void btnReset_Click(object sender, EventArgs e)
        {
            txtDetails.Clear();
            txtEmpNo.Text = "0000";
            txtEmpNo.Focus();
            txtEmpName.Clear();
            txtEmpAddress.Text = "";
            cmbCity.SelectedIndex = -1;
            cmbState.SelectedIndex = -1;
            dtDateOfBirth.Value = _maxDate;
            nudAge.Value = 18;
            radFemale.Checked = false;
            radMale.Checked = false;
            lstHobbies.ClearSelected();
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }


        private void PopulateItems()
        {
            //casts list of string to array of object so that we can fill the items by using addrange
            cmbState.Items.AddRange(PopulateStates().Cast<object>().ToArray());
            lstHobbies.Items.AddRange(PopulateHobbies().Cast<object>().ToArray());
            cmbCity.Items.AddRange(PopulateCities().Cast<object>().ToArray());
            dtDateOfBirth.MaxDate = _maxDate;
            dtDateOfBirth.MinDate = _minDate;
        }

        private Employee CreateEmployee()
        {
            List<string> hobbies = new List<string>();
            foreach (string h in lstHobbies.CheckedItems)
            {
                hobbies.Add(h);
            }
            //returns an object based on the userinput
            return new Employee()
            {
                EmpNo = int.Parse(txtEmpNo.Text),
                Name = txtEmpName.Text,
                Address = txtEmpAddress.Text,
                City = cmbCity.Text,
                State = cmbState.Text,
                DateOfBirth = dtDateOfBirth.Value,
                Age = (int)nudAge.Value,
                Gender = radFemale.Checked ? "Female" : "Male",
                Hobbies = hobbies,
            };
        }

        private List<string> PopulateCities()
        {
            //Returns a string list of enum type states
            //TODO
            List<string> result = new List<string>();
            foreach (var city in Enum.GetNames(typeof(Cities))) //looping through the states enum
            {
                result.Add(city.ToString());
            }
            return result;
        }
        private List<string> PopulateHobbies()
        {
            //TODO
            //Returns a string list of enum type hobbbies
            List<string> result = new List<string>();
            foreach (var hobby in Enum.GetNames(typeof(Hobbies))) //looping through the hobbies enum
            {
                result.Add(hobby.ToString());
            }
            return result;
        }
        private List<string> PopulateStates()
        {
            //TODO
            //Returns a string list of enum type states
            List<string> result = new List<string>();
            foreach (var state in Enum.GetNames(typeof(States))) //looping through the states enum
            {
                result.Add(state.ToString());
            }
            return result;
        }
        private bool InvalidFields()
        {
            try
            {
                var empNo = int.Parse(txtEmpNo.Text);


            }
            catch (Exception)
            {
                MessageBox.Show("You did not enter all the fields correctly!");
                return true;
            }
            try
            {
                var age = int.Parse(nudAge.Value.ToString());

                _validator.ValidateAge(age);
            }
            catch (Exception)
            {
                MessageBox.Show("Age has to be entered as an integer between 0001 - 9999");
                return true;
            }

            bool emptyfields = new List<string> { txtEmpNo.Text, txtEmpAddress.Text, cmbCity.Text, cmbState.Text }
                                               .All(text => string.IsNullOrEmpty(text));

            if (radFemale.Checked == false && radMale.Checked == false)
            {

                emptyfields = true;
            }

            return emptyfields;
        }


    }
}
