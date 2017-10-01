using System;
using System.Drawing;

namespace WinHunter.FlyObjects
{
	class Asteroid: BaseObject
	{
		#region Поля

		private readonly Image[] _images = { WinHunter.Properties.Resources.asteroid_1, WinHunter.Properties.Resources.asteroid_2 };
		private static readonly Random Rand = new Random();

		/// <summary>
		/// Максимально возможный размер астероида
		/// </summary>
		private static readonly int MAX_SIZE = 60;

		/// <summary>
		/// Минимально возможный размер астероида
		/// </summary>
		private static readonly int MIN_SIZE = 25;

		#endregion

		#region Свойства

		/// <summary>
		/// Мощность астероида, зависит напрямую от его размера
		/// Можность же и является количеством очков, которые присуждаются за уничтожение данного астероида.
		/// </summary>
		public int Power => (size.Width - MIN_SIZE) * 10 / (MAX_SIZE - MIN_SIZE);

		#endregion

		#region Конструктор

		/// <summary>
		/// Конструктор объекта - Астероид
		/// </summary>
		/// <param name="pos">Координата астероида</param>
		/// <param name="dir">Направление движения астероида</param>
		/// <param name="width">Ширина поля для астероида</param>
		/// <param name="height">Высота поля для астероида</param>
		public Asteroid(Point pos, Point dir, int width, int height) : base(pos, dir, new Size(MIN_SIZE, MIN_SIZE), width, height)
		{
			int s = Rand.Next(MIN_SIZE, MAX_SIZE+1);
			size = new Size(s, s);
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
				Respawn();	
			}
		}

		/// <summary>
		/// Метод пересоздания астероида - на случай ухода за поле или "поражения" пулей
		/// </summary>
		public void Respawn()
		{
			// Задаем новые координаты для создаваемого астероида:
			// Икс - за границей поля
			// Игрек - Случайное число от нуля но максимальной высоты поля
			pos.X = width + size.Width;
			pos.Y = (int)(Rand.NextDouble() * height);
			// Сгенерируем новый размер дял астероида
			int s = Rand.Next(MIN_SIZE, MAX_SIZE);

			size = new Size(s, s);
			// Сгенерируем число для случайного выбора изображения астероида из массива
			int num = Rand.Next(0, 2);
			image = new Bitmap(_images[num], size);
		}

		#endregion
	}
}
