using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ЛР2_Шмелева_Тарикова_Хаак__РС_
{
    /* Явные схемы с разностями против потока */

    class ImplicitMethodStencil
    {
        Double[,] matrix;

        /* Использование устойчивой/неустойчивой схемы для данного a. */
        private Boolean stability;

        /* Коэффициент при dv/dx в правой части уравнения переноса. */
        private Double a;

        /* Шаги сетки. */
        private Double tau, h;

        private Double k { get { return (a * tau) / h; } }

        /* Время сечения. */
        private UInt32 t;
        private const Int32 x = 1;

        /* Начальное условие v(0,x)=\psi(x). */
        public delegate Double Function(Double argument);

        public ImplicitMethodStencil(Double a, UInt32 n, UInt32 m, UInt32 t, Function function, Boolean stability = true)
        {
            if ((this.a = a) == 0)
            {
                throw new ArgumentException("a = 0");
            }

            this.h = (Double)x/n;
            this.tau = (Double)(this.t = t) / m;

            Console.WriteLine("a: " +  a);
            Console.WriteLine("h: " + h);
            Console.WriteLine("tau: " + tau);
            Console.WriteLine("k: " + k);

            matrix = new Double[n + 1, m + 1];

            /* Из начальных условий function определяем нулевой слой по времени. */
            for (Int32 i = 0; i < matrix.GetLength(0); i++)
            {
                matrix[i, 0] = function(i * h);
            }

            if (this.stability = stability)
            {
                if (a > 0)
                {
                    //Left();
                    Right();
                }
                else
                {
                    //Right();
                    Left();
                }
            }
            else
            {
                if (a > 0)
                {
                    //Right();
                    Left();
                }
                else
                {
                    //Left();
                    Right();
                }
            }
        }

        public Double[] Slice(Int32 level = 0)
        {
            Double[] result = new Double[matrix.GetLength(0)];

            for (Int32 i = 0; i < matrix.GetLength(0); i++)
            {
                result[i] = matrix[i, level];
            }

            return result;
        }

        private void Left()
        {
            for (Int32 j = 1; j < matrix.GetLength(1); j++)
            {
                for (Int32 i = 1; i < matrix.GetLength(0); i++)
                {
                    //matrix[i, j] = matrix[i, j - 1] - k * (matrix[i, j - 1] - matrix[i - 1, j - 1]);
                    matrix[i, j] = matrix[i, j - 1] + k * (matrix[i, j - 1] - matrix[i - 1, j - 1]);
                }
                matrix[0, j] = matrix[matrix.GetLength(0) - 1, j];
            }
        }

        private void Right()
        {
            for (Int32 j = 1; j < matrix.GetLength(1); j++)
            {
                for (Int32 i = 0; i < matrix.GetLength(0) - 1; i++)
                {
                    //matrix[i, j] = matrix[i, j - 1] - k * (matrix[i + 1, j - 1] - matrix[i, j - 1]);
                    matrix[i, j] = matrix[i, j - 1] + k * (matrix[i + 1, j - 1] - matrix[i, j - 1]);
                }
                matrix[matrix.GetLength(0) - 1, j] = matrix[0, j];
            }
        }
    }
}
