using System;

namespace hw_3 {
	public class Base {
		public virtual void OverrideMethod() {
			Console.WriteLine("Базовый класс");
		}

		public virtual void NewMethod() {
			Console.WriteLine("Базовый класс");
		}
	}


	public class Derived : Base {
        public override void OverrideMethod() {
            Console.WriteLine("Производный класс");
        }

        public new void NewMethod() {
            Console.WriteLine("Производный класс");
        }
    }

	class MainClass {
		public static void Main (string[] args) {
			Base a = new Base();
            Derived b = new Derived();
            Base c = new Derived();


            a.OverrideMethod();
            a.NewMethod();

            b.OverrideMethod();
            b.NewMethod();

            c.OverrideMethod();
            c.NewMethod();
		}
	}
}

