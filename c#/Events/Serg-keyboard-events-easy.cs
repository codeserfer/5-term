using System;

namespace ConsoleApplication1 {
	public class KeyboardManager {
		private ConsoleKeyInfo cki;
		public EventHandler Button3Pressed=null;

		protected void OnButton3Pressed (EventArgs e) {
			if (Button3Pressed!=null)
				Button3Pressed (this, e);
		}

		public void Run () {
			do {
				cki=Console.ReadKey (true);

				if (cki.Key==ConsoleKey.D3)
					OnButton3Pressed (new EventArgs ());

			}
			while (cki.Key != ConsoleKey.Escape);
		}
	}

	public class ThreeSubscriber {
		public void PrintNum (object sender, EventArgs e) {
			Console.WriteLine ("3");
		}

		public ThreeSubscriber (KeyboardManager manager) {
			manager.Button3Pressed+=PrintNum;
		}
	}

	class Program {
		static void Main (string [] args) {
			KeyboardManager a=new KeyboardManager ();

			ThreeSubscriber b=new ThreeSubscriber (a);


			a.Run ();
		}
	}
}
