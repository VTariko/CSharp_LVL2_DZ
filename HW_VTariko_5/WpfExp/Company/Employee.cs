namespace WpfExp.Company
{
	/// <summary>
	/// Описание сотрудника
	/// </summary>
	public class Employee
	{
		#region Свойства

		/// <summary>
		/// Имя
		/// </summary>
		public string FirstName { get; set; }

		/// <summary>
		/// Фамилия
		/// </summary>
		public string LastName { get; set; }
		
		/// <summary>
		/// Отображаемое имя сотрудника
		/// </summary>
		public string Name => $"{FirstName} {LastName}";

		/// <summary>
		/// Возраст
		/// </summary>
		public int Age { get; set; }

		/// <summary>
		/// Зарплата
		/// </summary>
		public double Salary { get; set; } 

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
			this.FirstName = firstName;
			this.LastName = lastName;
			Age = age;
			Salary = salary;
		}

		#endregion
	}
}
