using System;
using System.Drawing;
using System.Windows.Forms;
using WinHunter.FlyObjects;

namespace WinHunter.Area
{
	class Game: Field
	{
		#region Поля

		private static BaseObject[] _stars;
		private static BaseObject[] _asteroids;

		#endregion

		#region Конструктор

		public Game() : base() { }

		#endregion

		#region Методы

		public static void Init(Form sourceForm)
		{
			form = sourceForm;
			context = BufferedGraphicsManager.Current;
			// Создаем поверхность рисования и связываем ее с формой
			Graphics g = form.CreateGraphics();
			// Запоминаем размеры формы
			Width = form.Width;
			Height = form.Height;
			// Связываем буфер в памяти с графическим объектом
			Buffer = context.Allocate(g, new Rectangle(0, 0, Width, Height));

			// Загружаем объекты на игровое поле
			Load();

			// Создаем и стартуем таймер для регулярного обновления поля
			timer = new Timer { Interval = 100 };
			timer.Tick += Timer_Tick;
			timer.Start();
		}

		/// <summary>
		/// Обработка тика таймера
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private static void Timer_Tick(object sender, EventArgs e)
		{
			Draw();
			Update();
		}

		/// <summary>
		/// Отрисовка поля со всеми объектами
		/// </summary>
		public static void Draw()
		{
			Buffer.Graphics.Clear(Color.Black);
			Image background = new Bitmap(WinHunter.Properties.Resources.staticback, Width, Height);
			Buffer.Graphics.DrawImage(background, 0, 0);
			foreach (BaseObject o in objs)
			{
				o.Draw(Buffer);
			}
			Buffer.Render();
		}

		/// <summary>
		/// Загрузка объектов на поле
		/// </summary>
		private static void Load()
		{
			// Объявляем массивы звезд и астероидов
			_stars = new BaseObject[100];
			_asteroids = new BaseObject[15];
			Random r = new Random();
			int posX;
			int posY;
			// Создаем звезды
			for (int i = 0; i < _stars.Length; i++)
			{
				int speed = (int)(100 + r.NextDouble() * 100);
				int size = 1 + (int)(0.06 * speed);
				posX = (int)(r.NextDouble() * Width);
				posY = (int)(r.NextDouble() * Height);
				_stars[i] = new Star(new Point(posX, posY), new Point(speed, 0), new Size(size, size));
			}
			// Создаем астероиды
			for (int i = 0; i < _asteroids.Length; i++)
			{
				int speed = 40;
				int size = (2 + r.Next(5)) * 10;
				posX = (int)(r.NextDouble() * Width);
				posY = (int)(r.NextDouble() * Height);
				_asteroids[i] = new Asteroid(new Point(posX, posY), new Point(speed, 0), new Size(size, size));
			}
			// Объявляем массив объектов и заполянем его звездами и астероидами
			objs = new BaseObject[_stars.Length + _asteroids.Length];
			_stars.CopyTo(objs, 0);
			_asteroids.CopyTo(objs, _stars.Length);
		} 

		#endregion
	}
}
