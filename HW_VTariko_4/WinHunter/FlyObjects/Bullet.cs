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
		#region Константы

		private static readonly Point BASIC_SPEED = new Point(50, 50);
		private static readonly Size BASIC_SIZE = new Size(20, 5);

		#endregion

		#region Поля

		private readonly Ship _hero;

		#endregion

		#region Свойства

		public bool IsActive { get; private set; }

		#endregion

		#region Конструктор

		/// <summary>
		/// Конструктор объекта - Пуля
		/// За скорость принимается базовая константа.
		/// За размер берется базовый размер пули
		/// Позиция определяется исходя и координат корабля.
		/// </summary>
		/// <param name="width">Ширина поля для пули</param>
		/// <param name="height">Высота поля для пули</param>
		/// <param name="hero">Герой, стреляющий пулей</param>
		public Bullet(int width, int height, Ship hero) : this(BASIC_SPEED, BASIC_SIZE, width, height, hero)
		{
		}

		/// <summary>
		/// Конструктор объекта - Пуля
		/// За скорость принимается базовая константа.
		/// Позиция определяется исходя и координат корабля.
		/// </summary>
		/// <param name="size">Размер пули</param>
		/// <param name="width">Ширина поля для пули</param>
		/// <param name="height">Высота поля для пули</param>
		/// <param name="hero">Герой, стреляющий пулей</param>
		public Bullet(Size size, int width, int height, Ship hero) : this(BASIC_SPEED, size, width, height, hero)
		{
		}


		/// <summary>
		/// Конструктор объекта - Пуля
		/// Позиция определяется исходя и координат корабля.
		/// </summary>
		/// <param name="dir">Направление движения пули</param>
		/// <param name="size">Размер пули</param>
		/// <param name="width">Ширина поля для пули</param>
		/// <param name="height">Высота поля для пули</param>
		/// <param name="hero">Герой, стреляющий пулей</param>
		public Bullet(Point dir, Size size, int width, int height, Ship hero) : base(new Point(0, 0), dir, size, width, height)
		{
			_hero = hero;
			pos.X = _hero.Position.X + _hero.Size.Width;
			pos.Y = _hero.Position.Y + _hero.Size.Height / 2 - size.Height / 2;
			image = new Bitmap(WinHunter.Properties.Resources.bullet, size);
			IsActive = true;
		}

		#endregion

		#region Методы

		/// <summary>
		/// Описание обновления движения пули
		/// </summary>
		public override void Update()
		{
			pos.X += dir.X;

			if (pos.X > width)
			{
				Destroy();
			}
		}

		/// <summary>
		/// Метод пересоздания пули.
		/// Координаты новой пули будут зависеть от координаты корабля.
		/// </summary>
		public void Respawn()
		{
			pos.X = _hero.Position.X + _hero.Size.Width; ;
			pos.Y = _hero.Position.Y + _hero.Size.Height / 2 - size.Height / 2;
			IsActive = true;
		}

		/// <summary>
		/// Метод "унижтожения" пули
		/// </summary>
		public void Destroy()
		{
			IsActive = false;
		}

		#endregion

	}
}
