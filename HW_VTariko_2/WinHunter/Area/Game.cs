using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using WinHunter.Common;
using WinHunter.FlyObjects;

namespace WinHunter.Area
{
	class Game: Field
	{
		#region Поля

		private static Star[] _stars;
		private static Asteroid[] _asteroids;
		private static Hero _hero;
		private static Bullet _bullet;

		#endregion

		#region Конструктор

		public Game() : base() { }

		#endregion

		#region Методы

		/// <summary>
		/// Инициализация игрового поля
		/// </summary>
		/// <param name="sourceForm"></param>
		/// <param name="hero"></param>
		public static void Init(Form sourceForm, Hero hero)
		{
			form = sourceForm;
			CheckFormSize();
			_hero = hero;
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
			_hero.Draw(Buffer);
			foreach (BaseObject o in objs)
			{
				o.Draw(Buffer);
			}
			Buffer.Render();
		}

		/// <summary>
		/// Обновление поля игры
		/// </summary>
		public static void Update()
		{
			foreach (BaseObject o in objs)
			{
				o.Update();
			}
			// Проверяем астероиды на столкновение с пулей
			foreach (Asteroid asteroid in _asteroids)
			{
				if (asteroid.Collision(_bullet))
				{
					System.Media.SystemSounds.Hand.Play();
					asteroid.Respawn();
					_bullet.Respawn();
				}
			}
		}

		/// <summary>
		/// Загрузка объектов на поле
		/// </summary>
		private static void Load()
		{
			// Объявляем массивы звезд и астероидов
			_stars = new Star[100];
			_asteroids = new Asteroid[15];
			Random r = new Random();
			int posX;
			int posY;
			// Создаем звезды
			for (int i = 0; i < _stars.Length; i++)
			{
				int speed = (int)(100 + r.NextDouble() * 100);
				int size = 1 + (int)(0.04 * speed);
				posX = (int)(r.NextDouble() * Width);
				posY = (int)(r.NextDouble() * Height);

				try
				{
					_stars[i] = new Star(new Point(posX, posY), new Point(speed, 0), new Size(size, size), Width, Height);
				}
				catch (GameObjectException e)
				{
					MessageBox.Show($"{e.Message}{Environment.NewLine}Тип ошибки: {e.Type}");
					throw;
				}
			}
			// Создаем астероиды
			for (int i = 0; i < _asteroids.Length; i++)
			{
				int speed = 40;
				int size = (2 + r.Next(5)) * 10;
				posX = 1 + (int)(r.NextDouble() * Width);
				posY = 1 + (int)(r.NextDouble() * Height);

				try
				{
					_asteroids[i] = new Asteroid(new Point(posX, posY), new Point(speed, 0), new Size(size, size), Width, Height);
				}
				catch (GameObjectException e)
				{
					MessageBox.Show($"{e.Message}{Environment.NewLine}Тип ошибки: {e.Type}");
					throw;
				}
			}
			// Создаем пулю
			int bulletSpeed = 50;
			// Намеренно сделаем пулю ОГРОМНОЙ, чтобы сейчас, пока корабль не движется, все же почаще попадать по астероидам.
			//TODO: Вернуть размер пули к адекватному, когда корабль станет управляемым
			Size bulletSize = new Size(60, 40);
			int bulletX = _hero.Position.X + _hero.Size.Width;
			int bulletY = _hero.Position.Y + _hero.Size.Height/2 - bulletSize.Height/2;

			try
			{
				_bullet = new Bullet(new Point(bulletX, bulletY), new Point(bulletSpeed, 0), bulletSize, Width, Height, _hero);
			}
			catch (GameObjectException e)
			{
				MessageBox.Show($"{e.Message}{Environment.NewLine}Тип ошибки: {e.Type}");
				throw;
			}

			// Объявляем массив объектов и заполянем его звездами, астероидами, а так же героем и пулей
			List<BaseObject> baseObjects = new List<BaseObject> {_hero, _bullet};
			baseObjects.AddRange(_stars);
			baseObjects.AddRange(_asteroids);
			objs = baseObjects.ToArray();
		}

		#endregion

	}
}
