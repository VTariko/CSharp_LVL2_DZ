using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shooting.Arsenal
{
	/// <summary>
	/// Оружие - пистолет
	/// </summary>
	class Pistol : Weapon
	{
		/// <summary>
		/// Базовый конструктор. Задаем максимальный объем магазина.
		/// </summary>
		public Pistol() : base()
		{
			MaxBullets = 17;
		}
	}
}
