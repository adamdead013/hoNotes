using System;
using System.IO;
using System.Windows.Forms;

namespace MyNotes
{
    public partial class mainForm : Form
    {
        #region Переменные

        private readonly string path = @"Unnamed"; //переменная пути файла
        private readonly string tag = ".txt"; //расширение сохраняемого файла
        private readonly Random random = new(); //переменная? для имени файла, если файл Unnamed.txt уже существует в директории

        #endregion

        #region Запуск Form

        public mainForm()
        {
            InitializeComponent();
        }

        private void mainForm_Load(object sender, EventArgs e)
        {
            fileDirectory.Hide();

            textBoxNote.Hide();
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
            const int MAX_ATTEMPTS = 10;

            textBoxNote.Text = "";

            string fileName = path + tag;
            int maxValue = 10;      // --- 
            int currentAttempt = 0; // --- we want to limit number of attempts trying to find a proper file name

            while (File.Exists(fileName))
            {
                fileName = $"{path}{random.Next(0, maxValue)}{tag}";
                maxValue += 10;

                if (currentAttempt++ > MAX_ATTEMPTS)
                {
                    MessageBox.Show($"Cannot generate a new file name after {MAX_ATTEMPTS} attempts");
                    return;
                }
            }

            FileStream fs = File.Create(fileName); //  создаём файл Unnamed.txt
            fs.Dispose();                           // --- release file descriptor, otherwise it could cause to resource leaks.

            textBoxNote.Show(); //показываем текстбокс

            fileDirectory.Text = fileName; //указываем в текст лейбла путь до файла
            fileDirectory.Show(); //показываем лейбл
        }

        /// <summary>
        ///  Сохранение файла
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void buttonSave_Click(object sender, EventArgs e)
        {
            if (!File.Exists(fileDirectory.Text))
            {
                MessageBox.Show("Файл не существует"); //показываем уведомление
                return;
            }

            File.WriteAllText(fileDirectory.Text, textBoxNote.Text); //записываем в него текст
        }

        /// <summary>
        /// Открытие файла
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void fileOpen_Click(object sender, EventArgs e)
        {
            if (fileopenWindow.ShowDialog() != DialogResult.OK) //открываем окно в котором пользователь выбирает файл
                return;

            fileDirectory.Show(); //показываем лейбл в котором хранится текст пути до файла
            textBoxNote.Show(); //показываем текстбокс

            textBoxNote.Text = File.ReadAllText(fileopenWindow.FileName); //записываем текст из файла в текстбокс
            fileDirectory.Text = fileopenWindow.FileName; //передаем путь до файла в лейбл
        }
    }
}

