using System;
using System.Drawing;

namespace WinHunter.FlyObjects
{
	class Asteroid: BaseObject
	{
		#region Поля

		private readonly Image[] _images = { WinHunter.Properties.Resources.asteroid_1, WinHunter.Properties.Resources.asteroid_2 };
		private static readonly Random Rand = new Random();

		#endregion

		#region Конструктор

		/// <summary>
		/// Конструктор объекта - Астероид
		/// </summary>
		/// <param name="pos"></param>
		/// <param name="dir"></param>
		/// <param name="size"></param>
		/// <param name="width"></param>
		/// <param name="height"></param>
		public Asteroid(Point pos, Point dir, Size size, int width, int height) : base(pos, dir, size, width, height)
		{
			int num = Rand.Next(0, 2);
			image = new Bitmap(_images[num], size);
		} 

		#endregion

		#region Методы

		/// <summary>
		/// Описание движения астероида
		/// </summary>
		public override void Update()
		{
			pos.X -= dir.X;
			if (pos.X < 0)
			{
				pos.X = width + size.Width;
				int s = (1 + Rand.Next(5)) * 10;
				int posY = (int)(Rand.NextDouble() * height);
				pos.Y = posY;
				int num = Rand.Next(0, 2);
				size = new Size(s, s);
				image = new Bitmap(_images[num], size);
			}
		}

		public void Respawn()
		{
			
		}

		#endregion
	}
}
