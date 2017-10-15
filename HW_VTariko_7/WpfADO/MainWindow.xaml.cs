using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
using System.Windows.Navigation;
using System.Windows.Shapes;
using WpfADO.SqlConnect;

namespace WpfADO
{
	/// <summary>
	/// Interaction logic for MainWindow.xaml
	/// </summary>
	public partial class MainWindow : Window
	{
		SqlActivity activity = new SqlActivity();

		public MainWindow()
		{
			InitializeComponent();
			this.Loaded += (s, e) => MainWindow_OnLoaded(s, e);
			this.Closed += (s, e) => MainWindow_OnClosed(s, e);
			this.btnAdd.Click += (s, e) => BtnAdd_Click(s, e);
			this.btnEdit.Click += (s, e) => BtnEdit_Click(s, e);
			this.btnDel.Click += (s, e) => BtnDel_Click(s, e);
		}

		#region Обработка кнопок

		private void BtnAdd_Click(object sender, RoutedEventArgs e)
		{
			Logic.MButtonAdd(activity);
		}

		private void BtnEdit_Click(object sender, RoutedEventArgs e)
		{
			object select = empGrid.SelectedItem;
			if (select != null)
			{
				Logic.MButtonEdit(activity, select);
			}
		}

		private void BtnDel_Click(object sender, RoutedEventArgs e)
		{
			object select = empGrid.SelectedItem;
			if (select != null)
			{
				Logic.MButtonDel(activity, select);
			}
		}

		private void MainWindow_OnLoaded(object sender, RoutedEventArgs e)
		{
			empGrid.DataContext = activity.Table.DefaultView;
		}

		private void MainWindow_OnClosed(object sender, EventArgs e)
		{
			activity.ConnectionClose();
		} 
		#endregion
	}
}
