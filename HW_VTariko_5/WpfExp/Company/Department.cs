using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;

namespace WpfExp.Company
{
	public class Department
	{
		#region Свойства

		/// <summary>
		/// Список входящих сотрудников
		/// </summary>
		public ObservableCollection<Employee> Employees { get; set; }

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
			Employees = new ObservableCollection<Employee>(employees);
		} 

		#endregion
	}
}
