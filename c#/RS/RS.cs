using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace ЛР2_Шмелева_Тарикова_Хаак__РС_
{
	/*
     * Лабораторная работа #2
     * по дисциплине "Разностные схемы" 
     * 
     * Тарикова А.А.    Шмелева Е.К.    Хаак М.
     * ДКО-212б         ДКО-121б        ДКО-122б
     * 
     * 31 декабря 2014 г.
     */

	public static class SCHEME
	{
		//Параметры схемы
		public const double a = -0.25;
		public const double b = 0;
		public const double c = 0.25;
		//Параметры схемы
	}

	class Program
	{

		static Double Phi1(Double argument)
		{
			return Math.Sin(4.0 * Math.PI * argument);
		}

		static Double Phi2(Double argument)
		{
			return Math.Cos(4.0 * Math.PI * argument);
		}

		/*******************************************************/

		public delegate double fi1(double x);
		public delegate double fi2(double x);

		public static double func_fi1(double x)
		{
			return Math.Sin(4.0 * Math.PI * x);
		}
		public static double func_fi2(double x)
		{
			return Math.Cos(4.0 * Math.PI * x);
		}

		public static void Solve(fi1 fi, int T, int n_x, int n_t, double tau, double h, double k, string filename)
		{
			double[,] matrix = new double[n_t + 1, n_x + 1];

			//Вычисление значений нулевой строки сетки
			for (int i = 0; i <= n_x; i++)
			{
				matrix[0, i] = fi(i * h);
			}
			//Вычисление значений нулевой строки сетки

			//Вычисление строк 1 - T НАЧАЛО
			for (int t = 1; t <= n_t; t++)
			{
				double[] alpha = new double[n_x + 1];
				double[] beta = new double[n_x + 1];

				double a = -k / 2;
				double b = 1;
				double c = k / 2;

				//Решаем систему (19) НАЧАЛО
				alpha[1] = -c / b;
				beta[1] = matrix[t - 1, 0] / b;
				//Прямая прогонка, вычисление прогоночных коэффициентов альфа и бета НАЧАЛО
				for (int j = 2; j <= n_x; j++)
				{
					alpha[j] = (-c) / (a * alpha[j - 1] + b);
					//beta[j] = (matrix[t - 1, j] - a * beta[j - 1]) / (a * alpha[j - 1] + b);
					beta[j] = (matrix[t - 1, j] - a * beta[j - 1]) / (a * alpha[j - 1] + b);
				}
				//Прямая прогонка, вычисление прогоночных коэффициентов альфа и бета КОНЕЦ

				//Обратная прогонка, вычисление w НАЧАЛО
				double[] w = new double[n_x + 1];
				w[n_x] = 0;
				for (int j = n_x - 1; j >= 1; j--)
				{
					w[j] = alpha[j + 1] * w[j + 1] + beta[j + 1];
				}
				w[0] = 0;
				//Обратная прогонка, вычисление w КОНЕЦ
				//Решаем систему (19) КОНЕЦ


				//решаем систему (20) НАЧАЛО
				alpha[1] = -c / b;
				beta[1] = matrix[t - 1, 0] / b;
				//Прямая прогонка, вычисление прогоночных коэффициентов альфа и бета НАЧАЛО
				for (int j = 2; j <= n_x; j++)
				{
					alpha[j] = (-c) / (a * alpha[j - 1] + b);
					beta[j] = (-a * beta[j - 1]) / (a * alpha[j - 1] + b);
				}
				//Прямая прогонка, вычисление прогоночных коэффициентов альфа и бета КОНЕЦ

				double[] z = new double[n_x + 1];
				z[n_x] = 1;
				//обратная прогонка, вычисление z НАЧАЛО
				for (int j = n_x - 1; j >= 1; j--)
				{
					z[j] = alpha[j + 1] * z[j + 1] + beta[j + 1];
				}
				z[0] = 1;
				//обратная прогонка, вычисление z КОНЕЦ
				//решаем систему (20) КОНЕЦ


				//Решаем систему (18) НАЧАЛО
				matrix[t, 0] = (matrix[t - 1, 0] - a * w[n_x - 1] - c * w[1]) / (b + a * z[n_x - 1] + c * z[1]);
				for (int i = 1; i <= n_x; i++)
				{
					matrix[t, i] = w[i] + matrix[t, 0] * z[i];
				}
				//Решаем систему (18) КОНЕЦ

			}
			//Вычисление строк 1 - T НАЧАЛО

			//Запись в файл 
			using (System.IO.StreamWriter file = new System.IO.StreamWriter(filename)) {
				for (int i = 0; i <= n_t; i++) {
					if (i*tau==0 || i*tau==0.5 || i*tau==1) {
						file.WriteLine ("t={0}", i*tau);
						for (int j = 0; j <= n_x; j++) {
							file.WriteLine (matrix [i, j].ToString ()+" ");
						}
						file.WriteLine ();
						file.WriteLine ();
					}
				}

			}
			//Запись в файл

		}

		static void Main(string[] args)
		{
			#region Явный шаблон 

			//var v1 = new ImplicitMethodStencil(-0.25, 200, 1000, 1, Phi1);
			//var v2 = new ImplicitMethodStencil(0.25, 200, 1000, 1, Phi2);

			//var unstableV1 = new ImplicitMethodStencil(-0.25, 200, 1000, 1, Phi1, false);
			//var unstableV2 = new ImplicitMethodStencil(0.25, 200, 1000, 1, Phi2, false);

			//using (System.IO.StreamWriter file = new System.IO.StreamWriter(file2))
			//{
			//    foreach (var elem in v2.Slice())
			//    {
			//        file.WriteLine(elem.ToString());
			//    }
			//}

			#endregion

			//Ввод данных
			Console.WriteLine("Введите T");
			int T = int.Parse(Console.ReadLine());
			Console.WriteLine("Введите количество шагов по x");
			int n_x = int.Parse(Console.ReadLine()); //количество шагов по x
			Console.WriteLine("Введите количество шагов по t");
			int n_t = int.Parse(Console.ReadLine());
			//Ввод данных
			double tau = ((double)T / n_t); //шаг по t
			double h = ((double)1 / n_x); //шаг по x

			double k1 = SCHEME.a * tau / h;
			double k2 = -SCHEME.c * tau / h;

			//Console.WriteLine(func_fi1(0.005));
			//Console.WriteLine(Phi1(0.005));

			//Console.WriteLine(func_fi1(0.01));
			//Console.WriteLine(Phi1(0.01));

			//Console.WriteLine(func_fi1(0.1));
			//Console.WriteLine(Phi1(0.1));

			Solve(func_fi1, T, n_x, n_t, tau, h, k1, "out1.txt");
			Solve(func_fi2, T, n_x, n_t, tau, h, k2, "out2.txt");
		}
	}
}
