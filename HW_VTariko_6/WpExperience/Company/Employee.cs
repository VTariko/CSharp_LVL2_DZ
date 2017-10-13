using System.ComponentModel;
using System.Runtime.CompilerServices;
using WpExperience.Annotations;

namespace WpExperience.Company
{
	/// <summary>
	/// Описание сотрудника
	/// </summary>
	public class Employee: INotifyPropertyChanged
	{
		public event PropertyChangedEventHandler PropertyChanged;

		#region Поля

		private string _firstName;
		private string _lastName;
		private int _age;
		private double _salary;
		private string _departmentName;

		#endregion

		#region Свойства

		/// <summary>
		/// Имя
		/// </summary>
		public string FirstName
		{
			get { return _firstName; }
			set
			{
				if (_firstName != value)
				{
					_firstName = value;
					OnPropertyChanged("Name");
				}
			}
		}

		/// <summary>
		/// Фамилия
		/// </summary>
		public string LastName
		{
			get { return _lastName; }
			set
			{
				if (_lastName != value)
				{
					_lastName = value;
					OnPropertyChanged("Name");
				}
			}
		}

		/// <summary>
		/// Отображаемое имя сотрудника
		/// </summary>
		public string Name => $"{FirstName} {LastName}";

		/// <summary>
		/// Возраст
		/// </summary>
		public int Age
		{
			get { return _age; }
			set
			{
				if (_age != value)
				{
					_age = value;
					OnPropertyChanged("Age");
				}
			}
		}

		/// <summary>
		/// Зарплата
		/// </summary>
		public double Salary
		{
			get { return _salary; }
			set
			{
				if (_salary != value)
				{
					_salary = value;
					OnPropertyChanged("Salary");
				}
			}
		}

		/// <summary>
		/// Название соответствующего департамента
		/// </summary>
		public string DepartmentName
		{
			get { return _departmentName; }
			set
			{
				if (_departmentName != value)
				{
					_departmentName = value;
					OnPropertyChanged("DepartmentName");
				}
			}
		}

		#endregion

		#region Конструктор

		/// <summary>
		/// Конструктор сотрудника
		/// </summary>
		/// <param name="firstName">Имя</param>
		/// <param name="lastName">Фамилия</param>
		/// <param name="age">Возраст</param>
		/// <param name="salary">Зарплата</param>
		public Employee(string firstName, string lastName, int age, double salary)
		{
			_firstName = firstName;
			_lastName = lastName;
			_age = age;
			_salary = salary;
		}



		#endregion

		
		
		protected virtual void OnPropertyChanged(string propertyName = null)
		{
			PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
		}
	}
}
