using System;
using System.Windows.Forms;
using WinHunter.Area;

namespace WinHunter
{
	static class Program
	{
		static void Main()
		{
			Form form = new Form
			{
				Width = 800,
				Height = 600,
				Text = "Win Hunter"
			};
			try
			{
				SplashScreen.Init(form);
			}
			catch (ArgumentOutOfRangeException ex)
			{
				MessageBox.Show(ex.Message);
				return;
			}
			form.Show();
			Application.Run(form);
		}
	}
}
