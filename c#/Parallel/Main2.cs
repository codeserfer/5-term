using System;
using System.Threading;

namespace Seminar {
	class MainClass {
		private static void ComputeBoundOp (Object state) 
		{
			//Метод выполняется потоком из пула
			Console.WriteLine ("In ComputeBoundOp: state={0}", state);
			Thread.Sleep (1000); //Имитация работы - 1 сек
			//После возвращения управления методом поток возвращается в пул и ожидает следующего задания
		}


		public static void Main (string [] args) {
			Console.WriteLine ("Main thread: queing an asynchronous operation");
			ThreadPool.QueueUserWorkItem (ComputeBoundOp, 5);
			Console.WriteLine ("Main thread: Doind other work here...");
			Thread.Sleep (10000);
			Console.WriteLine ("Hit <Enter> to end this program");
			Console.ReadLine ();
		}
	}
}
