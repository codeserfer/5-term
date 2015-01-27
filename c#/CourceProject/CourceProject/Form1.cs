using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;

namespace CourceProject
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            Vocabulary.ReadVocabulary(); //Считывание словаря
        }

        /// <summary>
        /// Склонение слова "файл"
        /// </summary>
        /// <param name="number_of_loaded_files"></param>
        /// <returns></returns>
        private static string declension_file(int number_of_loaded_files)
        {
            if (number_of_loaded_files >= 10 && number_of_loaded_files <= 20)
                return CONSTANTS.file_lable[2];

            var temp1 = number_of_loaded_files % 10;
            if ((temp1 == 0) || (temp1 == 5) || (temp1 == 6) || (temp1 == 7) || (temp1 == 8) || (temp1 == 9))
                return CONSTANTS.file_lable[2];
            if (temp1 == 1)
                return CONSTANTS.file_lable[0];
            if ((temp1 == 2) || (temp1 == 3) || (temp1 == 4))
                return CONSTANTS.file_lable[1];
            else return CONSTANTS.file_lable[2];
        }

        #region temp
        private void openFileDialog1_FileOk(object sender, CancelEventArgs e)
        {

        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void label5_Click(object sender, EventArgs e)
        {

        }

        private void label6_Click(object sender, EventArgs e)
        {

        }

        private void Form1_Load(object sender, EventArgs e)
        {
            this.Text = "Поиск плагиата методом Рабина-Карпа";
        }
        #endregion

        #region Работа с папками
        //Выбор папки с исследуемыми текстами
        private void button3_Click(object sender, EventArgs e)
        {
            var folderBrowserDialog1 = new FolderBrowserDialog();
            folderBrowserDialog1.Description = "Выбор папки с текстовыми файлами";
            DialogResult result = folderBrowserDialog1.ShowDialog();
            var dir = folderBrowserDialog1.SelectedPath;
            if (result == DialogResult.OK)
            {
                var number_of_loaded_files = Int32.Parse(label7.Text);
                List<String> ListOfFiles = new List<string>();
                number_of_loaded_files += FileWorking.GetListOfFiles(ListOfFiles, dir);
                foreach (var i in ListOfFiles)
                {
                    var temp = i.Split('\\');
                    FileWorking.ListOfFilesTexts.Add(temp[temp.Length - 1]);
                }
                label7.Text = number_of_loaded_files.ToString();
                label8.Text=declension_file(number_of_loaded_files);

                foreach (var file in ListOfFiles)
                {
                    FileWorking.AddFile(file, false);

                }
            }
        }
        //Выбор папки с исследуемыми текстами

        //Выбор папки с текстами-образцами
        private void button2_Click(object sender, EventArgs e)
        {
            var folderBrowserDialog1 = new FolderBrowserDialog();
            folderBrowserDialog1.Description = "Выбор папки с текстовыми файлами";
            DialogResult result = folderBrowserDialog1.ShowDialog();
            var dir = folderBrowserDialog1.SelectedPath;
            if (result == DialogResult.OK)
            {
                var number_of_loaded_files = Int32.Parse(label3.Text);
                List<String> ListOfFiles = new List<string>();
                number_of_loaded_files += FileWorking.GetListOfFiles(ListOfFiles, dir);
                foreach (var i in ListOfFiles)
                {
                    var temp = i.Split('\\');
                    FileWorking.ListOfFilesSamples.Add(temp[temp.Length - 1]);
                }
                label3.Text = number_of_loaded_files.ToString();
                label4.Text=declension_file(number_of_loaded_files);

                foreach (var file in ListOfFiles)
                {
                    FileWorking.AddFile(file, true);

                }
            }
        }
        //Выбор папки с текстами-образцами
        #endregion


        #region Работа с файлами
        //Выбор файла с исследуемыми текстами
        private void button4_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialog1 = new OpenFileDialog();
            openFileDialog1.Title = "Выбор текстового файла";
            openFileDialog1.Multiselect = false; //Запрет множественного выбора
            openFileDialog1.InitialDirectory = "c:\\";
            openFileDialog1.Filter = "Текстовые файлы |*.TXT";
            openFileDialog1.RestoreDirectory = true;
            if (openFileDialog1.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                var number_of_loaded_files = Int32.Parse(label7.Text) + 1;
                label7.Text = number_of_loaded_files.ToString();
                label8.Text = declension_file(number_of_loaded_files);

                FileWorking.AddFile(openFileDialog1.FileName, false);
                var temp = openFileDialog1.FileName.Split('\\');
                FileWorking.ListOfFilesTexts.Add(temp[temp.Length - 1]);
            }
        }
        //Выбор файла с исследуемыми текстами

        //Выбор файла с текстами-образцами
        private void button1_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialog1 = new OpenFileDialog();
            openFileDialog1.Title = "Выбор текстового файла";
            openFileDialog1.Multiselect = false; //Запрет множественного выбора
            openFileDialog1.InitialDirectory = "c:\\";
            openFileDialog1.Filter = "Текстовые файлы |*.TXT";
            openFileDialog1.RestoreDirectory = true;
            if (openFileDialog1.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                var number_of_loaded_files = Int32.Parse(label3.Text) + 1;
                label3.Text = number_of_loaded_files.ToString();
                label4.Text = declension_file(number_of_loaded_files);

                FileWorking.AddFile(openFileDialog1.FileName, true);
                var temp = openFileDialog1.FileName.Split('\\');
                FileWorking.ListOfFilesSamples.Add(temp[temp.Length - 1]);
            }
        }        
        //Выбор файла с текстами-образцами
        #endregion

        private void button5_Click(object sender, EventArgs e)
        {
            if (Samples.GetCount () == 0)
            {
                MessageBox.Show("Нет текстов-образцов!");
                return;
            }
            if (Texts.GetCount() == 0)
            {
                MessageBox.Show("Анализируемых текстов!");
                return;
            }


            BarChart.bar_chart.Clear();
            chart1.Series.Clear();
            label1.Visible = label2.Visible = label3.Visible = label4.Visible = 
                label5.Visible = label6.Visible = label7.Visible = label8.Visible = false;

            button1.Visible = button2.Visible = button3.Visible = button4.Visible = button5.Visible = false;

            
            for (int j = 0; j < Texts.GetCount(); j++)
            {
                comboBox1.Items.Add(FileWorking.ListOfFilesTexts[j]);
                BarChart.bar_chart.Add(new List<double>());
                for (int i = 0; i < Samples.GetCount(); i++)
                {
                    var unique = RK.Check(i, j);
                    if (j==0) chart1.Series.Add(FileWorking.ListOfFilesSamples[i]);
                    
                    BarChart.bar_chart[j].Add(unique);
                    if (j==0) chart1.Series[i].Points.AddY(unique); //Отрисовка графика для первого текста
                    
                }
            }

            chart1.BackColor = System.Drawing.Color.White;

            comboBox1.SelectedIndex = 0;
            comboBox1.Visible = chart1.Visible = true;

        }
      

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            
            for (int i = 0; i < Samples.GetCount(); i++)
            {
                chart1.Series[FileWorking.ListOfFilesSamples[i]].Points.Clear();
                chart1.Series[FileWorking.ListOfFilesSamples[i]].Points.AddY(BarChart.bar_chart[comboBox1.SelectedIndex][i]); 
            }
            
        }

        private void comboBox2_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
    }
}