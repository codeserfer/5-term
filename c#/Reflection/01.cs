namespace a {
	class test {
		public int a { get; set; }
		public int c { get; set; }
		public int b;

		public void m1 () {}
		public string m2 () {
			return "f";
		}
	}
	class a {
		public static void Main () {
			test a=new test ();
			a.a=5;
			a.b=6;

			var properties=a.GetType ().GetProperties ();
			var methods=a.GetType ().GetMethods ();
			var type=a.GetType ().GetType ();
			var q=a.GetType ().GetFields ();


		}
	}
}
