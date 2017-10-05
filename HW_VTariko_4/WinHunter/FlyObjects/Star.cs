using System.Drawing;

namespace WinHunter.FlyObjects
{
	class Star: BaseObject
	{
		#region Конструктор

		/// <summary>
		/// Конструктор объекта - Звезда
		/// </summary>
		/// <param name="pos">Координата звезды</param>
		/// <param name="dir">Направление движения звезды</param>
		/// <param name="size">Размер звезды</param>
		/// <param name="width">Ширина поля для звезды</param>
		/// <param name="height">Высота поля для звезды</param>
		public Star(Point pos, Point dir, Size size, int width, int height) : base(pos, dir, size, width, height)
		{
			image = new Bitmap(WinHunter.Properties.Resources.star_white, size);
		}

		#endregion

		#region Методы

		/// <summary>
		/// Описание обновления звезды
		/// </summary>
		public override void Update()
		{
			pos.X -= dir.X;
			if (pos.X < 0)
				pos.X = width + size.Width;
		} 

		#endregion
		
	}
}
