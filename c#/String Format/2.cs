using System;
using System.Globalization;
using System.Threading;

namespace hw_9 {
	class MainClass {
		public static void Main (string[] args) {
			var dt = DateTime.Now;
			Console.WriteLine (dt.ToString ("t"));
			Console.WriteLine (dt.ToString ("T"));
			Console.WriteLine (dt.ToString ("d"));
			Console.WriteLine (dt.ToString ("D"));
			Console.WriteLine (dt.ToString ("f"));
			Console.WriteLine (dt.ToString ("F"));
			Console.WriteLine (dt.ToString ("g"));
			Console.WriteLine (dt.ToString ("G"));
			Console.WriteLine (dt.ToString ("M"));
			Console.WriteLine (dt.ToString ("Y"));
			Console.WriteLine (dt.ToString ("R"));
			Console.WriteLine (dt.ToString ("s"));
			Console.WriteLine (dt.ToString ("u"));

			Console.WriteLine ("\n\n");

			Console.WriteLine ("{0:d }", dt);
			Console.WriteLine ("{0:dd }", dt);
			Console.WriteLine ("{0:ddd }", dt);
			Console.WriteLine ("{0:dddd }", dt);

			Console.WriteLine ("{0:M }", dt);
			Console.WriteLine ("{0:MM }", dt);
			Console.WriteLine ("{0:MMM }", dt);
			Console.WriteLine ("{0:MMMM }", dt);

			Console.WriteLine ("{0:y }", dt);
			Console.WriteLine ("{0:yy }", dt);
			Console.WriteLine ("{0:yyy }", dt);
			Console.WriteLine ("{0:yyyy }", dt);

			Console.WriteLine ("{0:h }", dt);
			Console.WriteLine ("{0:hh }", dt);
			Console.WriteLine ("{0:H }", dt);
			Console.WriteLine ("{0:HH }", dt);

			Console.WriteLine ("{0:m }", dt);
			Console.WriteLine ("{0:mm }", dt);
			Console.WriteLine ("{0:s }", dt);
			Console.WriteLine ("{0:ss }", dt);

			Console.WriteLine ("\n\n");
			Console.WriteLine ("{0:f }", dt);
			Console.WriteLine ("{0:ff }", dt);
			Console.WriteLine ("{0:fff }", dt);
			Console.WriteLine ("{0:ffff }", dt);
			Console.WriteLine ("{0:F }", dt);
			Console.WriteLine ("\n\n");
			Console.WriteLine ("{0:z }", dt);
			Console.WriteLine ("{0:zz }", dt);
			Console.WriteLine ("{0:zzz }", dt);
			Console.WriteLine ("{0:t }", dt);
			Console.WriteLine ("{0:tt }", dt);
			Console.WriteLine ("\n");

			Console.WriteLine ("String Format for Date:");

			Console.WriteLine (String.Format("{0:d MM yyyy}", dt));
			Console.WriteLine (String.Format("{0:ss:mm:HH d.MM.yyyy}", dt));

			Console.WriteLine ("String Format for int:");
			string formatString ="{0,5:X}, {1,7}";
			int value1 = 16932;
			int value2 = 15421;
			Console.WriteLine (String.Format(formatString, value1, value2)); 

			/*
            var ci = new CultureInfo("en-GB");
            Console.WriteLine(dt.ToString("t ", ci));
            Console.WriteLine(dt.ToString("tt", ci));

            Thread.CurrentThread.CurrentCulture = ci;



            Console.WriteLine(dt.ToString("t"));
            Console.WriteLine(dt.ToString("T"));
            Console.WriteLine(dt.ToString("d"));
            Console.WriteLine(dt.ToString("D"));
            Console.WriteLine(dt.ToString("f"));
            Console.WriteLine(dt.ToString("F"));
            Console.WriteLine(dt.ToString("g"));
            Console.WriteLine(dt.ToString("G"));
            Console.WriteLine(dt.ToString("m"));
            Console.WriteLine(dt.ToString("M"));
            Console.WriteLine(dt.ToString("y"));
            Console.WriteLine(dt.ToString("Y"));
            Console.WriteLine(dt.ToString("r"));
            Console.WriteLine(dt.ToString("R"));
            Console.WriteLine(dt.ToString("s"));
            Console.WriteLine(dt.ToString("u"));

            Console.WriteLine("\n\n");

            Console.WriteLine("{0:d }", dt);
            Console.WriteLine("{0:dd }", dt);
            Console.WriteLine("{0:ddd }", dt);
            Console.WriteLine("{0:dddd }", dt);

            Console.WriteLine("{0:M }", dt);
            Console.WriteLine("{0:MM }", dt);
            Console.WriteLine("{0:MMM }", dt);
            Console.WriteLine("{0:MMMM }", dt);

            Console.WriteLine("{0:y }", dt);
            Console.WriteLine("{0:yy }", dt);
            Console.WriteLine("{0:yyy }", dt);
            Console.WriteLine("{0:yyyy }", dt);

            Console.WriteLine("{0:h }", dt);
            Console.WriteLine("{0:hh }", dt);
            Console.WriteLine("{0:H }", dt);
            Console.WriteLine("{0:HH }", dt);

            Console.WriteLine("{0:m }", dt);
            Console.WriteLine("{0:mm }", dt);
            Console.WriteLine("{0:s }", dt);
            Console.WriteLine("{0:ss }", dt);

            Console.WriteLine("\n\n");
            Console.WriteLine("{0:f }", dt);
            Console.WriteLine("{0:ff }", dt);
            Console.WriteLine("{0:fff }", dt);
            Console.WriteLine("{0:ffff }", dt);
            Console.WriteLine("{0:F }", dt);
            Console.WriteLine("\n\n");
            Console.WriteLine("{0:z }", dt);
            Console.WriteLine("{0:zz }", dt);
            Console.WriteLine("{0:zzz }", dt);
            Console.WriteLine("{0:t }", dt);
            Console.WriteLine("{0:tt }", dt);
			*/
		}
	}
}
