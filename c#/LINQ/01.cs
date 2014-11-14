using System;
using System.Collections;
using System.Collections.Generic;
using System.Data.Linq;
using System.Linq;

namespace seminar_1 {
	public class Record {
		public int ID { get; set; }

		public string Name { get; set; }

		public int? ParentID { get; set; }

		public Record (int id, string name, int? parentID) {
			this.ID=id;
			this.Name=name;
			this.ParentID=parentID;
		}
	}

	class MainClass {
		public static void Main (string [] args) {


			var r1=new Record (1, "Marc", null);
			var r2=new  {Id=1, Name="Mark", ParentID=(int?)null};

			var r11=new Record (1, "Mark", null);
			var r12=new Record (2, "Ivan", 1);
			var r13=new Record (3, "Vasay", 1);

			var l=new List<Record> { r11, r12, r13 };


			var l2=from r in l 
					where r.ParentID==1
					select new {Identificator=r.ID, FIO=r.Name};

			var l3=l.Where (r => r.ParentID==1)
				.Select (r => new {Identificator =r.ID, FIO=r.Name});


			foreach (var q in l2) {
				Console.WriteLine (q);
			}






		}
	}
}