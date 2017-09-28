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
		/// Конструктор объекта - Пуля
		/// </summary>
		/// <param name="pos">Координата пули</param>
		/// <param name="dir">Направление движения пули</param>
		/// <param name="size">Размер пули</param>
		/// <param name="width">Ширина поля для пули</param>
		/// <param name="height">Высота поля для пули</param>
		public Bullet(Point pos, Point dir, Size size, int width, int height) : base(pos, dir, size, width, height)
		{
			image = new Bitmap(WinHunter.Properties.Resources.bullet, size);
		}

		#endregion

		#region Методы

		/// <summary>
		/// Описание обновления движения пули
		/// </summary>
		public override void Update()
		{
			pos.X += 3;
		}

		#endregion

	}
}
