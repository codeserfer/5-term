using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace CourceProject
{
    public static class Vocabulary
    {
        private static List<String> ListForVocabulary = new List<string>();
        public static List<string> GetListOfFiles(string directory)
        {
            List<String> ListOfFiles = new List<String>();
            foreach (var i in Directory.GetFiles(directory))
            {
                ListOfFiles.Add(i);
            }
            return ListOfFiles;
        }
        public static void ReadVocabulary()
        {
            var List = GetListOfFiles(CONSTANTS.VocabularyFolder);
            foreach (var file in List)
            {
                string S_Vocabulary = "";
                using (StreamReader sr = new StreamReader(file))
                {
                    S_Vocabulary = sr.ReadToEnd();
                }
                foreach (var word in S_Vocabulary.Split(new char[] { '\n' }))
                {
                    ListForVocabulary.Add(word);
                }
            }
        }

        public static bool CheckWord(string CheckingWord)
        {
            CheckingWord = CheckingWord.ToLower().Trim();
            if (ListForVocabulary.Contains(CheckingWord))
                return true;
            return false;
        }
    }
}
