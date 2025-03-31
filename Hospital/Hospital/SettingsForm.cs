using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;
using MySql.Data.MySqlClient;

namespace Hospital
{
    public partial class SettingsForm : Form
    {
        public SettingsForm()
        {
            InitializeComponent();
        }

        private void buttonSave_Click(object sender, EventArgs e)
        {
            //Сохранение настроек
            GlobalValue.server = textBoxDatabaseServer.Text;
            GlobalValue.db = textBoxDatabaseName.Text;
            GlobalValue.uid = textBoxDatabaseUser.Text;
            GlobalValue.pwd = textBoxDatabasePassword.Text;

            //Попытка подключения
            try
            {
                MySqlConnection con = new MySqlConnection(GlobalValue.GetConnString());
                con.Open();
                con.Close();
            }
            catch (Exception ex)
            {
                //Подключение не удалось
                MessageBox.Show("Внимание! При проверке подключения произошла ошибка." + ex.Message, "Ошибка подключения.", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            MessageBox.Show("Настройки сохранены.", "Информация", MessageBoxButtons.OK, MessageBoxIcon.Information);
            this.Close();
        }

        private void buttonCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        //Ввод только английских букв и цифр
        private void textBoxDatabaseServer_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)Keys.Back)
            {
                e.Handled = false;
                return;
            }
            if (!Regex.IsMatch(e.KeyChar.ToString(), @"^[a-zA-Z0-9.]+$"))
            {
                e.Handled = true;
            }
        }
        //Ввод только английских букв и цифр
        private void textBoxDatabaseName_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)Keys.Back)
            {
                e.Handled = false;
                return;
            }
            if (!Regex.IsMatch(e.KeyChar.ToString(), @"^[a-zA-Z0-9]+$"))
            {
                e.Handled = true;
            }
        }
        //Ввод только английских букв и цифр
        private void textBoxDatabaseUser_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)Keys.Back)
            {
                e.Handled = false;
                return;
            }
            if (!Regex.IsMatch(e.KeyChar.ToString(), @"^[a-zA-Z0-9]+$"))
            {
                e.Handled = true;
            }
        }
        //Огранничение ввода для пароля
        private void textBoxDatabasePassword_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)Keys.Back)
            {
                e.Handled = false;
                return;
            }
            if (!Regex.IsMatch(e.KeyChar.ToString(), @"^[a-zA-Z0-9_]+$"))
            {
                e.Handled = true;
            }

            if (!Regex.IsMatch(e.KeyChar.ToString(), @"^[!@#$%^&]+$"))
            {
                e.Handled = true;
            }
        }
        //Обработчик закрытия формы
        private void SettingsForm_FormClosed(object sender, FormClosedEventArgs e)
        {
            if (Application.OpenForms["MainForm"] is MainForm mn)
                mn.Show();
            else
                new MainForm().Show();
        }
        //Заполнение полей при открытии формы
        private void SettingsForm_Load(object sender, EventArgs e)
        {
            textBoxDatabaseName.Text = GlobalValue.db;
            textBoxDatabaseServer.Text = GlobalValue.server;
            textBoxDatabaseUser.Text = GlobalValue.uid;
            textBoxDatabasePassword.Text = GlobalValue.pwd;
        }
    }
}
