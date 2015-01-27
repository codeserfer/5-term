using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CourceProject
{
    class RK
    {
        static List<Int64> pieces = new List<Int64>();
        static List<Int64> start_word = new List<Int64>();

        /// <summary>
        /// Выполняет поиск данного хеша в списке
        /// Возвращает найденную позицию, или -1, если хеш не найдет
        /// </summary>
        /// <param name="word">Искомых хеш</param>
        /// <returns>Позиция в списке, или -1, если хеш не найден</returns>
        public static int FindInStartWord(Int64 word)
        {
            for (int i = 0; i < start_word.Count; i++)
            {
                if (start_word[i] == word) return i;
            }
            return -1;
        }


        /// <summary>
        /// Возвращает длину фрагмента
        /// </summary>
        /// <param name="word">Хеш искомого фрагмента</param>
        /// <returns>Длина искомого фрагмента</returns>
        public static Int64 Find(Int64 word)
        {
            var position = FindInStartWord(word);
            if (position != -1)
            {
                return pieces[(int)position];
            }
            else
            {
                return 0; 
            }

        }

        /// <summary>
        /// Проверяет два текста на плагиат
        /// </summary>
        /// <param name="text1">Текст-образец</param>
        /// <param name="text2">Исследуемый текст</param>
        /// <returns>Возвращает процент уникальности</returns>
        public static double Check (int text1, int text2)
        {
            pieces.Clear();
            start_word.Clear();

            var doc1 = Samples.GetText(text1);
            var doc2 = Texts.GetText(text2);

            //Добавление считанных хешей в массивы
            var array1 = doc1.Split(new char[] { ',' });
            var array2 = doc2.Split(new char[] { ',' });
            //Добавление считанных хешей в массивы

            Int64 unique = array2.Count();

            Int64 lenght_of_piece = 0;

            for (Int64 j = 0; j < array2.Length; j++)
            {
                Int64 start = 0;
                Int64 found = 0;
                for (Int64 i = 0; i < array1.Length; i++)
                {
                    if (array1[i] == array2[j] && i<array1.Length-1)
                    {
                        if (start == 0) start = Int64.Parse(array2[j]);
                       
                        lenght_of_piece++;
                        i++;
                        Int64 k = j + 1;
                        for (; (k < array2.Length) && (i < array1.Length); k++, i++)
                        {
                            if (array1[i] == array2[k])
                            {
                                lenght_of_piece++;
                            }
                            else
                            {

                                break;
                            }
                        }
                    }
                    var found_lenght = Find(start);
                    if (lenght_of_piece > 1 && found_lenght < lenght_of_piece)
                    {

                        if (found_lenght == 0)
                        {
                            pieces.Add(lenght_of_piece);
                            start_word.Add(start);
                        }
                        else
                        {
                            pieces[FindInStartWord(start)] = lenght_of_piece;
                        }
                        found = lenght_of_piece;
                    }
                    lenght_of_piece = 0;


                }

                j += found;
            }

            for (int i = 0; i < pieces.Count; i++)
            {
                unique -= pieces[i];
            }


            return (100 - 100*Math.Round((double)unique / (double)array2.Count(), 2));

        }

    }
}
