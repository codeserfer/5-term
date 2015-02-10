using System;



public class Rational {
	int a, b;

	public Rational (int a, int b) {
		this.a=a;
		this.b=b;
	}

	public Rational (Int32 a): this (a, 1) {}
	public Int32 ToInt32 () {
		return a;
	}

	public static implicit operator Rational (Int32 a) {
		return new Rational (a);
	}

	public static explicit operator Int32 (Rational a) {
		return a.ToInt32 ();
	}

	public override string ToString () {
		return (a+"/"+b);
	}

}


class Q {
	static void Main () {
		Rational a=new Rational (1, 2);
		Console.WriteLine (a);
		Rational b=new Rational (2);
		Console.WriteLine (b);

		Rational c=5;
		Console.WriteLine (c);

		Int32 x=(Int32)a;
		Console.WriteLine (x);

	}
}
