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

namespace Hospital
{
    // Класс формы авторизации
    public partial class LoginForm : Form
    {
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
            try
            {
                // Попытка авторизации
                bool res = Authorize(textBoxLogin.Text, textBoxPassword.Text);

                if (res) // Если авторизация успешна
                {
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
                    MessageBox.Show("Не удалось провести авторизацию.", "Ошибка авторизации", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    textBoxLogin.Text = ""; // Очистка поля логина
                    textBoxPassword.Text = ""; // Очистка поля пароля

                    // Закомментированный код блокировки формы на 10 секунд
                    //this.Enabled = false;
                    //Thread.Sleep(10000);
                    //this.Enabled = true;

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
                MySqlCommand cmd = new MySqlCommand($"SELECT * FROM employee WHERE Login='{textBoxLogin.Text}' and Password='{password}'", con);
                MySqlDataAdapter ad = new MySqlDataAdapter(cmd);
                DataTable tb = new DataTable();

                ad.Fill(tb);

                if (tb.Rows.Count != 1) // Если пользователь не найден
                {
                    return false;
                }

                // Заполнение данных пользователя
                User.Role = Convert.ToInt32(tb.Rows[0].ItemArray.GetValue(7).ToString());
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
            // Подтверждение выхода из приложения
            if (DialogResult.Yes == MessageBox.Show("Вы действительно хотите выйти?", "Подтверждение", MessageBoxButtons.YesNo, MessageBoxIcon.Information))
                e.Cancel = false; // Закрытие формы
            else
                e.Cancel = true; // Отмена закрытия формы
        }
    }
}