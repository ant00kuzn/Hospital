using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Hospital
{
    public partial class DatabaseImport : Form
    {
        private string db = ConfigurationManager.AppSettings["db"];

        public DatabaseImport()
        {
            InitializeComponent();
        }

        private void LoadTables()
        {
            try
            {
                using (MySqlConnection connection = new MySqlConnection(GlobalValue.GetConnString()))
                {
                    connection.Open();
                    DataTable tables = connection.GetSchema("Tables");
                    foreach (DataRow row in tables.Rows)
                    {
                        cbTables.Items.Add(row[2].ToString()); //comboBox
                    }
                }
            }
            catch (MySqlException ex)
            {
                MessageBox.Show($"Ошибка подключения к базе данных: {ex.Message}", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка загрузки списка таблиц: {ex.Message}", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void DatabseImport_Load(object sender, EventArgs e)
        {
            if (DBConnect.DatabaseIsValid())
            {
                cbTables.Enabled = true;
                LoadTables();
            }
            else
            {
                MessageBox.Show("База данных не доступна или пуста. Выполните восстановление структуры.", "", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
        }

        private void cbTables_SelectedIndexChanged(object sender, EventArgs e)
        {
            _ = cbTables.SelectedIndex != 0 ? btnSelectFile.Enabled = true : btnSelectFile.Enabled = false;
        }

        private void DatabseImport_Load(object sender, EventArgs e)
        {
            if (GlobalValue.DatabaseIsValid())
            {
                cbTables.Enabled = true;
                LoadTables();
            }
            else
            {
                MessageBox.Show("База данных не доступна или пуста. Выполните восстановление структуры.", "", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void DatabseImport_FormClosed(object sender, FormClosedEventArgs e)
        {
            if (Application.OpenForms["Form1"] is LoginForm form1)
                form1.Show();
            else
                new LoginForm().Show();
        }

        private void btnRepairStructure_Click(object sender, EventArgs e)
        {
            try
            {
                string strcustureFilePath = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location) + "\\script.sql";
                string[] file = File.ReadAllLines(strcustureFilePath, Encoding.Default);
                string query = $"create database if not exists {db}; use {db}; " + string.Join(" ", file);

                MySqlConnection con = new MySqlConnection(GlobalValue.GetConnStringWithoutDB());
                con.Open();
                MySqlCommand cmd = new MySqlCommand(query, con);
                cmd.ExecuteNonQuery();
                con.Close();

                MessageBox.Show("Структура базы данных восстановлена.", "Успех", MessageBoxButtons.OK, MessageBoxIcon.Information);
                LoadTables();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка восстановления структуры бд: {ex.Message}", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
