using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
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
using WpfExp.Company;

namespace WpfExp
{
	//Домашняя работа C#-2
	//Выполнил: Вячеслав Тарико (VTariko)
	//
	// 1. Создать сущности Employee и Department и заполните списки сущностей начальными
	// данными.
	// 2. Для списка сотрудников и списка департаментов предусмотреть визуализацию
	// (отображение). Это можно сделать, например, с использованием ComboBox или ListView.
	// 3. Предусмотреть возможность редактирования сотрудников и департаментов. Должна быть
	// возможность изменить департамент у сотрудника. Список департаментов для выбора,
	// можно выводить в ComboBox, это все можно выводить на дополнительной форме.
	// 4. Предусмотреть возможность создания новых сотрудников и департаментов. Реализовать
	// данную возможность либо на форме редактирования, либо сделать новую форму.


	/// <summary>
	/// Interaction logic for MainWindow.xaml
	/// </summary>
	public partial class MainWindow : Window
	{
		private List<Department> company;

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
			company = new List<Department>();

			Employee[] employees =
			{
				new Employee("A1", "A2", 50, 600),
				new Employee("B1", "B2", 22, 400),
				new Employee("C1", "C2", 31, 700)
			};
			Department dep = new Department("Трудяги", employees);
			company.Add(dep);

			employees = new[]
			{
				new Employee("D1", "D2", 18, 100),
				new Employee("E1", "E2", 19, 150),
				new Employee("F1", "F2", 20, 200),
				new Employee("G1", "G2", 19, 100)
			};
			dep = new Department("Стажеры", employees);
			company.Add(dep);

			employees = new[]
			{
				new Employee("Ирина", "Семенова", 50, 1000),
				new Employee("Галина", "Важнецкая", 55, 1400)
			};
			dep = new Department("Бухгалтерия", employees);
			company.Add(dep);

			treeCompany.ItemsSource = company;
		}

		private void BtnCreate_OnClick(object sender, RoutedEventArgs e)
		{
			CreateEditWindow();
		}


		private void BtnEdit_OnClick(object sender, RoutedEventArgs e)
		{
			Employee item = treeCompany.SelectedItem as Employee;
			if (item != null)
			{
				CreateEditWindow(item);
			}
		}


		private void CreateEditWindow(Employee employee = null)
		{
			EditEmployee window = employee == null
				? new EditEmployee(company)
				: new EditEmployee(company, employee);
			window.Owner = this;
			window.Show();
			this.IsEnabled = false;
		}
	}

}
