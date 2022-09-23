using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Diagnostics.SymbolStore;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MyNotes;

namespace MyNotes
{
    public partial class mainForm : Form
    {
        #region Переменные
        string path = @"Unnamed"; //переменная пути файла
        string tag = ".txt"; //расширение сохраняемого файла
        Random random = new(); //переменная? для имени файла, если файл Unnamed.txt уже существует в директории


        #endregion


        #region Запуск Form
        public mainForm()
        {
            InitializeComponent();
        }

        private void mainForm_Load(object sender, EventArgs e)
        {
            textBoxNote.Hide();
            fileDirectory.Hide();
            textBoxNote.ScrollBars = ScrollBars.Both;
        }
        #endregion

        /// <summary>
        /// Создание файла
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void buttonCreate_Click(object sender, EventArgs e)
        {
            textBoxNote.Text = "";
            if (File.Exists(path + tag)) //если файл Unnamed.txt существует
            {
                File.Create(path + random.Next(0, 10).ToString() + tag); //создаём файл с рандомным числом в названии
            }
            else //иначе
            {

                File.Create(path + tag); //создаём файл Unnamed.txt
            }
            textBoxNote.Show(); //показываем текстбокс
            fileDirectory.Text = path + tag; //указываем в текст лейбла путь до файла
            fileDirectory.Show();//показываем лейбл
        }

        /// <summary>
        ///  Сохранение файла
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void buttonSave_Click(object sender, EventArgs e)
        {
            if (File.Exists(fileDirectory.Text)) //если файл сущетсвует
            {
                File.AppendAllText(fileDirectory.Text, textBoxNote.Text); //записываем в него текст
            }
            else //если нет 
            {
                MessageBox.Show("Файл не существует"); //показываем уведомление
            }
        }
        /// <summary>
        /// Открытие файла
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void fileOpen_Click(object sender, EventArgs e) 
        {
            fileopenWindow.ShowDialog(); //открываем окно в котором пользователь выбирает файл
            fileDirectory.Show(); //показываем лейбл в котором хранится текст пути до файла
            textBoxNote.Show(); //показываем текстбокс
            textBoxNote.Text = File.ReadAllText(fileopenWindow.FileName); //записываем текст из файла в текстбокс
            fileDirectory.Text = fileopenWindow.FileName; //передаем путь до файла в лейбл

        }


    }
}

