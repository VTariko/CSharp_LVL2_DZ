﻿using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WinHunter
{
	class Asteroid: BaseObject
	{
		#region Поля

		private readonly Image[] _images = { WinHunter.Properties.Resources.asteroid_1, WinHunter.Properties.Resources.asteroid_2 };

		private static readonly Random Rand = new Random();

		#endregion

		#region Конструктор

		public Asteroid(Point pos, Point dir, Size size) : base(pos, dir, size)
		{
			int num = Rand.Next(0, 2);
			image = new Bitmap(_images[num], size);
		} 

		#endregion


		#region Методы

		/// <summary>
		/// Описание движения астероида
		/// </summary>
		/// <param name="width"></param>
		/// <param name="height"></param>
		public override void Update(int width, int height)
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

		#endregion
	}
}
