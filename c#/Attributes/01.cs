namespace s {

	public class NameAttribute : System.Attribute {
		private string description;
		public NameAttribute (string description) {
			this.description=description;
		}
	}

	public class FirstClass {
		[Name("1")]
		public int first {get; set;}
		[Name("2")]
		public int second {get; set; }
		[Name("3")]
		public int third { get; set; }
		[Name("4")]
		public int fourth {get; set; }
		[Name("5")]
		public int fifth {get; set; }
		[Name("6")]
		public int sixth {get; set; }
		[Name("7")]
		public int seventh {get; set; }
		[Name("8")]
		public int eigths {get; set; }
		[Name("9")]
		public int nineth { get; set; }
		[Name("10")]
		public int tenth {get; set; }
	}

	class s {
		public static void Main () {
			FirstClass q=new FirstClass { 	
				first=1,
				second=2,
				third=3,
				fourth=4,
				fifth=5,
				sixth=6,
				seventh=7,
				eigths=8,
				nineth=9,
				tenth=10
			};

			var prop=q.GetType ().GetProperties ();
			foreach (var a in prop) {
				if (a!=null) {
					Console.WriteLine (a.Name);
					var attr=(NameAttribute)Attribute.GetCustomAttribute (a, typeof(NameAttribute));
				}
			
			}
		}
	}
}
