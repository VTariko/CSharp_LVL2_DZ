using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using WinHunter.FlyObjects;

namespace WinHunter.Area
{
	abstract class Field
	{
		#region Поля

		protected const string ErrorString = "{0}\r\nТип ошибки: {1}";
		protected static BufferedGraphicsContext context;
		protected static List<BaseObject> objs;
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

		#region Методы

		protected static void CheckFormSize()
		{
			if (form.Width > 1000 || form.Height > 1000 || form.Width < 0 || form.Height < 0)
			{
				throw new ArgumentOutOfRangeException(string.Format("{0}", "Недопустимые размеры для формы"));
			}
		}


		#endregion

	}
}
