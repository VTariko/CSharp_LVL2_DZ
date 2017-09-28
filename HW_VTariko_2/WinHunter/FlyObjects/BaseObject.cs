using System.Drawing;
using WinHunter.Common;

namespace WinHunter.FlyObjects
{
	abstract class BaseObject
	{
		#region Поля

		protected Point pos;
		protected Point dir;
		protected Size size;
		protected Image image;
		protected int width;
		protected int height;

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
		protected BaseObject(Point pos, Point dir, Size size, int width, int height) : this(pos, dir, size, WinHunter.Properties.Resources.network, width, height)
		{
		}

		/// <summary>
		/// Констурктор базового объекта с передаваемым изображением
		/// </summary>
		/// <param name="pos">Координата объекта</param>
		/// <param name="dir">Направление движения объекта</param>
		/// <param name="size">Размер объекта</param>
		/// <param name="image">Изображение объекта</param>
		/// <param name="width">Ширина поля</param>
		/// <param name="height">Высота поля</param>
		protected BaseObject(Point pos, Point dir, Size size, Image image, int width, int height)
		{
			if (width <0 || height < 0 || pos.X < 0 || pos.Y < 0 || pos.X > width || pos.Y>height || size.Height < 0 || size.Width < 0)
			{
				throw new GameObjectException("Попытка некорректного создания объекта!");
			}
			this.pos = pos;
			this.dir = dir;
			this.size = size;
			this.width = width;
			this.height = height;
			this.image = new Bitmap(image, size);
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
	}
}
