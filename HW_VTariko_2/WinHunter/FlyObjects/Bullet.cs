using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WinHunter.FlyObjects
{
	class Bullet: BaseObject
	{
		#region Конструктор

		/// <summary>
		/// Объект - пуля
		/// </summary>
		/// <param name="pos">координаты пули</param>
		/// <param name="dir">Напраление движения пули</param>
		/// <param name="size">Размер пули</param>
		public Bullet(Point pos, Point dir, Size size) : base(pos, dir, size)
		{
			image = new Bitmap(WinHunter.Properties.Resources.bullet, size);
		}

		#endregion

		#region Методы

		/// <summary>
		/// Описание обновления движения пули
		/// </summary>
		/// <param name="width">Ширины поля </param>
		/// <param name="height">Высота поля</param>
		public override void Update(int width, int height)
		{
			pos.X += 3;
		} 

		#endregion
	}
}
