using System;

namespace ConsoleApplication1
{
    class Quad<A,B,C,D>
    {
        public A first {get; set;}
        public B second {get; set;}
        public C third {get; set;}
        public D fourth {get; set;}

        public Quad(A a, B b, C c, D d)
        {
            first = a;
            second = b;
            third = c;
            fourth = d;
        }

        public override bool Equals(object obj)
        {
            if (obj == null)
                return false;

            Quad<A, B, C, D>tmp = obj as Quad<A, B, C, D>;
            if (tmp == null)
                return false;

            return (first.Equals(tmp.first) &&
                    second.Equals(tmp.second) &&
                    third.Equals(tmp.third) &&
                    fourth.Equals(tmp.fourth));
        }

        public override int GetHashCode()
        {
            return first.GetHashCode() + second.GetHashCode() + third.GetHashCode() + fourth.GetHashCode();
        }

        public static bool operator==(Quad<A,B,C,D> a, Quad<A,B,C,D>b)
        {
            return a.Equals(b);
        }

        public static bool operator!=(Quad<A, B, C, D> a, Quad<A, B, C, D> b)
        {
            return !a.Equals(b);
        }

    }

    class Program
    {
        static void Main(string[] args)
        {
            Quad<Int32, Boolean, long, String> q1 =
                new Quad<int, bool, long, string>(10, false, 100, "abc");
            Quad<Int32, Boolean, long, String> q2 =
                new Quad<int, bool, long, string>(10, false, 100, "abc");
            Quad<Int32, Boolean, long, String> q3 =
                new Quad<int, bool, long, string>(10, false, 100, "abcd");


            Console.WriteLine(q1 == q2);

            Console.WriteLine(q1.Equals(q2));
            Console.WriteLine(q1.Equals(q3));

            Console.Read();
        }
    }
}
