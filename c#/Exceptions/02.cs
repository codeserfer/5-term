using System;
using System.Collections.Generic;

namespace man {

	public class ManException : Exception {
		public ManException (string message) : base (message) {}
	}

	public class TeenagerException : ManException {
		public TeenagerException () : base ("Возраст подростка должен быть от 14 до 19") {}
	}

	public class WorkerException : ManException {
		public WorkerException () : base ("Возраст работника должен быть от 16 до 70") {}
	}



	class Man {
		protected string _name;
		protected int _age;
		public string name{ get; set; }
		public virtual int age{ get; set;}
		public override string ToString() {
			return "Человек, " + name + ", " + age;
		}
	}
	class Teenager : Man {
		public override int age {
			get {
				return _age;
			}
			set {
				try {
					if (value<13 || value>19)
						throw new TeenagerException ();
					_age = value;
				}
				catch (TeenagerException e) {
					Console.WriteLine (e.Message);
				}
			}
		}
		public override string ToString() {
			return "Подросток, " + name + ", " + age;
		}
	}
	class Worker : Man {
		public override int age {
			get {
				return _age;
			}
			set {
				try {
					if (value < 16 || value > 70) throw new WorkerException();
					_age = value;
				}
				catch (WorkerException e) {
					Console.WriteLine (e.Message);
				}
			}

		}
		public override string ToString() {
			return "Работник, " + name + ", " + age;
		}
	}
	class List {
		Man[] mass;
		public List (params Man[] values) {
			mass = new Man[values.Length];
			for (int i = 0; i < values.Length; i++) mass[i] = values[i];
		}
		public Man this[int i] {
			set {
				mass[i] = value;
			}
			get {
				return mass[i];
			}
		}
	}
	class Program {

		private static void Add (ref List<Man> array, params Man[] values) {
			for (int i = 0; i < values.Length; i++) {
				array.Add (values [i]);
			}
		}


		static void Main(string[] args) {
			Man m=new Man { name="Михаил", age=28 };
			Teenager t=new Teenager { name="олег", age=53 };
			Worker w=new Worker {name="Борис", age=69 };
			Teenager tt=new Teenager { name="Антон", age=14 };
			Worker ww=new Worker { name="Петр", age=2 };

			List<Man> l = new List<Man>();
			Add (ref l, m, t, w, tt, ww);

			foreach (var man in l) {
				Console.WriteLine (man);
			}
		}
	}
}
