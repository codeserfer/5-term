using System;
using System.Threading.Tasks;
using System.Threading;


namespace seminar29 {
	public class Matrix {
		public int n, m;
		public int[,] matrix;
		public Matrix (int n, int m) {
			this.n=n;
			this.m=m;
		}
		public override string ToString () {
			string s="";
			for (int i=0; i<n; i++) {
				for (int j=0; j<m; j++) {
					s+=matrix [i, j]+" ";
				}
				s+="\n";
			}
			return s;
		}

		public static Matrix operator* (Matrix A, Matrix B) {
			int[,] rezult = new int[A.n, B.m];
			Parallel.For (0, A.n, new ParallelOptions { MaxDegreeOfParallelism=3 }, i =>
				Parallel.For (0, B.m, new ParallelOptions { MaxDegreeOfParallelism=3 }, j =>
					Parallel.For (0, B.n, new ParallelOptions { MaxDegreeOfParallelism=3 }, k =>
						{rezult [i, j]+=A.matrix [i, k]*B.matrix [k, j]; })));

			Matrix Q=new Matrix (A.n, B.m);
			Q.matrix=rezult;
			return Q;
		}
	}

	class MainClass {
		public static void Main (string [] args) {
			Matrix A=new Matrix (3, 3);
			A.matrix=new int[,] {
				{1, 2, 3},
				{4, 5, 6},
				{7, 8, 9}
			};

			Matrix B=new Matrix (3, 2);
			B.matrix=new int[,] {
				{9, 8},
				{7, 6},
				{5, 4}
			};
			Console.WriteLine (A);
			Console.WriteLine (B);
			Console.WriteLine (A*B);

		}
	}
}
