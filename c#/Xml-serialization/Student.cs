using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace Database
{
    [Serializable]
    [XmlTypeAttribute("student")]
    public class Student
    {
        public Student() { }
        public Student(Int32 ID, Int32 GroupID, String Name, Int32 EnrollYear)
        {
            this.ID = ID;
            this.GroupID = GroupID;
            this.Name = Name;
            this.EnrollYear = EnrollYear;
        }
        public override string ToString()
        {
            return (String.Format("№{0} Группа №{1} Имя: {2} Год: {3}", ID, GroupID, Name, EnrollYear));
        }

        [XmlAttributeAttribute]
        public Int32 ID { get; set; }

        [XmlAttributeAttribute]
        public Int32 GroupID { get; set; }

        [XmlAttributeAttribute]
        public String Name { get; set; }

        [XmlAttributeAttribute]
        public Int32 EnrollYear { get; set; }
    }
}
