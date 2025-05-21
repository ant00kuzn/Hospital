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

        private const string BackupFolderName = "Backups";

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
                btnExportTable.Enabled = false;
                return;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка загрузки списка таблиц: {ex.Message}", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                tbFileName.Enabled = false;
                btnExportTable.Enabled = false;
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

            btnImportTable.Enabled = true;
            btnExportTable.Enabled = false;
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
            _ = cbTables.SelectedIndex != -1 ? btnExportTable.Enabled = true : btnExportTable.Enabled = false;
        }

        //Смена пути до файла
        private void tbFileName_TextChanged(object sender, EventArgs e)
        {
            _ = tbFileName.Text != "" ? btnImportTable.Enabled = true : btnImportTable.Enabled = false;
            _ = tbFileName.Text != "" ? btnExportTable.Enabled = false : btnImportTable.Enabled = true;
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

        private void button1_Click(object sender, EventArgs e)
        {
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
                    writer.WriteLine($"CREATE DATABASE IF NOT EXISTS `{db}` CHARACTER SET utf8mb4 COLLATE utf8mb4_unicode_ci;");
                    writer.WriteLine($"USE `{db}`;");
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

        private void btnExportTable_Click(object sender, EventArgs e)
        {
            // Проверка, что таблица выбрана
            if (cbTables.SelectedIndex == -1)
            {
                MessageBox.Show("Пожалуйста, выберите таблицу для экспорта.", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            string tableName = cbTables.SelectedItem.ToString();

            // Настройка диалога сохранения файла
            SaveFileDialog saveFileDialog = new SaveFileDialog();
            saveFileDialog.Filter = "CSV Files (*.csv)|*.csv";
            saveFileDialog.Title = "Экспорт таблицы в CSV";
            saveFileDialog.FileName = $"{tableName}_export_{DateTime.Now:yyyyMMdd_HHmmss}.csv";
            saveFileDialog.OverwritePrompt = true;

            if (saveFileDialog.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    ExportTableToCsv(tableName, saveFileDialog.FileName);
                    MessageBox.Show($"Таблица '{tableName}' успешно экспортирована в файл: {saveFileDialog.FileName}",
                                  "Успех", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Ошибка при экспорте таблицы: {ex.Message}", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void ExportTableToCsv(string tableName, string filePath)
        {
            using (MySqlConnection connection = new MySqlConnection(GlobalValue.GetConnString()))
            {
                connection.Open();

                // Получаем названия столбцов
                List<string> columnNames = new List<string>();
                using (MySqlCommand cmd = new MySqlCommand(
                    $"SELECT COLUMN_NAME FROM INFORMATION_SCHEMA.COLUMNS WHERE TABLE_SCHEMA = @db AND TABLE_NAME = @table ORDER BY ORDINAL_POSITION",
                    connection))
                {
                    cmd.Parameters.AddWithValue("@db", db);
                    cmd.Parameters.AddWithValue("@table", tableName);

                    using (MySqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            columnNames.Add(reader.GetString(0));
                        }
                    }
                }

                // Записываем данные в файл
                using (StreamWriter writer = new StreamWriter(filePath, false, Encoding.UTF8))
                {
                    // Записываем заголовки столбцов
                    writer.WriteLine(string.Join(";", columnNames));

                    // Получаем и записываем данные
                    using (MySqlCommand cmd = new MySqlCommand($"SELECT * FROM `{tableName}`", connection))
                    using (MySqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            List<string> rowValues = new List<string>();
                            for (int i = 0; i < reader.FieldCount; i++)
                            {
                                if (reader.IsDBNull(i))
                                {
                                    rowValues.Add("");
                                }
                                else
                                {
                                    string value = reader.GetValue(i).ToString();
                                    // Экранируем кавычки и сами значения заключаем в кавычки, если содержат разделитель
                                    value = value.Replace("\"", "\"\"");
                                    if (value.Contains(";") || value.Contains("\n") || value.Contains("\r"))
                                    {
                                        value = $"\"{value}\"";
                                    }
                                    rowValues.Add(value);
                                }
                            }
                            writer.WriteLine(string.Join(";", rowValues));
                        }
                    }
                }
            }
        }
    }
}
