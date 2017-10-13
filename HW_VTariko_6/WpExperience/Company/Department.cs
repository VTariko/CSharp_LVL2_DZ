using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using WpExperience.Annotations;

namespace WpExperience.Company
{
	public class Department: INotifyPropertyChanged
	{
		#region Свойства

		private List<Employee> _employees;

		#endregion

		#region Свойства

		/// <summary>
		/// Список входящих сотрудников
		/// </summary>
		public List<Employee> Employees
		{
			get { return _employees; }
			set
			{
				if (_employees != value)
				{
					_employees = value;
					OnPropertyChanged("Employees");
				}
			}
		}

		/// <summary>
		/// Название департамента
		/// </summary>
		public string DepartmentName { get; }

		#endregion

		#region Конструктор

		/// <summary>
		/// Базовый конструктор
		/// </summary>
		/// <param name="departmentName">Имя департамента</param>
		/// <param name="employees">Список сотрудников</param>
		public Department(string departmentName, params Employee[] employees)
		{
			DepartmentName = departmentName;
			foreach (Employee employee in employees)
			{
				employee.DepartmentName = departmentName;
			}
			Employees = new List<Employee>(employees);
		}

		#endregion

		public event PropertyChangedEventHandler PropertyChanged;
		
		protected virtual void OnPropertyChanged(string propertyName = null)
		{
			PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
		}
	}
}
