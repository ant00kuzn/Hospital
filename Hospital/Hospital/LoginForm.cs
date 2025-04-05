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
        public int LoginAttemps = 0; //Счетчик попыток входа
        public bool capthaIsVisible = false; //Видимость капчи
        private const string BackupFolderName = "Backups"; //Название папки для автоматического резервного копирования

        private bool inactive = false;

        private int prevRole;
        // Конструктор формы
        public LoginForm()
        {
            InitializeComponent();
            RoundButtonCorners(this.buttonLogin, 10); // Скругление углов кнопки
            textBoxLogin.Text = ""; // Очистка поля логина
            textBoxPassword.Text = ""; // Очистка поля пароля
        }
        //Конструктор формы при инактиве пользователя
        public LoginForm(bool inactive)
        {
            InitializeComponent();
            RoundButtonCorners(this.buttonLogin, 10); // Скругление углов кнопки
            textBoxLogin.Text = ""; // Очистка поля логина
            textBoxPassword.Text = ""; // Очистка поля пароля

            this.inactive = inactive;
        }

        // Обработчик клика по кнопке входа
        private void buttonLogin_Click(object sender, EventArgs e)
        {
            if (LoginAttemps == 0)
            {
                //Если данные для входа соответствуют данным по умолчанию, то вход на форму управления бд 
                if (textBoxLogin.Text == Properties.Settings.Default.AdminUsername && textBoxPassword.Text == Properties.Settings.Default.AdminPassword)
                {
                    DatabaseImport databaseImport = new DatabaseImport();
                    this.Hide();
                    databaseImport.ShowDialog();
                    textBoxLogin.Text = "";
                    textBoxPassword.Text = "";
                    showCaptha();
                    return;
                }

                try
                {
                    // Попытка авторизации
                    bool res = Authorize(textBoxLogin.Text, textBoxPassword.Text);

                    if (res) // Если авторизация успешна
                    {
                        if (!inactive)
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
                        else if (inactive && prevRole == User.Role)
                        {
                            LoginAttemps = 0;
                            resetCaptha();

                            this.Close();

                            // Очистка полей после выхода из главной формы
                            textBoxLogin.Text = "";
                            textBoxPassword.Text = "";
                        }
                        else
                        {
                            MessageBox.Show("Предыдущий вход был осуществлен из под другой роли пользователя.", "Ошибка входа", MessageBoxButtons.OK, MessageBoxIcon.Stop);

                            textBoxLogin.Text = "";
                            textBoxPassword.Text = "";
                            LoginAttemps++;
                            showCaptha();

                            return;
                        }
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
                                if (!inactive)
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
                                else if (inactive && prevRole == User.Role)
                                {
                                    LoginAttemps = 0;
                                    resetCaptha();

                                    this.Close();

                                    // Очистка полей после выхода из главной формы
                                    textBoxLogin.Text = "";
                                    textBoxPassword.Text = "";
                                }
                                else
                                {
                                    MessageBox.Show("Предыдущий вход был осуществлен из под другой роли пользователя.", "Ошибка входа", MessageBoxButtons.OK, MessageBoxIcon.Stop);

                                    textBoxLogin.Text = "";
                                    textBoxPassword.Text = "";
                                    LoginAttemps++;
                                    showCaptha();

                                    return;
                                }
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

        # region Автоматическое резервное копирование
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

                BackupDatabase(backupPath);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка автоматического резервного копирования: {ex.Message}", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
        }

        public static void BackupDatabase(string backupPath)
        {
            try
            {
                using (MySqlConnection con = new MySqlConnection(GlobalValue.GetConnString()))
                {
                    con.Open();

                    List<string> tableNames = new List<string>();
                    using (MySqlCommand command = new MySqlCommand("SHOW TABLES", con))
                    using (MySqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            tableNames.Add(reader.GetString(0));
                        }
                    }

                    foreach (string tableName in tableNames)
                    {
                        string csvFilePath = Path.Combine(backupPath, $"{tableName}.csv");
                        ExportTableToCsv(con, tableName, csvFilePath);
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception($"Ошибка при создании резервной копии базы данных: {ex.Message}");
            }
        }

        private static void ExportTableToCsv(MySqlConnection connection, string tableName, string filePath)
        {
            try
            {
                using (StreamWriter writer = new StreamWriter(filePath, false, System.Text.Encoding.UTF8))
                {
                    List<string> columnNames = new List<string>();
                    using (MySqlCommand command = new MySqlCommand($"SHOW COLUMNS FROM `{tableName}`", connection))
                    using (MySqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            columnNames.Add(reader.GetString("Field"));
                        }
                    }

                    writer.WriteLine(string.Join(";", columnNames));

                    using (MySqlCommand command = new MySqlCommand($"SELECT * FROM `{tableName}`", connection))
                    using (MySqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            List<string> rowValues = new List<string>();
                            for (int i = 0; i < reader.FieldCount; i++)
                            {
                                string value = reader[i].ToString();
                                value = value.Replace("\"", "\"\"");
                                value = $"\"{value}\"";
                                rowValues.Add(value);
                            }
                            writer.WriteLine(string.Join(";", rowValues));
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception($"Ошибка при экспорте таблицы {tableName} в CSV: {ex.Message}");
            }
        }
        #endregion

        // Ограничение ввода в поле логина (только латинские буквы и подчеркивание)
        private void textBoxLogin_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!Regex.IsMatch(e.KeyChar.ToString(), @"[a-zA-Z0-9_]"))
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
                MySqlCommand cmd = new MySqlCommand($"SELECT * FROM employee WHERE Login='{textBoxLogin.Text}' and Password='{password}'", con);
                MySqlDataAdapter ad = new MySqlDataAdapter(cmd);
                DataTable tb = new DataTable();

                ad.Fill(tb);

                if (tb.Rows.Count != 1) // Если пользователь не найден
                {
                    return false;
                }

                // Заполнение данных пользователя
                prevRole = User.Role;
                User.Role = Convert.ToInt32(tb.Rows[0].ItemArray.GetValue(10).ToString());
                User.SurName = tb.Rows[0].ItemArray.GetValue(1).ToString();
                User.Name = tb.Rows[0].ItemArray.GetValue(2).ToString();
                User.Patronymic = tb.Rows[0].ItemArray.GetValue(3).ToString();

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
                this.DialogResult = DialogResult.Cancel;
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