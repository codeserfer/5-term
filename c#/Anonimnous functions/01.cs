using System;

namespace seminar_1
{

	public delegate void P ();

	class A {
		public int i; 
		public override string ToString () {
			return i.ToString ();
		}
	}

	class MainClass
	{
		public static void Main(string[] args)
		{
			P p=Console.WriteLine;
			p+=Console.WriteLine;

			foreach (var i in new [] {new A {i=1}, new A {i=2}, new A {i=3}, new A {i=4}}) {
				p+=delegate () { Console.WriteLine (i); };

			}

			p();
		}

	}
}
