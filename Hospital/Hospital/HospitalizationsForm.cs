// Импорт необходимых пространств имен
using Microsoft.Office.Interop.Word;
using MySql.Data.MySqlClient;
using MySqlX.XDevAPI.Relational;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;
using Table = Microsoft.Office.Interop.Word.Table;
using word = Microsoft.Office.Interop.Word;

namespace Hospital
{
    // Класс формы для работы с госпитализациями
    public partial class HospitalizationsForm : Form
    {
        // Поля для хранения данных о госпитализациях
        private DataTable hospitalizationTable;
        private DataView hospitalizationView;

        // Флаги для отслеживания состояния формы
        private bool isAdding = false;
        private bool isDischarge = false;

        // Индекс выбранной строки
        private int selectedRowIndex = -1;

        // Конструктор формы
        public HospitalizationsForm()
        {
            InitializeComponent();
            LoadHospitalizationData(); // Загрузка данных о госпитализациях
            LoadComboBoxData(); // Загрузка данных в комбо-боксы

            if (User.Role == 3 || User.Role == 4)
            {
                ContextMenuSetup(); // Настройка контекстного меню

                // Загрузка дополнительных данных
                LoadPatientData();
                LoadBedsData(false);
                LoadDoctorsData();

                // Установка максимальных дат для элементов выбора даты
                this.dateTimePickerDischargeDate.MaxDate = DateTime.Now.Date.AddDays(182);
                this.dateTimePickerAdmissionDate.MaxDate = DateTime.Now.Date;
            }
        }

        // Настройка контекстного меню для DataGridView
        private void ContextMenuSetup()
        {
            ContextMenuStrip contextMenu = new ContextMenuStrip();
            ToolStripMenuItem addMenuItem = new ToolStripMenuItem("Добавить");
            contextMenu.Items.Add(addMenuItem);
            addMenuItem.Click += AddMenuItem_Click;

            // Добавление пункта "Редактировать" только для ролей, отличных от 4
            if (User.Role!= 4)
            {
                ToolStripMenuItem editMenuItem = new ToolStripMenuItem("Редактировать");
                contextMenu.Items.Add(editMenuItem);
                editMenuItem.Click += EditMenuItem_Click;
            }

            dataGridViewHospitalizations.ContextMenuStrip = contextMenu;
        }

        // Обработчик клика по пункту "Добавить" в контекстном меню
        private void AddMenuItem_Click(object sender, EventArgs e)
        {
            this.button2.Text = "Добавить";
            this.button2.Click += button2_Click;
            button2.Click -= button3_Click;
            dateTimePickerAdmissionDate.Value = DateTime.Now.Date;
            dateTimePickerAdmissionDate.Enabled = false;

            // Установка минимальной даты выписки (через 3 дня от текущей)
            this.dateTimePickerDischargeDate.MinDate = DateTime.Now.Date.AddDays(3);

            isAdding = true;
            selectedRowIndex = -1;

            ClearInputFields(); // Очистка полей ввода

            // Изменение размера и положения формы
            this.Size = new Size(1914, 708);
            this.Location = new Point(this.Location.X, 132);

            LoadBedsData(true); // Загрузка только свободных коек

            dataGridViewHospitalizations.ClearSelection(); // Снятие выделения
        }

        // Обработчик клика по пункту "Редактировать" в контекстном меню
        private void EditMenuItem_Click(object sender, EventArgs e)
        {
            this.button2.Text = "Применить";
            this.button2.Click += button3_Click;
            this.button2.Click -= button2_Click;

            // Установка минимальной даты выписки
            this.dateTimePickerDischargeDate.MinDate = DateTime.Parse("01.01.2000");

            isAdding = false;
            dateTimePickerAdmissionDate.Enabled = true;
            if (dataGridViewHospitalizations.SelectedRows.Count > 0)
            {
                LoadBedsData(false); // Загрузка всех коек
                selectedRowIndex = dataGridViewHospitalizations.SelectedRows[0].Index;
                LoadSelectedRowData(); // Загрузка данных выбранной строки
                // Изменение размера формы
                this.Size = new Size(1914, 708);
                this.Location = new Point(this.Location.X, 132);
            }
            else
            {
                MessageBox.Show("Выберите строку для редактирования.", "Предупреждение", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
        }

        // Метод для очистки полей ввода
        private void ClearInputFields()
        {
            comboBoxPatient.SelectedIndex = -1;
            comboBoxBed.SelectedIndex = 0;
            comboBox1.SelectedIndex = -1;
            dateTimePickerAdmissionDate.Value = DateTime.Now.Date;
            dateTimePickerDischargeDate.Value = DateTime.Now.Date.AddDays(3);
        }

        // Загрузка данных из выбранной строки в поля формы
        private void LoadSelectedRowData()
        {
            if (selectedRowIndex >= 0)
            {
                // Получаем выбранную строку
                DataGridViewRow selectedRow = dataGridViewHospitalizations.Rows[selectedRowIndex];

                // Заполнение Patient
                if (selectedRow.Cells["PatientFIO"].Value != null && selectedRow.Cells["PatientFIO"].Value != DBNull.Value)
                {
                    // Ищем пациента в ComboBox
                    foreach (DataRowView rowView in comboBoxPatient.Items)
                    {
                        // Объединяем ФИО из отдельных полей
                        string patientFIO = rowView.Row[1].ToString();
                        // Получаем ФИО из DataGridView
                        string selectedPatientSurname = selectedRow.Cells["PatientFIO"].Value.ToString();

                        string selectedPatientFIO = selectedPatientSurname;

                        if (patientFIO == selectedPatientFIO)
                        {
                            comboBoxPatient.SelectedItem = rowView;
                            break;
                        }
                    }
                }

                //Заполняем Bed
                if (selectedRow.Cells["BedID"].Value != null && selectedRow.Cells["BedID"].Value != DBNull.Value)
                {
                    // Ищем койку в ComboBox
                    if (selectedRow.Cells["BedID"].Value != null && selectedRow.Cells["BedID"].Value != DBNull.Value)
                    {
                        // Ищем койку в ComboBox
                        foreach (DataRowView rowView in comboBoxBed.Items)
                        {
                            if (rowView.Row["BedID"].ToString() == selectedRow.Cells["BedID"].Value.ToString())
                            {
                                comboBoxBed.SelectedValue = Convert.ToInt32(rowView.Row["BedID"].ToString());
                                break;
                            }
                        }
                    }
                }

                // Заполнение Doctor (Employee)
                if (selectedRow.Cells["EmployeeFIO"].Value != null && selectedRow.Cells["EmployeeFIO"].Value != DBNull.Value)
                {
                    // Ищем врача в ComboBox
                    foreach (DataRowView rowView in comboBox1.Items)
                    {
                        string employeeFIO = rowView.Row[1].ToString();
                        // Получаем ФИО из DataGridView
                        string selectedEmployeeSurname = selectedRow.Cells["EmployeeFIO"].Value.ToString();

                        string selectedEmployeeFIO = selectedEmployeeSurname;

                        if (employeeFIO == selectedEmployeeFIO)
                        {
                            comboBox1.SelectedItem = rowView;
                            break;
                        }
                    }
                }

                // Заполнение DateOfReceipt
                if (selectedRow.Cells["DateOfReceipt"].Value != null && selectedRow.Cells["DateOfReceipt"].Value != DBNull.Value)
                {
                    if (DateTime.TryParse(selectedRow.Cells["DateOfReceipt"].Value.ToString(), out DateTime dateOfReceipt))
                    {
                        dateTimePickerAdmissionDate.Value = dateOfReceipt;
                        dateTimePickerAdmissionDate.Checked = true;
                    }
                }

                // Заполнение DateOfDischarge
                if (selectedRow.Cells["DateOfDischarge"].Value != null && selectedRow.Cells["DateOfDischarge"].Value != DBNull.Value)
                {
                    if (DateTime.TryParse(selectedRow.Cells["DateOfDischarge"].Value.ToString(), out DateTime dateOfDischarge))
                    {
                        dateTimePickerDischargeDate.Value = dateOfDischarge;
                        dateTimePickerDischargeDate.Checked = true;
                    }
                }
            }
        }

        // Загрузка данных о пациентах в ComboBox
        private void LoadPatientData()
        {
            MySqlConnection con = new MySqlConnection(GlobalValue.GetConnString());
            try
            {
                con.Open();
                // SQL-запрос для получения ID и ФИО пациентов
                MySqlCommand cmd = new MySqlCommand("SELECT PatientID, CONCAT(PatientSurname, ' ', PatientName, ' ', PatientPatronymic) AS PatientFIO FROM patient", con);
                MySqlDataReader rd = cmd.ExecuteReader();
                comboBoxPatient.Items.Clear();
                DataTable patientTable = new DataTable();
                patientTable.Columns.Add("PatientID", typeof(int));
                patientTable.Columns.Add("PatientFIO", typeof(string));

                // Заполнение таблицы данными из БД
                while (rd.Read())
                {
                    PatientItem newItem = new PatientItem(rd.GetInt32(0), rd.GetString(1));
                    patientTable.Rows.Add(newItem.PatientID, newItem.PatientFIO);
                }
                comboBoxPatient.DataSource = patientTable;
                comboBoxPatient.DisplayMember = "PatientFIO";
                comboBoxPatient.ValueMember = "PatientID";
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка при загрузке данных Patient: {ex.Message}", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                con.Close();
            }
        }

        // Загрузка данных о врачах в ComboBox
        private void LoadDoctorsData()
        {
            MySqlConnection con = new MySqlConnection(GlobalValue.GetConnString());
            try
            {
                con.Open();
                // SQL-запрос для получения ID и ФИО врачей (роль = 3)
                MySqlCommand cmd = new MySqlCommand("SELECT EmployeeID, CONCAT(EmployeeSurname, ' ', EmployeeName, ' ', EmployeePatronymic) AS EmployeeFIO FROM employee WHERE Role = 3", con);
                MySqlDataReader rd = cmd.ExecuteReader();
                DataTable employeeTable = new DataTable();
                employeeTable.Columns.Add("EmployeeID", typeof(int));
                employeeTable.Columns.Add("EmployeeFIO", typeof(string));

                // Заполнение таблицы данными из БД
                while (rd.Read())
                {
                    DoctorItem newItem = new DoctorItem(rd.GetInt32(0), rd.GetString(1));
                    employeeTable.Rows.Add(newItem.DoctorID, newItem.DoctorFIO);
                }
                comboBox1.DataSource = employeeTable;
                comboBox1.DisplayMember = "EmployeeFIO";
                comboBox1.ValueMember = "EmployeeID";
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка при загрузке данных Employee: {ex.Message}", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                con.Close();
            }
        }

        // Загрузка данных о койках в ComboBox
        private void LoadBedsData(bool onlyFree)
        {
            comboBoxBed.DataSource = null;
            comboBoxBed.Items.Clear();

            using (MySqlConnection con = new MySqlConnection(GlobalValue.GetConnString()))
            {
                try
                {
                    con.Open();
                    string query = @"SELECT 
                                   b.BedID,
                                   CONCAT(d.DepartmentName, ' | ', w.TypeOfWard, ' (', w.WardID, ') | ', b.BedID) AS BedInfo
                            FROM bed b
                            INNER JOIN Ward w ON b.WardID = w.WardID
                            INNER JOIN Department d ON w.DepartmentID = d.DepartmentID";

                    // Добавляем условие для фильтрации только свободных коек
                    if (onlyFree)
                    {
                        query += " WHERE b.BedStatus = 'Свободна'";
                    }

                    using (MySqlCommand cmd = new MySqlCommand(query, con))
                    {
                        using (MySqlDataReader rd = cmd.ExecuteReader())
                        {
                            DataTable bedTable = new DataTable();
                            bedTable.Columns.Add("BedID", typeof(int));
                            bedTable.Columns.Add("BedInfo", typeof(string));

                            // Заполнение таблицы данными из БД
                            while (rd.Read())
                            {
                                bedTable.Rows.Add(rd.GetInt32(0), rd.GetString(1));
                            }

                            comboBoxBed.DataSource = bedTable;
                            comboBoxBed.DisplayMember = "BedInfo";
                            comboBoxBed.ValueMember = "BedID";
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Ошибка при загрузке данных Bed: {ex.Message}", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                finally
                {
                    if (con.State == ConnectionState.Open)
                        con.Close();
                }
            }
        }

        // Загрузка данных о госпитализациях в DataGridView
        private void LoadHospitalizationData()
        {
            dataGridViewHospitalizations.DataSource = null;
            dataGridViewHospitalizations.Columns.Clear();
            dataGridViewHospitalizations.Rows.Clear();

            try
            {
                using (MySqlConnection connection = new MySqlConnection(GlobalValue.GetConnString()))
                {
                    connection.Open();

                    // SQL-запрос для получения данных о госпитализациях
                    string query = @"SELECT h.HospitalizationID, CONCAT(p.PatientSurname, ' ', p.PatientName, ' ', p.PatientPatronymic) AS PatientFIO, " +
                        "d.DepartmentName, w.TypeOfWard, b.BedStatus, CONCAT(e.EmployeeSurname, ' ', e.EmployeeName, ' ', e.EmployeePatronymic) AS EmployeeFIO, " +
                        "h.DateOfReceipt, h.DateOfDischarge, h.PatientID, h.DepartmentID, h.EmployeeID, h.BedID " +
                        "FROM Hospitalization h " +
                        "INNER JOIN Patient p ON h.PatientID = p.PatientID " +
                        "INNER JOIN Department d ON h.DepartmentID = d.DepartmentID " +
                        "INNER JOIN Bed b ON h.BedID = b.BedID " +
                        "INNER JOIN Ward w ON b.WardID = w.WardID " +
                        "INNER JOIN Employee e ON h.EmployeeID = e.EmployeeID " +
                        "ORDER BY p.PatientSurname ASC";

                    using (MySqlCommand command = new MySqlCommand(query, connection))
                    {
                        MySqlDataAdapter adapter = new MySqlDataAdapter(command);
                        hospitalizationTable = new DataTable();
                        adapter.Fill(hospitalizationTable);

                        hospitalizationView = hospitalizationTable.DefaultView;

                        dataGridViewHospitalizations.AutoGenerateColumns = false;
                        dataGridViewHospitalizations.Columns.Clear();

                        // Добавление колонок в DataGridView
                        AddDataGridViewColumn("HospitalizationID", "HospitalizationID", "HospitalizationID");
                        AddDataGridViewColumn("PatientFIO", "ФИО пациента", "PatientFIO");
                        AddDataGridViewColumn("DepartmentName", "Отделение", "DepartmentName");
                        AddDataGridViewColumn("TypeOfWard", "Палата", "TypeOfWard");
                        AddDataGridViewColumn("BedStatus", "Статус койки", "BedStatus");
                        AddDataGridViewColumn("EmployeeFIO", "ФИО врача", "EmployeeFIO");
                        AddDataGridViewColumn("DateOfReceipt", "Дата Поступления", "DateOfReceipt");
                        AddDataGridViewColumn("DateOfDischarge", "Дата Выписки", "DateOfDischarge");
                        AddDataGridViewColumn("PatientID", "PatientID", "PatientID");
                        AddDataGridViewColumn("DepartmentID", "DepartmentID", "DepartmentID");
                        AddDataGridViewColumn("EmployeeID", "EmployeeID", "EmployeeID");
                        AddDataGridViewColumn("BedID", "BedID", "BedID");

                        dataGridViewHospitalizations.DataSource = hospitalizationView;

                        // Скрытие технических колонок
                        dataGridViewHospitalizations.Columns["HospitalizationID"].Visible = false;
                        dataGridViewHospitalizations.Columns["PatientID"].Visible = false;
                        dataGridViewHospitalizations.Columns["DepartmentID"].Visible = false;
                        dataGridViewHospitalizations.Columns["EmployeeID"].Visible = false;
                        dataGridViewHospitalizations.Columns["BedID"].Visible = false;
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка при загрузке данных: {ex.Message}", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        // Метод для добавления колонки в DataGridView
        private void AddDataGridViewColumn(string name, string headerText, string dataPropertyName)
        {
            DataGridViewTextBoxColumn column = new DataGridViewTextBoxColumn();
            column.Name = name;
            column.HeaderText = headerText;
            column.DataPropertyName = dataPropertyName;
            dataGridViewHospitalizations.Columns.Add(column);
        }

        // Загрузка данных в ComboBox для фильтрации
        private void LoadComboBoxData()
        {
            comboBoxDepartment.Items.Add("Все отделения");

            try
            {
                using (MySqlConnection connection = new MySqlConnection(GlobalValue.GetConnString()))
                {
                    connection.Open();
                    string query = "SELECT DepartmentName FROM Department";
                    using (MySqlCommand command = new MySqlCommand(query, connection))
                    {
                        MySqlDataReader reader = command.ExecuteReader();
                        while (reader.Read())
                        {
                            comboBoxDepartment.Items.Add(reader["DepartmentName"].ToString());
                        }
                        reader.Close();
                    }
                }

                comboBoxDepartment.SelectedIndex = 0;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка при загрузке отделений: {ex.Message}", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            // Добавление вариантов сортировки
            comboBoxSorting.Items.Add("Дата поступления (по возрастанию)");
            comboBoxSorting.Items.Add("Дата поступления (по убыванию)");
            comboBoxSorting.Items.Add("Дата выписки (по возрастанию)");
            comboBoxSorting.Items.Add("Дата выписки (по убыванию)");
            comboBoxSorting.SelectedIndex = 1;

            UpdateSorting(); // Применение сортировки
        }

        // Обновление фильтрации данных
        private void UpdateFilter()
        {
            string filter = "";

            // Фильтр по ФИО пациента
            string surnameFilter = textBoxSearch.Text.Trim();
            if (!string.IsNullOrEmpty(surnameFilter))
            {
                filter += $"PatientFIO LIKE '%{surnameFilter}%'";
            }

            // Фильтр по отделению
            if (comboBoxDepartment.SelectedItem != null)
            {
                string departmentFilter = comboBoxDepartment.SelectedItem.ToString();
                if (departmentFilter != "Все отделения")
                {
                    if (!string.IsNullOrEmpty(filter))
                    {
                        filter += " AND ";
                    }
                    filter += $"DepartmentName = '{departmentFilter}'";
                }
            }

            hospitalizationView.RowFilter = filter; // Применение фильтра
        }

        // Обработчик изменения текста в поле поиска
        private void textBoxSearch_TextChanged(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(textBoxSearch.Text.Trim()))
            {
                LoadHospitalizationData(); // Перезагрузка данных при очистке поля
            }
            else
            {
                UpdateFilter(); // Применение фильтра
            }
        }

        // Обработчик изменения выбранного отделения
        private void ComboBoxDepartment_SelectedIndexChanged(object sender, EventArgs e)
        {
            UpdateFilter(); // Обновление фильтра
        }

        // Обновление сортировки данных
        private void UpdateSorting()
        {
            string sortingString = comboBoxSorting.SelectedItem.ToString();

            string sortColumn;
            if (sortingString == "Дата поступления (по возрастанию)")
            {
                sortColumn = "DateOfReceipt ASC";
            }
            else if (sortingString == "Дата поступления (по убыванию)")
            {
                sortColumn = "DateOfReceipt DESC";
            }
            else if (sortingString == "Дата выписки (по возрастанию)")
            {
                sortColumn = "DateOfDischarge ASC";
            }
            else
            {
                sortColumn = "DateOfDischarge DESC";
            }

            hospitalizationView.Sort = sortColumn; // Применение сортировки
        }

        // Обработчик изменения способа сортировки
        private void ComboBoxSorting_SelectedIndexChanged(object sender, EventArgs e)
        {
            UpdateSorting(); // Обновление сортировки
        }

        // Обработчик нажатия клавиш в поле диагноза
        private void textBoxDiagnosis_KeyPress(object sender, KeyPressEventArgs e)
        {
            // Разрешаем только русские буквы, пробелы и дефисы
            if (!Regex.IsMatch(e.KeyChar.ToString(), @"^[А-Яа-яЁё\s-]+$"))
            {
                e.Handled = true;
            }
        }

        // Обработчик нажатия кнопки "Добавить"
        private void button2_Click(object sender, EventArgs e) // Add
        {
            // Проверка валидности дат
            if (!DateIsValid(dateTimePickerAdmissionDate) || !DateIsValid(dateTimePickerDischargeDate))
            {
                MessageBox.Show("Пожалуйста, выберите даты поступления и выписки.", "Предупреждение", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            // Проверка, чтобы дата выписки была позже или равна дате поступления
            if (dateTimePickerDischargeDate.Value.Date < dateTimePickerAdmissionDate.Value.Date)
            {
                MessageBox.Show("Дата выписки не может быть раньше даты поступления.", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            // Проверка выбора пациента и врача
            if (comboBoxPatient.SelectedItem == null || comboBox1.SelectedItem == null)
            {
                MessageBox.Show("Пожалуйста, выберите пациента и врача.", "Предупреждение", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            int patientId = (int)comboBoxPatient.SelectedValue;
            int bedId = (int)comboBoxBed.SelectedValue;
            int employeeId = (int)comboBox1.SelectedValue;

            MySqlConnection con = new MySqlConnection(GlobalValue.GetConnString());
            con.Open();
            MySqlCommand cmd = new MySqlCommand($"SELECT DepartmentID From department LIMIT 1", con);
            int departmentId = (int)cmd.ExecuteScalar();
            con.Close();

            using (MySqlConnection connection = new MySqlConnection(GlobalValue.GetConnString()))
            {
                connection.Open();

                // Проверка на дублирование госпитализации (активная госпитализация на ту же койку)
                string checkDuplicateQuery = @"SELECT COUNT(*)
                                               FROM Hospitalization
                                               WHERE PatientID = @PatientID
                                                 AND BedID = @BedID
                                                 AND DateOfDischarge IS NULL  -- Проверяем только активные госпитализации
                                                 AND NOT (DateOfReceipt > @DateOfDischarge OR IFNULL(DateOfDischarge, @DateOfReceipt) < @DateOfReceipt)";
                using (MySqlCommand checkDuplicateCommand = new MySqlCommand(checkDuplicateQuery, connection))
                {
                    checkDuplicateCommand.Parameters.AddWithValue("@PatientID", patientId);
                    checkDuplicateCommand.Parameters.AddWithValue("@BedID", bedId);
                    checkDuplicateCommand.Parameters.AddWithValue("@DateOfReceipt", dateTimePickerAdmissionDate.Value.ToString("yyyy-MM-dd"));
                    checkDuplicateCommand.Parameters.AddWithValue("@DateOfDischarge", dateTimePickerDischargeDate.Value.ToString("yyyy-MM-dd"));

                    int duplicateCount = Convert.ToInt32(checkDuplicateCommand.ExecuteScalar());

                    if (duplicateCount > 0)
                    {
                        MessageBox.Show("У этого пациента уже есть активная госпитализация на эту койку.", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return; // Прерываем добавление, если дубликат
                    }
                }

                // Вставка данных о госпитализации
                string query = $"INSERT INTO Hospitalization (PatientID, DepartmentID, EmployeeID, BedID, DateOfReceipt, DateOfDischarge) " +
                               $"VALUES (@PatientID, @DepartmentID, @EmployeeID, @BedID, @DateOfReceipt, @DateOfDischarge)";

                // Обновление статуса койки
                string updateBedQuery = "UPDATE Bed SET BedStatus = 'Занята' WHERE BedID = @BedID";

                try
                {
                    MySqlCommand cmd1 = new MySqlCommand($"SELECT DepartmentID From department LIMIT 1", connection);
                    int departmentId1 = Convert.ToInt32(cmd1.ExecuteScalar());

                    using (MySqlCommand command = new MySqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@PatientID", patientId);
                        command.Parameters.AddWithValue("@DepartmentID", departmentId1);
                        command.Parameters.AddWithValue("@EmployeeID", employeeId);
                        command.Parameters.AddWithValue("@BedID", bedId);
                        command.Parameters.AddWithValue("@DateOfReceipt", dateTimePickerAdmissionDate.Value.ToString("yyyy-MM-dd"));
                        command.Parameters.AddWithValue("@DateOfDischarge", dateTimePickerDischargeDate.Value.ToString("yyyy-MM-dd"));
                        command.ExecuteNonQuery();
                    }

                    using (MySqlCommand updateBedCommand = new MySqlCommand(updateBedQuery, connection))
                    {
                        updateBedCommand.Parameters.AddWithValue("@BedID", bedId);
                        updateBedCommand.ExecuteNonQuery();
                    }

                    MessageBox.Show("Госпитализация успешно добавлена, статус койки обновлен.", "Успех.", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    LoadHospitalizationData(); // Обновляем DataGridView
                    LoadBedsData(false);

                    dataGridViewHospitalizations.Rows[dataGridViewHospitalizations.Rows.Count - 1].Selected = true;

                    // Очистка полей после добавления
                    ClearInputFields();

                    this.Size = new Size(1914, 539);
                    if (User.Role == 3)
                    {
                        // Предложение создать направление на госпитализацию
                        if (DialogResult.Yes == MessageBox.Show("Создать направление на госпитализацию?", "Создание направления", MessageBoxButtons.YesNo, MessageBoxIcon.Information))
                        {
                            MySqlConnection co = new MySqlConnection(GlobalValue.GetConnString());
                            co.Open();
                            MySqlCommand cm = new MySqlCommand($"SELECT HospitalizationID FROM hospital.hospitalization order by HospitalizationID desc limit 1;", co);
                            int i = (int)cm.ExecuteScalar();
                            co.Close();
                            GenerateRefferalForHospitalization(i); // Генерация направления
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Ошибка при добавлении госпитализации или обновлении статуса койки: {ex.Message}", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
            }
        }

        // Обработчик нажатия кнопки "Редактировать"
        private void button3_Click(object sender, EventArgs e) // Edit
        {
            if (dataGridViewHospitalizations.SelectedRows.Count != 1)
            {
                MessageBox.Show("Выберите одну строку для редактирования.", "Предупреждение", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // Проверка валидности дат
            if (!DateIsValid(dateTimePickerAdmissionDate) || !DateIsValid(dateTimePickerDischargeDate))
            {
                MessageBox.Show("Пожалуйста, выберите даты поступления и выписки.", "Предупреждение", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // Проверка, чтобы дата выписки была позже или равна дате поступления
            if (dateTimePickerDischargeDate.Value.Date < dateTimePickerAdmissionDate.Value.Date)
            {
                MessageBox.Show("Дата выписки не может быть раньше даты поступления.", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            // Проверка выбора пациента, отделения и врача
            if (comboBoxPatient.SelectedItem == null || comboBox1.SelectedItem == null)
            {
                MessageBox.Show("Пожалуйста, выберите пациента, отделение и врача.", "Предупреждение", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            int patientId = (int)comboBoxPatient.SelectedValue;
            int bedId = (int)comboBoxBed.SelectedValue;
            int employeeId = (int)comboBox1.SelectedValue;

            MySqlConnection con = new MySqlConnection(GlobalValue.GetConnString());
            con.Open();
            MySqlCommand cmd = new MySqlCommand($"SELECT DepartmentID From department LIMIT 1", con);
            int departmentId = (int)cmd.ExecuteScalar();
            con.Close();

            int hospitalizationId = Convert.ToInt32(dataGridViewHospitalizations.SelectedRows[0].Cells["HospitalizationID"].Value);

            using (MySqlConnection connection = new MySqlConnection(GlobalValue.GetConnString()))
            {
                connection.Open();

                // SQL-запрос для обновления данных о госпитализации
                string query = @"UPDATE Hospitalization 
                         SET PatientID = @PatientID, DepartmentID = @DepartmentID, EmployeeID = @EmployeeID,BedID = @BedID,
                             DateOfReceipt = @DateOfReceipt, DateOfDischarge = @DateOfDischarge
                         WHERE HospitalizationID = @HospitalizationID";

                using (MySqlCommand command = new MySqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@PatientID", patientId);
                    command.Parameters.AddWithValue("@DepartmentID", departmentId);
                    command.Parameters.AddWithValue("@EmployeeID", employeeId);
                    command.Parameters.AddWithValue("@BedID", bedId);
                    command.Parameters.AddWithValue("@DateOfReceipt", dateTimePickerAdmissionDate.Value.Date.ToString("yyyy-MM-dd"));
                    command.Parameters.AddWithValue("@DateOfDischarge", dateTimePickerDischargeDate.Value.Date.ToString("yyyy-MM-dd"));
                    command.Parameters.AddWithValue("@HospitalizationID", hospitalizationId);

                    try
                    {
                        command.ExecuteNonQuery();
                        MessageBox.Show("Госпитализация успешно отредактирована.", "Информация", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        LoadHospitalizationData(); // Обновляем DataGridView
                        this.Size = new Size(1914, 539);
                        ClearInputFields();
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show($"Ошибка при редактировании госпитализации: {ex.Message}", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
        }

        // Генерация направления на госпитализацию в Word
        private void GenerateRefferalForHospitalization(int hospitalizationId)
        {
            try
            {
                // Создаем экземпляр приложения Word и документ
                word.Application app = new word.Application();
                word.Document doc = app.Documents.Add();

                // Получение данных для направления
                ReferralData referralData = GetReferralData(hospitalizationId);

                if (referralData == null)
                {
                    MessageBox.Show("Данные для направления не найдены.", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    app.Quit();
                    return;
                }

                // Форматирование документа
                object missing = Type.Missing;

                // Добавление заголовка министерства
                Paragraph ministryHeader = doc.Content.Paragraphs.Add(ref missing);
                ministryHeader.Range.Text = "Министерство здравоохранения и социального\nразвития Российской Федерации";
                ministryHeader.Range.Font.Size = 10;
                ministryHeader.Alignment = WdParagraphAlignment.wdAlignParagraphLeft;
                ministryHeader.Range.InsertParagraphAfter();

                // Добавление пустого абзаца
                Paragraph space = doc.Content.Paragraphs.Add(ref missing);
                space.Range.Text = "\n";
                space.Alignment = WdParagraphAlignment.wdAlignParagraphRight;
                space.Range.InsertParagraphAfter();

                // Добавление информации о форме документа
                Paragraph documentationForm = doc.Content.Paragraphs.Add(ref missing);
                documentationForm.Range.Text = "Медицинская документация\nФорма N 057/у-04________\nутверждена приказом Минздравсоцразвития России\nот 22.11.2004 г. N 255";
                documentationForm.Range.Font.Size = 10;
                documentationForm.Alignment = WdParagraphAlignment.wdAlignParagraphRight;
                ministryHeader.BaseLineAlignment = WdBaselineAlignment.wdBaselineAlignCenter;
                documentationForm.Range.InsertParagraphAfter();

                Paragraph space1 = doc.Content.Paragraphs.Add(ref missing);
                space1.Alignment = WdParagraphAlignment.wdAlignParagraphCenter;
                space1.Range.InsertParagraphAfter();

                // Добавление заголовка направления
                Paragraph referralTitle = doc.Content.Paragraphs.Add();
                referralTitle.Range.Text = "Направление на госпитализацию";
                referralTitle.Range.Font.Size = 14;
                referralTitle.Range.Font.Bold = 1;
                referralTitle.Alignment = WdParagraphAlignment.wdAlignParagraphCenter;
                referralTitle.Range.InsertParagraphAfter();

                // Добавление информации об отделении
                Paragraph referralInstitution = doc.Content.Paragraphs.Add(ref missing);
                referralInstitution.Range.Font.Size = 10;
                referralInstitution.Range.Font.Bold = 0;
                referralInstitution.Range.Text = $"__________{referralData.DepartmentName}_________";
                referralInstitution.Range.Font.Underline = WdUnderline.wdUnderlineSingle;
                referralInstitution.Alignment = WdParagraphAlignment.wdAlignParagraphCenter;
                referralInstitution.Range.InsertParagraphAfter();

                Paragraph referralInstitution1 = doc.Content.Paragraphs.Add(ref missing);
                referralInstitution1.Range.Text = $"(наименование медицинского учреждения/отделения, куда направлен пациент)";
                referralInstitution1.Range.Font.Size = 10;
                referralInstitution1.Range.Font.Bold = 0;
                referralInstitution1.Range.Font.Underline = WdUnderline.wdUnderlineNone;
                referralInstitution1.Alignment = WdParagraphAlignment.wdAlignParagraphCenter;
                referralInstitution1.Range.InsertParagraphAfter();

                Paragraph space3 = doc.Content.Paragraphs.Add(ref missing);
                space3.Range.Text = " ";
                space3.Range.Font.Size = 1;
                space3.Range.Font.Underline = WdUnderline.wdUnderlineNone;
                space3.Alignment = WdParagraphAlignment.wdAlignParagraphLeft;
                space3.Range.InsertParagraphAfter();

                // Добавление информации о страховом полисе
                Paragraph insurancePolicy = doc.Content.Paragraphs.Add(ref missing);
                insurancePolicy.Range.Text = $"1. Номер страхового полиса ОМС: {referralData.InsurancePolicy}";
                insurancePolicy.Range.Font.Size = 10;
                insurancePolicy.Alignment = WdParagraphAlignment.wdAlignParagraphLeft;
                insurancePolicy.Range.Font.Underline = WdUnderline.wdUnderlineNone;
                insurancePolicy.Range.InsertParagraphAfter();

                // Добавление информации о пациенте
                Paragraph patientInfo = doc.Content.Paragraphs.Add(ref missing);
                patientInfo.Range.Text = $"2. Фамилия, имя, отчество: {referralData.PatientFIO}\n" +
                                          $"3. Дата рождения: {referralData.PatientBirthday:dd.MM.yyyy}\n" +
                                          $"4. Адрес постоянного места жительства: {referralData.PatientAddress}";
                patientInfo.Range.Font.Size = 10;
                patientInfo.Alignment = WdParagraphAlignment.wdAlignParagraphLeft;
                patientInfo.Range.InsertParagraphAfter();

                // Добавление обоснования направления
                Paragraph justification = doc.Content.Paragraphs.Add(ref missing);
                justification.Range.Text = "5. Обоснование направления:\n" + referralData.Justification;
                justification.Range.Font.Size = 10;
                justification.Alignment = WdParagraphAlignment.wdAlignParagraphLeft;
                justification.Range.InsertParagraphAfter();

                // Добавление информации о враче
                Paragraph doctorInfo = doc.Content.Paragraphs.Add(ref missing);
                doctorInfo.Range.Text = $"Должность медицинского работника, направившего больного: {referralData.EmployeePost}\n" +
                                           $"{referralData.EmployeeFIO} ________________\n" +
                                           $"Ф.И.О.                                      подпись\n" +
                                           $"\"{DateTime.Now:dd}\" {DateTime.Now:MMMM} {DateTime.Now:yyyy}г.";
                doctorInfo.Range.Font.Size = 10;
                doctorInfo.Alignment = WdParagraphAlignment.wdAlignParagraphLeft;
                doctorInfo.Range.InsertParagraphAfter();

                // Сохранение и отображение документа
                string fileName = $"Направление_{DateTime.Now:yyyyMMdd_HHmmss}.docx";
                doc.SaveAs(FileName: fileName);

                app.Visible = true;

                MessageBox.Show("Направление успешно сформировано по пути: " + doc.FullName, "Информация", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка при формировании направления: {ex.Message}", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
        }

        // Получение данных для направления на госпитализацию
        private ReferralData GetReferralData(int hospitalizationId)
        {
            ReferralData data = new ReferralData();

            string connectionString = GlobalValue.GetConnString();
            // SQL-запрос для получения данных о госпитализации
            string query = @"SELECT
                        p.Insurance_Policy,
                        CONCAT(p.PatientSurname, ' ', p.PatientName, ' ', p.PatientPatronymic) AS PatientFIO,
                        p.Birthday,
                        p.Address,
                        p.PhoneNumber,
                        p.PatientID,
                        CONCAT(e.EmployeeSurname, ' ', e.EmployeeName, ' ', e.EmployeePatronymic) AS EmployeeFIO,
                        e.Post,
                        h.DateOfReceipt,
                        h.DateOfDischarge,
                        d.DepartmentName,
                        h.EmployeeID,
                        h.HospitalizationID,
                        mh.Diagnosis,
                        mh.Description
                    FROM
                        Hospitalization h
                    INNER JOIN
                        Patient p ON h.PatientID = p.PatientID
                    INNER JOIN
                        Employee e ON h.EmployeeID = e.EmployeeID
                         INNER JOIN
                        Department d ON h.DepartmentID = d.DepartmentID
                    INNER JOIN
                        MedicalHistory mh ON p.MedicalHistoryID = mh.MedicalHistoryID
                    WHERE h.HospitalizationID = @HospitalizationID;";
            try
            {
                using (MySqlConnection connection = new MySqlConnection(connectionString))
                {
                    connection.Open();
                    using (MySqlCommand command = new MySqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@HospitalizationID", hospitalizationId);

                        using (MySqlDataReader reader = command.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                // Заполнение объекта данными из БД
                                data.InsurancePolicy = reader["Insurance_Policy"].ToString();
                                data.PatientFIO = reader["PatientFIO"].ToString();
                                data.PatientBirthday = Convert.ToDateTime(reader["Birthday"]);
                                data.PatientAddress = reader["Address"].ToString();
                                data.PhoneNumber = reader["PhoneNumber"].ToString();

                                data.DepartmentName = reader["DepartmentName"].ToString();

                                data.EmployeeFIO = reader["EmployeeFIO"].ToString();
                                data.EmployeePost = reader["Post"].ToString();
                                // Объединяем диагноз и описание как обоснование
                                data.Justification = $"Диагноз: {reader["Diagnosis"]}\nОписание: {reader["Description"]}";

                                return data;
                            }
                            else
                            {
                                return null; // Данные для направления не найдены
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка при получении данных: {ex.Message}", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return null;
            }
        }

        // Класс для хранения информации о направлении
        private class ReferralData
        {
            public string InsurancePolicy { get; set; }
            public string PatientFIO { get; set; }
            public DateTime PatientBirthday { get; set; }
            public string PatientAddress { get; set; }
            public string EmployeeFIO { get; set; }
            public string EmployeePost { get; set; }
            public string Justification { get; set; }
            public string PhoneNumber { get; set; }
            public string DepartmentName { get; set; }
        }

        // Проверка валидности даты
        private bool DateIsValid(DateTimePicker dt)
        {
            return dt.Checked;
        }

        // Обработчик закрытия формы
        private void HospitalizationsForm_FormClosed(object sender, FormClosedEventArgs e)
        {
            // Показываем главную форму при закрытии
            if (System.Windows.Forms.Application.OpenForms["MainForm"] is MainForm mn)
                mn.Show();
            else
                new MainForm().Show();
        }

        // Обработчик нажатия клавиш в поле поиска
        private void textBoxSearch_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)Keys.Back || e.KeyChar == (char)Keys.Space)
            {
                e.Handled = false;
                return;
            }

            // Разрешаем только русские буквы
            if (!Regex.IsMatch(e.KeyChar.ToString(), @"^[А-Яа-яЁё]+$"))
            {
                e.Handled = true;
            }
        }

        // Обработчик нажатия кнопки "Отмена"
        private void button1_Click(object sender, EventArgs e)
        {
            // Возвращение формы к исходному состоянию
            this.Size = new Size(1914, 539);
            this.Location = new Point(this.Location.X, 257);
            this.button1.Text = "Добавить";

            // Сброс выбранных значений
            comboBoxPatient.SelectedIndex = -1;
            comboBoxDepartment.SelectedIndex = -1;
            comboBox1.SelectedIndex = -1;
            dateTimePickerAdmissionDate.Value = DateTime.Now.Date;
            dateTimePickerDischargeDate.Value = DateTime.Now.Date.AddDays(3);

            dataGridViewHospitalizations.ClearSelection();
        }

        // Обработчик клика по ячейке DataGridView
        private void dataGridViewHospitalizations_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                dataGridViewHospitalizations.Rows[e.RowIndex].Selected = true;
            }
        }

        // Обработчик события перед отрисовкой строки DataGridView
        private void dataGridViewHospitalizations_RowPrePaint(object sender, DataGridViewRowPrePaintEventArgs e)
        {
            dataGridViewHospitalizations.Columns[0].Visible = false;

            // Подсветка строк в зависимости от даты выписки
            if (e.RowIndex >= 0 && dataGridViewHospitalizations.Rows[e.RowIndex].Cells[7].Value != null)
            {
                if (DateTime.Parse(dataGridViewHospitalizations.Rows[e.RowIndex].Cells[7].Value.ToString()) > DateTime.Now.Date)
                {
                    dataGridViewHospitalizations.Rows[e.RowIndex].DefaultCellStyle.BackColor = Color.LightCyan;
                }
                else if (DateTime.Parse(dataGridViewHospitalizations.Rows[e.RowIndex].Cells[7].Value.ToString()) < DateTime.Now.Date)
                {
                    dataGridViewHospitalizations.Rows[e.RowIndex].DefaultCellStyle.BackColor = Color.LightGreen;
                }
                else if (DateTime.Parse(dataGridViewHospitalizations.Rows[e.RowIndex].Cells[7].Value.ToString()) == DateTime.Now.Date)
                {
                    if (isDischarge)
                    {
                        isDischarge = false;
                        dataGridViewHospitalizations.Rows[e.RowIndex].DefaultCellStyle.BackColor = Color.LightGreen;
                    }
                    else
                    {
                        dataGridViewHospitalizations.Rows[e.RowIndex].DefaultCellStyle.BackColor = Color.LightSalmon;
                    }
                }
            }
        }

        // Обработчик двойного клика по ячейке DataGridView
        private void dataGridViewHospitalizations_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                // Проверка, что пациент должен быть выписан сегодня
                if (dataGridViewHospitalizations.Rows[e.RowIndex].Cells[7].Value.ToString() == DateTime.Now.Date.ToString() &&
                    dataGridViewHospitalizations.Rows[e.RowIndex].DefaultCellStyle.BackColor == Color.LightSalmon)
                {
                    // Подтверждение выписки
                    if (DialogResult.Yes == MessageBox.Show("Выписать пациента?", "Подтверждение выписки пациента", MessageBoxButtons.YesNo, MessageBoxIcon.Information))
                    {
                        int bedID = Convert.ToInt32(dataGridViewHospitalizations.Rows[e.RowIndex].Cells[0].Value.ToString());

                        // Обновление статуса койки
                        MySqlConnection con = new MySqlConnection(GlobalValue.GetConnString());
                        con.Open();
                        MySqlCommand cmd = new MySqlCommand($"UPDATE Bed SET BedStatus = 'Свободна' WHERE BedID = @BedID", con);
                        cmd.Parameters.AddWithValue("@BedID", bedID);
                        cmd.ExecuteNonQuery();
                        con.Close();
                        isDischarge = true;
                        MessageBox.Show("Пациент выписан. Статус койко-места изменен.", "Успех", MessageBoxButtons.OK, MessageBoxIcon.Information);

                        LoadHospitalizationData(); // Обновление данных

                        // Сброс ограничений дат
                        this.dateTimePickerDischargeDate.MaxDate = DateTime.Now.Date.AddDays(182);
                        this.dateTimePickerAdmissionDate.MaxDate = DateTime.Now.Date;
                    }
                }
            }
        }
    }

    // Класс для хранения информации о койке
    public class BedIteme
    {
        public int BedIdd { get; set; }
        public string TypeOfBedd { get; set; }

        public BedIteme(int bedId, string typeOfBed)
        {
            BedIdd = bedId;
            TypeOfBedd = typeOfBed;
        }

        public override string ToString()
        {
            return $"{BedIdd} ({TypeOfBedd})";
        }
    }

    // Класс для хранения информации о пациенте
    public class PatientItem
    {
        public int PatientID { get; set; }
        public string PatientFIO { get; set; }

        public PatientItem(int pid, string pfio)
        {
            PatientID = pid;
            PatientFIO = pfio;
        }

        public override string ToString()
        {
            return $"{PatientFIO}";
        }
    }

    // Класс для хранения информации о враче
    public class DoctorItem
    {
        public int DoctorID { get; set; }
        public string DoctorFIO { get; set; }

        public DoctorItem(int did, string dfio)
        {
            DoctorID = did;
            DoctorFIO = dfio;
        }

        public override string ToString()
        {
            return $"{DoctorFIO}";
        }
    }
}