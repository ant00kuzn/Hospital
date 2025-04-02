// Импорт необходимых пространств имен
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
                    buttonEmployees.Visible = true;
                    buttonBeds.Visible = true;
                    buttonBeds.Location = new Point(buttonBeds.Location.X, buttonBeds.Location.Y - 110);
                    button2.Location = new Point(button2.Location.X, button2.Location.Y - 110);
                    pictureBox1.Visible = true;
                    break;
                case 2: // Главный врач
                    buttonHospitalizations.Visible = true;
                    buttonBeds.Visible = true;
                    buttonBeds.Location = new Point(buttonBeds.Location.X, buttonBeds.Location.Y - 110);
                    buttonHospitalizations.Location = new Point(buttonHospitalizations.Location.X, buttonHospitalizations.Location.Y - 110);
                    break;
                case 3: // Врач
                    buttonPatients.Visible = true;
                    buttonHospitalizations.Visible = true;
                    buttonBeds.Visible = true;
                    buttonBeds.Location = new Point(buttonBeds.Location.X, buttonBeds.Location.Y - 50);
                    buttonHospitalizations.Location = new Point(buttonHospitalizations.Location.X, buttonHospitalizations.Location.Y - 50);
                    buttonPatients.Location = new Point(buttonPatients.Location.X, buttonPatients.Location.Y - 50);
                    break;
                case 4: // Регистратура
                    buttonPatients.Visible = true;
                    buttonHospitalizations.Visible = true;
                    buttonHospitalizations.Location = new Point(buttonHospitalizations.Location.X, buttonHospitalizations.Location.Y - 50);
                    buttonPatients.Location = new Point(buttonPatients.Location.X, buttonPatients.Location.Y - 50);
                    break;
                default:
                    break;
            }
        }

        // Включение/отключение элементов управления в зависимости от роли
        private void EnableDisableControls(int role)
        {
            buttonEmployees.Visible = (role == 1); // Только для администратора
            buttonPatients.Visible = (role == 3 || role == 4); // Для всех, кроме администратора и глав. врача
            buttonHospitalizations.Visible = (role == 2 || role == 3); // Для главного врача и врача
            buttonBeds.Visible = (role == 1 || role == 2 || role == 3); // Для администратора, главного врача и врача
            button2.Visible = (role == 1); // Только для администратора
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
            HospitalizationsForm hospitalizationsFrom = new HospitalizationsForm();
            this.Hide(); // Скрытие текущей формы
            hospitalizationsFrom.ShowDialog(); // Отображение формы госпитализаций
        }

        // Обработчик клика по кнопке "Койки"
        private void buttonBeds_Click(object sender, EventArgs e)
        {
            BedsForm bedsForm = new BedsForm();
            this.Hide(); // Скрытие текущей формы
            bedsForm.ShowDialog(); // Отображение формы коек
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
    }
}