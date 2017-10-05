using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using WinHunter.Area;
using WinHunter.Common;

namespace WinHunter.FlyObjects
{
	class Star: BaseObject
	{
		#region Поля

		private static readonly Random Rand = new Random();

		/// <summary>
		/// Количество создаваемых звёзд
		/// </summary>
		private const int STARS_SIZE = 100;

		#endregion

		#region Конструктор

		/// <summary>
		/// Конструктор объекта - Звезда
		/// </summary>
		/// <param name="pos">Координата звезды</param>
		/// <param name="dir">Направление движения звезды</param>
		/// <param name="size">Размер звезды</param>
		/// <param name="width">Ширина поля для звезды</param>
		/// <param name="height">Высота поля для звезды</param>
		public Star(Point pos, Point dir, Size size, int width, int height) : base(pos, dir, size, width, height)
		{
			image = new Bitmap(WinHunter.Properties.Resources.star_white, size);
		}

		#endregion

		#region Методы

		/// <summary>
		/// Описание обновления звезды
		/// </summary>
		public override void Update()
		{
			pos.X -= dir.X;
			if (pos.X < 0)
				pos.X = width + size.Width;
		}

		/// <summary>
		/// Создание звёзд для фона
		/// </summary>
		/// <returns></returns>
		public static List<Star> CreateStars()
		{
			List<Star> stars = new List<Star>();

			for (int i = 0; i < STARS_SIZE; i++)
			{
				int speed = Rand.Next(100, 200);
				int size = 1 + (int)(0.04 * speed);
				int posX = Rand.Next(1, width);
				int posY = Rand.Next(1, height);

				try
				{
					stars.Add(new Star(new Point(posX, posY), new Point(speed, 0), new Size(size, size), width, height));
				}
				catch (GameObjectException e)
				{
					MessageBox.Show(string.Format(Field.ErrorString, e.Message, e.Type));
					throw;
				}
			}

			return stars;
		}
		#endregion
		
	}
}
