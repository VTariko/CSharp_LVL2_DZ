using System.Drawing;
using System.Windows.Forms;
using WinHunter.FlyObjects;

namespace WinHunter.Area
{
	abstract class Field
	{
		#region Поля

		protected static BufferedGraphicsContext context;
		protected static BaseObject[] objs;
		protected static Timer timer;
		protected static Form form;

		#endregion

		#region Свойства

		/// <summary>
		/// Графический буфер
		/// </summary>
		public static BufferedGraphics Buffer { get; protected set; }

		/// <summary>
		/// Ширина формы
		/// </summary>
		public static int Width { get; set; }

		/// <summary>
		/// Высота формы
		/// </summary>
		public static int Height { get; set; }

		#endregion


		/// <summary>
		/// Обновление поля
		/// </summary>
		public static void Update()
		{
			foreach (BaseObject o in objs)
			{
				o.Update(SplashScreen.Width, SplashScreen.Height);
			}
		}
	}
}
