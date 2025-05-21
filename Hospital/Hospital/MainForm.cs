// Импорт необходимых пространств имен
using Hospital.Справочники;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Hospital
{
    // Класс главной формы приложения
    public partial class MainForm : Form
    {

        // Конструктор по умолчанию
        public MainForm()
        {
            InitializeComponent();
        }

        // Конструктор с параметром (роль пользователя)
        public MainForm(int id)
        {
            InitializeComponent();
            User.Role = id; // Установка роли пользователя
            EnableDisableControls(User.Role); // Настройка доступных элементов управления
            ResizeWorm(User.Role); // Изменение размера и положения элементов
        }

        // Изменение размера и положения элементов в зависимости от роли
        private void ResizeWorm(int role)
        {
            switch (role)
            {
                case 1: // Администратор
                    buttonEmployees.Location = new Point(buttonEmployees.Location.X, buttonEmployees.Location.Y - 165);
                    buttonUsers.Location = new Point(buttonUsers.Location.X, buttonUsers.Location.Y - 165);
                    buttonGuides.Location = new Point(buttonGuides.Location.X, buttonGuides.Location.Y - 165);
                    button1.Location = new Point(button1.Location.X, button1.Location.Y - 110);
                    button2.Location = new Point(button2.Location.X, button2.Location.Y - 55);
                    buttonExit.Location = new Point(buttonExit.Location.X, buttonExit.Location.Y - 15);
                    break;
                case 2: // Главный врач
                    buttonWards.Location = new Point(buttonWards.Location.X, buttonWards.Location.Y - 55);
                    buttonHospitalizations.Location = new Point(buttonHospitalizations.Location.X, buttonHospitalizations.Location.Y - 55);
                    break;
                case 3: // Врач
                    buttonHospitalizations.Location = new Point(buttonHospitalizations.Location.X, buttonHospitalizations.Location.Y - 50);
                    break;
                case 4:
                    break;
                default:
                    break;
            }
        }

        // Включение/отключение элементов управления в зависимости от роли
        private void EnableDisableControls(int role)
        {
            buttonEmployees.Visible = (role == 1); // Только для администратора
            buttonUsers.Visible = (role == 1); // Только для администратора
            buttonPatients.Visible = (role == 4); // Для регистратора
            buttonHospitalizations.Visible = (role == 2 || role == 3 || role == 4); // Для главного врача и врача
            buttonWards.Visible = (role == 4); // Для регистратор и врача
            buttonGuides.Visible = (role == 1); // Только для администратора
            button1.Visible = (role == 1);
            button2.Visible = (role == 1);
        }

        // Обработчик клика по кнопке "Сотрудники"
        private void buttonEmployees_Click(object sender, EventArgs e)
        {
            EmployeesForm employeesForm = new EmployeesForm();
            this.Hide(); // Скрытие текущей формы
            employeesForm.ShowDialog(); // Отображение формы сотрудников
        }

        // Обработчик клика по кнопке "Пациенты"
        private void buttonPatients_Click(object sender, EventArgs e)
        {
            PatientsForm patientsForm = new PatientsForm();
            this.Hide(); // Скрытие текущей формы
            patientsForm.ShowDialog(); // Отображение формы пациентов
        }

        // Обработчик клика по кнопке "Госпитализации"
        private void buttonHospitalizations_Click(object sender, EventArgs e)
        {
            if (User.Role == 4)
            {
                HospitalizationIteractionForm hospitalizationIteractionForm = new HospitalizationIteractionForm();
                this.Hide();
                hospitalizationIteractionForm.ShowDialog();
            }
            else
            {
                HospitalizationsForm hospitalizationsFrom = new HospitalizationsForm();
                this.Hide(); // Скрытие текущей формы
                hospitalizationsFrom.ShowDialog(); // Отображение формы госпитализаций 
            }
        }

        // Обработчик клика по кнопке "Палаты"
        private void buttonBeds_Click(object sender, EventArgs e)
        {
            WardsForm wards = new WardsForm();
            this.Hide(); // Скрытие текущей формы
            wards.ShowDialog();
        }

        // Обработчик закрытия формы
        private void MainForm_FormClosed(object sender, FormClosedEventArgs e)
        {
            // Показываем форму авторизации при закрытии
            if (Application.OpenForms["LoginForm"] is LoginForm lg)
                lg.Show();
        }

        // Обработчик клика по кнопке "Выход"
        private void button1_Click(object sender, EventArgs e)
        {
            this.Close(); // Закрытие формы
        }

        // Обработчик клика по кнопке "Справочники"
        private void button2_Click(object sender, EventArgs e)
        {
            GuidesForm guideForm = new GuidesForm();
            this.Hide(); // Скрытие текущей формы
            guideForm.ShowDialog(); // Отображение формы справочников
        }

        // Обработчик клика по иконке настроек
        private void pictureBox1_Click(object sender, EventArgs e)
        {
            SettingsForm settings = new SettingsForm();
            this.Hide(); // Скрытие текущей формы
            settings.ShowDialog(); // Отображение формы настроек
        }

        private void buttonUsers_Click(object sender, EventArgs e)
        {
            UsersForm uf = new UsersForm();
            this.Hide();
            uf.ShowDialog();
        }

        private void MainForm_Load(object sender, EventArgs e)
        {
            label1.Text = User.Fio;
            label2.Text = User.Post;

            label1.Left = pictureBox2.Left + (pictureBox2.Width - label1.Width) / 2;
            label2.Left = pictureBox2.Left + (pictureBox2.Width - label2.Width) / 2;
        }

        private void button2_Click_1(object sender, EventArgs e)
        {
            DatabaseImport databaseImport = new DatabaseImport();
            this.Hide();
            databaseImport.ShowDialog();
        }
    }
}