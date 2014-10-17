using System;

namespace ConsoleApplication1 {
	public class ButtonNumberPressedEventArgs : EventArgs {
		public ConsoleKey key { get; protected set; }

		public ButtonNumberPressedEventArgs (ConsoleKey key) {
			this.key = key;
		}
	}

	public class KeyPressedEventArgs : EventArgs {
		public ConsoleKey key { get; protected set; }

		public KeyPressedEventArgs (ConsoleKey key) {
			this.key = key;
		}
	}

	public class KeyboardManager {
		private ConsoleKeyInfo cki;
		public EventHandler Button3Pressed = null;
		public EventHandler Button5Pressed = null;
		public EventHandler<ButtonNumberPressedEventArgs> ButtonNumberPressed = null;
		public EventHandler<KeyPressedEventArgs> KeyPressed = null;

		protected void OnButton3Pressed (EventArgs e) {
			if (Button3Pressed != null)
				Button3Pressed (this, e);
		}

		protected void OnButton5Pressed (EventArgs e) {
			if (Button5Pressed != null)
				Button5Pressed (this, e);
		}

		protected void OnButtonNumberPressed (ButtonNumberPressedEventArgs e) {
			if (ButtonNumberPressed != null)
				ButtonNumberPressed (this, e);
		}

		protected void OnKeyPressed (KeyPressedEventArgs e) {
			if (KeyPressed != null)
				KeyPressed (this, e);
		}

		/*public KeyboardManager () {
		}*/

		public void Run () {
			do {
				cki = Console.ReadKey (true);

				if (cki.Key == ConsoleKey.D3)
					OnButton3Pressed (new EventArgs ());

				if (cki.Key == ConsoleKey.D5)
					OnButton5Pressed (new EventArgs ());

				if (cki.Key >= ConsoleKey.D0 && cki.Key <= ConsoleKey.D9)
					OnButtonNumberPressed (new ButtonNumberPressedEventArgs (cki.Key));

				OnKeyPressed (new KeyPressedEventArgs (cki.Key));

			} while (cki.Key != ConsoleKey.Escape);
		}
	}

	public class ThreeSubscriber {
		public void PrintNum (object sender, EventArgs e) {
			Console.WriteLine ("3");
		}

		public ThreeSubscriber (KeyboardManager manager) {
			manager.Button3Pressed += PrintNum;
		}
	}

	public class FiveSubscriber {
		public void PrintNum (object sender, EventArgs e) {
			Console.WriteLine ("5");
		}

		public FiveSubscriber (KeyboardManager manager) {
			manager.Button5Pressed += PrintNum;
		}
	}

	public class DigiSubscriber {
		public void Print (object sender, ButtonNumberPressedEventArgs e) {
			Console.WriteLine (e.key.ToString () [1]);
		}

		public DigiSubscriber (KeyboardManager manager) {
			manager.ButtonNumberPressed += Print;
		}
	}

	public class LogSubscriber {
		public void Print (object sender, KeyPressedEventArgs e) {
			string k;

			if (e.key >= ConsoleKey.D0 && e.key <= ConsoleKey.D9)
				k = e.key.ToString () [1].ToString ();
			else
				k = e.key.ToString ();



			Console.WriteLine (k);
			using (System.IO.StreamWriter sw = System.IO.File.AppendText("log.txt")) {
				sw.WriteLine (k);
			}
		}

		public LogSubscriber (KeyboardManager manager) {
			manager.KeyPressed += Print;
		}
	}

	class Program {
		static void Main (string[] args) {
			KeyboardManager a = new KeyboardManager ();
			ThreeSubscriber b = new ThreeSubscriber (a);
			FiveSubscriber c = new FiveSubscriber (a);
			DigiSubscriber d = new DigiSubscriber (a);
			LogSubscriber e = new LogSubscriber (a);

			a.Run ();
		}
	}
}
