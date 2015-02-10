using System;
using System.Text;



public static class StringBuilderExtensions {
	public static Int32 IndexOf (this StringBuilder sb, Char value) {
		for (int index = 0; index < sb.Length; index++) {
			if (sb [index]==value)
				return index;
		}
		return -1;
	}
}

class Q {
	static void Main () {
		StringBuilder sb=new StringBuilder ("Hello, world!");
		Console.WriteLine (sb.IndexOf ('o'));

	}
}
