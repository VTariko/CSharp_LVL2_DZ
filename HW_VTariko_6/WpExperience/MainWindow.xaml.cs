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

namespace WpExperience
{
	/// <summary>
	/// Interaction logic for MainWindow.xaml
	/// </summary>
	public partial class MainWindow : Window
	{
		private ObservableCollection<User> users = new ObservableCollection<User>();

		public MainWindow()
		{
			InitializeComponent();
			users.Add(new User {Name = "Пётр"});
			users.Add(new User {Name = "Константин"});
			lbUsers.ItemsSource = users;
		}

		private void BtnAddUser_OnClick(object sender, RoutedEventArgs e)
		{
			users.Add(new User {Name = "Вася"});
		}

		private void BtnChangeUser_OnClick(object sender, RoutedEventArgs e)
		{
			User user = lbUsers.SelectedItem as User;
			if (user != null)
			{
				user.Name += user.Name;
			}
		}

		private void BtnDelUser_OnClick(object sender, RoutedEventArgs e)
		{
			User user = lbUsers.SelectedItem as User;
			if (user != null)
			{
				users.Remove(user);
			};
		}
	}

	public class User : INotifyPropertyChanged
	{
		public event PropertyChangedEventHandler PropertyChanged;

		private string _name;

		public string Name
		{
			get { return this._name;}
			set
			{
				if (_name != value)
				{
					_name = value;
					OnPropertyChanged("Name");
				}
			}
		}
		
		
		protected virtual void OnPropertyChanged(string propertyName = null)
		{
			PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
		}
	}
}
