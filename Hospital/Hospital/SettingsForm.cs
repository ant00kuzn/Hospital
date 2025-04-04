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
using System.Configuration;

namespace Hospital
{
    public partial class SettingsForm : Form
    {
        public SettingsForm()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Сохранение настроек
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void buttonSave_Click(object sender, EventArgs e)
        {
            //Обращение к сгенерированному файлу конфигурации
            Configuration config = ConfigurationManager.OpenExeConfiguration(Application.ExecutablePath);

            var appSettings = config.AppSettings;

            if (textBoxDatabaseServer.Text != "" && textBoxDatabaseName.Text != "" && textBoxDatabaseUser.Text != "")
            {
                //Занесение новых данных с полей
                appSettings.Settings["host"].Value = textBoxDatabaseServer.Text;
                appSettings.Settings["db"].Value = textBoxDatabaseName.Text;
                appSettings.Settings["uid"].Value = textBoxDatabaseUser.Text;
                appSettings.Settings["password"].Value = textBoxDatabasePassword.Text;
                appSettings.Settings["timerInactive"].Value = numericUpDown1.Value.ToString();

                config.Save(ConfigurationSaveMode.Modified);
                ConfigurationManager.RefreshSection("appSettings");

                //Проверка подключения
                if (!GlobalValue.DatabaseIsValid())
                {
                    MessageBox.Show("При подключении к базе произошла ошибка. Проверьте правильность введенных данных и повторите попытку.", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    textBoxDatabaseServer.Text = "";
                    textBoxDatabaseName.Text = "";
                    textBoxDatabaseUser.Text = "";
                    textBoxDatabasePassword.Text = "";
                    numericUpDown1.Value = 0;
                }
                else
                {
                    if (GlobalValue.dbIsntExist)
                    {
                        this.Close();
                    }
                    else
                    {
                        MessageBox.Show("Подключение успешно! Данные сохранены.", "Успех", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        this.Close();
                    }
                }
            }
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
            Configuration config = ConfigurationManager.OpenExeConfiguration(Application.ExecutablePath);

            var appSettings = config.AppSettings;

            textBoxDatabaseServer.Text = appSettings.Settings["host"].Value;
            textBoxDatabaseName.Text = appSettings.Settings["db"].Value;
            textBoxDatabaseUser.Text = appSettings.Settings["uid"].Value;
            textBoxDatabasePassword.Text = appSettings.Settings["password"].Value;
            numericUpDown1.Value = Convert.ToInt32(appSettings.Settings["timerInactive"].Value);
        }
    }
}
