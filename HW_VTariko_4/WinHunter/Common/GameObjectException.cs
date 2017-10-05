using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WinHunter.Common
{
	/// <summary>
	/// Исключение попытки создания объекта с некорректными параметрами
	/// </summary>
	class GameObjectException: Exception
	{
		public GameObjectExceptionType Type { get; }

		public GameObjectException(string message, GameObjectExceptionType type) : base(message)
		{
			Type = type;
		}
	}
}
