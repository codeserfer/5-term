using System;
using System.IO;

namespace Seminar {
	public class Num : IDisposable {
		private FileStream File;
		private int position;
		private int readposition;
		public Num () {
			File=new FileStream ("num.txt", FileMode.Open);
		}

		public void Dispose () {
			File.Close ();


			GC.SuppressFinalize (this);
		}

		public Int32 Read () {
			var buf=new byte [4];
			File.Seek (0, SeekOrigin.Begin);
			File.Read (buf, 0, 4);
			readposition=4;
			return BitConverter.ToInt32 (buf, 0);
		}

		public Int32 ReadNext () {
			var buf=new byte [4];
			File.Seek (readposition, SeekOrigin.Current);
			File.Read (buf, 0, 4);
			readposition+=4;
			return BitConverter.ToInt32 (buf, 0);
		}

		public void Write (int num) {
			var buf=BitConverter.GetBytes (num);
			File.Seek (0, SeekOrigin.Begin);
			File.Write (buf, 0, buf.Length);
			position=4;
		}

		public void WriteNext (int num) {
			var buf=BitConverter.GetBytes (num);
			File.Seek (position, SeekOrigin.Current);
			File.Write (buf, 0, buf.Length);
			position+=4;
		}

		public void WriteCurrent (int position, int num) {
			var buf=BitConverter.GetBytes (num);
			if (position==0) 
				File.Seek (0, SeekOrigin.Begin);
			else 
				File.Seek (position*4, SeekOrigin.Current);
			File.Write (buf, 0, buf.Length);
		}

		~Num () {
			if (File != null)
				File.Close ();
		}

		public Int32 ReadCurrent (int position) {
			var buf=new byte [4];
			if (position==0) 
				File.Seek (0, SeekOrigin.Begin);
			else {
				for (int i=0; i<position; i++)
					ReadCurrent (i);
				File.Seek (position*4, SeekOrigin.Current);
			}
			File.Read (buf, 0, 4);
			return BitConverter.ToInt32 (buf, 0);
		}

	}
	


	public class Seminar {
		public static void Main () {
			Num q=new Num ();
			/*q.Write (2);
			q.WriteNext (4);
			q.WriteNext (8);
			q.WriteNext (2);
			q.WriteNext (8);
			q.WriteNext (2);
			q.WriteNext (8);
			q.WriteNext (2);
			q.WriteNext (8);*/
			q.WriteCurrent (0, 1);
			q.WriteCurrent (1, 2);
			q.WriteCurrent (2, 3);
			q.WriteCurrent (3, 4);

			/*Console.WriteLine (q.ReadCurrent (0)); //1
			Console.WriteLine (q.ReadCurrent (1)); //2
			Console.WriteLine (q.ReadCurrent (2)); //3
			Console.WriteLine (q.ReadCurrent (0)); //1
			Console.WriteLine (q.ReadCurrent (1)); //2
			Console.WriteLine (q.ReadCurrent (2)); //3
			Console.WriteLine (q.ReadCurrent (3)); //4
			Console.WriteLine (q.ReadCurrent (1)); //2*/
			Console.WriteLine (q.ReadCurrent(1));

			q.Dispose ();
			//Console.WriteLine (q.Read ());
		}


	}

}


