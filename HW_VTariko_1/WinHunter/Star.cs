using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WinHunter
{
	class Star: BaseObject
	{
		#region Конструктор

		/// <summary>
		/// Объект - звезда
		/// </summary>
		/// <param name="pos">Координата звезды</param>
		/// <param name="dir">Направление движения звезды</param>
		/// <param name="size">Размер звезды</param>
		public Star(Point pos, Point dir, Size size) : base(pos, dir, size)
		{
			image = new Bitmap(WinHunter.Properties.Resources.star_white, size);
		} 

		#endregion

		#region Методы

		/// <summary>
		/// Описание обновления звезды
		/// </summary>
		/// <param name="width"></param>
		/// <param name="height"></param>
		public override void Update(int width, int height)
		{
			pos.X -= dir.X;
			if (pos.X < 0)
				pos.X = width + size.Width;
		} 

		#endregion
	}
}
