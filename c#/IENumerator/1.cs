using System;
using System.Collections;

namespace ConsoleApplication1 {
	public interface ICustomComparable<T> {
		int CustomCompare (T var);
	}

	public class CustomArray<T> : IEnumerable where T : ICustomComparable<T> {
		T [] arr;

		public CustomArray (int size) {
			arr=new T[size];
		}

		public void Sort () {
			for (int i = 0; i < arr.Length - 1; i++)
				for (int j = i+1; j < arr.Length; j++)
					if (arr [i].CustomCompare (arr [j])>0) {
						T buf=arr [i];
						arr [i]=arr [j];
						arr [j]=buf;
					}
		}

		public T this [int i] {
			get {
				return arr [i];
			}
			set {
				arr [i]=value;
			}
		}

		IEnumerator IEnumerable.GetEnumerator () {
			return (IEnumerator)GetEnumerator ();
		}

		public CustomArrayEnum<T> GetEnumerator () {
			return new CustomArrayEnum<T> (arr);
		}
	}

	public class CustomArrayEnum<T> : IEnumerator {
		T [] arr;
		int position=-1;

		public CustomArrayEnum (T [] list) {
			arr=list;
		}

		public bool MoveNext () {
			position++;
			return (position<arr.Length);
		}

		public void Reset () {
			position=-1;
		}

		object IEnumerator.Current {
			get {
				return Current;
			}
		}

		public T Current {
			get {
				try {
					return arr [position];
				} catch (IndexOutOfRangeException) {
					throw new InvalidOperationException ();
				}
			}
		}
	}

	class A : ICustomComparable<A> {
		public int op1 { get; set; }

		public int op2 { get; set; }

		public A (int a, int b) {
			op1=a;
			op2=b;
		}

		public int CustomCompare (A var) {
			if (op1==var.op1) {
				if (op2==var.op2)
					return 0;
				else if (op2>var.op2)
					return 1;
				else
					return -1;
			} else if (op1>var.op1)
				return 1;
			else
				return -1;
		}
	}

	class B : ICustomComparable<B> {
		public string op1 { get; set; }

		public double op2 { get; set; }

		public B (string a, double b) {
			op1=a;
			op2=b;
		}

		public int CustomCompare (B var) {
			if (op1==var.op1) {
				if (op2==var.op2)
					return 0;
				else if (op2>var.op2)
					return 1;
				else
					return -1;
			} else if (String.Compare (op1, var.op1)>0)
				return 1;
			else
				return -1;
		}
	}

	class Program {
		static void Main (string [] args) {
			var arr=new CustomArray<A> (3);
			arr [0]=new A (3, 5);
			arr [1]=new A (2, 4);
			arr [2]=new A (1, 3);

			foreach (var a in arr)
				Console.WriteLine ("{0} {1}", a.op1, a.op2);

			arr.Sort ();
			Console.WriteLine ();

			foreach (var a in arr)
				Console.WriteLine ("{0} {1}", a.op1, a.op2);

			Console.Read ();
		}
	}
}
