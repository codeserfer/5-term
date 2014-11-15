using System;

namespace seminar_15 {


	public class Triangle {
		private int a, b, c;
		public Triangle (int a, int b, int c) {
			if (a>=b+c || b>=a+c || c>=a+b || a<=0 || b<=0 || c<=0) 
				throw new TriangleException (a, b, c);
			this.a=a;
			this.b=b;
			this.c=c;
		}
	}

	public class Quadrangle {
		private int a, b, c, d;
		public Quadrangle (int a, int b, int c, int d) {
			if (a>=b+c+d || b>=a+c+d || c>=a+b+d || d>=a+b+c || a<=0 || b<=0 || c<=0 || d<=0)
				throw new QuadrangleException (a, b, c, d);

			this.a=a;
			this.b=b;
			this.c=c;
			this.d=d;
		}
	}

	public class Circle { 
		private int r;
		public Circle (int r) {
			if (r<=0)
				throw new CircleException (r);
			this.r=r;
		}
	}


	public class GeometryException : System.Exception {
		public virtual Int32[] Parametrs { get; private set; }
		public GeometryException (Int32[] Parametrs) {
			this.Parametrs=Parametrs;
		}
	}

	public class TriangleException : GeometryException {
		public TriangleException (int a, int b, int c) : base (new Int32[] {a, b, c}) {}
	}

	public class QuadrangleException : GeometryException {
		public QuadrangleException (int a, int b, int c, int d) : base (new Int32[] {a, b, c, d}) {}
	}

	public class CircleException : GeometryException {
		public CircleException (int r) : base (new Int32[] {r}) {}
	}


	class MainClass {
		public static void Main (string [] args) {

			Random rnd = new Random();

			System.IO.StreamWriter file1 = new System.IO.StreamWriter(@"file1.txt");
			System.IO.StreamWriter file2 = new System.IO.StreamWriter(@"file2.txt");

			for (int i = 0; i < 1000; i++)
			{
				try
				{
					int a = rnd.Next(10);
					int b = rnd.Next(10);
					int c = rnd.Next(10);
					int d = rnd.Next(10);

					try
					{
						switch (rnd.Next(3))
						{
							case 0:
							new Triangle(a, b, c);
							break;
							case 1:
							new Quadrangle(a, b, c, d);
							break;
							case 2:
							new Circle(a);
							break;
						}
					}
					catch (TriangleException e)
					{
						string str = "Triangle";
						foreach (int j in e.Parametrs)
							str += (":" + j.ToString());
						file1.WriteLine(str);

						Console.WriteLine(str);
						throw;
					}
					catch (QuadrangleException e)
					{
						string str = "Quadrangle";
						foreach (int j in e.Parametrs)
							str += (":" + j.ToString());
						file1.WriteLine(str);

						Console.WriteLine(str);
						throw;
					}
				}
				catch (GeometryException e)
				{
					string str;
					switch (e.Parametrs.Length)
					{
						case 1:
						str = "Circle";
						break;
						case 3:
						str = "Triangle";
						break;
						case 4:
						str = "Quadrangle";
						break;
						default:
						str = "Figure";
						break;
					}

					foreach (int j in e.Parametrs)
						str += (":" + j.ToString());
					file2.WriteLine(str);
					Console.WriteLine(str);
				}
			}
			file1.Close();
			file2.Close();
			Console.ReadLine();
		}
	}
}
