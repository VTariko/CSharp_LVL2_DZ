using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Windows.Forms;
using WinHunter.Common;
using WinHunter.FlyObjects;

namespace WinHunter.Area
{
	class Game: Field
	{
		#region Константы

		/// <summary>
		/// Строка записи события столкновения
		/// </summary>
		private const string LOG_CRASH_MESSAGE = "{0} - Объект {1} поражает объект {2}.\r\nЭнергия героя: {3}\r\nОчки героя: {4}";

		/// <summary>
		/// Строка записи события получения аптечки
		/// </summary>
		private const string LOG_AID_MESSAGE = "{0} - Аптечка получена.\r\nЭнергия героя: {1}\r\nОчки героя: {2}";

		/// <summary>
		/// Строка записи события смерти героя
		/// </summary>
		private const string LOG_DIE_MESSAGE = "{0} - Герой умер.\r\nОчки героя: {1}";

		/// <summary>
		/// Путь к файлу логирования
		/// </summary>
		private const string LOG_FILE_PATH = @"C:\Temp\SpaceHunter.log";

		#endregion

		#region Поля

		private static readonly Random Rand = new Random();

		/// <summary>
		/// Массив существующих звезд
		/// </summary>
		private static List<Star> _stars;

		/// <summary>
		/// Массив существующих астероидов
		/// </summary>
		private static List<Asteroid> _asteroids;

		/// <summary>
		/// Действующий герой
		/// </summary>
		private static Ship _hero;

		/// <summary>
		/// Список пуль
		/// </summary>
		private static List<Bullet> _bullets;

		/// <summary>
		/// Таймер для срабатывания аптечки
		/// </summary>
		private static Timer _timerAid;

		/// <summary>
		/// Аптечка
		/// </summary>
		private static FirstAid _firstAid;

		/// <summary>
		/// Номер волны
		/// </summary>
		private static int _waveNumber;

		/// <summary>
		/// Флаг того, что начинается новая волна
		/// </summary>
		private static bool _isNewWave;

		/// <summary>
		/// Количество тиков - для замеров дилтелньости существования надписи новой волны
		/// </summary>
		private static int _numberTick;

		#endregion

		#region Методы

		/// <summary>
		/// Инициализация игрового поля
		/// </summary>
		/// <param name="sourceForm"></param>
		/// <param name="hero"></param>
		public static void Init(Form sourceForm, Ship hero)
		{
			form = sourceForm;
			CheckFormSize();
			_hero = hero;
			form.KeyDown += Form_KeyDown;
			Ship.MessageDie += Finish;
			context = BufferedGraphicsManager.Current;
			// Создаем поверхность рисования и связываем ее с формой
			Graphics g = form.CreateGraphics();
			// Запоминаем размеры формы
			Width = form.Width;
			Height = form.Height;
			// Связываем буфер в памяти с графическим объектом
			Buffer = context.Allocate(g, new Rectangle(0, 0, Width, Height));

			// Обновляем номер волны и загружаем объекты на игровое поле
			_waveNumber = 1;
			_isNewWave = false;
			Load();

			// Инициализируем и стартуем таймер для регулярного обновления поля
			timer = new Timer { Interval = 100 };
			timer.Tick += Timer_Tick;
			timer.Start();

			// Инициализируем и стартуем таймер дял генерации аптечек
			_timerAid = new Timer {Interval = 10000};
			_timerAid.Tick += TimerAid_Tick;
			_timerAid.Start();
		}

		#region Обработка таймеров

		/// <summary>
		/// Обработка тика таймера срабатываняи аптечки
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="eventArgs"></param>
		private static void TimerAid_Tick(object sender, EventArgs eventArgs)
		{
			_firstAid.Respawn();
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

		#endregion

		#region Методы отрисовки

		/// <summary>
		/// Отрисовка поля со всеми объектами
		/// </summary>
		public static void Draw()
		{
			Buffer.Graphics.Clear(Color.Black);
			Image background = new Bitmap(Properties.Resources.staticback, Width, Height);
			Buffer.Graphics.DrawImage(background, 0, 0);
			_hero.Draw(Buffer);
			foreach (BaseObject o in objs)
			{
				if (o is Bullet)
				{
					if ((o as Bullet).IsActive)
					{
						o.Draw(Buffer);
					}
				}
				else
				{
					o.Draw(Buffer);
				}
			}
			foreach (Asteroid asteroid in _asteroids)
			{
				if (asteroid.IsActive)
					asteroid.Draw(Buffer);
			}
			Buffer.Graphics.DrawString($"Energy: {_hero.Energy}{Environment.NewLine}Scores: {_hero.Score}", SystemFonts.DefaultFont,
				Brushes.White, 0, 0);
			if (_isNewWave)
			{
				NewWave();
				_numberTick++;
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
				if (o is Bullet)
				{
					if ((o as Bullet).IsActive)
					{
						o.Update();
					}
				}
				else
				{
					o.Update();
				}
			}
			foreach (Asteroid asteroid in _asteroids)
			{
				if (asteroid.IsActive)
					asteroid.Update();
			}
			// Проверяем на столкновение героя и аптечку
			if (_firstAid.Collision(_hero))
			{
				_firstAid.Destroy();
				_hero.EnergyHigh(_firstAid.Power);
			}

			// Проверяем астероиды на столкновение с пулей
			AsteroidsCrash();
		} 

		/// <summary>
		/// Метод првоерки на столкновения астероидов с тем или иным игровым объектом
		/// </summary>
		private static void AsteroidsCrash()
		{
			foreach (Asteroid asteroid in _asteroids)
			{
				foreach (Bullet bullet in _bullets)
				{
					if (asteroid.IsActive && bullet.IsActive && bullet.Collision(asteroid))
					{
						System.Media.SystemSounds.Hand.Play();
						WriteLog(false, bullet.GetType().Name, asteroid.GetType().Name);
						_hero.ScoreHigh(asteroid.Power);
						asteroid.Destroy();
						bullet.Destroy();
						break;
					}
				}
				if (asteroid.IsActive && _hero.Collision(asteroid))
				{
					WriteLog(false, asteroid.GetType().Name, _hero.GetType().Name);
					asteroid.Respawn();
					_hero.EnergyLow(asteroid.Power);
					System.Media.SystemSounds.Beep.Play();
					if (_hero.Energy <= 0)
					{
						WriteLog(true);
						_hero.Die();
					}
				}
			}
			if (_asteroids.All(a => !a.IsActive) && !_isNewWave)
			{
				_isNewWave = true;
				_numberTick = 0;
				NewWave();
			}
		}
		
		#endregion

		/// <summary>
		/// Загрузка объектов на поле
		/// </summary>
		private static void Load()
		{
			// Объявляем звёзд
			_stars = new List<Star>();
			_stars = Star.CreateStars();
			// Создаем астероиды
			_asteroids = new List<Asteroid>();
			_asteroids = Asteroid.CreateAsteroidsWave(_waveNumber);

			//Инициализируем список пуль на будущее
			_bullets = new List<Bullet>();
			//Инициализируем аптечку
			_firstAid = new FirstAid(Width, Height);

			// Инициализируем массив объектов и заполянем его звездами, астероидами, а так же аптечкой и пулей
			objs = new List<BaseObject>();
			objs.AddRange(_stars);
			//objs.AddRange(_asteroids);
			objs.Add(_firstAid);
		}

		private static void NewWave()
		{
			if (_numberTick < 20)
			{
				Buffer.Graphics.DrawString($"Волна {_waveNumber + 1}", new Font(FontFamily.GenericSansSerif, 60, FontStyle.Underline), Brushes.White,
					200, 100);
			}
			else
			{
				_isNewWave = !_isNewWave;
				_asteroids = Asteroid.CreateAsteroidsWave(++_waveNumber);
			}
		}

		/// <summary>
		/// Метод завершения игры
		/// </summary>
		private static void Finish()
		{
			timer.Stop();
			Buffer.Graphics.DrawString("The End!", new Font(FontFamily.GenericSansSerif, 60, FontStyle.Underline), Brushes.White,
				200, 100);
			Buffer.Render();
		}

		/// <summary>
		/// Логирование основных игровых событий 
		/// </summary>
		/// <param name="isDie">Умер ли герой?</param>
		/// <param name="objName1">Имя объекта, поразившего цель</param>
		/// <param name="objName2">Имя поврежденного объекта</param>
		private static void WriteLog(bool isDie, string objName1 = null, string objName2 = null)
		{
			
			// Формируем текст события:
			string message = !isDie
				? !string.IsNullOrWhiteSpace(objName1) && !string.IsNullOrWhiteSpace(objName2)
					? string.Format(LOG_CRASH_MESSAGE, DateTime.Now, objName1, objName2, _hero.Energy, _hero.Score)
					: string.Format(LOG_AID_MESSAGE, DateTime.Now, _hero.Energy, _hero.Score)
				: string.Format(LOG_DIE_MESSAGE, DateTime.Now, _hero.Score);

			// Логируем событие в консоль:
			Console.WriteLine(message);

			//Логируем событие в файл
			// Проверяем на существование директорию, где будет храниться файл, если её не существет - создаем.
			string fileDirectory = Path.GetDirectoryName(LOG_FILE_PATH);
			if (!Directory.Exists(fileDirectory))
			{
				Directory.CreateDirectory(fileDirectory);
			}
			StreamWriter sw = File.AppendText(LOG_FILE_PATH);
			sw.WriteLine(message);
			sw.Close();
		}

		#region Обработка нажатия клавиш

		/// <summary>
		/// Обработка нажатия клавиш
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private static void Form_KeyDown(object sender, KeyEventArgs e)
		{
			// TODO: Реализовать одноврменнную оработку нажатий
			if (e.KeyCode == Keys.Up)
				_hero.Up();
			if (e.KeyCode == Keys.Down)
				_hero.Down();
			// Если нажали пробел - генерируем новую пулю
			if (e.KeyCode == Keys.Space)
			{
				//Если есть неактивная пуля - берём её. Иначе создаем новую.
				Bullet bullet;
				if (_bullets.FirstOrDefault(b => !b.IsActive) != null)
				{
					bullet = _bullets.First(b => !b.IsActive);
					bullet.Respawn();
				}
				else
				{
					bullet = new Bullet(Width, Height, _hero);
					_bullets.Add(bullet);
					objs.Add(bullet);
				}
				

			}
		}

		#endregion

		#endregion

	}
}
