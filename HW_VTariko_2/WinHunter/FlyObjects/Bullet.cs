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
		#region Поля

		private readonly Hero _hero;

		#endregion

		#region Конструктор

		/// <summary>
		/// Конструктор объекта - Пуля
		/// </summary>
		/// <param name="pos">Координата пули</param>
		/// <param name="dir">Направление движения пули</param>
		/// <param name="size">Размер пули</param>
		/// <param name="width">Ширина поля для пули</param>
		/// <param name="height">Высота поля для пули</param>
		/// <param name="hero">Герой, стреляющий пулей</param>
		public Bullet(Point pos, Point dir, Size size, int width, int height, Hero hero) : base(pos, dir, size, width, height)
		{
			_hero = hero;
			image = new Bitmap(WinHunter.Properties.Resources.bullet, size);
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
				Respawn();
			}
		}

		/// <summary>
		/// Метод пересоздания пули.
		/// Координаты новой пули будут зависеть от координаты корабля.
		/// </summary>
		public void Respawn()
		{
			int newX = _hero.Position.X + _hero.Size.Width;
			int newY = _hero.Position.Y + _hero.Size.Height / 2 - size.Height / 2;
			pos.X = newX;
			pos.Y = newY;
		}

		#endregion

	}
}
