using System.Drawing;
using WinHunter.Common;
using WinHunter.Interfaces;

namespace WinHunter.FlyObjects
{
	delegate void Message();

	abstract class BaseObject : ICollision
	{
		#region Поля

		/// <summary>
		/// Позиция объекта на поле
		/// </summary>
		protected Point pos;

		/// <summary>
		/// Направление движения объекта
		/// </summary>
		protected Point dir;

		/// <summary>
		/// Размер объекта
		/// </summary>
		protected Size size;

		/// <summary>
		/// Изображение объекта
		/// </summary>
		protected Image image;

		/// <summary>
		/// Ширина поля для объекта
		/// </summary>
		protected static int width;

		/// <summary>
		/// Высота поля для объекта
		/// </summary>
		protected static int height;

		#endregion

		#region Конструкторы

		/// <summary>
		/// Констурктор базового объекта
		/// </summary>
		/// <param name="pos">Координата объекта</param>
		/// <param name="dir">Направление движения объекта</param>
		/// <param name="size">Размер объекта</param>
		/// <param name="width">Ширина поля</param>
		/// <param name="height">Высота поля</param>
		protected BaseObject(Point pos, Point dir, Size size, int width, int height) : this(pos, dir, size,
			Properties.Resources.network, width, height)
		{
		}

		/// <summary>
		/// Констурктор базового объекта с передаваемым изображением
		/// </summary>
		/// <param name="pos">Координата объекта</param>
		/// <param name="dir">Направление движения объекта</param>
		/// <param name="size">Размер объекта</param>
		/// <param name="image">Изображение объекта</param>
		/// <param name="outWidth">Ширина поля</param>
		/// <param name="outHeight">Высота поля</param>
		protected BaseObject(Point pos, Point dir, Size size, Image image, int outWidth, int outHeight)
		{
			if (outWidth <= 0 || outHeight <= 0)
			{
				throw new GameObjectException("Попытка некорректного создания объекта!", GameObjectExceptionType.SizeField);
			}
			if (pos.X < 0 || pos.Y < 0 || pos.X > 2 * outWidth || pos.Y > outHeight)
			{
				throw new GameObjectException("Попытка некорректного создания объекта!", GameObjectExceptionType.Position);
			}
			if (size.Height <= 0 || size.Width <= 0)
			{
				throw new GameObjectException("Попытка некорректного создания объекта!", GameObjectExceptionType.SizeObject);
			}
			this.pos = pos;
			this.dir = dir;
			this.size = size;
			this.image = new Bitmap(image, size);
			width = outWidth;
			height = outHeight;
		}

		#endregion

		#region Методы

		/// <summary>
		/// Отрисовка объекта
		/// </summary>
		/// <param name="buffer"></param>
		public void Draw(BufferedGraphics buffer)
		{
			buffer.Graphics.DrawImage(image, pos.X, pos.Y);
		}

		/// <summary>
		/// Описание движения объекта
		/// </summary>
		public abstract void Update();

		#endregion

		#region Реализация интерефейса ICollision

		public bool Collision(ICollision obj)
		{
			return obj.Rect.IntersectsWith(Rect);
		}

		public Rectangle Rect => new Rectangle(pos, size);

		#endregion
	}
}
