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
        //Получение наименования базы данных
        private string db = ConfigurationManager.AppSettings["db"];

        public DatabaseImport()
        {
            InitializeComponent();
        }
        //Загрузка таблиц
        private void LoadTables()
        {
            cbTables.Items.Clear();
            try
            {
                using (MySqlConnection connection = new MySqlConnection(GlobalValue.GetConnString()))
                {
                    connection.Open();
                    DataTable tables = connection.GetSchema("Tables");
                    foreach (DataRow row in tables.Rows)
                    {
                        cbTables.Items.Add(row[2].ToString());
                    }
                }
            }
            catch (MySqlException ex)
            {
                MessageBox.Show($"Ошибка подключения к базе данных: {ex.Message}", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                tbFileName.Enabled = false;
                return;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка загрузки списка таблиц: {ex.Message}", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                tbFileName.Enabled = false;
                return;
            }
        }

        //Обработчик нажатия на кнопку выбора файла для импорта
        private void btnSelectFile_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "CSV Files (*.csv)|*.csv";
            openFileDialog.InitialDirectory = "C:/Documents";
            openFileDialog.Multiselect = false;
            openFileDialog.SupportMultiDottedExtensions = false;
            openFileDialog.Title = "Выберите файл для импорта данных";
            openFileDialog.CheckFileExists = true;
            openFileDialog.CheckPathExists = true;

            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                tbFileName.Text = openFileDialog.FileName;
            }
        }

        //Обработчик нажатия на кнопку импорта данных
        private void btnImportTable_Click(object sender, EventArgs e)
        {
            //Проверка на выбранность файла и таблицы
            if (string.IsNullOrWhiteSpace(tbFileName.Text) || cbTables.SelectedItem == null)
            {
                MessageBox.Show("Пожалуйста, выберите файл и таблицу для импорта.", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            string csvFilePath = tbFileName.Text;
            string tableName = cbTables.SelectedItem.ToString();

            //Подтверждение от пользователя
            if (DialogResult.No == MessageBox.Show($"Вы уверены, что хотите импортировать данные в таблицу {tableName}?", "Предупреждение", MessageBoxButtons.YesNo, MessageBoxIcon.Stop))
            {
                cbTables.Text = "";
                tbFileName.Text = "";
                btnSelectFile.Enabled = false;
                btnImportTable.Enabled = false;
                return;
            }

            try
            {
                int importedCount = ImportData(csvFilePath, tableName);

                MessageBox.Show($"Импортировано {importedCount} записей в таблицу '{tableName}'.", "Успех", MessageBoxButtons.OK, MessageBoxIcon.Information);
                cbTables.Text = "";
                tbFileName.Text = "";
                btnSelectFile.Enabled = false;
                btnImportTable.Enabled = false;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка импорта данных: {ex.Message}", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                cbTables.Text = "";
                tbFileName.Text = "";
                btnSelectFile.Enabled = false;
                btnImportTable.Enabled = false;
                return;
            }
        }

        /// <summary>
        /// Метод для импорта данных из файла csv
        /// </summary>
        /// <param name="csvFilePath">Путь до файла</param>
        /// <param name="tableName">Имя таблицы</param>
        /// <returns>Количество импортированных записей</returns>
        private int ImportData(string csvFilePath, string tableName)
        {
            string query = "";
            int importedCount = 0;

            using (MySqlConnection connection = new MySqlConnection(GlobalValue.GetConnString()))
            {
                connection.Open();

                // Получаем количество столбцов и имена столбцов
                using (MySqlCommand columnCommand = new MySqlCommand(
                    $"SELECT COLUMN_NAME, EXTRA FROM INFORMATION_SCHEMA.COLUMNS WHERE TABLE_SCHEMA='{db}' " +
                    $"AND TABLE_NAME = '{tableName}' ORDER BY ORDINAL_POSITION", connection))
                {
                    List<TwoDimensonal> columnNames = new List<TwoDimensonal>();
                    using (MySqlDataReader reader = columnCommand.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            columnNames.Add(new TwoDimensonal(reader.GetString(0), reader.GetString(1)));
                        }
                    }

                    int tableColumnCount = columnNames.Count;
                    if (tableColumnCount == 0)
                    {
                        throw new Exception($"Таблица {tableName} пуста. Проверьте структуру базы данных.");
                    }


                    string[] readText = File.ReadAllLines(csvFilePath, Encoding.Default);
                    if (readText.Length <= 1)
                    {
                        throw new Exception("Файл CSV не содержит данных для импорта.");
                    }

                    char delimiter = readText[0].Contains(';') ? ';' : ',';

                    query = $"INSERT INTO `{tableName}` VALUES ";

                    int countColumnsInRow = readText[0].Split(delimiter).Length;
                    if (tableColumnCount == countColumnsInRow)
                    {
                        for (int i = 1; i < readText.Length; i++)
                        {
                            query += "(";

                            foreach (string value in readText[i].Split(delimiter))
                            {
                                if (columnNames[0].Extra == "auto_increment")
                                {
                                    columnNames[0].Extra = "a";
                                    query += $" null,";
                                }
                                else
                                {
                                    query += $" '{value}',";
                                }
                            }

                            char[] ss = query.ToCharArray();
                            ss[ss.Length - 1] = ' ';
                            query = String.Concat<char>(ss);
                            query.TrimEnd(' ');

                            query += "),";

                            columnNames[0].Extra = "auto_increment";
                        }

                        char[] strArr = query.ToCharArray();
                        strArr[strArr.Length - 1] = ';';
                        query = String.Concat<char>(strArr);
                        Console.WriteLine();
                    }
                    else
                    {
                        cbTables.Text = "";
                        tbFileName.Text = "";
                        btnSelectFile.Enabled = false;
                        btnImportTable.Enabled = false;
                        throw new Exception($"Количество полей в файле {csvFilePath} не соответсвует количеству полей в таблице базы данных.");
                    }
                }

                MySqlConnection con = new MySqlConnection(GlobalValue.GetConnString());
                con.Open();
                MySqlCommand cmd = new MySqlCommand(query, con);
                importedCount = cmd.ExecuteNonQuery();
                con.Close();
            }
            return importedCount;
        }

        //Метод при загрузке формы
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

        //Смена таблицы
        private void cbTables_SelectedIndexChanged(object sender, EventArgs e)
        {
            _ = cbTables.SelectedIndex != -1 ? btnSelectFile.Enabled = true : btnSelectFile.Enabled = false;
        }

        //Смена пути до файла
        private void tbFileName_TextChanged(object sender, EventArgs e)
        {
            _ = tbFileName.Text != "" ? btnImportTable.Enabled = true : btnImportTable.Enabled = false;
        }

        //Обработчик нажатия по кнопке восстановления структуры
        private void btnRepairStructure_Click(object sender, EventArgs e)
        {
            try
            {
                string strcustureFilePath = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location) + "\\hospital_withoutdata.sql"; //Получение файла со структурой из выходной директории
                string[] file = File.ReadAllLines(strcustureFilePath, Encoding.Default);
                string query = $"create database if not exists {db}; use {db}; " + string.Join(" ", file);
                 
                MySqlConnection con = new MySqlConnection(GlobalValue.GetConnStringWithoutDB());
                con.Open();
                MySqlCommand cmd = new MySqlCommand(query, con);
                cmd.ExecuteNonQuery();
                con.Close();

                MessageBox.Show("Структура базы данных восстановлена.", "Успех", MessageBoxButtons.OK, MessageBoxIcon.Information);
                LoadTables();
                cbTables.Enabled = true;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка восстановления структуры бд: {ex.Message}", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void DatabseImport_FormClosed(object sender, FormClosedEventArgs e)
        {
            if (Application.OpenForms["LoginForm"] is LoginForm form1)
                form1.Show();
            else
                new LoginForm().Show();
        }
    }
}
