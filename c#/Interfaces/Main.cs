using System;

namespace ConsoleApplication1 {

	public interface Test {
		void foo ();
		void goo ();
	}

	public interface Test2 {
		void doo ();
	}


	public class My : Test, Test2 {
		public void foo () {
			Console.WriteLine ("foo");
		}

		public void goo () {
			Console.WriteLine ("goo");
		}

		public void doo () {
			Console.WriteLine ("doo");
		}
	}

	class Program {
		static void Main (string[] args) {
			var q=new My ();
			q.doo ();
			q.foo ();
			q.goo ();
		}
	}
}
