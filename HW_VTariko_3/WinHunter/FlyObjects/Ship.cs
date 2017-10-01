using System.Drawing;

namespace WinHunter.FlyObjects
{
	class Ship: BaseObject
	{
		#region События

		/// <summary>
		/// Событие гибели героя
		/// </summary>
		public static event Message MessageDie;

		#endregion

		#region Свойства

		/// <summary>
		/// Позиция героя на поле
		/// Вынуждены сделать публичной для корректной генерации пуль, вылетающих ИЗ корабля
		/// </summary>
		public Point Position => pos;

		/// <summary>
		/// Размер героя
		/// Вынуждены сделать публичной для корректной генерации пуль, вылетающих ИЗ корабля
		/// </summary>
		public Size Size => size;

		/// <summary>
		/// Запас энергии героя
		/// </summary>
		public int Energy { get; private set; } = 100;

		public int Score { get; private set; }

		#endregion

		#region Конструктор

		/// <summary>
		/// Конструктор объекта - герой
		/// </summary>
		/// <param name="pos">Координата героя</param>
		/// <param name="size">Размер героя</param>
		/// <param name="image">Изображение героя</param>
		/// <param name="width">Ширина поля для героя</param>
		/// <param name="height">Высота поля для героя</param>
		public Ship(Point pos, Size size, Image image, int width, int height) : base(pos, new Point(10, 10), size, image, width, height)
		{
		}

		#endregion

		#region Методы

		/// <summary>
		/// Обновление геряо на экране
		/// </summary>
		public override void Update()
		{
		}

		/// <summary>
		/// Потеря героем энергии
		/// </summary>
		/// <param name="n"></param>
		public void EnergyLow(int n)
		{
			Energy -= n;
		}

		/// <summary>
		/// Получение героем энергии
		/// </summary>
		/// <param name="n"></param>
		public void EnergyHigh(int n)
		{
			Energy += n;
		}

		/// <summary>
		/// Получение героем очков
		/// </summary>
		/// <param name="n"></param>
		public void ScoreHigh(int n)
		{
			Score += n;
		}

		/// <summary>
		/// Движение корабля вверх (пока не упремся в верхню юкромку экрана)
		/// </summary>
		public void Up()
		{
			if (pos.Y>0)
				pos.Y -= dir.Y;
		}

		/// <summary>
		/// Движение корабля вниз (пока не упремся в нижнюю кромку экрана с учетом размеров самого корабля)
		/// </summary>
		public void Down()
		{
			if (pos.Y + this.Size.Height < height)
				pos.Y += dir.Y;
		}

		/// <summary>
		/// Смерть героя
		/// </summary>
		public void Die()
		{
			MessageDie?.Invoke();
		}
		#endregion

	}
}
