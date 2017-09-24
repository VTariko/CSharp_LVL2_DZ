using System;
using System.Windows.Forms;

namespace WinHunter
{
	static class Program
	{
		static void Main()
		{
			Form form = new Form
			{
				Width = 800,
				Height = 600
			};
			SplashScreen.Init(form);
			form.Show();
			Application.Run(form);
		}
	}
}
