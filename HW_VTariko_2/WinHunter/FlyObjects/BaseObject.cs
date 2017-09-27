using System.Drawing;

namespace WinHunter.FlyObjects
{
	abstract class BaseObject
	{
		#region Поля

		protected Point pos;
		protected Point dir;
		protected Size size;
		protected Image image;

		#endregion

		#region Конструкторы

		/// <summary>
		/// Констурктор базового объекта
		/// </summary>
		/// <param name="pos">Координата объекта</param>
		/// <param name="dir">Направление движения объекта</param>
		/// <param name="size">Размер объекта</param>
		protected BaseObject(Point pos, Point dir, Size size) : this(pos, dir, size, WinHunter.Properties.Resources.network)
		{
		}

		/// <summary>
		/// Констурктор базового объекта с передаваемым изображением
		/// </summary>
		/// <param name="pos">Координата объекта</param>
		/// <param name="dir">Направление движения объекта</param>
		/// <param name="size">Размер объекта</param>
		/// <param name="image">Изображение объекта</param>
		protected BaseObject(Point pos, Point dir, Size size, Image image)
		{
			this.pos = pos;
			this.dir = dir;
			this.size = size;
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
		/// <param name="width"></param>
		/// <param name="height"></param>
		public abstract void Update(int width, int height);

		#endregion
	}
}
