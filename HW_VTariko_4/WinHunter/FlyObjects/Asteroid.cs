using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using WinHunter.Area;
using WinHunter.Common;

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
		private const int MAX_SIZE = 60;

		/// <summary>
		/// Минимально возможный размер астероида
		/// </summary>
		private const int MIN_SIZE = 25;

		/// <summary>
		/// Стартовый размер волны астероидов
		/// </summary>
		private const int WAVE_START_SIZE = 15;

		/// <summary>
		/// Прирост количества астероидов с каждой волной
		/// </summary>
		private const int WAVE_INCREMENT = 3;

		#endregion

		#region Свойства

		/// <summary>
		/// Мощность астероида, зависит напрямую от его размера
		/// Можность же и является количеством очков, которые присуждаются за уничтожение данного астероида.
		/// </summary>
		public int Power => (size.Width - MIN_SIZE) * 10 / (MAX_SIZE - MIN_SIZE);

		/// <summary>
		/// Флаг, показывающий, что астероид не сбит
		/// </summary>
		public bool IsActive { get; private set; }

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
			IsActive = true;
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

		/// <summary>
		/// Создание волны астероидов
		/// </summary>
		/// <param name="waveNumber">Номер волны</param>
		/// <returns></returns>
		public static List<Asteroid> CreateAsteroidsWave(int waveNumber)
		{
			List<Asteroid> asteroids = new List<Asteroid>();
			int numberAsteroids = WAVE_START_SIZE + WAVE_INCREMENT * (waveNumber - 1);

			// Создаем астероиды
			for (int i = 0; i < numberAsteroids; i++)
			{
				int speed = 40;
				int posX = Rand.Next(width + 1, width + width);
				int posY = Rand.Next(1, height);

				try
				{
					asteroids.Add(new Asteroid(new Point(posX, posY), new Point(speed, 0), width, height));
				}
				catch (GameObjectException e)
				{
					MessageBox.Show(string.Format(Field.ErrorString, e.Message, e.Type));
					throw;
				}
			}

			return asteroids;
		}

		/// <summary>
		/// Метод "унижтожения" астероида
		/// </summary>
		public void Destroy()
		{
			pos.X = 0;
			pos.Y = 0;
			IsActive = false;
		}


		#endregion
	}
}
