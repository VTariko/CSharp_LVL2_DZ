using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
using WpExperience.Company;

namespace WpExperience
{
	/// <summary>
	/// Interaction logic for EditEmployee.xaml
	/// </summary>
	public partial class EditEmployee : Window
	{
		private ObservableCollection<Employee> _company;
		private Employee _employee;

		/// <summary>
		/// Конструктор нового окна
		/// </summary>
		/// <param name="company"></param>
		/// <param name="employee"></param>
		public EditEmployee(ObservableCollection<Employee> company, Employee employee = null)
		{
			_company = company;
			_employee = employee;
			InitializeComponent();
			// Вызываем инициализацию формы
			InitForm();
		}

		/// <summary>
		/// Инициализация формы
		/// </summary>
		private void InitForm()
		{
			// Выбираем из всей структуры компании все имена департаментов и заполняем этим списком жэлемент выбора департамента
			List<string> depList = _company.Select(d => d.DepartmentName).Distinct().ToList();
			boxDepartments.ItemsSource = depList;
			// Если был передан работник (то есть форма открыта в режиме редактирвоания) - заполняем поля его данными
			if (_employee != null)
			{
				txtFirstName.Text = _employee.FirstName;
				txtLastName.Text = _employee.LastName;
				txtAge.Text = _employee.Age.ToString();
				txtSalary.Text = _employee.Salary.ToString();
				//Определяем департамент работника и делаем его выбранным по умолчанию
				boxDepartments.SelectedItem = _employee.DepartmentName;
			}
		}

		/// <summary>
		/// Проверка вводимого значения, чтобы в полях возраста и зарплаты можно было вводить только цифры
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void CheckValidOnNumber(object sender, TextCompositionEventArgs e)
		{
			Regex reg = new Regex("[^0-9]+");
			e.Handled = reg.IsMatch(e.Text);
		}

		/// <summary>
		/// Обработка нажатия кнопки завершеняи работы с формой
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void BtnOk_OnClick(object sender, RoutedEventArgs e)
		{
			// Сразу определяем выбранный департамент
			string depSelected = (string)boxDepartments.SelectedItem;

			// ЕСли был передан сотрудник:
			if (_employee != null)
			{
				_employee.FirstName = txtFirstName.Text;
				_employee.LastName = txtLastName.Text;
				_employee.Age = Convert.ToInt32(txtAge.Text);
				_employee.Salary = Convert.ToDouble(txtSalary.Text);
				_employee.DepartmentName = depSelected;
			}
			//Если создавали нового:
			else
			{
				if (!string.IsNullOrWhiteSpace(txtFirstName.Text) && !string.IsNullOrWhiteSpace(txtLastName.Text) &&
					!string.IsNullOrWhiteSpace(txtAge.Text) && !string.IsNullOrWhiteSpace(txtSalary.Text))
				{
					string name = txtFirstName.Text;
					string family = txtLastName.Text;
					int age;
					double salary;
					// Создаем работника с параметрами, взятыми из полей ввода и ждобавляем его в выбранный департамент
					Employee emp = new Employee(name, family, int.TryParse(txtAge.Text, out age) ? age : 18,
						double.TryParse(txtSalary.Text, out salary) ? salary : 50);
					emp.DepartmentName = depSelected;

					_company.Add(emp);
					
				}
				else
				{
					// TODO: Проработать создание нового департамента
				}

			}
			this.Close();
		}

		/// <summary>
		/// Переопределяем метод закрытия, чтобы всегда открывать базовое окно на редкатирование:
		/// </summary>
		/// <param name="e"></param>
		protected override void OnClosed(EventArgs e)
		{
			this.Owner.IsEnabled = true;
			base.OnClosed(e);
		}
	}
}
