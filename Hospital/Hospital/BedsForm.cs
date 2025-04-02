using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices.ComTypes;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml.Linq;

namespace Hospital
{
    public partial class BedsForm : Form
    {
        private DataView dt; // Представление данных для фильтрации
        private int rowId = -1; // ID выбранной строки

        public BedsForm()
        {
            InitializeComponent(); // Инициализация компонентов формы
            LoadBedData(); // Загрузка данных о койках
            if (User.Role == 1)
            {
                LoadComboBoxData(); // Загрузка данных для ComboBox
            }
            else
            {
                tabControl1.Visible = false;
            }

            if (User.Role == 3)
            {
                button4.Visible = true;
            }

            comboBoxStatus.SelectedIndex = 0; // Установка начального значения
            comboBoxTypeBed.SelectedIndex = 0;
        }

        private void LoadBedData()
        {
            try
            {
                using (MySqlConnection connection = new MySqlConnection(GlobalValue.GetConnString()))
                {
                    connection.Open(); // Открытие соединения с БД

                    // Запрос данных о койках
                    string bedQuery = "SELECT BedID, BedStatus, WardID FROM Bed";
                    using (MySqlCommand bedCommand = new MySqlCommand(bedQuery, connection))
                    {
                        MySqlDataAdapter bedAdapter = new MySqlDataAdapter(bedCommand);
                        DataTable bedTable = new DataTable();
                        bedAdapter.Fill(bedTable); // Заполнение таблицы данными

                        // Добавление дополнительных колонок
                        bedTable.Columns.Add("Дата Госпитализации", typeof(string));
                        bedTable.Columns.Add("Дата Выписки", typeof(string));
                        bedTable.Columns.Add("TypeOfWard", typeof(string));
                        bedTable.Columns.Add("DepartmentName", typeof(string));

                        dataGridViewBeds.AutoGenerateColumns = false;
                        dataGridViewBeds.Columns.Clear(); // Очистка колонок DataGridView

                        // Добавление колонок в DataGridView
                        DataGridViewTextBoxColumn bedIdColumn = new DataGridViewTextBoxColumn();
                        bedIdColumn.DataPropertyName = "BedID";
                        bedIdColumn.Name = "ID";
                        dataGridViewBeds.Columns.Add(bedIdColumn);

                        DataGridViewTextBoxColumn department = new DataGridViewTextBoxColumn();
                        department.DataPropertyName = "DepartmentName";
                        department.HeaderText = "Отделение";
                        dataGridViewBeds.Columns.Add(department);

                        DataGridViewTextBoxColumn typeOfBedColumn = new DataGridViewTextBoxColumn();
                        typeOfBedColumn.DataPropertyName = "TypeOfWard";
                        typeOfBedColumn.HeaderText = "Тип палаты";
                        dataGridViewBeds.Columns.Add(typeOfBedColumn);
                        
                        DataGridViewTextBoxColumn wardID = new DataGridViewTextBoxColumn();
                        wardID.DataPropertyName = "WardID";
                        wardID.Name = "WardID";
                        dataGridViewBeds.Columns.Add(wardID);

                        DataGridViewTextBoxColumn bedStatusColumn = new DataGridViewTextBoxColumn();
                        bedStatusColumn.DataPropertyName = "BedStatus";
                        bedStatusColumn.HeaderText = "Статус";
                        dataGridViewBeds.Columns.Add(bedStatusColumn);

                        DataGridViewTextBoxColumn dateOfReceiptColumn = new DataGridViewTextBoxColumn();
                        dateOfReceiptColumn.DataPropertyName = "Дата Госпитализации";
                        dateOfReceiptColumn.HeaderText = "Дата Госпитализации";
                        dataGridViewBeds.Columns.Add(dateOfReceiptColumn);

                        DataGridViewTextBoxColumn dateOfDischargeColumn = new DataGridViewTextBoxColumn();
                        dateOfDischargeColumn.DataPropertyName = "Дата Выписки";
                        dateOfDischargeColumn.HeaderText = "Дата Выписки";
                        dataGridViewBeds.Columns.Add(dateOfDischargeColumn);

                        if (User.Role == 1)
                        {
                            DataGridViewButtonColumn deleteButtonColumn = new DataGridViewButtonColumn();
                            deleteButtonColumn.Name = "DeleteColumn";
                            deleteButtonColumn.Text = "Удалить";
                            deleteButtonColumn.HeaderText = "Удаление";
                            deleteButtonColumn.UseColumnTextForButtonValue = true;
                            dataGridViewBeds.Columns.Add(deleteButtonColumn);
                        }

                        // Заполнение данных о госпитализации
                        foreach (DataRow row in bedTable.Rows)
                        {
                            int bedId = Convert.ToInt32(row["BedID"]);
                            string bedStatus = row["BedStatus"].ToString();

                            if (bedStatus == "Свободна")
                            {
                                row["Дата Госпитализации"] = "-";
                                row["Дата Выписки"] = "-";
                            }
                            else if (bedStatus == "Занята")
                            {
                                // Запрос данных о госпитализации для занятой койки
                                string hospitalizationQuery = @"SELECT DateOfReceipt, DateOfDischarge FROM Hospitalization WHERE BedID = @BedID ORDER BY HospitalizationID DESC LIMIT 1";

                                using (MySqlCommand hospitalizationCommand = new MySqlCommand(hospitalizationQuery, connection))
                                {
                                    hospitalizationCommand.Parameters.AddWithValue("@BedID", bedId);
                                    MySqlDataReader reader = hospitalizationCommand.ExecuteReader();

                                    if (reader.Read())
                                    {
                                        row["Дата Госпитализации"] = reader.IsDBNull(0) ? "-" : reader.GetDateTime(0).ToShortDateString();
                                        row["Дата Выписки"] = reader.IsDBNull(1) ? "-" : reader.GetDateTime(1).ToShortDateString();
                                    }
                                    else
                                    {
                                        row["Дата Госпитализации"] = "-";
                                        row["Дата Выписки"] = "-";
                                    }
                                    reader.Close();
                                }
                            }
                            else
                            {
                                row["Дата Госпитализации"] = "-";
                                row["Дата Выписки"] = "-";
                            }

                            // Запрос типа палаты
                            string TypeOfWardQuery = @"SELECT w.TypeOfWard FROM Ward w INNER JOIN bed b ON w.WardID = b.WardID WHERE b.BedID = @BedID ORDER BY w.WardID DESC LIMIT 1";

                            using (MySqlCommand TypeOfWardCommand = new MySqlCommand(TypeOfWardQuery, connection))
                            {
                                TypeOfWardCommand.Parameters.AddWithValue("@BedID", bedId);
                                using (MySqlDataReader reader = TypeOfWardCommand.ExecuteReader())

                                    if (reader.Read())
                                    {
                                        row["TypeOfWard"] = reader.IsDBNull(0) ? "-" : reader.GetString(0);
                                    }
                            }

                            // Запрос типа палаты
                            string departQuery = @"SELECT d.DepartmentName FROM department d INNER JOIN ward w ON w.DepartmentID = d.DepartmentID inner join bed on bed.WardID = w.WardID WHERE bed.BedID = @BedID ORDER BY w.WardID DESC LIMIT 1";

                            using (MySqlCommand TypeOfWardCommand = new MySqlCommand(departQuery, connection))
                            {
                                TypeOfWardCommand.Parameters.AddWithValue("@BedID", bedId);
                                using (MySqlDataReader reader = TypeOfWardCommand.ExecuteReader())

                                    if (reader.Read())
                                    {
                                        row["DepartmentName"] = reader.IsDBNull(0) ? "-" : reader.GetString(0);
                                    }
                            }
                        }

                        dt = bedTable.DefaultView; // Установка представления данных
                        dataGridViewBeds.DataSource = dt; // Привязка данных к DataGridView
                        dataGridViewBeds.Columns["ID"].Visible = false; // Скрытие колонки ID
                        dataGridViewBeds.Columns["WardID"].Visible = false; // Скрытие колонки WardID

                        connection.Close(); // Закрытие соединения
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Ошибка при загрузке данных: " + ex.Message, "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
        }

        private void textBoxRoomNumber_KeyPress(object sender, KeyPressEventArgs e)
        {
            // Ограничение ввода только цифр
            if (!Regex.IsMatch(e.KeyChar.ToString(), @"^[0-9]+$"))
            {
                e.Handled = true;
            }
        }

        private void textBox1_KeyPress(object sender, KeyPressEventArgs e)
        {
            // Ограничение ввода только цифр
            if (!Regex.IsMatch(e.KeyChar.ToString(), @"^[0-9]+$"))
            {
                e.Handled = true;
            }
        }

        private void BedsForm_FormClosed(object sender, FormClosedEventArgs e)
        {
            // Показ MainForm при закрытии формы
            if (Application.OpenForms["MainForm"] is MainForm mn)
                mn.Show();
            else
                new MainForm().Show();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            // Проверка выбранных значений в ComboBox
            if (comboBox1.SelectedIndex != -1 && comboBoxType.SelectedIndex != -1)
            {
                int ward = Convert.ToInt32(comboBoxType.SelectedValue);
                string bedStatus = comboBox1.SelectedItem.ToString();

                using (MySqlConnection con = new MySqlConnection(GlobalValue.GetConnString()))
                {
                    con.Open(); // Открытие соединения

                    // Добавление новой койки
                    string query = $"INSERT INTO bed (WardID, BedStatus) VALUES (@WardID, @BedStatus)";
                    using (MySqlCommand cmd = new MySqlCommand(query, con))
                    {
                        cmd.Parameters.AddWithValue("@WardID", ward);
                        cmd.Parameters.AddWithValue("@BedStatus", bedStatus);

                        try
                        {
                            int res = cmd.ExecuteNonQuery();
                            MessageBox.Show("Запись добавлена.", "Успех.", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            LoadBedData(); // Обновление данных
                            comboBoxType.SelectedIndex = -1;
                            comboBox1.SelectedIndex = -1;
                        }
                        catch (Exception ex)
                        {
                            MessageBox.Show($"Ошибка при добавлении записи: {ex.Message}", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                    }

                    con.Close(); // Закрытие соединения
                }
            }
            else
            {
                MessageBox.Show("Пожалуйста, выберите тип койки и статус.", "Предупреждение", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            // Обновление данных о койке
            if (comboBox3.SelectedIndex != -1 && comboBox2.SelectedIndex != -1)
            {
                MySqlConnection con = new MySqlConnection(GlobalValue.GetConnString());
                con.Open();
                MySqlCommand cmd = new MySqlCommand($"UPDATE Bed SET BedStatus='" + comboBox2.SelectedItem.ToString() + "'," +
                    "WardID = " + comboBox3.SelectedValue + " WHERE BedID = " + dataGridViewBeds.Rows[rowId].Cells[0].Value.ToString() + "", con);
                int res = cmd.ExecuteNonQuery();
                con.Close();
                LoadBedData(); // Обновление данных
            }
        }

        private void dataGridViewBeds_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0 && User.Role == 1)
            {
                dataGridViewBeds.ClearSelection();
                dataGridViewBeds.Rows[e.RowIndex].Selected = true;

                rowId = e.RowIndex; // Сохранение ID выбранной строки
                tabControl1.SelectTab(1); // Переключение на вкладку редактирования

                // Установка значений в ComboBox
                foreach (DataRowView asda in comboBox3.Items)
                {
                    if ((dataGridViewBeds.Rows[e.RowIndex].Cells[1].Value.ToString() + " | " + dataGridViewBeds.Rows[e.RowIndex].Cells[2].Value.ToString() + " (" + dataGridViewBeds.Rows[e.RowIndex].Cells[3].Value.ToString() + ")").Contains(asda[1].ToString()))
                    {
                        comboBox3.SelectedItem = asda;
                    }
                }
                foreach (object aaa in comboBox2.Items)
                {
                    if (dataGridViewBeds.Rows[e.RowIndex].Cells[4].Value.ToString().Contains(aaa.ToString()))
                    {
                        comboBox2.SelectedItem = aaa;
                    }
                }
            }
            else if (e.RowIndex >= 0)
            {
                dataGridViewBeds.ClearSelection();
                dataGridViewBeds.Rows[e.RowIndex].Selected = true;
            }
        }

        private void tabControl1_SelectedIndexChanged(object sender, EventArgs e)
        {
            // Сброс выбранных значений при переключении вкладок
            if (tabControl1.SelectedTab == tabPage1)
            {
                comboBox3.SelectedIndex = -1;
                comboBox2.SelectedIndex = -1;
            }
            else
            {
                if (rowId != -1 && dataGridViewBeds.SelectedRows.Count != 0)
                {
                    comboBoxType.SelectedIndex = -1;
                    comboBox1.SelectedIndex = -1;
                }
                else
                {
                    MessageBox.Show("Выберите строку для редактирования.", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    tabControl1.SelectTab(tabPage1);
                    return;
                }
            }
        }

        private void dataGridViewBeds_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (User.Role == 1)
                DeleteButton_Click(sender, e); // Обработчик удаления
        }

        private void DeleteButton_Click(object sender, DataGridViewCellEventArgs e)
        {
            int relatedCount = 0;
            if (e.ColumnIndex == dataGridViewBeds.Columns["DeleteColumn"].Index && e.RowIndex >= 0)
            {
                // Подтверждение удаления
                if (DialogResult.Yes == MessageBox.Show("Вы уверены, что хотите удалить запись о выбранном койко месте?", "Подтверждение", MessageBoxButtons.YesNo, MessageBoxIcon.Warning))
                {
                    using (MySqlConnection connection = new MySqlConnection(GlobalValue.GetConnString()))
                    {
                        connection.Open();
                        using (MySqlCommand command = new MySqlCommand($"DELETE FROM Bed WHERE BedID = " + dataGridViewBeds.Rows[e.RowIndex].Cells[0].Value.ToString() + "", connection))
                        {
                            relatedCount = Convert.ToInt32(command.ExecuteNonQuery());
                        }
                    }
                    MessageBox.Show("Запись удалена.", "Успешно.", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    LoadBedData(); // Обновление данных
                }
                else
                {
                    dataGridViewBeds.ClearSelection();
                    return;
                }
            }
        }

        private void LoadComboBoxData()
        {
            UpdateFilter(); // Обновление фильтра
            comboBox1.Items.Clear();
            comboBox1.Items.Add("Свободна");
            comboBox1.Items.Add("Занята");
            comboBox1.Items.Add("Санитарная обработка");

            comboBox3.DataSource = null;
            comboBox3.Items.Clear();
            MySqlConnection con1 = new MySqlConnection(GlobalValue.GetConnString());
            con1.Open();
            MySqlCommand cmd1 = new MySqlCommand(@"SELECT 
                                   b.WardID,
                                   CONCAT(d.DepartmentName, ' | ', w.TypeOfWard, ' (', w.WardID, ')') AS BedInfo
                            FROM bed b
                            INNER JOIN Ward w ON b.WardID = w.WardID
                            INNER JOIN Department d ON w.DepartmentID = d.DepartmentID", con1);
            using (MySqlDataReader rd1 = cmd1.ExecuteReader())
            {
                DataTable bedTable1 = new DataTable();
                bedTable1.Columns.Add("WardID", typeof(int));
                bedTable1.Columns.Add("BedInfo", typeof(string));

                // Заполнение таблицы данными из БД
                while (rd1.Read())
                {
                    bedTable1.Rows.Add(rd1.GetInt32(0), rd1.GetString(1));
                }

                comboBox3.DataSource = bedTable1;
                comboBox3.DisplayMember = "BedInfo";
                comboBox3.ValueMember = "WardID";
            }
            con1.Close();

            comboBoxType.DataSource = null;
            comboBoxType.Items.Clear();

            MySqlConnection con = new MySqlConnection(GlobalValue.GetConnString());
            con.Open();
            MySqlCommand cmd = new MySqlCommand(@"SELECT 
                                   b.WardID,
                                   CONCAT(d.DepartmentName, ' | ', w.TypeOfWard, ' (', w.WardID, ')') AS BedInfo
                            FROM bed b
                            INNER JOIN Ward w ON b.WardID = w.WardID
                            INNER JOIN Department d ON w.DepartmentID = d.DepartmentID", con);
            using (MySqlDataReader rd = cmd.ExecuteReader())
            {
                DataTable bedTable = new DataTable();
                bedTable.Columns.Add("WardID", typeof(int));
                bedTable.Columns.Add("BedInfo", typeof(string));

                // Заполнение таблицы данными из БД
                while (rd.Read())
                {
                    bedTable.Rows.Add(rd.GetInt32(0), rd.GetString(1));
                }

                comboBoxType.DataSource = bedTable;
                comboBoxType.DisplayMember = "BedInfo";
                comboBoxType.ValueMember = "WardID";
            }
            con.Close();

            comboBoxType.SelectedIndex = -1;
            comboBox3.SelectedIndex = -1;
        }

        private void UpdateFilter()
        {
            string filter = "";
            string bedStatusFilter = "";
            string typeOfBedFilter = "";

            // Фильтрация по статусу койки
            if (comboBoxStatus.SelectedIndex != -1 && comboBoxStatus.SelectedIndex != 0)
            {
                bedStatusFilter = $"BedStatus = '{comboBoxStatus.SelectedItem}'";
            }

            // Фильтрация по типу палаты
            if (comboBoxTypeBed.SelectedIndex != -1 && comboBoxTypeBed.SelectedIndex != 0)
            {
                typeOfBedFilter = $"TypeOfWard = '{comboBoxTypeBed.SelectedItem}'";
            }

            // Объединение фильтров
            if (!string.IsNullOrEmpty(bedStatusFilter))
            {
                filter = bedStatusFilter;
            }
            if (!string.IsNullOrEmpty(typeOfBedFilter))
            {
                if (!string.IsNullOrEmpty(filter))
                {
                    filter += " AND ";
                }
                filter += typeOfBedFilter;
            }

            dt.RowFilter = filter; // Применение фильтра
            dataGridViewBeds.Columns["ID"].Visible = false; // Скрытие колонки ID
            dataGridViewBeds.Columns["WardID"].Visible = false; // Скрытие колонки ID

            dataGridViewBeds.ClearSelection();
        }

        private void comboBoxDepartment_SelectedIndexChanged(object sender, EventArgs e)
        {
            UpdateFilter(); // Обновление фильтра при изменении выбора
        }

        private void comboBoxSorting_SelectedIndexChanged(object sender, EventArgs e)
        {
            UpdateFilter(); // Обновление фильтра при изменении сортировки
        }

        private void buttonCancel_Click(object sender, EventArgs e)
        {
            // Сброс выбранной строки и переключение вкладки
            rowId = -1;
            tabControl1.SelectTab(0);
            comboBoxType.SelectedIndex = -1;
            comboBox1.SelectedIndex = -1;
            dataGridViewBeds.ClearSelection();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            // Сброс выбранной строки и переключение вкладки
            rowId = -1;
            tabControl1.SelectTab(0);
            comboBox2.SelectedIndex = -1;
            comboBox3.SelectedIndex = -1;
            dataGridViewBeds.ClearSelection();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            // Открытие формы отчета
            ReportForm rf = new ReportForm();
            this.Hide();
            rf.ShowDialog();
        }
    }
}