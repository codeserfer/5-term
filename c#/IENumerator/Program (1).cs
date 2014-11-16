
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Data.Odbc;
using System.IO;
using System.Xml;

namespace DatabaseReading
{

    public class Group
    {

        public int Id { get; set; }

        public string Description { get; set; }

        public Group(int id, string description)
        {
            this.Id = id;
            this.Description = description;
        }

        public override string ToString()
        {
            return Id + " " + Description;
        }
    }

    public class Student
    {
        public Student(int id, int groupId, string name, int enrollYear)
        {
            this.EnrollYear = enrollYear;
            this.Id = id;
            this.GroupId = groupId;
            this.Name = name;
        }

        public int EnrollYear { get; set; }

        public int Id { get; set; }

        public int GroupId { get; set; }

        public string Name { get; set; }

        public override string ToString()
        {
            return Id + " " + GroupId + " " + Name + " " + EnrollYear;
        }
    }

    public interface IDataSource
    {
        void LoadData();
        int GroupCount { get; }
        int StudentCount { get; }
        IEnumerable<Group> Groups { get; }
        IEnumerable<Student> Students { get; }
    }

    class CustomArray<T> : IEnumerable<T>
    {
        private T[] a;

        public CustomArray(int n)
        {
            a = new T[n];
        }

        public T this[int i]
        {
            get { return a[i]; }
            set { a[i] = value; }
        }

        public IEnumerator<T> GetEnumerator()
        {
            return new CustomArrayEnumerator<T>(a);
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }

    public class CustomArrayEnumerator<T> : IEnumerator<T>
    {
        private int i;
        private T[] a;

        public CustomArrayEnumerator(T[] a)
        {
            this.a = a;
            i = -1;
        }

        public bool MoveNext()
        {
            i++;
            return i != a.Length;
        }

        public void Reset()
        {
            i = -1;
        }

        public T Current { get { return a[i]; } private set { a[i] = value; } }

        object IEnumerator.Current
        {
            get { return Current; }
        }

        public void Dispose()
        {

        }
    }

    public class TxtDataSource : IDataSource
    {
        private FileStream fs;
        private StreamReader sw;

        private CustomArray<Group> groups;
        private CustomArray<Student> students;

        private void OpenFile()
        {
            fs = new FileStream("DB\\file.txt", FileMode.Open);
            sw = new StreamReader(fs);
        }
        private void CloseFile()
        {
            sw.Close();
            fs.Close();
        }
        public void LoadData()
        {
            OpenFile();
            int n = Int32.Parse(sw.ReadLine());
            groups = new CustomArray<Group>(n);
            for (int i = 0; i < n; i ++)
            {
                string s = sw.ReadLine();
                string[] sA = s.Split(';');
                Group gr = new Group(Int32.Parse(sA[0]), sA[1]);
                groups[i] = gr;
            }
            n = Int32.Parse(sw.ReadLine());
            students = new CustomArray<Student>(n);
            for (int i = 0; i < n; i++)
            {
                string s = sw.ReadLine();
                string[] sA = s.Split(';');
                Student st = new Student(Int32.Parse(sA[0]), Int32.Parse(sA[1]), sA[2], Int32.Parse(sA[3]));
                students[i] = st;
            }

            CloseFile();

            Console.WriteLine("TXT");
            foreach (var @group in Groups)
            {
                Console.WriteLine(@group);
            }

            foreach (var student in students)
            {
                Console.WriteLine(student);
            }

            Console.WriteLine("Групп: {0}, студентов: {1}", GroupCount, StudentCount);

        }

        public int GroupCount {
            get
            {
                OpenFile();
                int n = Int32.Parse(sw.ReadLine());
                CloseFile();
                return n;
            }
        }
        public int StudentCount {
            get
            {
                OpenFile();
                int n = Int32.Parse(sw.ReadLine());
                for (int i = 0; i < n; i++)
                    sw.ReadLine();
                n = Int32.Parse(sw.ReadLine());
                CloseFile();
                return n;
            }
        }

        public IEnumerable<Group> Groups {
            get { return groups; }
        }

        public IEnumerable<Student> Students
        {
            get { return (IEnumerable<Student>) students.GetEnumerator(); }
        }
    }

    public class SqlDataSource : IDataSource
    {

        private OdbcConnection connection; // для открытия доступа
        private OdbcDataAdapter dataAdapter; //для загрузки РБД в ВБД
        //private OdbcCommandBuilder commandBuilder; //для сохранения изменений в РБД
        private DataSet dataSet; // для соединения
        private DataTable dataTable; //объявление таблицы (ВБД)

        private CustomArray<Group> groups;
        private CustomArray<Student> students;

        private void OpenConnection()
        {
            connection = new OdbcConnection("Driver={Microsoft Access Driver (*.mdb)};DBQ=DB\\Database11.mdb;"); //источник данных
            connection.Open();   
        }

        private void Query(string sqlQuery)
        {
            dataAdapter = new OdbcDataAdapter(sqlQuery, connection);
            //commandBuilder = new OdbcCommandBuilder(dataAdapter);
            dataSet = new DataSet();
            dataAdapter.Fill(dataSet);
        }

        private void CloseConnection()
        {
            connection.Close();
        }

        public void LoadData()
        {
            OpenConnection();

            Query("select * from Groups");
            int n = dataSet.Tables[0].Rows.Count;
            groups = new CustomArray<Group>(n);
            for (int i = 0; i < n; i++)
                groups[i] = new Group(Int32.Parse(dataSet.Tables[0].Rows[i]["Id"].ToString()), 
                    dataSet.Tables[0].Rows[i]["Description"].ToString());

            Query("select * from Students");

            n = dataSet.Tables[0].Rows.Count;
            students = new CustomArray<Student>(n);
            for (int i = 0; i < n; i++)
                students[i] = new Student(
                    Int32.Parse(dataSet.Tables[0].Rows[i]["Id"].ToString()),
                    Int32.Parse(dataSet.Tables[0].Rows[i]["GroupId"].ToString()),
                    dataSet.Tables[0].Rows[i]["Name"].ToString(),
                    Int32.Parse(dataSet.Tables[0].Rows[i]["EnrollYear"].ToString())
                    );

            CloseConnection();

            Console.WriteLine("Sql");

            foreach (var @group in groups)
            {
                Console.WriteLine(@group);
            }

            foreach (var student in students)
            {
                Console.WriteLine(student);
            }

            Console.WriteLine("Групп: {0}, студентов: {1}", GroupCount, StudentCount);
        }

        public int GroupCount
        {
            get
            {
                OpenConnection();
                Query("select id from Groups");
                int n = dataSet.Tables[0].Rows.Count;
                CloseConnection();
                return n;
            }
        }

        public int StudentCount
        {   
            get
            {
                OpenConnection();
                Query("select id from Students");
                int n = dataSet.Tables[0].Rows.Count;
                CloseConnection();
                return n;
            }
        }

        public IEnumerable<Group> Groups
        {
            get
            {
                return groups;
            }
        }
        public IEnumerable<Student> Students 
        {
            get { return students; }
        }
    }

    public class XmlDataSource : IDataSource
    {
        private XmlDocument xd;
        private FileStream fs;

        private CustomArray<Group> groups;
        private CustomArray<Student> students;

        private void OpenXML()
        {
            xd = new XmlDocument();
            fs = new FileStream("DB\\xml.xml", FileMode.Open);
            xd.Load(fs);
        }

        private void CloseXml()
        {
            fs.Close();
        }

        public void LoadData()
        {
            OpenXML();

            XmlNodeList xnl = xd.GetElementsByTagName("group");
            groups = new CustomArray<Group>(xnl.Count);
            for (int i = 0; i < xnl.Count; i++)
                groups[i] = new Group(
                    Int32.Parse(xnl.Item(i).Attributes["id"].InnerText),
                    xnl.Item(i).Attributes["description"].InnerText);

            xnl = xd.GetElementsByTagName("student");
            students = new CustomArray<Student>(xnl.Count);
            for (int i = 0; i < xnl.Count; i++)
                students[i] = new Student(
                    Int32.Parse(xnl.Item(i).Attributes["id"].InnerText),
                    Int32.Parse(xnl.Item(i).Attributes["groupid"].InnerText),
                    xnl.Item(i).Attributes["name"].InnerText,
                    Int32.Parse(xnl.Item(i).Attributes["year"].InnerText));

            CloseXml();

            Console.WriteLine("XML");

            foreach (var @group in groups)
            {
                Console.WriteLine(@group);
            }

            foreach (var student in students)
            {
                Console.WriteLine(student);
            }

            Console.WriteLine("Групп: {0}, студентов: {1}", GroupCount, StudentCount);

        }

        public int GroupCount
        {
            get
            {
                OpenXML();
                int n = xd.GetElementsByTagName("group").Count;
                CloseXml();
                return n;
            }
        }

        public int StudentCount {
            get
            {
                OpenXML();
                int n = xd.GetElementsByTagName("student").Count;
                CloseXml();
                return n;
            }
        }
        public IEnumerable<Group> Groups {
            get { return groups; }
        }
        public IEnumerable<Student> Students {
            get { return students; }
        }
    }

    class Program
    {
        static void Main()
        {
            IDataSource a = new TxtDataSource();
            a.LoadData();
            a = new SqlDataSource();
            a.LoadData();
            a = new XmlDataSource();
            a.LoadData();
            Console.ReadKey();
        }
    }
}
