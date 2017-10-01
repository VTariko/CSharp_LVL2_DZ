using System;

namespace Shooting.Arsenal
{
	public delegate void Message(string msg);

	/// <summary>
	/// Базовый класс абстрактного оружия
	/// </summary>
	abstract class Weapon
	{
		#region События

		public event Message MessageShoot;

		#endregion

		#region Поля

		/// <summary>
		/// Текущее количество патронов в магазине - скрытая информация от конечного пользователя
		/// </summary>
		protected int bullets;

		#endregion

		#region Свойства

		/// <summary>
		/// Максимально возможно количество патронов в магазине
		/// </summary>
		protected int MaxBullets { get; set; }

		/// <summary>
		/// Флаг, говорящий о том, нацелено ли оружие
		/// </summary>
		public bool IsAim { get; protected set; }

		#endregion

		#region Конструктор

		/// <summary>
		/// Базовый конструктор
		/// </summary>
		protected Weapon()
		{
			bullets = 0;
			IsAim = false;
		}

		#endregion

		#region Методы

		/// <summary>
		/// Перезарядка
		/// </summary>
		public void Recharge()
		{
			bullets = MaxBullets;
			IsAim = false;
		}

		/// <summary>
		/// Метод прицеливания
		/// </summary>
		public void Target()
		{
			IsAim = true;
		}

		/// <summary>
		/// Метод выстрела
		/// </summary>
		public void Shoot()
		{
			string res;
			if (bullets > 0)
			{
				if (!IsAim)
				{
					res = "Выстрел произведен, но патрон улетел неизвестно куда";
				}
				else
				{
					// Вероятность попадания  = 90%
					Random rand = new Random();
					int r = rand.Next(0, 10);
					res = r == 0
						? "Выстрел произведен. Промах."
						: "Выстрел произведен. Прямое попадание!";
				}
				bullets--;
			}
			else
			{
				res = "В магазине нет патронов!";
			}

			IsAim = false;

			if (MessageShoot != null)
			{
				MessageShoot(res);
			}
		}

		#endregion
	}
}
