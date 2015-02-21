using System;
using System.Collections.Generic;
using System.Reflection;

namespace homework {
	[AttributeUsage(AttributeTargets.Field | AttributeTargets.Property | AttributeTargets.Struct, AllowMultiple = true)]
	class ValidateInt32Attribute : System.Attribute {
		public Int32 MinValue { get; set; }
		public Int32 MaxValue { get; set; }
		public Boolean ZeroEnabled { get; set; }

		public ValidateInt32Attribute(Int32 MinValue, Int32 MaxValue, Boolean ZeroEnabled) {
			this.MinValue = MinValue;
			this.MaxValue = MaxValue;
			this.ZeroEnabled = ZeroEnabled;
		}
	}

	class InvalidValueException : Exception {
		public String FieldOrPropertyName; //Имя поля или свойства
		public List<ValidateInt32Attribute> Limitations; //Список ограничений поля
		public Int32 CurrentFieldOrPropertyValue; //Текущее значение свойства или поля 
		public String ErrorDescription; //Текстовое описание ошибки

		public InvalidValueException(	String fieldOrPropertyName, 
		                             	ValidateInt32Attribute[] limitations,
		                             	Int32 currentFieldOrPropertyValue, 
		                             	String errorDescription = "Выход за ограничения."
		                             ){
			this.FieldOrPropertyName = fieldOrPropertyName;

			this.Limitations = new List<ValidateInt32Attribute>();
			foreach (ValidateInt32Attribute a in limitations)
				this.Limitations.Add(a);

			this.CurrentFieldOrPropertyValue = currentFieldOrPropertyValue;

			this.ErrorDescription = errorDescription;
		}
	}
	static class Int32Validate {
		public static void Validate(Object o) {
			ValidateFields(o);
			ValidateProperties(o);
		}

		private static void ValidateFields(Object o) {
			Type t = o.GetType();

			/*
             * t.GetFields() возвращает все открытые поля объекта t.
             * FieldInfo извлекает атрибуты и обеспечивает доступ к метаданным поля.
             */
			FieldInfo[] fields = t.GetFields();

			foreach (FieldInfo f in fields)
			{
				/* Attribute.GetCustomAttributes(f, typeof(ValidateInt32Attribute)) извлекает массив настраиваемых атрибутов */
				ValidateInt32Attribute[] validateAttributes = (ValidateInt32Attribute[])Attribute.GetCustomAttributes(f, typeof(ValidateInt32Attribute));

				if (f.FieldType == typeof(Int32) && validateAttributes != null)
					foreach (ValidateInt32Attribute a in validateAttributes)
						if ((Int32)f.GetValue(o) > a.MaxValue || (Int32)f.GetValue(o) < a.MinValue || ((Int32)f.GetValue(o) == 0 && a.ZeroEnabled == false))
							throw new InvalidValueException(f.Name, validateAttributes, (Int32)f.GetValue(o));
			}
		}

		private static void ValidateProperties(Object o) {
			Type t = o.GetType();

			PropertyInfo[] properties = t.GetProperties();
			foreach (PropertyInfo p in properties) {
				ValidateInt32Attribute[] validateAttributes =
					(ValidateInt32Attribute[])Attribute.GetCustomAttributes(p, typeof(ValidateInt32Attribute));

				if (p.PropertyType == typeof(Int32) && validateAttributes != null)
					foreach (ValidateInt32Attribute a in validateAttributes)

						//p.GetValue (

						if ((Int32)p.GetValue(o, null) > a.MaxValue ||
						    (Int32)p.GetValue(o, null) < a.MinValue ||
						    ((Int32)p.GetValue(o, null) == 0 && a.ZeroEnabled == false))
								throw new InvalidValueException(p.Name, validateAttributes, (Int32)p.GetValue(o, null));
			}
		}
	}

	class CustomClass {
		[field: ValidateInt32Attribute(12, 34, true)]
		[field: ValidateInt32Attribute(12, 30, true)]
		public Int32 a;

		[field: ValidateInt32Attribute(-3, 7, false)]
		public Int32 b;

		[property: ValidateInt32Attribute(-3, 28, true)]
		public Int32 c { get; set; }

		[property: ValidateInt32Attribute(-3, 28, true)]
		public String d { get; set; }

		public CustomClass(Int32 a = 33, Int32 b = 0, Int32 c = 29, String d = "Your number has been called.") {
			this.a = a;
			this.b = b;
			this.c = c;
			this.d = d;
		}
	}

	class Program {
		static void Main() {

			try {
				CustomClass cc = new CustomClass();
				Int32Validate.Validate(cc);

				CustomClass q=new CustomClass (0, 0, 333, "ERROR!");
				Int32Validate.Validate (q);

			}
			catch (InvalidValueException e) {
				Console.WriteLine(e.FieldOrPropertyName);
				foreach (ValidateInt32Attribute a in e.Limitations)
					Console.WriteLine(a.MaxValue);
			}

		}
	}
}
