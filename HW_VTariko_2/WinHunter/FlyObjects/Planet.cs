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
		public Planet(Point pos, Point dir, Size size, Image image) : base(pos, dir, size, image)
		{
		}

		/// <summary>
		/// Описание обновления звезды
		/// </summary>
		/// <param name="width"></param>
		/// <param name="height"></param>
		public override void Update(int width, int height)
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
