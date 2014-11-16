using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DisposeExample
{
    class Num : IDisposable
    {
        private FileStream _innerFile = null;

        public Num()
        {
            _innerFile = new FileStream("num.txt", FileMode.OpenOrCreate);
        }

        ~Num()
        {
            if (_innerFile != null)
            {
                try
                {
                    _innerFile.Close();
                }
                catch { }
            }
        }

        public void Dispose()
        {
            if(_innerFile != null)
                _innerFile.Close();

            _innerFile = null;

            GC.SuppressFinalize(this);
        }

        public int Read()
        {
            if (_innerFile == null)
                throw new ObjectDisposedException("");

            var buf = new byte[4];
            _innerFile.Seek(0, SeekOrigin.Begin);
            _innerFile.Read(buf, 0, 4);

            return BitConverter.ToInt32(buf, 0);
        }

        public void Write(int num)
        {
            if (_innerFile == null)
                throw new ObjectDisposedException("");

            var buf = BitConverter.GetBytes(num);
            _innerFile.Seek(0, SeekOrigin.Begin);
            _innerFile.Write(buf, 0, buf.Length);
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
        }
    }
}
