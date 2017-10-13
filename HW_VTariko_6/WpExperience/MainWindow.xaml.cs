using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Globalization;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using WpExperience.Annotations;
using WpExperience.Company;

namespace WpExperience
{
	/// <summary>
	/// Interaction logic for MainWindow.xaml
	/// </summary>
	public partial class MainWindow : Window
	{
		private ObservableCollection<Department> _company;
		private ObservableCollection<Employee> _employees;

		public MainWindow()
		{
			InitializeComponent();
			CreateCompany();
		}


		/// <summary>
		/// Метод "заполнения" компании сотрудниками и отделами
		/// </summary>
		private void CreateCompany()
		{
			_company = new ObservableCollection<Department>();

			Employee[] employees =
			{
				new Employee("A1", "A2", 50, 600),
				new Employee("B1", "B2", 22, 400),
				new Employee("C1", "C2", 31, 700)
			};
			Department dep = new Department("Трудяги", employees);
			_company.Add(dep);
			

			employees = new[]
			{
				new Employee("D1", "D2", 18, 100),
				new Employee("E1", "E2", 19, 150),
				new Employee("F1", "F2", 20, 200),
				new Employee("G1", "G2", 19, 100)
			};
			dep = new Department("Стажеры", employees);
			_company.Add(dep);

			employees = new[]
			{
				new Employee("Ирина", "Семенова", 50, 1000),
				new Employee("Галина", "Важнецкая", 55, 1400)
			};
			dep = new Department("Бухгалтерия", employees);
			_company.Add(dep);
			_employees = new ObservableCollection<Employee>();
			foreach (Department department in _company)
			{
				foreach (Employee employee in department.Employees)
				{
					_employees.Add(employee);
				}
			}

			lbUsers.ItemsSource = _employees;
		}


		private void BtnAddUser_OnClick(object sender, RoutedEventArgs e)
		{
			CreateEditWindow();
		}

		private void BtnChangeUser_OnClick(object sender, RoutedEventArgs e)
		{
			Employee emp = lbUsers.SelectedItem as Employee;
			if (emp != null)
			{
				CreateEditWindow(emp);
			}
		}

		private void BtnDelUser_OnClick(object sender, RoutedEventArgs e)
		{
			Employee emp = lbUsers.SelectedItem as Employee;
			if (emp != null)
			{
				_company.First(d => d.Employees.Contains(emp)).Employees.Remove(emp);
			}
		}

		/// <summary>
		/// Метод вызова нового окна с редактированием/созданием работников
		/// </summary>
		/// <param name="employee"></param>
		private void CreateEditWindow(Employee employee = null)
		{
			// Если в метод передан работник - отправляем его в конструктор нового окна
			EditEmployee window = employee == null
				? new EditEmployee(_employees)
				: new EditEmployee(_employees, employee);
			window.Owner = this;
			window.Show();
			// текущая форма будет неактивна, пока идет создание/редактирование работника
			this.IsEnabled = false;
		}
	}
	
}
