using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WinHunter.FlyObjects
{
	class Planet: BaseObject
	{
		/// <summary>
		/// Констурктор объекта - Планета
		/// </summary>
		/// <param name="pos">Координата планеты</param>
		/// <param name="dir">Направление движения планеты</param>
		/// <param name="size">Размер планеты</param>
		/// <param name="image">Изображение планеты</param>
		/// <param name="width">Ширина поля для планеты</param>
		/// <param name="height">Высота поля для планеты</param>
		public Planet(Point pos, Point dir, Size size, Image image, int width, int height) : base(pos, dir, size, image, width, height)
		{
		}

		/// <summary>
		/// Описание обновления звезды
		/// </summary>
		public override void Update()
		{
			pos.X += dir.X;
			pos.Y += dir.Y;
			if (pos.X < 0 || pos.X > width)
				dir.X = -dir.X;
			if (pos.Y < 0 || pos.Y > height)
				dir.Y = -dir.Y;
		}

		
	}
}
