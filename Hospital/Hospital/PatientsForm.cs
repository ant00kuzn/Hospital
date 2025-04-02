// Импорт необходимых пространств имен
using MySql.Data.MySqlClient;
using System;
using System.Data;
using System.Drawing;
using System.Text.RegularExpressions;
using System.Windows.Forms;

namespace Hospital
{
    // Класс формы для работы с пациентами
    public partial class PatientsForm : Form
    {
        // Поля для хранения данных о пациентах
        private DataTable patientTable;
        private int selectedRowIndex = -1;
        private bool isAdding = false;

        // Конструктор формы
        public PatientsForm()
        {
            InitializeComponent();
            LoadPatientData(); // Загрузка данных о пациентах
            if (User.Role == 4)
            {
                ContextMenuSetup(); // Настройка контекстного меню
            }
        }

        // Настройка контекстного меню для DataGridView
        private void ContextMenuSetup()
        {
            ContextMenuStrip contextMenu = new ContextMenuStrip();
            ToolStripMenuItem addMenuItem = new ToolStripMenuItem("Добавить");
            ToolStripMenuItem editMenuItem = new ToolStripMenuItem("Редактировать");

            contextMenu.Items.Add(addMenuItem);
            contextMenu.Items.Add(editMenuItem);

            addMenuItem.Click += AddMenuItem_Click;
            editMenuItem.Click += EditMenuItem_Click;

            dataGridViewPatients.ContextMenuStrip = contextMenu;

        }

        // Обработчик клика по пункту "Добавить" в контекстном меню
        private void AddMenuItem_Click(object sender, EventArgs e)
        {
            isAdding = true;
            this.Size = new Size(1006, 787); // Изменение размера формы
            selectedRowIndex = -1;
            ClearInputFields(); // Очистка полей ввода
            EnableInputFields(true); // Активация полей ввода

            button2.Text = "Добавить";
        }

        // Обработчик клика по пункту "Редактировать" в контекстном меню
        private void EditMenuItem_Click(object sender, EventArgs e)
        {
            isAdding = false;
            if (dataGridViewPatients.SelectedRows.Count > 0)
            {
                selectedRowIndex = dataGridViewPatients.SelectedRows[0].Index;
                LoadSelectedRowData(); // Загрузка данных выбранной строки
                EnableInputFields(true); // Активация полей ввода

                button2.Text = "Редактировать";

                this.Size = new Size(1006, 787); // Изменение размера формы
            }
            else
            {
                MessageBox.Show("Выберите строку для редактирования.", "Предупреждение", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
        }

        // Загрузка данных о пациентах из БД
        private void LoadPatientData()
        {
            try
            {
                using (MySqlConnection connection = new MySqlConnection(GlobalValue.GetConnString()))
                {
                    connection.Open();

                    // SQL-запрос для получения данных о пациентах
                    using (MySqlCommand command = new MySqlCommand("SELECT PatientID, PatientSurname, PatientName, PatientPatronymic, Birthday, Address," +
                        "PhoneNumber, Insurance_Policy FROM patient order by PatientSurname ASC", connection))
                    {
                        MySqlDataAdapter adapter = new MySqlDataAdapter(command);
                        patientTable = new DataTable();
                        adapter.Fill(patientTable);

                        dataGridViewPatients.AutoGenerateColumns = false;
                        dataGridViewPatients.Columns.Clear();

                        // Добавление колонок в DataGridView
                        AddDataGridViewColumn("PatientID", "ID", "PatientID");
                        AddDataGridViewColumn("PatientSurname", "Фамилия", "PatientSurname");
                        AddDataGridViewColumn("PatientName", "Имя", "PatientName");
                        AddDataGridViewColumn("PatientPatronymic", "Отчество", "PatientPatronymic");
                        AddDataGridViewColumn("Birthday", "Дата рождения", "Birthday");
                        AddDataGridViewColumn("Address", "Адрес", "Address");
                        AddDataGridViewColumn("PhoneNumber", "Номер телефона", "PhoneNumber");
                        AddDataGridViewColumn("Insurance_Policy", "Страховой полис", "Insurance_Policy");
                        if (User.Role == 4)
                        {
                            AddDeleteButtonColumn(); // Добавляем колонку с кнопкой "Удалить"
                        }

                        dataGridViewPatients.DataSource = patientTable;

                        dataGridViewPatients.Columns[0].Visible = false; // Скрытие колонки с ID
                        dataGridViewPatients.Refresh();
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка при загрузке данных: {ex.Message}", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
        }

        // Добавление колонки с кнопкой "Удалить"
        private void AddDeleteButtonColumn()
        {
            DataGridViewButtonColumn deleteButtonColumn = new DataGridViewButtonColumn();
            deleteButtonColumn.Name = "DeleteColumn";
            deleteButtonColumn.Text = "Удалить";
            deleteButtonColumn.HeaderText = "Удаление";
            deleteButtonColumn.UseColumnTextForButtonValue = true;
            dataGridViewPatients.Columns.Add(deleteButtonColumn);
        }

        // Метод для добавления колонки в DataGridView
        private void AddDataGridViewColumn(string name, string headerText, string dataPropertyName)
        {
            DataGridViewTextBoxColumn column = new DataGridViewTextBoxColumn();
            column.Name = name;
            column.HeaderText = headerText;
            column.DataPropertyName = dataPropertyName;
            dataGridViewPatients.Columns.Add(column);
        }

        // Очистка полей ввода
        private void ClearInputFields()
        {
            textBoxSurName.Clear();
            textBoxName.Clear();
            textBoxPatro.Clear();
            dateTimePickerDateOfBirth.Value = DateTime.Now.Date;
            textBoxAddress.Clear();
            maskedTextBoxPhoneNumber.Clear();
            textBoxPolicy.Clear();
        }

        // Загрузка данных из выбранной строки в поля формы
        private void LoadSelectedRowData()
        {
            if (selectedRowIndex >= 0)
            {
                DataRow selectedRow = patientTable.Rows[selectedRowIndex];

                textBoxSurName.Text = selectedRow["PatientSurname"].ToString();
                textBoxName.Text = selectedRow["PatientName"].ToString();
                textBoxPatro.Text = selectedRow["PatientPatronymic"].ToString();
                if (selectedRow["Birthday"] != DBNull.Value)
                {
                    dateTimePickerDateOfBirth.Value = Convert.ToDateTime(selectedRow["Birthday"]);
                }
                textBoxAddress.Text = selectedRow["Address"].ToString();
                maskedTextBoxPhoneNumber.Text = selectedRow["PhoneNumber"].ToString();
                textBoxPolicy.Text = selectedRow["Insurance_Policy"].ToString(); // Заполняем поле для страхового полиса
            }
        }

        // Включение/выключение полей ввода
        private void EnableInputFields(bool enable)
        {
            textBoxSurName.Enabled = enable;
            textBoxName.Enabled = enable;
            textBoxPatro.Enabled = enable;
            dateTimePickerDateOfBirth.Enabled = enable;
            textBoxAddress.Enabled = enable;
            maskedTextBoxPhoneNumber.Enabled = enable;
            textBoxPolicy.Enabled = enable;
            button2.Enabled = enable;
            button1.Enabled = enable;
        }

        // Обработчик нажатия кнопки "Сохранить"
        private void button2_Click(object sender, EventArgs e)
        {
            if (isAdding)
            {
                AddPatient(); // Добавление нового пациента
            }
            else
            {
                UpdatePatient(); // Обновление данных пациента
            }
        }

        // Обработчик нажатия кнопки "Отмена"
        private void button1_Click(object sender, EventArgs e)
        {
            EnableInputFields(false); // Деактивация полей ввода
            ClearInputFields(); // Очистка полей ввода
        }

        // Добавление нового пациента
        private void AddPatient()
        {
            if (!ValidateInput()) return; // Проверка введенных данных

            using (MySqlConnection connection = new MySqlConnection(GlobalValue.GetConnString()))
            {
                connection.Open();
                string query = @"INSERT INTO Patient (PatientSurname, PatientName, PatientPatronymic, Birthday, Address, PhoneNumber, Insurance_Policy) 
                                VALUES (@PatientSurname, @PatientName, @PatientPatronymic, @Birthday, @Address, @PhoneNumber, @Insurance_Policy)";

                // Проверка на уникальность страхового полиса
                string checkQuery = "SELECT COUNT(*) FROM Patient WHERE Insurance_Policy = @Insurance_Policy";
                using (MySqlCommand checkCommand = new MySqlCommand(checkQuery, connection))
                {
                    checkCommand.Parameters.AddWithValue("@Insurance_Policy", textBoxPolicy.Text);
                    int count = Convert.ToInt32(checkCommand.ExecuteScalar());

                    if (count > 0)
                    {
                        MessageBox.Show("Пациент с таким страховым полисом уже существует.", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return; // Прерываем добавление, если пациент с таким полисом уже есть
                    }
                }

                using (MySqlCommand command = new MySqlCommand(query, connection))
                {
                    // Добавление параметров для SQL-запроса
                    command.Parameters.AddWithValue("@PatientSurname", textBoxSurName.Text);
                    command.Parameters.AddWithValue("@PatientName", textBoxName.Text);
                    command.Parameters.AddWithValue("@PatientPatronymic", textBoxPatro.Text);
                    command.Parameters.AddWithValue("@Birthday", dateTimePickerDateOfBirth.Value.ToString("yyyy-MM-dd"));
                    command.Parameters.AddWithValue("@Address", textBoxAddress.Text);
                    command.Parameters.AddWithValue("@PhoneNumber", maskedTextBoxPhoneNumber.Text);
                    command.Parameters.AddWithValue("@Insurance_Policy", textBoxPolicy.Text); // Добавляем параметр для страхового полиса

                    try
                    {
                        command.ExecuteNonQuery();
                        MessageBox.Show("Пациент успешно добавлен.", "Успех", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        LoadPatientData(); // Обновление данных
                        ClearInputFields();
                        EnableInputFields(false);
                        this.Size = new Size(1006, 520); // Возвращение формы к исходному размеру

                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show($"Ошибка при добавлении пациента: {ex.Message}", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
        }

        // Обновление данных о пациенте
        private void UpdatePatient()
        {
            if (!ValidateInput()) return; // Проверка введенных данных

            using (MySqlConnection connection = new MySqlConnection(GlobalValue.GetConnString()))
            {
                connection.Open();
                // SQL-запрос для обновления данных
                string query = @"UPDATE Patient SET PatientSurname = @PatientSurname, PatientName = @PatientName, PatientPatronymic = @PatientPatronymic,
                                 Birthday = @Birthday, Address = @Address, PhoneNumber = @PhoneNumber, Insurance_Policy = @Insurance_Policy
                                 WHERE PatientID = @PatientID";
                using (MySqlCommand command = new MySqlCommand(query, connection))
                {
                    // Добавление параметров для SQL-запроса
                    command.Parameters.AddWithValue("@PatientSurname", textBoxSurName.Text);
                    command.Parameters.AddWithValue("@PatientName", textBoxName.Text);
                    command.Parameters.AddWithValue("@PatientPatronymic", textBoxPatro.Text);
                    command.Parameters.AddWithValue("@Birthday", dateTimePickerDateOfBirth.Value.ToString("yyyy-MM-dd"));
                    command.Parameters.AddWithValue("@Address", textBoxAddress.Text);
                    command.Parameters.AddWithValue("@PhoneNumber", maskedTextBoxPhoneNumber.Text);
                    command.Parameters.AddWithValue("@Insurance_Policy", textBoxPolicy.Text);
                    command.Parameters.AddWithValue("@PatientID", patientTable.Rows[selectedRowIndex]["PatientID"]);

                    try
                    {
                        command.ExecuteNonQuery();
                        MessageBox.Show("Данные пациента успешно обновлены.", "Успех", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        LoadPatientData(); // Обновление данных
                        EnableInputFields(false);
                        this.Size = new Size(1006, 520); // Возвращение формы к исходному размеру
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show($"Ошибка при обновлении данных пациента: {ex.Message}", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
        }

        // Удаление пациента
        private void DeletePatient()
        {
            if (DialogResult.No == MessageBox.Show("Вы действительно хотите удалить выбранную запись?", "Подтверждение", MessageBoxButtons.YesNo, MessageBoxIcon.Information))
            {
                return;
            }
            int patientId = Convert.ToInt32(dataGridViewPatients.Rows[selectedRowIndex].Cells["PatientID"].Value);
            using (MySqlConnection connection = new MySqlConnection(GlobalValue.GetConnString()))
            {
                connection.Open();
                string query = "DELETE FROM Patient WHERE PatientID = @PatientID";
                using (MySqlCommand command = new MySqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@PatientID", patientId);
                    try
                    {
                        command.ExecuteNonQuery();
                        MessageBox.Show("Пациент успешно удален.", "Успех", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        LoadPatientData(); // Обновление данных
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show($"Ошибка при удалении пациента: {ex.Message}", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
        }

        // Валидация введенных данных
        private bool ValidateInput()
        {
            if (!DateIsValid(dateTimePickerDateOfBirth))
            {
                return false;
            }
            if (string.IsNullOrWhiteSpace(textBoxSurName.Text))
            {
                MessageBox.Show("Пожалуйста, введите фамилию.", "Ошибка ввода", MessageBoxButtons.OK, MessageBoxIcon.Error);
                textBoxSurName.Focus();
                return false;
            }
            if (string.IsNullOrWhiteSpace(textBoxName.Text))
            {
                MessageBox.Show("Пожалуйста, введите имя.", "Ошибка ввода", MessageBoxButtons.OK, MessageBoxIcon.Error);
                textBoxName.Focus();
                return false;
            }
            if (string.IsNullOrWhiteSpace(textBoxPatro.Text))
            {
                MessageBox.Show("Пожалуйста, введите отчество.", "Ошибка ввода", MessageBoxButtons.OK, MessageBoxIcon.Error);
                textBoxPatro.Focus();
                return false;
            }
            if (string.IsNullOrWhiteSpace(textBoxAddress.Text))
            {
                MessageBox.Show("Пожалуйста, введите адрес.", "Ошибка ввода", MessageBoxButtons.OK, MessageBoxIcon.Error);
                textBoxAddress.Focus();
                return false;
            }
            if (!maskedTextBoxPhoneNumber.MaskCompleted)
            {
                MessageBox.Show("Пожалуйста, введите корректный номер телефона.", "Ошибка ввода", MessageBoxButtons.OK, MessageBoxIcon.Error);
                maskedTextBoxPhoneNumber.Focus();
                return false;
            }

            return true;
        }

        // Валидация даты рождения
        private bool DateIsValid(DateTimePicker dt)
        {
            if (dt.Value > DateTime.Now.Date)
            {
                MessageBox.Show("Дата рождения не может быть в будущем.", "Ошибка ввода", MessageBoxButtons.OK, MessageBoxIcon.Error);
                dt.Focus();
                return false;
            }

            DateTime minBirthdate = DateTime.Now.AddYears(-120);
            if (dt.Value < minBirthdate)
            {
                MessageBox.Show("Возраст должен быть до 120 лет.", "Ошибка ввода", MessageBoxButtons.OK, MessageBoxIcon.Error);
                dt.Focus();
                return false;
            }

            return true;
        }

        // Обработчик изменения текста в поле поиска
        private void richTextBox1_TextChanged(object sender, EventArgs e)
        {
            string filterText = richTextBox1.Text.Trim();

            if (string.IsNullOrEmpty(filterText))
            {
                LoadPatientData(); // Перезагрузка данных при очистке поля
            }
            else
            {
                // Фильтрация данных по фамилии
                DataView dv = patientTable.DefaultView;
                dv.RowFilter = $"PatientSurname LIKE '%{filterText}%'";
                dataGridViewPatients.DataSource = dv;
            }
        }

        // Обработчик закрытия формы
        private void PatientsForm_FormClosed(object sender, FormClosedEventArgs e)
        {
            // Показываем главную форму при закрытии
            if (Application.OpenForms["MainForm"] is MainForm mn)
                mn.Show();
            else
                new MainForm().Show();
        }

        // Обработчики KeyPress для ограничения ввода

        // Ограничение ввода в поле поиска (только русские буквы, пробелы и дефисы)
        private void richTextBox1_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)Keys.Back)
            {
                e.Handled = false;
                return;
            }
            if (!Regex.IsMatch(e.KeyChar.ToString(), @"[А-Яа-яЁё\s-]+$"))
            {
                e.Handled = true;
            }
        }

        // Ограничение ввода в поле имени (только русские буквы и пробелы)
        private void textBoxName_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)Keys.Back)
            {
                e.Handled = false;
                return;
            }
            if (!Regex.IsMatch(e.KeyChar.ToString(), @"^[А-Яа-яЁё\s]+$"))
            {
                e.Handled = true;
            }
        }

        // Ограничение ввода в поле фамилии (только русские буквы, пробелы и дефисы)
        private void textBoxSurName_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)Keys.Back)
            {
                e.Handled = false;
                return;
            }
            if (!Regex.IsMatch(e.KeyChar.ToString(), @"^[А-Яа-яЁё\s-]+$"))
            {
                e.Handled = true;
            }
        }

        // Ограничение ввода в поле отчества (только русские буквы, пробелы и дефисы)
        private void textBoxPatro_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)Keys.Back)
            {
                e.Handled = false;
                return;
            }
            if (!Regex.IsMatch(e.KeyChar.ToString(), @"^[А-Яа-яЁё\s-]+$"))
            {
                e.Handled = true;
            }
        }

        // Ограничение ввода в поле адреса (только русские буквы, цифры, пробелы и дефисы)
        private void textBoxAddress_KeyPress(object sender, KeyPressEventArgs e)
        {
            //kladr
        }

        // Обработчик клика по ячейке DataGridView
        private void dataGridViewPatients_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                dataGridViewPatients.Rows[e.RowIndex].Selected = true;
                selectedRowIndex = e.RowIndex;

                // Если нажата кнопка "Удалить"
                if (e.ColumnIndex == 8)
                {
                    DeletePatient(); // Удаление пациента
                }
            }
        }

        // Проверка формата номера телефона при потере фокуса
        private void maskedTextBoxPhoneNumber_Leave(object sender, EventArgs e)
        {
            string phoneNumberText = maskedTextBoxPhoneNumber.Text.Replace("(", "").Replace(")", "").Replace("-", "").Replace(" ", "");
            if (!Regex.IsMatch(phoneNumberText, @"^\+7\d{10}$"))
            {
                MessageBox.Show("Неверный формат номера телефона. Пример: +7 (9XX) XXX-XX-XX", "Ошибка ввода", MessageBoxButtons.OK, MessageBoxIcon.Error);
                maskedTextBoxPhoneNumber.Focus();
            }
        }

        // Проверка формата страхового полиса при потере фокуса
        private void textBoxPolicy_Leave(object sender, EventArgs e)
        {
            if (!Regex.IsMatch(textBoxPolicy.Text, "^\\d{11}$"))
            {
                MessageBox.Show("Неверный формат полиса. Пример: 12345678912", "Ошибка ввода", MessageBoxButtons.OK, MessageBoxIcon.Error);
                textBoxPolicy.Focus();
            }
        }

        private void button1_Click_1(object sender, EventArgs e)
        {
            LoadPatientData(); // Обновление данных
            EnableInputFields(false);
            this.Size = new Size(1006, 520); // Возвращение формы к исходному размеру
        }
    }
}