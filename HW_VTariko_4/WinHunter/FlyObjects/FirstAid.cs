using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WinHunter.FlyObjects
{
	class FirstAid: BaseObject
	{
		#region Константы

		/// <summary>
		/// Максимально возможная скорость аптечки
		/// </summary>
		private const int MAX_SPEED = 30;

		/// <summary>
		/// Минимально возможная скорость аптечки
		/// </summary>
		private const int MIN_SPEED = 20;

		#endregion

		#region Поля

		private static readonly Random Rand = new Random();

		private static readonly Size SizeAid = new Size(50, 50);

		#endregion

		#region Свойства

		/// <summary>
		/// Заряд аптечки - на сколько она помогает герою
		/// </summary>
		public int Power { get; private set; }

		#endregion

		#region Конструктор

		/// <summary>
		/// Конструктор объекта - аптечка
		/// </summary>
		/// <param name="width">Ширина поля для астероида</param>
		/// <param name="height">Высота поля для астероида</param>
		public FirstAid(int width, int height) : base(new Point(0, 0), new Point(0, 0), SizeAid, width, height)
		{
			// Задаем аптечки координаты
			Respawn();
			// Задаем аптечке ее мощность
			Power = Rand.Next(5, 15);
			// Задаем случайную скорость для апеетчки в заданном диапазоне
			int speed = Rand.Next(MIN_SPEED, MAX_SPEED + 1);
			dir = new Point(speed, 0);
			// Добавляем аптечке изображение
			image = new Bitmap(Properties.Resources.first_aid_kit, SizeAid);
		}

		#endregion

		#region Методы

		/// <summary>
		/// Описание движения аптечки
		/// </summary>
		public override void Update()
		{
			pos.X -= dir.X;
		}

		/// <summary>
		/// Метод пересоздания аптечки по срабатыванию таймера
		/// </summary>
		public void Respawn()
		{
			// Задаем новые координаты для создаваемой аптечки:
			// Икс - за границей поля
			// Игрек - Случайное число от нуля но максимальной высоты поля
			pos.X = width + size.Width;
			pos.Y = Rand.Next(0, height);
			// Задаем аптечке ее мощность
			Power = Rand.Next(5, 15);
		}

		/// <summary>
		/// Метод "унижтожения" аптечки - после использования "выкидываем" её за экран
		/// </summary>
		public void Destroy()
		{
			pos.X = -100;
			pos.Y = -100;
		}

		#endregion
	}
}
