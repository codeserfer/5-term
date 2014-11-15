using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace Database
{
    [Serializable]
    [XmlTypeAttribute("group")]
    public class Group
    {
        public Group() { }
        public Group(Int32 ID, String Description)
        {
            this.ID = ID;
            this.Description = Description;
        }
        public override string ToString()
        {
            return (String.Format("№{0} - Группа {1}", ID, Description));
        }

        [XmlAttributeAttribute]
        public Int32 ID { get; /*private*/ set; }

        [XmlAttributeAttribute]
        public String Description { get; /*private*/ set; }
    }
}
