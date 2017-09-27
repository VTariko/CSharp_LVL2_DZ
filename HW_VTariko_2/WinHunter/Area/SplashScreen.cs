using System;
using System.Drawing;
using System.Windows.Forms;
using WinHunter.FlyObjects;

namespace WinHunter.Area
{
	class SplashScreen: Field
	{
		#region Конструктор

		public SplashScreen() : base() { }

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
			// Загузка планет
			LoadPlanets();
			// Добавление на форму кнопок
			AddControls();

			// Определение и старт таймера для обновления поля
			timer = new Timer { Interval = 100 };
			timer.Start();
			timer.Tick += Timer_Tick;
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
			DrawSurname();
			foreach (BaseObject o in objs)
			{
				o.Draw(Buffer);
			}
			Buffer.Render();
		}

		/// <summary>
		/// Отрисовка фамилии
		/// </summary>
		public static void DrawSurname()
		{
			string surname = "VTariko";
			Font font = new Font("Courier", 16, FontStyle.Bold);
			Buffer.Graphics.DrawString(surname, font, Brushes.White, new Point(5, 5));
		}



		/// <summary>
		/// Создание и добавления на форму контролов
		/// </summary>
		private static void AddControls()
		{
			// Общий размер дял всех кнопок
			Size size = new Size(100, 30);

			// Кнопка старта игры
			Button btnStart = new Button
			{
				Text = "Новая игра",
				Size = size,
				Location = new Point((form.Width - size.Width) / 2, (form.Height - size.Height) / 2 - 2 * size.Height)
			};

			// Кнопка рекордов
			Button btnRecords = new Button()
			{
				Text = "Рекорды",
				Size = size,
				Location = new Point((form.Width - size.Width) / 2, (form.Height - size.Height) / 2)
			};

			// Кнопка выхода из игры
			Button btnExit = new Button
			{
				Text = "Выход",
				Size = size,
				Location = new Point((form.Width - size.Width) / 2, (form.Height - size.Height) / 2 + 2 * size.Height),
			};

			// Добавляем кнопкам действия
			btnStart.Click += StartGame_Click;
			btnExit.Click += Exit_Click;

			//добавляем кнопки на форму
			form.Controls.Add(btnStart);
			form.Controls.Add(btnRecords);
			form.Controls.Add(btnExit);
		}


		private static void StartGame_Click(object sender, EventArgs e)
		{
			timer.Dispose();
			form.Controls.Clear();
			Game.Init(form);
		}

		private static void Exit_Click(object sender, EventArgs e)
		{
			Application.Exit();
		}

		private static void LoadPlanets()
		{
			Image[] planetsImages =
			{
				WinHunter.Properties.Resources.planet_1,
				WinHunter.Properties.Resources.planet_2,
				WinHunter.Properties.Resources.planet_3,
				WinHunter.Properties.Resources.planet_4,
				WinHunter.Properties.Resources.planet_5,
				WinHunter.Properties.Resources.planet_6,
				WinHunter.Properties.Resources.planet_7,
				WinHunter.Properties.Resources.planet_8,
				WinHunter.Properties.Resources.planet_9
			};
			Planet[] planets = new Planet[9];
			Random r = new Random();
			for (int i = 0; i < planets.Length; i++)
			{
				int speedX = r.Next(-10, 11);
				int speedY = r.Next(-10, 11);
				int size = (2 + r.Next(5)) * 10;
				int posX = (int)(r.NextDouble() * Width);
				int posY = (int)(r.NextDouble() * Height);
				planets[i] = new Planet(new Point(posX, posY), new Point(speedX, speedY), new Size(size, size), planetsImages[i % 10]);
			}

			objs = new BaseObject[planets.Length];
			planets.CopyTo(objs, 0);
		} 

		#endregion
	}
}
