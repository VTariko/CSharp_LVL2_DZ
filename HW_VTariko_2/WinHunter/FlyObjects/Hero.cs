using System.Drawing;

namespace WinHunter.FlyObjects
{
	class Hero: BaseObject
	{
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

		#endregion
		/// <summary>
		/// Конструктор объекта - герой
		/// </summary>
		/// <param name="pos">Координата героя</param>
		/// <param name="size">Размер героя</param>
		/// <param name="image">Изображение героя</param>
		/// <param name="width">Ширина поля для героя</param>
		/// <param name="height">Высота поля для героя</param>
		public Hero(Point pos, Size size, Image image, int width, int height) : base(pos, new Point(0, 0), size, image, width, height)
		{
		}

		/// <summary>
		/// Обновление геряо на экране
		/// </summary>
		public override void Update()
		{
			// TODO: Реализовать движения героя
		}

	}
}
