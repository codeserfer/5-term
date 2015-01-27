using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CourceProject
{
    public static class BarChart
    {
        public static List<List<double>> bar_chart = new List<List<double>>();
        public static int prv = 0;
    }


    /// <summary>
    /// Класс для хранения обработанных текстов-образцов
    /// </summary>
    public static class Samples
    {
        private static List<String> ListOfSamples = new List<String>();
        public static void Add(String text)
        {
            ListOfSamples.Add(text);
        }

        public static int GetCount()
        {
            return ListOfSamples.Count;
        }

        public static String GetText(int i)
        {
            return ListOfSamples[i];
        }
    }

    /// <summary>
    /// Класс для хранения анализируемых текстов
    /// </summary>
    public static class Texts
    {
        private static List<String> ListOfTexts = new List<String>();
        public static void Add(String text)
        {
            ListOfTexts.Add(text);
        }

        public static int GetCount()
        {
            return ListOfTexts.Count;
        }

        public static String GetText(int i)
        {
            return ListOfTexts[i];
        }
    }
}
