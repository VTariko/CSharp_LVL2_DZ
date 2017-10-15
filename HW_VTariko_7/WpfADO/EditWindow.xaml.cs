using System;
using System.Collections.Generic;
using System.Data;
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
using System.Windows.Shapes;

namespace WpfADO
{
	/// <summary>
	/// Interaction logic for EditWindow.xaml
	/// </summary>
	public partial class EditWindow : Window
	{
		public DataRow ResultRow { get; set; }
		private readonly Dictionary<string, string> _departments;

		public EditWindow(DataRow dataRow, Dictionary<string, string> deps)
		{
			InitializeComponent();
			this.btnOk.Click += (s, e) => BtnOk_OnClick(s, e);
			this.btnCancel.Click += (s, e) => BtnCancel_OnClick(s, e);
			ResultRow = dataRow;
			_departments = deps;
			InitForm();
		}

		#region Методы

		/// <summary>
		/// Инициализация формы окна
		/// </summary>
		private void InitForm()
		{
			boxDepartments.ItemsSource = _departments.Select(d => d.Value);

			string dep = ResultRow["Department"].ToString().Trim();
			if (!string.IsNullOrWhiteSpace(dep))
			{
				boxDepartments.SelectedItem = dep;
			}
			txtLastName.Text = ResultRow["LastName"].ToString();
			txtFirstName.Text = ResultRow["FirstName"].ToString();
			if (!string.IsNullOrWhiteSpace( ResultRow["BirthDate"].ToString()))
			{
				dpAge.DisplayDate = Convert.ToDateTime(ResultRow["BirthDate"]);
			}
			dpAge.Text = ResultRow["BirthDate"].ToString();
		}

		#region Обработка кнопок

		private void BtnOk_OnClick(object sender, RoutedEventArgs routedEventArgs)
		{
			Logic.EButtonOk(this, _departments);
		}

		private void BtnCancel_OnClick(object sender, RoutedEventArgs routedEventArgs)
		{
			DialogResult = false;
		}

		#endregion

		#endregion
	}
}
