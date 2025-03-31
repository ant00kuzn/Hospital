using MySql.Data.MySqlClient;
using System;
using System.Data;
using System.Drawing;
using System.Runtime.Remoting.Lifetime;
using System.Text.RegularExpressions;
using System.Windows.Forms;

namespace Hospital
{
    public partial class GuidesForm : Form
    {
        private DataTable dataTable; // Таблица для хранения данных
        private MySqlDataAdapter dataAdapter; // Адаптер для работы с БД
        private string currentTable; // Текущая выбранная таблица
        private bool isAdding = false; // Флаг режима добавления
        private int selectedRowIndex = -1; // Индекс выбранной строки

        public GuidesForm()
        {
            InitializeComponent(); // Инициализация компонентов
            ToggleTable("Role"); // Загрузка таблицы "Role" по умолчанию
            ContextMenuSetup(); // Настройка контекстного меню
            LoadDataForComboBoxes(); // Загрузка данных для ComboBox
            Size defaultSize = new Size(633, 570); // Размер формы по умолчанию
            radioButtonRole.Checked = true; // Выбор радиокнопки "Role"
            radioButtonDepartment.Checked = false;
            radioButtonWard.Checked = false;
        }

        private void LoadDataForComboBoxes()
        {
            LoadWardDataForComboBox(); // Загрузка данных о палатах
        }

        private void LoadWardDataForComboBox()
        {
            MySqlConnection con = new MySqlConnection(GlobalValue.GetConnString());
            try
            {
                con.Open(); // Открытие соединения
                MySqlCommand cmd = new MySqlCommand("SELECT WardID, TypeOfWard FROM ward", con);
                MySqlDataReader rd = cmd.ExecuteReader();
                comboBox1.Items.Clear(); // Очистка ComboBox
                while (rd.Read())
                {
                    // Добавление элементов в ComboBox
                    comboBox1.Items.Add(new WardItem(rd.GetInt32(0), rd.GetString(1)).ToString());
                }
                comboBox1.DisplayMember = "WardID"; // Настройка отображаемого поля
                comboBox1.ValueMember = "WardID"; // Настройка поля значения
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка при загрузке данных Ward: {ex.Message}", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                con.Close(); // Закрытие соединения
            }
        }

        private void LoadTypeOfWardForComboBox()
        {
            comboBox2.Items.Clear(); // Очистка ComboBox
            comboBox2.Items.Add("Общая"); // Добавление типов палат
            comboBox2.Items.Add("ВИП");
            comboBox2.Items.Add("Специализированная");
        }

        private void LoadBedDataForComboBox()
        {
            comboBox2.Items.Clear(); // Очистка ComboBox
            comboBox2.Items.Add("Свободна"); // Добавление статусов коек
            comboBox2.Items.Add("Занята");
            comboBox2.Items.Add("Санитарная обработка");
        }

        private void ContextMenuSetup()
        {
            ContextMenuStrip contextMenu = new ContextMenuStrip(); // Создание контекстного меню
            ToolStripMenuItem addMenuItem = new ToolStripMenuItem("Добавить"); // Пункт "Добавить"
            ToolStripMenuItem editMenuItem = new ToolStripMenuItem("Редактировать"); // Пункт "Редактировать"
            contextMenu.Items.Add(addMenuItem);
            contextMenu.Items.Add(editMenuItem);

            addMenuItem.Click += AddMenuItem_Click; // Обработчик добавления
            editMenuItem.Click += EditMenuItem_Click; // Обработчик редактирования

            dataGridView.ContextMenuStrip = contextMenu; // Привязка меню к DataGridView
        }

        private void AddMenuItem_Click(object sender, EventArgs e)
        {
            isAdding = true; // Установка флага добавления
            selectedRowIndex = -1; // Сброс выбранной строки
            ClearInputFields(); // Очистка полей ввода
            EnableInputFields(true); // Активация полей ввода
            this.Size = new Size(1073, 570); // Изменение размера формы
        }

        private void EditMenuItem_Click(object sender, EventArgs e)
        {
            isAdding = false; // Сброс флага добавления
            if (dataGridView.SelectedRows.Count > 0) // Проверка выбранной строки
            {
                selectedRowIndex = dataGridView.SelectedRows[0].Index; // Сохранение индекса
                LoadSelectedRowData(); // Загрузка данных выбранной строки
                EnableInputFields(true); // Активация полей ввода
                this.Size = new Size(1073, 570); // Изменение размера формы
            }
            else
            {
                MessageBox.Show("Выберите строку для редактирования.", "Предупреждение", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void EnableInputFields(bool enable)
        {
            textBoxName.Enabled = enable; // Активация/деактивация поля имени
            buttonSave.Enabled = enable; // Активация кнопки сохранения
            buttonCancel.Enabled = enable; // Активация кнопки отмены

            // Настройка видимости ComboBox в зависимости от текущей таблицы
            comboBox1.Visible = (currentTable == "Ward" || currentTable == "Bed") && enable;
            labelBedID.Visible = comboBox1.Visible;

            comboBox2.Visible = (currentTable == "Department" || currentTable == "Bed" || currentTable == "Ward") && enable;
            labelDepartment.Visible = comboBox2.Visible;

            if (currentTable == "Department")
            {
                LoadEmployeeDataForComboBox(); // Загружаем данные сотрудников
            }

            // Настройка текста меток в зависимости от текущей таблицы
            labelBedID.Text = currentTable == "Bed" ? "ID палаты:" : "Отделение:";
            labelDepartment.Text = currentTable == "Department" ? "Руководитель:" : (currentTable == "Bed" ? "Статус койки:" : "Тип палаты:");

            if (currentTable == "Bed")
            {
                LoadWardDataForComboBox(); // Загрузка данных о палатах
                LoadBedDataForComboBox(); // Загрузка статусов коек
            }
            else if (currentTable == "Ward")
            {
                LoadDepartmentDataForComboBox(); // Загрузка данных об отделениях
                LoadTypeOfWardForComboBox(); // Загрузка типов палат
            }

            this.Size = new Size(633, 570); // Возврат к размеру по умолчанию
        }

        private void ClearInputFields()
        {
            textBoxName.Clear(); // Очистка поля имени
            comboBox1.SelectedIndex = -1; // Сброс выбора в ComboBox
            comboBox2.SelectedIndex = -1;
        }

        private void LoadSelectedRowData()
        {
            if (selectedRowIndex >= 0) // Проверка валидности индекса
            {
                // Определение имени столбца в зависимости от текущей таблицы
                string nameColumn = (currentTable == "Role") ? "RoleName" : (currentTable == "Department" ? "DepartmentName" : (currentTable == "Ward" ? "TypeOfWard" : "BedStatus"));
                if (dataGridView.Columns.Contains(nameColumn)) // Проверка наличия столбца
                {
                    textBoxName.Text = dataGridView.Rows[selectedRowIndex].Cells[nameColumn].Value?.ToString() ?? ""; // Заполнение поля имени
                }
                else
                {
                    MessageBox.Show($"Столбец '{nameColumn}' не найден в DataGridView.", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }

                // Заполнение ComboBox в зависимости от текущей таблицы
                if (currentTable == "Department")
                {
                    // Загружаем данные сотрудников перед установкой значения
                    LoadEmployeeDataForComboBox();

                    if (dataGridView.Rows[selectedRowIndex].Cells["SupervisiorID"].Value != null)
                    {
                        int supervisiorId = Convert.ToInt32(dataGridView.Rows[selectedRowIndex].Cells["SupervisiorID"].Value);

                        // Устанавливаем выбранное значение
                        for (int i = 0; i < comboBox2.Items.Count; i++)
                        {
                            if (((EmployeeItem)comboBox2.Items[i]).EmployeeID == supervisiorId)
                            {
                                comboBox2.SelectedIndex = i;
                                break;
                            }
                        }
                    }
                }
                else if (currentTable == "Ward")
                {
                    if (dataGridView.Rows[selectedRowIndex].Cells["DepartmentID"].Value != null)
                    {
                        if (comboBox1.Items.Count > 0)
                        {
                            comboBox1.SelectedValue = ((DepartmentItem)comboBox1.SelectedItem).DepartmentId;
                        }
                    }
                    if (dataGridView.Rows[selectedRowIndex].Cells["TypeOfWard"].Value != null)
                    {
                        comboBox2.SelectedItem = dataGridView.Rows[selectedRowIndex].Cells["TypeOfWard"].Value.ToString();
                    }
                }
            }
        }

        private void SaveChanges()
        {
            // Проверка заполненности полей
            if (string.IsNullOrEmpty(textBoxName.Text) || (comboBox1.Visible && comboBox1.SelectedIndex == -1) || (comboBox2.Visible && comboBox2.SelectedIndex == -1))
            {
                MessageBox.Show("Пожалуйста, заполните все поля.", "Предупреждение", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            try
            {
                using (MySqlConnection connection = new MySqlConnection(GlobalValue.GetConnString()))
                {
                    connection.Open(); // Открытие соединения
                    MySqlCommand command;
                    string query;

                    if (isAdding) // Режим добавления
                    {
                        string duplicateCheckQuery = "";
                        // Проверка на дублирование в зависимости от таблицы
                        switch (currentTable)
                        {
                            case "Role":
                                duplicateCheckQuery = "SELECT COUNT(*) FROM Role WHERE RoleName = @RoleName";
                                break;
                            case "Department":
                                duplicateCheckQuery = "SELECT COUNT(*) FROM Department WHERE DepartmentName = @DepartmentName";
                                break;
                            case "Ward":
                                duplicateCheckQuery = "SELECT COUNT(*) FROM Ward WHERE TypeOfWard = @TypeOfWard";
                                break;
                        }

                        if (!string.IsNullOrEmpty(duplicateCheckQuery))
                        {
                            using (MySqlCommand checkCmd = new MySqlCommand(duplicateCheckQuery, connection))
                            {
                                // Добавление параметров в зависимости от таблицы
                                switch (currentTable)
                                {
                                    case "Role":
                                        checkCmd.Parameters.AddWithValue("@RoleName", textBoxName.Text);
                                        break;
                                    case "Department":
                                        checkCmd.Parameters.AddWithValue("@DepartmentName", textBoxName.Text);
                                        break;
                                    case "Ward":
                                        checkCmd.Parameters.AddWithValue("@TypeOfWard", textBoxName.Text);
                                        break;
                                }

                                int duplicateCount = Convert.ToInt32(checkCmd.ExecuteScalar());
                                if (duplicateCount > 0)
                                {
                                    MessageBox.Show("Запись с такими данными уже существует.", "Предупреждение", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                                    return;
                                }
                            }
                        }

                        // Формирование запроса INSERT в зависимости от таблицы
                        if (currentTable == "Role")
                        {
                            query = "INSERT INTO Role (RoleName) VALUES (@RoleName)";
                            command = new MySqlCommand(query, connection);
                            command.Parameters.AddWithValue("@RoleName", textBoxName.Text);
                        }
                        else if (currentTable == "Department")
                        {
                            query = "INSERT INTO Department VALUES (null, @DepartmentName, @SupervisiorID)";
                            command = new MySqlCommand(query, connection);
                            command.Parameters.AddWithValue("@DepartmentName", textBoxName.Text);
                            if (comboBox2.SelectedItem is EmployeeItem selectedEmployee)
                            {
                                command.Parameters.AddWithValue("@SupervisiorID", selectedEmployee.EmployeeID);
                            }
                            else
                            {
                                MessageBox.Show("Не выбран Заведущий отделением.", "Предупреждение", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                                return;
                            }
                        }
                        else if (currentTable == "Ward")
                        {
                            query = "INSERT INTO Ward (DepartmentID, TypeOfWard) VALUES (@DepartmentID, @TypeOfWard)";
                            command = new MySqlCommand(query, connection);
                            if (comboBox1.SelectedItem is DepartmentItem selectedDepartment)
                            {
                                command.Parameters.AddWithValue("@DepartmentID", selectedDepartment.DepartmentId);
                            }
                            else
                            {
                                MessageBox.Show("Не выбрано отделение.", "Предупреждение", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                                return;
                            }
                            if (comboBox2.SelectedItem != null)
                            {
                                command.Parameters.AddWithValue("@TypeOfWard", comboBox2.SelectedItem.ToString());
                            }
                            else
                            {
                                MessageBox.Show("Не выбран Type Of Ward", "Предупреждение", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                                return;
                            }
                        }
                        else
                        {
                            MessageBox.Show("Неизвестная таблица для добавления.", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            return;
                        }
                    }
                    else // Режим редактирования
                    {
                        // Формирование запроса UPDATE в зависимости от таблицы
                        if (currentTable == "Role")
                        {
                            query = "UPDATE Role SET RoleName = @RoleName WHERE RoleID = @RoleID";
                            command = new MySqlCommand(query, connection);
                            command.Parameters.AddWithValue("@RoleName", textBoxName.Text);
                            command.Parameters.AddWithValue("@RoleID", dataGridView.Rows[selectedRowIndex].Cells["RoleID"].Value);
                        }
                        else if (currentTable == "Department")
                        {
                            query = "UPDATE Department SET DepartmentName = @DepartmentName, SupervisiorID = @SupervisiorID WHERE DepartmentID = @DepartmentID";
                            command = new MySqlCommand(query, connection);
                            command.Parameters.AddWithValue("@DepartmentName", textBoxName.Text);
                            if (comboBox2.SelectedItem is EmployeeItem selectedEmployee)
                            {
                                command.Parameters.AddWithValue("@SupervisiorID", selectedEmployee.EmployeeID);
                            }
                            else
                            {
                                MessageBox.Show("Не выбран Заведущий отделением.", "Предупреждение", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                                return;
                            }
                            command.Parameters.AddWithValue("@DepartmentID", dataGridView.Rows[selectedRowIndex].Cells["DepartmentID"].Value);
                        }
                        else if (currentTable == "Ward")
                        {
                            query = "UPDATE Ward SET DepartmentID = @DepartmentID, TypeOfWard = @TypeOfWard WHERE WardID = @WardID";
                            command = new MySqlCommand(query, connection);
                            if (comboBox1.SelectedItem is DepartmentItem selectedDepartment)
                            {
                                command.Parameters.AddWithValue("@DepartmentID", selectedDepartment.DepartmentId);
                            }
                            else
                            {
                                MessageBox.Show("Не выбрано отделение.", "Предупреждение", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                                return;
                            }
                            if (comboBox2.SelectedItem != null)
                            {
                                command.Parameters.AddWithValue("@TypeOfWard", comboBox2.SelectedItem.ToString());
                            }
                            else
                            {
                                MessageBox.Show("Не выбран Type Of Ward", "Предупреждение", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                                return;
                            }
                            command.Parameters.AddWithValue("@WardID", dataGridView.Rows[selectedRowIndex].Cells["WardID"].Value);
                        }
                        else
                        {
                            MessageBox.Show("Неизвестная таблица для обновления.", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            return;
                        }
                    }
                    int rowsAffected = command.ExecuteNonQuery(); // Выполнение запроса
                    if (rowsAffected > 0)
                    {
                        MessageBox.Show("Изменения сохранены.", "Успешно", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        ToggleTable(currentTable); // Обновление таблицы
                        ClearInputFields(); // Очистка полей
                        EnableInputFields(false); // Деактивация полей
                        this.Size = new Size(633, 570); // Возврат к размеру по умолчанию
                    }
                    else
                    {
                        MessageBox.Show("Не удалось сохранить изменения.", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка при сохранении данных: {ex.Message}", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            finally
            {
                isAdding = false; // Сброс флага добавления
                selectedRowIndex = -1; // Сброс выбранной строки
            }
        }

        private void buttonSave_Click(object sender, EventArgs e)
        {
            SaveChanges(); // Сохранение изменений
        }

        private void buttonCancel_Click(object sender, EventArgs e)
        {
            this.Size = new Size(633, 570); // Возврат к размеру по умолчанию
            ClearInputFields(); // Очистка полей
            EnableInputFields(false); // Деактивация полей
        }

        private void dataGridView_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.ColumnIndex == dataGridView.Columns["DeleteColumn"].Index && e.RowIndex >= 0)
            {
                DeleteButton_Click(sender, e); // Обработчик удаления
            }
        }

        private void DeleteButton_Click(object sender, DataGridViewCellEventArgs e)
        {
            if (e.ColumnIndex == dataGridView.Columns["DeleteColumn"].Index && e.RowIndex >= 0)
            {
                string tableName = "";
                string foreignKeyColumn = "";
                string primaryKeyColumn = "";
                string primaryKeyValue = "";
                string deleteQuery = "";

                // Определение параметров удаления в зависимости от таблицы
                switch (currentTable)
                {
                    case "Role":
                        tableName = "Employee";
                        foreignKeyColumn = "Role";
                        primaryKeyColumn = "RoleID";
                        primaryKeyValue = dataGridView.Rows[e.RowIndex].Cells[primaryKeyColumn].Value.ToString();
                        deleteQuery = $"DELETE FROM role WHERE RoleID = @{primaryKeyColumn}";
                        break;
                    case "Department":
                        tableName = "Hospitalization";
                        foreignKeyColumn = "DepartmentID";
                        primaryKeyColumn = "DepartmentID";
                        primaryKeyValue = dataGridView.Rows[e.RowIndex].Cells[primaryKeyColumn].Value.ToString();
                        deleteQuery = $"DELETE FROM department WHERE DepartmentID = @{primaryKeyColumn}";
                        break;
                    case "Ward":
                        tableName = "Bed";
                        foreignKeyColumn = "WardID";
                        primaryKeyColumn = "WardID";
                        primaryKeyValue = dataGridView.Rows[e.RowIndex].Cells[primaryKeyColumn].Value.ToString();
                        deleteQuery = $"DELETE FROM ward WHERE WardID = @{primaryKeyColumn}";
                        break;
                    default:
                        MessageBox.Show("Удаление для данной таблицы не реализовано.", "Предупреждение", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return;
                }

                string relatedCountQuery = $"SELECT COUNT(*) FROM {tableName} WHERE {foreignKeyColumn} = @{primaryKeyColumn}";

                int relatedCount = 0;
                try
                {
                    using (MySqlConnection connection = new MySqlConnection(GlobalValue.GetConnString()))
                    {
                        connection.Open();
                        using (MySqlCommand command = new MySqlCommand(relatedCountQuery, connection))
                        {
                            command.Parameters.AddWithValue($"@{primaryKeyColumn}", primaryKeyValue);
                            relatedCount = Convert.ToInt32(command.ExecuteScalar()); // Проверка связанных записей
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Ошибка при проверке связанных записей: {ex.Message}", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                if (relatedCount > 0) // Если есть связанные записи
                {
                    MessageBox.Show($"Внимание! Существуют связанные записи в таблице {tableName}. Удаление записи в справочнике может нарушить целостность данных.", "Предупреждение", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
                else
                {
                    if (DialogResult.Yes == MessageBox.Show("Подвтердить удаление?", "Уточнение", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Information))
                    {
                        try
                        {
                            using (MySqlConnection connection = new MySqlConnection(GlobalValue.GetConnString()))
                            {
                                connection.Open();
                                using (MySqlCommand command = new MySqlCommand(deleteQuery, connection))
                                {
                                    command.Parameters.AddWithValue($"@{primaryKeyColumn}", primaryKeyValue);
                                    relatedCount = command.ExecuteNonQuery(); // Удаление записи
                                }
                            }

                            ToggleTable(currentTable); // Обновление таблицы
                            MessageBox.Show("Удалено " + relatedCount + " записей.", "Успешно.", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            this.Size = new Size(633, 570); // Возврат к размеру по умолчанию
                        }
                        catch (Exception ex)
                        {
                            MessageBox.Show($"Ошибка при удалении записи: {ex.Message}", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                    }
                    else
                    {
                        ToggleTable(currentTable); // Обновление таблицы
                        this.Size = new Size(633, 570); // Возврат к размеру по умолчанию
                    }
                }
            }
        }

        private void textBoxName_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)Keys.Back) // Разрешение Backspace
            {
                e.Handled = false;
                return;
            }

            // Ограничение ввода в зависимости от таблицы
            if ((currentTable == "Ward") ? !Regex.IsMatch(e.KeyChar.ToString(), @"[А-Яа-я]+$") : !Regex.IsMatch(e.KeyChar.ToString(), @"^[А-Яа-яЁё]+$"))
            {
                if (currentTable == "Ward")
                    textBoxName.MaxLength = 20;
                else
                    textBoxName.MaxLength = 10;

                e.Handled = true; // Блокировка недопустимых символов
            }
        }

        private void dataGridView_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                dataGridView.Rows[e.RowIndex].Selected = true; // Выбор строки
            }
        }

        private void LoadData(string table)
        {
            currentTable = table; // Сохранение текущей таблицы
            try
            {
                using (MySqlConnection connection = new MySqlConnection(GlobalValue.GetConnString()))
                {
                    connection.Open(); // Открытие соединения

                    string query = "";
                    // Формирование запроса в зависимости от таблицы
                    if (table == "Ward")
                    {
                        query = @"SELECT w.WardID, d.DepartmentName, w.TypeOfWard
                                FROM Ward w
                                INNER JOIN Department d ON w.DepartmentID = d.DepartmentID";
                    }
                    else if (table == "Department")
                    {
                        query = @"SELECT d.DepartmentID, d.DepartmentName, 
                                    CONCAT(e.EmployeeSurname, ' ', e.EmployeeName, ' ', e.EmployeePatronymic) AS SupervisorName,
                                    SupervisiorID
                                FROM Department d
                                INNER JOIN Employee e ON d.SupervisiorID = e.EmployeeID";
                    }
                    else
                    {
                        query = $"SELECT * FROM `{table}`";
                    }

                    dataTable = new DataTable(); // Создание таблицы
                    using (MySqlCommand command = new MySqlCommand(query, connection))
                    {
                        dataAdapter = new MySqlDataAdapter(command);
                        dataAdapter.Fill(dataTable); // Заполнение данными
                        dataGridView.DataSource = dataTable; // Привязка к DataGridView
                    }

                    if (dataGridView.Columns.Contains("DeleteColumn"))
                    {
                        dataGridView.Columns.Remove("DeleteColumn"); // Удаление колонки удаления, если есть
                    }

                    DataGridViewButtonColumn deleteButtonColumn = new DataGridViewButtonColumn(); // Добавление колонки удаления
                    deleteButtonColumn.Name = "DeleteColumn";
                    deleteButtonColumn.Text = "Удалить";
                    deleteButtonColumn.HeaderText = "Удаление";
                    deleteButtonColumn.UseColumnTextForButtonValue = true;
                    dataGridView.Columns.Add(deleteButtonColumn);

                    // Настройка заголовков колонок в зависимости от таблицы
                    switch (currentTable)
                    {
                        case "Role":
                            dataGridView.Columns["RoleID"].HeaderText = "ID Роли";
                            dataGridView.Columns["RoleID"].Visible = false;
                            dataGridView.Columns["RoleName"].HeaderText = "Название Роли";
                            break;
                        case "Bed":
                            dataGridView.Columns["BedID"].HeaderText = "ID койки";
                            dataGridView.Columns["BedStatus"].HeaderText = "Статус койки";
                            dataGridView.Columns["TypeOfWard"].HeaderText = "Палата";
                            break;
                        case "Department":
                            dataGridView.Columns["DepartmentID"].HeaderText = "ID Отделения";
                            dataGridView.Columns["DepartmentName"].HeaderText = "Название Отделения";
                            dataGridView.Columns["SupervisorName"].HeaderText = "Заведующий отделением";
                            dataGridView.Columns["SupervisiorID"].Visible = false;
                            dataGridView.Columns["DepartmentID"].Visible = false;
                            break;
                        case "Ward":
                            dataGridView.Columns["WardID"].HeaderText = "ID Палаты";
                            dataGridView.Columns["DepartmentName"].HeaderText = "Название Отделения";
                            dataGridView.Columns["TypeOfWard"].HeaderText = "Тип палаты";
                            break;
                        default:
                            break;
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка при загрузке данных: {ex.Message}", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
        }

        private void ToggleTable(string table)
        {
            comboBox1.Items.Clear(); // Очистка ComboBox
            comboBox2.Items.Clear();
            if (table == "Role")
            {
                LoadData("Role"); // Загрузка данных ролей
                labelName.Text = "Название\nроли";
                labelName.Visible = true;
                textBoxName.Visible = true;
            }
            else if (table == "Department")
            {
                LoadData("Department"); // Загрузка данных отделений
                labelName.Text = "Название\nотделения";
                labelName.Visible = true;
                textBoxName.Visible = true;
                LoadEmployeeDataForComboBox(); // Загрузка данных сотрудников
                labelDepartment.Text = "Руководитель:";
            }
            else if (table == "Ward")
            {
                LoadData("Ward"); // Загрузка данных палат
                labelName.Visible = false;
                textBoxName.Visible = false;
                LoadDepartmentDataForComboBox(); // Загрузка данных отделений
                LoadTypeOfWardForComboBox(); // Загрузка типов палат
            }

            currentTable = table; // Сохранение текущей таблицы
            this.Size = new Size(633, 570); // Возврат к размеру по умолчанию
            ClearInputFields(); // Очистка полей
            EnableInputFields(false); // Деактивация полей
        }

        private void radioButtonRole_CheckedChanged(object sender, EventArgs e)
        {
            if (radioButtonRole.Checked)
            {
                ToggleTable("Role"); // Переключение на таблицу ролей
            }
        }

        private void radioButtonDepartment_CheckedChanged(object sender, EventArgs e)
        {
            if (radioButtonDepartment.Checked)
            {
                ToggleTable("Department"); // Переключение на таблицу отделений
            }
        }

        private void radioButtonWard_CheckedChanged(object sender, EventArgs e)
        {
            if (radioButtonWard.Checked)
            {
                ToggleTable("Ward"); // Переключение на таблицу палат
            }
        }

        private void LoadEmployeeDataForComboBox()
        {
            MySqlConnection con = new MySqlConnection(GlobalValue.GetConnString());
            try
            {
                con.Open();
                string query = @"SELECT EmployeeID, 
                               CONCAT(EmployeeSurname, ' ', EmployeeName, ' ', EmployeePatronymic) AS EmployeeInfo 
                        FROM employee
                        ORDER BY EmployeeSurname";
                MySqlCommand cmd = new MySqlCommand(query, con);

                // Сохраняем текущее выбранное значение
                EmployeeItem currentSelection = comboBox2.SelectedItem as EmployeeItem;
                int? currentId = currentSelection?.EmployeeID;

                comboBox2.Items.Clear();

                MySqlDataReader rd = cmd.ExecuteReader();
                while (rd.Read())
                {
                    var item = new EmployeeItem(rd.GetInt32(0), rd.GetString(1));
                    comboBox2.Items.Add(item);

                    // Восстанавливаем выбранное значение, если оно было
                    if (currentId.HasValue && item.EmployeeID == currentId.Value)
                    {
                        comboBox2.SelectedItem = item;
                    }
                }

                comboBox2.DisplayMember = "FullName";
                comboBox2.ValueMember = "EmployeeID";
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка при загрузке данных сотрудника: {ex.Message}",
                              "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                con.Close();
            }
        }

        private void LoadDepartmentDataForComboBox()
        {
            MySqlConnection con = new MySqlConnection(GlobalValue.GetConnString());
            try
            {
                con.Open(); // Открытие соединения
                MySqlCommand cmd = new MySqlCommand("SELECT DepartmentID, DepartmentName FROM department", con);
                comboBox1.Items.Clear(); // Очистка ComboBox
                MySqlDataReader rd = cmd.ExecuteReader();
                while (rd.Read())
                {
                    // Добавление отделений в ComboBox
                    comboBox1.Items.Add(new DepartmentItem(rd.GetInt32(0), rd.GetString(1)));
                }
                comboBox1.DisplayMember = "DepartmentName"; // Настройка отображаемого поля
                comboBox1.ValueMember = "DepartmentID"; // Настройка поля значения
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка при загрузке данных Department: {ex.Message}", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                con.Close(); // Закрытие соединения
            }
        }

        private void GuidesForm_FormClosed(object sender, FormClosedEventArgs e)
        {
            if (Application.OpenForms["MainForm"] is MainForm mn)
                mn.Show();
        }
    }
    public class WardItem
    {
        public int WardId { get; set; } // ID палаты
        public string TypeOfWard { get; set; } // Тип палаты

        public WardItem(int wardId, string typeOfWard)
        {
            WardId = wardId;
            TypeOfWard = typeOfWard;
        }

        public override string ToString()
        {
            return $"{WardId} ({TypeOfWard})"; // Форматирование строки
        }
    }

    public class BedItem
    {
        public int BedID { get; set; } // ID койки
        public int WardID { get; set; } // ID палаты
        public string BedStatus { get; set; } // Статус койки

        public BedItem(int bedID, int wardID, string bedStatus)
        {
            BedID = bedID;
            WardID = wardID;
            BedStatus = bedStatus;
        }

        public BedItem(int bedID, string bedStatus)
        {
            BedID = bedID;
            BedStatus = bedStatus;
        }

        public override string ToString()
        {
            return $"{BedID} ({BedStatus})"; // Форматирование строки
        }
    }
    public class DepartmentItem
    {
        public int DepartmentId { get; set; } // ID отделения
        public string DepartmentName { get; set; } // Название отделения

        public DepartmentItem(int departmentId, string departmentName)
        {
            DepartmentId = departmentId;
            DepartmentName = departmentName;
        }

        public override string ToString()
        {
            return $"{DepartmentId} ({DepartmentName})"; // Форматирование строки
        }
    }
    public class EmployeeItem
    {
        public int EmployeeID { get; set; }
        public string FullName { get; set; }

        public EmployeeItem(int employeeID, string fullName)
        {
            EmployeeID = employeeID;
            FullName = fullName;
        }

        public override bool Equals(object obj)
        {
            return obj is EmployeeItem item &&
                   EmployeeID == item.EmployeeID;
        }

        public override int GetHashCode()
        {
            return EmployeeID.GetHashCode();
        }

        public override string ToString()
        {
            return FullName;
        }
    }
}