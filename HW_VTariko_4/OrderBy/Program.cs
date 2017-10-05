using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrderBy
{

	//Домашняя работа C#-1
	//Выполнил: Вячеслав Тарико (VTariko)
	//
	//* Дан фрагмент программы
	//а)	Свернуть обращение к OrderBy с использованием лямбда-выражения$
	//б) *	Развернуть обращение к OrderBy с использованием делегата Func<KeyValuePair<string,int>, int>.

	class Program
	{
		static void Main(string[] args)
		{
			Console.WriteLine("Выполнение изначального фрагмента:");
			OrderBySource();
			Console.WriteLine("Выполнение фрагмента с лямбда-выражением:");
			OrderByLambda();
			Console.WriteLine("Выполнение фрагмента с делегатом Func:");
			OrderByFunc();
			Console.ReadKey();
		}

		/// <summary>
		/// Исходный фрагмент программы
		/// </summary>
		private static void OrderBySource()
		{
			Dictionary<string, int> dict = new Dictionary<string, int>()
			{
				{ "four" , 4 },
				{ "two" , 2 },
				{ "one" , 1 },
				{ "three" , 3 },
			};
			var d = dict.OrderBy(delegate (KeyValuePair<string, int> pair) { return pair.Value; });
			foreach (var pair in d)
			{
				Console.WriteLine("{0} - {1}", pair.Key, pair.Value);
			}
		}

		/// <summary>
		/// Фрагмент програмы с применением лямбда-выражений
		/// </summary>
		private static void OrderByLambda()
		{
			Dictionary<string, int> dict = new Dictionary<string, int>()
			{
				{ "four" , 4 },
				{ "two" , 2 },
				{ "one" , 1 },
				{ "three" , 3 },
			};
			var d = dict.OrderBy(v => v.Value);
			foreach (var pair in d)
			{
				Console.WriteLine("{0} - {1}", pair.Key, pair.Value);
			}
		}

		/// <summary>
		/// Фрагмент програмы с применением делегата Func
		/// </summary>
		private static void OrderByFunc()
		{
			Dictionary<string, int> dict = new Dictionary<string, int>()
			{
				{ "four" , 4 },
				{ "two" , 2 },
				{ "one" , 1 },
				{ "three" , 3 },
			};

			Func<KeyValuePair<string, int>, int> func = (k) =>
			{
				return k.Value;
			};

			var d = dict.OrderBy(func);
			foreach (var pair in d)
			{
				Console.WriteLine("{0} - {1}", pair.Key, pair.Value);
			}
		}
	}
}
