using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;
using System.Runtime.Serialization;
using System.IO;
using System.Collections;

namespace Database
{
    [XmlRootAttribute("db", IsNullable = false)]
    public class XMLSerializableDatabase : IDataSource
    {
        private const String sourceFileName = "serializableDatabase.xml";

        private const String groupsTag = "groups";
        private const String studentsTag = "students";

        public Group[] groups;
        public Student[] students;


        public Int32 GroupCount
        {
            get
            {
                //return Groups.ToList<Group>().Count;
                return groups.Length;
            }
        }
        public Int32 StudentCount
        {
            get
            {
                //return Students.ToList<Student>().Count;
                return students.Length;
                //return students.Count;
            }
        }

        //[XmlArrayAttribute("groups")]
        //public IEnumerable<Group> Groups { get; set; }

        //[XmlArrayAttribute("students")]
        //public IEnumerable<Student> Students { get; set; }

        [XmlIgnore]
        public IEnumerable<Group> Groups
        {
            get
            {
                return this.groups;
            }
        }

        [XmlIgnore]
        public IEnumerable<Student> Students
        {
            get
            {
                return this.students;
            }
        }

        public XMLSerializableDatabase()
        {
            //groups = new List<Group>();
            //students = new List<Student>();

            //try
            //{
            //    LoadData();
            //}
            //catch(Exception e)
            //{
            //    groups = new Group[0];
            //    students = new Student[0];

            //    Console.WriteLine(e.Message);
            //}

            groups = new Group[0];
            students = new Student[0];
        }

        public void LoadData()
        {
            try
            {
                var buffer = CustomXMLSerializer.DeserializeDatabase(sourceFileName);
                //XMLSerializableDatabase buffer = DeserializeDatabase();
                students = buffer.students;
                groups = buffer.groups;
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                students = new Student[0];
                groups = new Group[0];
            }

            /************************************************************/

            //students = new Student[buffer.StudentCount];
            //groups = new Group[buffer.GroupCount];

            //for (int i = 0; i < StudentCount; i++)
            //    students[i] = buffer.students[i];

            //for (int i = 0; i < GroupCount; i++)
            //    groups[i] = buffer.groups[i];
        }

        private void SerializeDatabase()
        {
            //Student first = new Student(1, 1, "first", 1111);
            //Student second = new Student(2, 2, "second", 2222);

            //Group _first = new Group(1, "_first");
            //Group _second = new Group(2, "_second");

            //List<Student> _students = new List<Student>();
            //_students.Add(first);
            //_students.Add(second);
            ////Students = students;

            //List<Group> _groups = new List<Group>();
            //_groups.Add(_first);
            //_groups.Add(_second);

            //students = _students.ToArray();
            //groups = _groups.ToArray();

            //Groups = groups;

            ////Students.ToList<Student>().Add(first);
            //Students.ToList<Student>().Add(second);
            //Console.WriteLine(Students.ToList<Student>()[0].EnrollYear);
            //Groups.ToList<Group>().Add(_first);
            //Groups.ToList<Group>().Add(_second);

            //foreach (Student elem in students)
            //    Console.WriteLine(elem.EnrollYear);

            using (FileStream stream = new FileStream(sourceFileName, FileMode.Create))
            {
                XmlSerializer XML = new XmlSerializer(typeof(XMLSerializableDatabase));
                XML.Serialize(stream, this);
            }
        }
        private XMLSerializableDatabase DeserializeDatabase()
        {
            using (FileStream stream = new FileStream(sourceFileName, FileMode.Open))
            {
                XmlSerializer XML = new XmlSerializer(typeof(XMLSerializableDatabase));
                return (XMLSerializableDatabase)XML.Deserialize(stream);
            }
        }
    }

    internal static class CustomXMLSerializer
    {
        private static void SerializeDatabase(String sourceFileName, XMLSerializableDatabase o)
        {
            using (FileStream stream = new FileStream(sourceFileName, FileMode.Create))
            {
                XmlSerializer XML = new XmlSerializer(typeof(XMLSerializableDatabase));
                XML.Serialize(stream, o);
            }
        }
        public static XMLSerializableDatabase DeserializeDatabase(String sourceFileName)
        {
            using (FileStream stream = new FileStream(sourceFileName, FileMode.Open))
            {
                XmlSerializer XML = new XmlSerializer(typeof(XMLSerializableDatabase));
                return (XMLSerializableDatabase)XML.Deserialize(stream);
            }
        }
    }
}
