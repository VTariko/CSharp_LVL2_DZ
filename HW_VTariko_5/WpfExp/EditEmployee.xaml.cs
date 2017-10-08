using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using WpfExp.Company;

namespace WpfExp
{
	/// <summary>
	/// Interaction logic for EditEmployee.xaml
	/// </summary>
	public partial class EditEmployee : Window
	{
		private List<Department> _company;
		private Employee _employee;

		public EditEmployee(List<Department> company, Employee employee = null)
		{
			_company = company;
			_employee = employee;
			InitializeComponent();
			InitForm();
		}

		private void InitForm()
		{
			List<string> depList = _company.Select(d => d.DepartmentName).ToList();
			boxDepartments.ItemsSource = depList;
			if (_employee != null)
			{
				txtFirstName.Text = _employee.FirstName;
				txtLastName.Text = _employee.LastName;
				txtAge.Text = _employee.Age.ToString();
				txtSalary.Text = _employee.Salary.ToString();
				txtFirstName.IsEnabled = false;
				txtLastName.IsEnabled = false;
				txtAge.IsEnabled = false;
				txtSalary.IsEnabled = false;
				Department dep = _company.FirstOrDefault(d => d.Employees.Contains(_employee));
				if (dep != null)
				{
					boxDepartments.SelectedItem = dep.DepartmentName;
				}
			}
		}

		private void CheckValidOnNumber(object sender, TextCompositionEventArgs e)
		{
			Regex reg = new Regex("[^0-9]+");
			e.Handled = reg.IsMatch(e.Text);
		}

		private void BtnOk_OnClick(object sender, RoutedEventArgs e)
		{
			string depSelected = (string)boxDepartments.SelectedItem;

			if (_employee != null)
			{
				Department dep = _company.First(d => d.Employees.Contains(_employee));
				if (depSelected != dep.DepartmentName)
				{
					dep.Employees.Remove(_employee);
					_company.First(c => c.DepartmentName == depSelected).Employees.Add(_employee);
				}
			}
			else
			{
				if (!string.IsNullOrWhiteSpace(txtFirstName.Text) && !string.IsNullOrWhiteSpace(txtLastName.Text) &&
				    !string.IsNullOrWhiteSpace(txtAge.Text) && !string.IsNullOrWhiteSpace(txtSalary.Text))
				{
					string name = txtFirstName.Text;
					string family = txtLastName.Text;

					Employee emp = new Employee(name, family, int.TryParse(txtAge.Text, out int age) ? age : 18,
						double.TryParse(txtSalary.Text, out double salary) ? salary : 50);
					
					_company.First(c => c.DepartmentName == depSelected).Employees.Add(_employee);
				}
				else
				{
					// TODO: Проработать создание нового департамента
				}
				
			}
			this.Close();
		}

		protected override void OnClosed(EventArgs e)
		{
			this.Owner.IsEnabled = true;
			base.OnClosed(e);
		}
	}
}
