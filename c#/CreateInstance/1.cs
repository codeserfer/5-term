using System;

namespace array {
	class MainClass {
		static Int32 rank;
		static Int32[] lower;
		static Int32[] upper;
		static Int32[] lengths;
		static Array array;

		public static void Main(string[] args) {
			Console.Write("Введите количество измерений:");
			rank = Int32.Parse(Console.ReadLine());

			lower = new Int32[rank];
			upper = new Int32[rank];
			lengths = new Int32[rank];

			for (Int32 i=0; i<rank; i++) {
				Console.Write("Введите нижнюю границу {0} ",(i + 1));
				lower[i] = Int32.Parse(Console.ReadLine());

				Console.Write("Введите верхнюю границу {0} ",(i + 1));
				upper[i] = Int32.Parse(Console.ReadLine()) + 1;

				lengths[i] = upper[i] - lower[i];
			}

			array = Array.CreateInstance (typeof(Int32), lengths, lower);

			int[] ind = new Int32[rank];

			Input (ref ind, 0);

			Int32 mul = CalcMul (ref ind, 0);

			Console.WriteLine (mul);
		}

		private static void Input  (ref Int32[] ind, Int32 rank) {
			for (Int32 i=lower[rank]; i<upper[rank]; i++) {
				ind[rank] = i;
				if (rank < (ind.Length - 1)) Input(ref ind, rank + 1);
				else {
					Console.Write("Введите значение ");
					for (Int32 j = 0; j < ind.Length; j++) Console.Write("[{0}]", ind[j]);
					Console.Write(':');
					array.SetValue(Int32.Parse(Console.ReadLine()), ind);
				}
			}
		}

		private static Int32 CalcMul (ref Int32[] ind, Int32 rank) {
			Int32 mul = 1;
			for (Int32 i = lower[rank]; i < upper[rank]; i++) {
				ind[rank] = i;
				if (rank < (ind.Length - 1)) mul *= CalcMul(ref ind, rank + 1);
				else mul *= (Int32)array.GetValue(ind);
			}
			return mul;
		}
	}
}
