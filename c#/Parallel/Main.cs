using System;
using System.Threading;

namespace Seminar {
	class MainClass {

		private static void ComputeBoundOp (Object state) {
			Console.WriteLine ("In ComputeBoundOp: state={0}", state);
			Thread.Sleep (1000);
		}

		public static void Main (string [] args) {
			Console.WriteLine ("Main thread: starting");
			var DedicatedThread=new Thread (ComputeBoundOp);
			DedicatedThread.Start (5);

			Console.WriteLine ("Main thread: Doing other work here...");
			Thread.Sleep (10000);

			DedicatedThread.Join (); //Ожидание завершения потока
			Console.WriteLine ("Hit Enter to end this program");
			Console.ReadLine ();


		}
	}
}
