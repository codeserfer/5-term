using System;
using System.Collections;
using System.Collections.Generic;
using System.Data.Linq;
using System.Linq;

namespace q {

	public class Customer {
		public int Key {get; set; }
		public string Name {get; set; }
		public string City { get; set; }
		public int Bought { get; set; }
		public override string ToString () {
			return string.Format ("[Distributor: Key={0}, Name={1}, City={2}, Bought={3}]", Key, Name, City, Bought);
		}
	}

	public class Distributor {
		public int Key { get; set; }
		public string Name {get; set; }
		public string City {get; set; }
		public override string ToString () {
			return string.Format ("[Distributor: Key={0}, Name={1}, City={2}]", Key, Name, City);
		}
	}



	public class q {
		public static void Main () {
			var Customers=new Customer [] {
				new Customer {Key=1, Name="Петров", City="Москва", Bought=10},
				new Customer {Key=2, Name="Иванов", City="Калуга", Bought=15},
				new Customer {Key=3, Name="Сидоров", City="Москва", Bought=20},
				new Customer {Key=4, Name="Сергеев", City="Санкт-Петербург", Bought=4},
				new Customer {Key=5, Name="Александров", City="Санкт-Петербург", Bought=7},
				new Customer {Key=6, Name="Михайлов", City="Санкт-Петербург", Bought=34},
				new Customer {Key=7, Name="Дмитриевский", City="Москва", Bought=23},
				new Customer {Key=8, Name="Филиппов", City="Калуга", Bought=11}

			};

			var Distributors=new Distributor [] {
				new Distributor {Key=1, Name="Технотрейд", City="Москва"},
				new Distributor {Key=2, Name="Моби", City="Калуга"},
				new Distributor {Key=3, Name="R-Style", City="Чехов"},
				new Distributor {Key=4, Name="М-Видео", City="Санкт-Петербург"},

			};


			//1. Отобрать всех клиентов
			var allcustoms=Customers.Select (cust=>cust);

			//2. Отобрать клиетов из Москвы
			var moscowCustoms=Customers.Where (cust => cust.City=="Москва");

			//3. Отобрать клиентов из Москвы и Санкт-Петербурга
			var mskspbCustomers1=Customers.Where (cust => cust.City=="Москва" || cust.City=="Санкт-Петербург");

			//4. Отобрать клиентов из Санкт-Петербурга и упорядочить по имени
			var spbOrderedCustomers1=Customers.Where (cust => cust.City=="Санкт-Петербург").OrderBy (cust => cust.Name);

			//5. Сгруппировать клиентов по городам
			var customerByCity=Customers.Select (cust => cust).GroupBy (cust => cust.City);

			//Вывод для этого GroupBy
			foreach (var q in customerByCity) {
				Console.WriteLine (q.Key);
				foreach (var customers in q) {
					Console.WriteLine (customers);
				}
			}

			//6. Сгруппировать клиентов по городам и отобрать только те группы, у которых клиентов больше 2
			var customersByCityGreatedTwo1=Customers.Select (cust => cust)
				.GroupBy (cust => cust.City)
					.Where (custGroup => custGroup.Count()>2);


			//7. 	Отобрать суммы купленных товаров клиентов с группировкой по городам, то есть
			//		результирующая коллекция есть кортеж из имени и суммы
			/*var boughtByCity2=Customers	.GroupBy (cust => cust.City)
										.Select (custGroup =>
				         					new {
												City=custGroup.Key;
												TotalBought=custGroup.Sum (r => r.Gought)
											});
		

			*/
			//8. Для каждого клиента найти дистрибьютора по городу. В итоге собрать коллекцию кортежей
			// {Customer.Name, Distributors.Name}
			/*var distributorToCustomer1=Customers.Join (Distributors,
			                                           cust=>cust.City,
			                                           dist=>dist.City,
			                                           (cust, dist) => new {
				CustomerName=cust.Name;
				DistributorName=dist.Name;
			});*/


			//9. Выбрать клентов в одну строку вида:
			// клиент1, клиент2, клиент3
			// Использовать Agregate
			//var customersAgregated=Customers.Select(cust => cust.Name).Aggregate ((currentCust, nextCust) => a+", "+b);


			/*foreach (var q in boughtByCity2) {
				Console.WriteLine (q.Key);
				foreach (var customers in q) {
					Console.WriteLine (customers);
				}
			}*/




		}

	}



}
