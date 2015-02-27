using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

namespace Seminar {




	class MainClass {

		private static MemoryStream SerializeToMemory (Object ObjectGraph) {
			//Конструирование потока, который будет содержать сериализованные объекты
			var Stream=new MemoryStream ();

			//Задание форматирования при сериализации
			var Formatter=new BinaryFormatter ();

			//Заставляем модуль форматирования сериализовать объекты в поток
			Formatter.Serialize (Stream, ObjectGraph);

			//Возвращение потока сериализованных объектов вызывающему методу
			return Stream;
		}

		private static Object DeserializeFromMemory (Stream Stream) {
			//Задание форматирования при сериализации
			var Formatter=new BinaryFormatter ();

			//Заставляем модуль форматирования десериализовать объекты из потока
			return Formatter.Deserialize (Stream);
		}

		public static void Main (string [] args) {
			 //Создание графа объектов для последующей сериализации в поток
			var ObjectGraph=new List<String> { "Jef", "Kristin", "Aidan", "Grant", "Oleg" };
			var Stream=SerializeToMemory (ObjectGraph);

			//Обнуляем все для данного примера
			Stream.Position=0;
			ObjectGraph=null;

			//Десериализация объектов и проверка их работоспособности
			ObjectGraph=(List<String>)DeserializeFromMemory (Stream);
			foreach (var s in ObjectGraph) {
				Console.WriteLine (s);
			}


		}
	}
}
