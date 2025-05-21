// Импорт необходимых пространств имен
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using MySql.Data.MySqlClient;
using System.Configuration;
using System.IO;

namespace Hospital
{
    // Класс формы авторизации
    public partial class LoginForm : Form
    {
        public int LoginAttemps = 0;
        public bool capthaIsVisible = false;
        private const string BackupFolderName = "Backups";
        // Конструктор формы
        public LoginForm()
        {
            InitializeComponent();
            RoundButtonCorners(this.buttonLogin, 10); // Скругление углов кнопки
            textBoxLogin.Text = ""; // Очистка поля логина
            textBoxPassword.Text = ""; // Очистка поля пароля
        }

        // Обработчик клика по кнопке входа
        private void buttonLogin_Click(object sender, EventArgs e)
        {
            if (LoginAttemps == 0)
            {
                try
                {
                    // Попытка авторизации
                    bool res = Authorize(textBoxLogin.Text, textBoxPassword.Text);

                    if (res) // Если авторизация успешна
                    {
                        LoginAttemps = 0;
                        resetCaptha();

                        // Создание и отображение главной формы
                        MainForm mainForm = new MainForm(User.Role);
                        this.Hide(); // Скрытие формы авторизации
                        mainForm.ShowDialog();

                        // Очистка полей после выхода из главной формы
                        textBoxLogin.Text = "";
                        textBoxPassword.Text = "";
                    }
                    else // Если авторизация не удалась
                    {
                        MessageBox.Show("Не удалось провести авторизацию. Повторите попытку входа впосле ввода капчи.", "Ошибка авторизации", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        textBoxLogin.Text = ""; // Очистка поля логина
                        textBoxPassword.Text = ""; // Очистка поля пароля
                        LoginAttemps++;
                        showCaptha();

                        return;
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Возникла ошибка: {ex.Message}", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    textBoxLogin.Text = ""; // Очистка поля логина
                    textBoxPassword.Text = ""; // Очистка поля пароля
                    return;
                }
            }
            else
            {
                if (capthaIsVisible)
                {
                    if (!string.IsNullOrWhiteSpace(richTextBoxCaptcha.Text) && richTextBoxCaptcha.Text.Length == 4 && CapthaGenerate.generatedCaptha == richTextBoxCaptcha.Text.Trim(' '))
                    {
                        try
                        {
                            bool res = Authorize(textBoxLogin.Text, textBoxPassword.Text);

                            if (res)
                            {
                                LoginAttemps = 0;
                                resetCaptha();

                                // Создание и отображение главной формы
                                MainForm mainForm = new MainForm(User.Role);
                                this.Hide(); // Скрытие формы авторизации
                                mainForm.ShowDialog();

                                // Очистка полей после выхода из главной формы
                                textBoxLogin.Text = "";
                                textBoxPassword.Text = "";
                            }
                            else
                            {
                                MessageBox.Show("Не удалось провести авторизацию. Блокировка системы на 10 секунд.", "Ошибка авторизации", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                textBoxLogin.Text = "";
                                textBoxPassword.Text = "";
                                richTextBoxCaptcha.Text = "";
                                LoginAttemps++;

                                this.Enabled = false;
                                Thread.Sleep(10000);
                                this.Enabled = true;

                                return;
                            }
                        }
                        catch (Exception ex)
                        {
                            MessageBox.Show($"Возникла ошибка: {ex.Message}", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            textBoxLogin.Text = "";
                            textBoxPassword.Text = "";
                            richTextBoxCaptcha.Text = "";
                            return;
                        }
                    }
                    else
                    {
                        MessageBox.Show("Капча не введена или неверна. Повторите попытку через 10 секунд.", "Ошибка капчи", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        textBoxLogin.Text = "";
                        textBoxPassword.Text = "";
                        richTextBoxCaptcha.Text = "";
                        LoginAttemps++;

                        this.Enabled = false;
                        Thread.Sleep(10000);
                        this.Enabled = true;

                        pictureCaptha.Image = CapthaGenerate.Gena(pictureCaptha.Width, pictureCaptha.Height);

                        return;
                    }
                }
            }
        }

        //Отображение капчи
        public void showCaptha()
        {
            this.Size = new Size(295, 469);
            pictureCaptha.Image = CapthaGenerate.Gena(pictureCaptha.Width, pictureCaptha.Height);
            capthaIsVisible = true;
            pictureCaptha.Visible = true;
        }

        //Скрытие капчи
        public void resetCaptha()
        {
            this.Size = new Size(295, 324);
            pictureCaptha.Image = null;
            capthaIsVisible = false;
            pictureCaptha.Visible = true;
        }

        private void AutomaticBackup()
        {
            try
            {
                string appDir = AppDomain.CurrentDomain.BaseDirectory;
                string backupsDir = Path.Combine(appDir, BackupFolderName);

                if (!Directory.Exists(backupsDir))
                {
                    Directory.CreateDirectory(backupsDir);
                }

                string backupFolderName = DateTime.Now.ToString("yyyyMMdd_HHmmss");
                string backupPath = Path.Combine(backupsDir, backupFolderName);

                Directory.CreateDirectory(backupPath);

                SaveFileDialog saveFileDialog = new SaveFileDialog();
                saveFileDialog.Filter = "SQL Files (*.sql)|*.sql";
                saveFileDialog.Title = "Сохранить резервную копию базы данных";
                saveFileDialog.FileName = $"hospital_backup_{DateTime.Now:yyyyMMdd_HHmmss}.sql";

                if (saveFileDialog.ShowDialog() == DialogResult.OK)
                {
                    try
                    {
                        BackupDatabaseToSql(saveFileDialog.FileName);
                        MessageBox.Show($"Резервная копия успешно создана: {saveFileDialog.FileName}", "Успех", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show($"Ошибка при создании резервной копии: {ex.Message}", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка автоматического резервного копирования: {ex.Message}", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
        }

        private void BackupDatabaseToSql(string filePath)
        {
            using (MySqlConnection connection = new MySqlConnection(GlobalValue.GetConnString()))
            {
                connection.Open();

                using (StreamWriter writer = new StreamWriter(filePath, false, Encoding.UTF8))
                {
                    // Записываем заголовок
                    writer.WriteLine("-- Резервная копия базы данных Hospital");
                    writer.WriteLine($"-- Создана: {DateTime.Now}");
                    writer.WriteLine("SET FOREIGN_KEY_CHECKS = 0;");
                    writer.WriteLine();

                    // Получаем список всех таблиц
                    List<string> tables = new List<string>();
                    using (MySqlCommand cmd = new MySqlCommand("SHOW TABLES", connection))
                    using (MySqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            tables.Add(reader.GetString(0));
                        }
                    }

                    // Создаем базу данных и используем ее
                    writer.WriteLine($"CREATE DATABASE IF NOT EXISTS `{GlobalValue.db}` CHARACTER SET utf8mb4 COLLATE utf8mb4_unicode_ci;");
                    writer.WriteLine($"USE `{GlobalValue.db}`;");
                    writer.WriteLine();

                    // Для каждой таблицы
                    foreach (string table in tables)
                    {
                        // Получаем структуру таблицы
                        writer.WriteLine($"-- Структура таблицы `{table}`");
                        writer.WriteLine($"DROP TABLE IF EXISTS `{table}`;");

                        using (MySqlCommand cmd = new MySqlCommand($"SHOW CREATE TABLE `{table}`", connection))
                        using (MySqlDataReader reader = cmd.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                writer.WriteLine(reader.GetString("Create Table") + ";");
                            }
                        }
                        writer.WriteLine();

                        // Получаем данные таблицы
                        writer.WriteLine($"-- Дамп данных таблицы `{table}`");
                        writer.WriteLine($"LOCK TABLES `{table}` WRITE;");
                        writer.WriteLine($"/*!40000 ALTER TABLE `{table}` DISABLE KEYS */;");

                        using (MySqlCommand cmd = new MySqlCommand($"SELECT * FROM `{table}`", connection))
                        using (MySqlDataReader reader = cmd.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                StringBuilder insertQuery = new StringBuilder($"INSERT INTO `{table}` VALUES (");

                                for (int i = 0; i < reader.FieldCount; i++)
                                {
                                    if (i > 0) insertQuery.Append(", ");

                                    if (reader.IsDBNull(i))
                                    {
                                        insertQuery.Append("NULL");
                                    }
                                    else
                                    {
                                        string value = reader.GetValue(i).ToString();
                                        value = value.Replace("'", "''");
                                        insertQuery.Append($"'{value}'");
                                    }
                                }

                                insertQuery.Append(");");
                                writer.WriteLine(insertQuery.ToString());
                            }
                        }

                        writer.WriteLine($"/*!40000 ALTER TABLE `{table}` ENABLE KEYS */;");
                        writer.WriteLine("UNLOCK TABLES;");
                        writer.WriteLine();
                    }

                    writer.WriteLine("SET FOREIGN_KEY_CHECKS = 1;");
                    writer.WriteLine("-- Конец резервной копии");
                }
            }
        }

        // Ограничение ввода в поле логина (только латинские буквы и подчеркивание)
        private void textBoxLogin_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!Regex.IsMatch(e.KeyChar.ToString(), @"[a-zA-Z_]"))
            {
                e.Handled = true;
            }
        }

        // Ограничение ввода в поле пароля (только латинские буквы, цифры и подчеркивание)
        private void textBoxPassword_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)Keys.Back) // Разрешаем Backspace
            {
                e.Handled = false;
                return;
            }

            if (!Regex.IsMatch(e.KeyChar.ToString(), @"[a-zA-Z0-9_]"))
            {
                e.Handled = true;
                return;
            }
        }

        // Метод для скругления углов кнопки
        private void RoundButtonCorners(Button button, int radius)
        {
            GraphicsPath path = new GraphicsPath();
            path.AddArc(0, 0, radius * 2, radius * 2, 180, 90); // Top-left
            path.AddArc(button.Width - radius * 2, 0, radius * 2, radius * 2, 270, 90); // Top-right
            path.AddArc(button.Width - radius * 2, button.Height - radius * 2, radius * 2, radius * 2, 0, 90); // Bottom-right
            path.AddArc(0, button.Height - radius * 2, radius * 2, radius * 2, 90, 90); // Bottom-left
            path.CloseAllFigures();
            button.Region = new Region(path);
        }

        // Метод авторизации пользователя
        private bool Authorize(string login, string password)
        {
            // Хеширование пароля с помощью SHA256
            using (var sh2 = SHA256.Create())
            {
                var sh2byte = sh2.ComputeHash(Encoding.UTF8.GetBytes(password));

                password = BitConverter.ToString(sh2byte).Replace("-", "").ToLower();
            }

            try
            {
                // Подключение к БД и проверка учетных данных
                MySqlConnection con = new MySqlConnection(GlobalValue.GetConnString());
                con.Open();
                MySqlCommand cmd = new MySqlCommand($"SELECT * FROM user WHERE login='{textBoxLogin.Text}' and password='{password}'", con);
                MySqlDataAdapter ad = new MySqlDataAdapter(cmd);
                DataTable tb = new DataTable();

                ad.Fill(tb);

                if (tb.Rows.Count != 1) // Если пользователь не найден
                {
                    return false;
                }

                // Заполнение данных пользователя
                User.Role = Convert.ToInt32(tb.Rows[0].ItemArray.GetValue(3).ToString());
                User.Id = tb.Rows[0].ItemArray.GetValue(0).ToString();
                User.Fio = GlobalValue.GetUserFIO(User.Id);
                User.Post = GlobalValue.GetUserPost(User.Id);

                con.Close();

                return true; // Авторизация успешна
            }
            catch 
            {
                return false; // Авторизация не удалась
            }
        }

        // Обработчик клика по иконке отображения пароля
        private void pictureBox2_Click(object sender, EventArgs e)
        {
            if (textBoxPassword.UseSystemPasswordChar)
            {
                textBoxPassword.UseSystemPasswordChar = false; // Показываем пароль
                pictureBox2.Image = Properties.Resources.eye_close;
            }
            else
            {
                textBoxPassword.UseSystemPasswordChar = true; // Скрываем пароль
                pictureBox2.Image = Properties.Resources.eye_open;
            }
        }

        // Обработчик закрытия формы
        private void LoginForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (!inactive)
            {
                AutomaticBackup();
                // Подтверждение выхода из приложения
                if (DialogResult.Yes == MessageBox.Show("Вы действительно хотите выйти?", "Подтверждение", MessageBoxButtons.YesNo, MessageBoxIcon.Information))
                    e.Cancel = false; // Закрытие формы
                else
                    e.Cancel = true; // Отмена закрытия формы
            }
            else
            {
                inactive = false;
                e.Cancel = false;
            }
        }

        //Обноление капчи
        private void picatureReloadCaptcha_Click(object sender, EventArgs e)
        {
            richTextBoxCaptcha.Text = "";
            pictureCaptha.Image = CapthaGenerate.Gena(pictureCaptha.Width, pictureCaptha.Height);
        }
    }
}