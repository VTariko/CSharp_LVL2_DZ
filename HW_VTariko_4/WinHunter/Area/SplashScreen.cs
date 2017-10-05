using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using WinHunter.Common;
using WinHunter.FlyObjects;

namespace WinHunter.Area
{
	class SplashScreen: Field
	{
		#region Конструктор

		public SplashScreen() : base() { }

		#endregion

		#region Методы

		/// <summary>
		/// Инициализация поля заставки
		/// </summary>
		/// <param name="sourceForm"></param>
		public static void Init(Form sourceForm)
		{
			form = sourceForm;
			CheckFormSize();
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
			AddStartButtons();

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
		/// Обновление поля заставки
		/// </summary>
		public static void Update()
		{
			foreach (BaseObject o in objs)
			{
				o.Update();
			}
		}


		/// <summary>
		/// Создание и добавления на форму контролов
		/// </summary>
		private static void AddStartButtons()
		{
			// Общий размер для всех кнопок
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
				Location = new Point((form.Width - size.Width) / 2, (form.Height - size.Height) / 2 + 2 * size.Height)
			};

			// Добавляем кнопкам действия
			btnStart.Click += ChoiceHero_Click;
			btnExit.Click += Exit_Click;

			//добавляем кнопки на форму, предварительно удаляя старые.
			form.Controls.Clear();
			Draw();
			form.Controls.Add(btnStart);
			form.Controls.Add(btnRecords);
			form.Controls.Add(btnExit);
		}

		/// <summary>
		/// Добавление контролов для выбора героя которым будет проводитсья игра
		/// </summary>
		private static void AddHeroesButton()
		{
			// Общий размер для всех кнопок героев
			Size size = new Size(75, 75);
			// Размер изображения героя на кнопке
			Size sizeHeroes = new Size(64, 64);
			// Высота для заголовка и для кнопки возврата 
			int btnHeight = 40;

			// Заголовок
			Label lblChoice = new Label
			{
				Text = "Выберите героя",
				TextAlign = ContentAlignment.MiddleCenter,
				Size = new Size(3 * size.Width, btnHeight),
				Location = new Point(form.Width / 2 - size.Width - size.Width / 2, (int)(form.Height / 2 - 2 * size.Height - (double)3 / 2 * btnHeight))
			};

			// Кнопоки героев
			Button btnHero1 = new Button
			{
				Size = size,
				Image = new Bitmap(WinHunter.Properties.Resources.hero_1, sizeHeroes),
				Location = new Point(form.Width / 2 - size.Width - size.Width/2, form.Height / 2 - 2 * size.Height)
			};
			Button btnHero2 = new Button
			{
				Size = size,
				Image = new Bitmap(WinHunter.Properties.Resources.hero_2, sizeHeroes),
				Location = new Point(form.Width / 2 - size.Width - size.Width / 2, (form.Height - size.Height) / 2)
			};
			Button btnHero3 = new Button
			{
				Size = size,
				Image = new Bitmap(WinHunter.Properties.Resources.hero_3, sizeHeroes),
				Location = new Point(form.Width / 2 - size.Width - size.Width / 2, form.Height / 2 + size.Height)
			};
			Button btnHero4 = new Button
			{
				Size = size,
				Image = new Bitmap(WinHunter.Properties.Resources.hero_4, sizeHeroes),
				Location = new Point(form.Width / 2 + size.Width/2, form.Height / 2 - 2 * size.Height)
			};
			Button btnHero5 = new Button
			{
				Size = size,
				Image = new Bitmap(WinHunter.Properties.Resources.hero_5, sizeHeroes),
				Location = new Point(form.Width / 2 + size.Width/2, (form.Height - size.Height) / 2)
			};
			Button btnHero6 = new Button
			{
				Size = size,
				Image = new Bitmap(WinHunter.Properties.Resources.hero_6, sizeHeroes),
				Location = new Point(form.Width / 2 + size.Width/2, form.Height / 2 + size.Height)
			};

			// Кнопка возврата
			Button btnReturn = new Button
			{
				Text = "Назад",
				Size = new Size(3 * size.Width, btnHeight),
				Location = new Point(form.Width / 2 - size.Width - size.Width / 2, form.Height / 2 + 2*size.Height + btnHeight/2)
			};

			// На все кнопки героев вешаем событие "Старт игры"
			btnHero1.Click += StartGame_Click;
			btnHero2.Click += StartGame_Click;
			btnHero3.Click += StartGame_Click;
			btnHero4.Click += StartGame_Click;
			btnHero5.Click += StartGame_Click;
			btnHero6.Click += StartGame_Click;
			
			// Кнопка возврата возвращает нам предыдущие варианты
			btnReturn.Click += Return_Click;

			// Добавлям на форму все выбранные контролы, предварительно удаляя старые.
			form.Controls.Clear();
			Draw();
			form.Controls.Add(lblChoice);
			form.Controls.Add(btnHero1);
			form.Controls.Add(btnHero2);
			form.Controls.Add(btnHero3);
			form.Controls.Add(btnHero4);
			form.Controls.Add(btnHero5);
			form.Controls.Add(btnHero6);
			form.Controls.Add(btnReturn);
		}

		/// <summary>
		/// Обработки наджатяи кнопки "Старт игры" - переход к выбору героя
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private static void ChoiceHero_Click(object sender, EventArgs e)
		{
			AddHeroesButton();
			
		}

		/// <summary>
		/// Обработка кнопки с выбранным героем
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private static void StartGame_Click(object sender, EventArgs e)
		{
			// Создаем переменную дял хранения выбранного изображения героя
			Image heroImage = null;
			Button button = sender as Button;
			if (button != null)
			{
				// запоминаем выбраннное изображение
				heroImage = button.Image;
			}

			if (heroImage != null)
			{
				// Удаляем таймер
				timer.Dispose();
				// очищаем контролы
				form.Controls.Clear();
				
				// TODO: Подумать над зависимостю размера героя от размера игрового поля
				// Герой будет статичного размера - 50*50
				Size heroSize = new Size(50, 50);
				// Начинать герой будет по вертикали в центре экрана с небольшгим отспуом от левого края
				Point heroStartPoint = new Point(heroSize.Width, (Height - heroSize.Height)/2);

				Ship hero;
				try
				{
					hero = new Ship(heroStartPoint, heroSize, heroImage, Width, Height);
				}
				catch (GameObjectException ex)
				{
					MessageBox.Show(string.Format(ErrorString, ex.Message, ex.Type));
					throw;
				}

				// Инициализируем саму игру
				try
				{
					Game.Init(form, hero);
				}
				catch (ArgumentOutOfRangeException ex)
				{
					MessageBox.Show(ex.Message);
					return;
				}
				
			}
		}

		/// <summary>
		/// Обратботка нажатия кнопки возврата- возвращает стартовые три варианта.
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private static void Return_Click(object sender, EventArgs e)
		{
			AddStartButtons();
		}

		/// <summary>
		/// Обработка кнопки выхода из игры
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private static void Exit_Click(object sender, EventArgs e)
		{
			Application.Exit();
		}

		/// <summary>
		/// Метод загрузки Планет на поле заставки
		/// </summary>
		private static void LoadPlanets()
		{
			// Создаем массив изображений планет
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
			// Объявляем массив планет
			Planet[] planets = new Planet[9];
			Random r = new Random();
			// заполняем массив планет различными планетами
			for (int i = 0; i < planets.Length; i++)
			{
				//Случайные скорости дял планет, внебольших диапазаонах (планеты же не носятся, как угорелые!)
				int speedX = r.Next(-10, 11);
				int speedY = r.Next(-10, 11);
				// Случайный размер для планеты
				int size = (2 + r.Next(5)) * 10;
				// И случайная стартовая позиция для планеты:
				int posX = (int)(r.NextDouble() * Width);
				int posY = (int)(r.NextDouble() * Height);

				try
				{
					planets[i] = new Planet(new Point(posX, posY), new Point(speedX, speedY), new Size(size, size), planetsImages[i % 10], Width, Height);
				}
				catch (GameObjectException e)
				{
					MessageBox.Show($"{e.Message}{Environment.NewLine}Тип ошибки: {e.Type}");
					throw;
				}
			}

			objs = new List<BaseObject>(planets);
		} 

		#endregion

	}
}
