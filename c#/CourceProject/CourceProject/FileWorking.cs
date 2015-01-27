using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;

namespace CourceProject
{
    public static class FileWorking
    {
        public static List<String> ListOfFilesTexts = new List<string>();
        public static List<String> ListOfFilesSamples = new List<string>();

        private static bool IsRightSymbol(char c)
        {
            if (char.IsLetter(c))
                return true;
            if (char.IsDigit(c))
                return true;
            if (c == ' ')
                return true;
            //if (c == '\n')
             //   return true;
            if (c == '.')
                return true;
            return false;
        }


        public static StringBuilder ReadFile(string filename)
        {
            StringBuilder text = new StringBuilder();
            StringBuilder temp = new StringBuilder();
            using (StreamReader sr = File.OpenText(filename))
            {
                char prvC = 'f';
                while (!sr.EndOfStream)
                {
                    char c = (char)sr.Read();
                    c = (c.ToString().ToLower().ToCharArray())[0];
                    if (c=='ё') c='е';
                    if (!IsRightSymbol(c)) c = ' ';
                    if (prvC == c && c == ' ') //Отсечение двойных пробелов
                        continue;
                    prvC = c;
                    temp.Append(c);
                    if (c == ' ')
                        {
                        string CheckString = temp.ToString().Trim();
                        if (!Vocabulary.CheckWord(CheckString))
                        {
                            text.Append(temp);
                            temp.Clear();
                        }
                        else
                        {
                            temp.Clear();
                        }
                    }
                }
            }
            text.Append(temp);
            return (text);
        }

        private static string GetType(string filename)
        {
            var temp = filename.Split(new char[] { '.' });
            return temp[temp.Length - 1];
        }

        /// <summary>
        /// Проверяет, хешировался ли данный файл ранее
        /// Захешированная версия файла располагается в той же папке с именем:
        /// имяфайла.txt.Processed
        /// </summary>
        /// <param name="path">Путь до проверяемого файла</param>
        /// <returns>true - файл хешировался ранее</returns>
        public static bool IsProcessed(string path)
        {
            var separated_path = path.Split(new char[] { '.' });
            var filename = path + ".Processed";
            if (!File.Exists(filename)) return false;
            return true;
        }

        /// <summary>
        /// Проверяет, является ли данный файл служебным файлом с хешами
        /// </summary>
        /// <param name="path">Путь</param>
        /// <returns>true, если файл содердит хеши</returns>
        public static bool IsProcessedFile(string path)
        {
            var separated_path = path.Split(new char[] { '\\' });
            var separated_filename = separated_path[separated_path.Length - 1].Split(new char[] { '.' });
            if (separated_filename[separated_filename.Length - 1] == "processed") return true;
            return false;
        }

        /// <summary>
        /// Рекунсивно находит все текстовые файлы в папке и во вложенных папках
        /// Записывает все найденные файлы в переденный FileList
        /// Возвращает количество найденных файлов
        /// </summary>
        /// <param name="FileList">Пустой List для записи путей найденных файлов</param>
        /// <param name="directory">Папка для поиска</param>
        /// <returns>Количество найденных файлов</returns>
        public static int GetListOfFiles(List<string> FileList, string directory)
        {
            foreach (var i in Directory.GetFiles(directory))
            {
                if (GetType(i.ToLower()) == "txt")
                {
                    if (!IsProcessedFile(i)) FileList.Add(i);
                }
            }
            foreach (var i in Directory.GetDirectories(directory))
            {
                GetListOfFiles(FileList, i);
            }
            return FileList.Count;
        }

        public static Int64 WordHash(string s)
        {
            Int64 hash = 0;
            for (int i = 0; i < s.Length; i++)
            {
                var temp = (Math.Pow(s[i], (double)(s.Length / (i + 1))));
                hash += (Int64)temp;
            }
            return hash;
        }

        /// <summary>
        /// Добавляет хеш-файл в анализируемый список
        /// </summary>
        /// <param name="filename">Путь до анализируемого файла</param>
        /// <param name="f">true - добавление в образцы, false - добавление в исследуемые тексты</param>
        public static void AddFile(string filename, bool f)
        {

            //Если выбран служебный файл с хешами
            if (FileWorking.IsProcessedFile(filename))
            {
                //Exception
                MessageBox.Show("Выбран служебный файл!");
                return;
            }
            //Если выбран служебный файл с хешами

            //Проверка, не устарел ли файл с хешами
            if (FileWorking.IsProcessed(filename))
            {
                if (File.GetLastWriteTime(filename) > File.GetLastWriteTime(filename + ".processed"))
                {
                    File.Delete(filename + ".processed");
                }

            }
            //Проверка, не устарел ли файл с хешами

            //Если требуется хеширование
            if (!FileWorking.IsProcessed(filename))
            {

                //Хеширование по словам
                var text = FileWorking.ReadFile(filename).ToString().Replace(".", ""); //Удаление точек
                var words = text.Split(new char[] { ' ', '\n', '\t' });
                List<Int64> HashList = new List<Int64>();
                foreach (var word in words)
                {
                    if (word!="") HashList.Add(WordHash(word));
                }
                //Хеширование по словам

                StringBuilder hash_line = new StringBuilder();
                var i = 0;
                foreach (var hash in HashList)
                {
                    if (i!=HashList.Count-1) hash_line.Append(hash.ToString() + ",");
                    else hash_line.Append(hash.ToString());
                    i++;
                }
                File.WriteAllText(filename + ".processed", hash_line.ToString());


                if (f == true) Samples.Add(hash_line.ToString());
                else Texts.Add(hash_line.ToString());
            }
            //Если требуется хеширование

            //Если хеширование не требуется
            else
            {
                if (f == true) Samples.Add(File.ReadAllText(filename+".processed"));
                else Texts.Add(File.ReadAllText(filename+".processed"));

            }
            //Если хеширование не требуется
        }
    }
}
