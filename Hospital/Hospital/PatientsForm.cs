using MySql.Data.MySqlClient;
using System;
using System.Data;
using System.Drawing;
using System.Text.RegularExpressions;
using System.Windows.Forms;

namespace Hospital
{
    public partial class PatientsForm : Form
    {
        private DataTable patientTable;
        private int selectedRowIndex = -1;
        private bool isAdding = false;
        private bool isRowSelected = false;

        public PatientsForm()
        {
            InitializeComponent();
            LoadPatientData();
            UpdateRowCount();
        }

        private void LoadPatientData()
        {
            try
            {
                using (MySqlConnection connection = new MySqlConnection(GlobalValue.GetConnString()))
                {
                    connection.Open();

                    using (MySqlCommand command = new MySqlCommand("SELECT patientSnils, patientFIO, g.genderID, g.genderName, " +
                        "patientAddress, TIMESTAMPDIFF(YEAR, patientBirthday, CURDATE()) AS patientAge, patientPhoneNumber, diagnosisID, s.statusID, s.statusName " +
                        "from patient p " +
                        "inner join gender g on p.genderID = g.genderID " +
                        "inner join patientstatus s on p.statusID = s.statusID " +
                        "order by patientFIO asc", connection))
                    {
                        MySqlDataAdapter adapter = new MySqlDataAdapter(command);
                        patientTable = new DataTable();
                        adapter.Fill(patientTable);

                        dataGridViewPatients.AutoGenerateColumns = false;
                        dataGridViewPatients.Columns.Clear();

                        AddDataGridViewColumn("patientSnils", "СНИЛС", "patientSnils");
                        AddDataGridViewColumn("patientFIO", "ФИО", "patientFIO");
                        AddDataGridViewColumn("genderID", "genderID", "genderID");
                        AddDataGridViewColumn("genderName", "Пол", "genderName");
                        AddDataGridViewColumn("patientAddress", "Адрес", "patientAddress");
                        AddDataGridViewColumn("patientAge", "Возраст", "patientAge");
                        AddDataGridViewColumn("patientPhoneNumber", "Номер телефона", "patientPhoneNumber");
                        AddDataGridViewColumn("diagnosisID", "Код диагноза", "diagnosisID");
                        AddDataGridViewColumn("statusID", "statusID", "statusID");
                        AddDataGridViewColumn("statusName", "Текущий статус", "statusName");

                        dataGridViewPatients.DataSource = patientTable;
                        dataGridViewPatients.Columns[2].Visible = false;
                        dataGridViewPatients.Columns[8].Visible = false;
                        dataGridViewPatients.Refresh();
                        UpdateRowCount();
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка при загрузке данных: {ex.Message}", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void AddDataGridViewColumn(string name, string headerText, string dataPropertyName)
        {
            DataGridViewTextBoxColumn column = new DataGridViewTextBoxColumn();
            column.Name = name;
            column.HeaderText = headerText;
            column.DataPropertyName = dataPropertyName;
            dataGridViewPatients.Columns.Add(column);
        }

        private void ClearInputFields()
        {
            snils.Clear();
            passport.Clear();
            policy.Clear();
            surname.Clear();
            name.Clear();
            patronymic.Clear();
            data.Value = DateTime.Now.Date;
            comboBoxGender.SelectedIndex = -1;
            address.Clear();
            phone.Clear();
            comboBoxBenefit.SelectedIndex = -1;
            diagnos.Clear();
            FIOrod.Clear();
            phoneRod.Clear();
            rab.Clear();
        }

        private void EnableInputFields(bool enable)
        {
            snils.Enabled = enable;
            passport.Enabled = enable;
            policy.Enabled = enable;
            surname.Enabled = enable;
            name.Enabled = enable;
            patronymic.Enabled = enable;
            data.Enabled = enable;
            comboBoxGender.Enabled = enable;
            address.Enabled = enable;
            phone.Enabled = enable;
            comboBoxBenefit.Enabled = enable;
            diagnos.Enabled = enable;
            FIOrod.Enabled = enable;
            phoneRod.Enabled = enable;
            rab.Enabled = enable;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (isAdding)
            {
                AddPatient();
            }
            else
            {
                UpdatePatient();
            }
        }

        private void AddPatient()
        {
            if (!ValidateInput()) return;

            using (MySqlConnection connection = new MySqlConnection(GlobalValue.GetConnString()))
            {
                connection.Open();

                // Проверка на дубликаты
                if (CheckForDuplicates(connection))
                {
                    return;
                }

                string query = @"INSERT INTO Patient (patientSnils, passportNumber, policyNumber, patientSurname, patientName, patientPatronymic, 
                                patientBirthday, genderID, patientAddress, patientPhoneNumber, statusID, diagnosisID, 
                                relativeFIO, relativePhone, workplace, benefitID) 
                                VALUES (@Snils, @Passport, @Policy, @Surname, @Name, @Patronymic, @Birthday, @Gender, 
                                @Address, @Phone, 1, @Diagnosis, @RelativeFIO, @RelativePhone, @Workplace, @Benefit)";

                using (MySqlCommand command = new MySqlCommand(query, connection))
                {
                    SetCommandParameters(command);

                    try
                    {
                        command.ExecuteNonQuery();
                        MessageBox.Show("Пациент успешно добавлен.", "Успех", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        LoadPatientData();
                        ClearInputFields();
                        EnableInputFields(false);
                        isAdding = false;
                        UpdateButtonStates();
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show($"Ошибка при добавлении пациента: {ex.Message}", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
        }

        private bool CheckForDuplicates(MySqlConnection connection)
        {
            // Проверка СНИЛС
            string checkSnilsQuery = "SELECT COUNT(*) FROM Patient WHERE patientSnils = @Snils";
            using (MySqlCommand checkCommand = new MySqlCommand(checkSnilsQuery, connection))
            {
                checkCommand.Parameters.AddWithValue("@Snils", snils.Text);
                if (Convert.ToInt32(checkCommand.ExecuteScalar()) > 0)
                {
                    MessageBox.Show("Пациент с таким СНИЛС уже существует.", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return true;
                }
            }

            // Проверка паспорта
            string checkPassportQuery = "SELECT COUNT(*) FROM Patient WHERE passportNumber = @Passport";
            using (MySqlCommand checkCommand = new MySqlCommand(checkPassportQuery, connection))
            {
                checkCommand.Parameters.AddWithValue("@Passport", passport.Text);
                if (Convert.ToInt32(checkCommand.ExecuteScalar()) > 0)
                {
                    MessageBox.Show("Пациент с таким паспортом уже существует.", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return true;
                }
            }

            // Проверка полиса
            string checkPolicyQuery = "SELECT COUNT(*) FROM Patient WHERE policyNumber = @Policy";
            using (MySqlCommand checkCommand = new MySqlCommand(checkPolicyQuery, connection))
            {
                checkCommand.Parameters.AddWithValue("@Policy", policy.Text);
                if (Convert.ToInt32(checkCommand.ExecuteScalar()) > 0)
                {
                    MessageBox.Show("Пациент с таким полисом уже существует.", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return true;
                }
            }

            return false;
        }

        private void UpdatePatient()
        {
            if (!ValidateInput() || selectedRowIndex < 0) return;

            using (MySqlConnection connection = new MySqlConnection(GlobalValue.GetConnString()))
            {
                connection.Open();

                string currentSnils = dataGridViewPatients.Rows[selectedRowIndex].Cells["patientSnils"].Value.ToString();
                string currentPassport = passport.Text;
                string currentPolicy = policy.Text;

                // Проверка на дубликаты (исключая текущую запись)
                string checkSnilsQuery = "SELECT COUNT(*) FROM Patient WHERE patientSnils = @Snils AND patientSnils != @CurrentSnils";
                using (MySqlCommand checkCommand = new MySqlCommand(checkSnilsQuery, connection))
                {
                    checkCommand.Parameters.AddWithValue("@Snils", snils.Text);
                    checkCommand.Parameters.AddWithValue("@CurrentSnils", currentSnils);
                    if (Convert.ToInt32(checkCommand.ExecuteScalar()) > 0)
                    {
                        MessageBox.Show("Пациент с таким СНИЛС уже существует.", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }
                }

                string checkPassportQuery = "SELECT COUNT(*) FROM Patient WHERE passportNumber = @Passport AND patientSnils != @CurrentSnils";
                using (MySqlCommand checkCommand = new MySqlCommand(checkPassportQuery, connection))
                {
                    checkCommand.Parameters.AddWithValue("@Passport", passport.Text);
                    checkCommand.Parameters.AddWithValue("@CurrentSnils", currentSnils);
                    if (Convert.ToInt32(checkCommand.ExecuteScalar()) > 0)
                    {
                        MessageBox.Show("Пациент с таким паспортом уже существует.", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }
                }

                string checkPolicyQuery = "SELECT COUNT(*) FROM Patient WHERE policyNumber = @Policy AND patientSnils != @CurrentSnils";
                using (MySqlCommand checkCommand = new MySqlCommand(checkPolicyQuery, connection))
                {
                    checkCommand.Parameters.AddWithValue("@Policy", policy.Text);
                    checkCommand.Parameters.AddWithValue("@CurrentSnils", currentSnils);
                    if (Convert.ToInt32(checkCommand.ExecuteScalar()) > 0)
                    {
                        MessageBox.Show("Пациент с таким полисом уже существует.", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }
                }

                string query = @"UPDATE Patient SET 
                                patientSnils = @Snils, 
                                passportNumber = @Passport, 
                                policyNumber = @Policy, 
                                patientSurname = @Surname, 
                                patientName = @Name, 
                                patientPatronymic = @Patronymic,
                                patientBirthday = @Birthday, 
                                genderID = @Gender, 
                                patientAddress = @Address, 
                                patientPhoneNumber = @Phone,
                                diagnosisID = @Diagnosis, 
                                relativeFIO = @RelativeFIO, 
                                relativePhone = @RelativePhone, 
                                workplace = @Workplace, 
                                benefitID = @Benefit
                                WHERE patientSnils = @CurrentSnils";

                using (MySqlCommand command = new MySqlCommand(query, connection))
                {
                    SetCommandParameters(command);
                    command.Parameters.AddWithValue("@CurrentSnils", currentSnils);

                    try
                    {
                        command.ExecuteNonQuery();
                        MessageBox.Show("Данные пациента успешно обновлены.", "Успех", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        LoadPatientData();
                        ClearInputFields();
                        EnableInputFields(false);
                        isRowSelected = false;
                        UpdateButtonStates();
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show($"Ошибка при обновлении данных пациента: {ex.Message}", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
        }

        private void SetCommandParameters(MySqlCommand command)
        {
            command.Parameters.AddWithValue("@Snils", snils.Text);
            command.Parameters.AddWithValue("@Passport", passport.Text);
            command.Parameters.AddWithValue("@Policy", policy.Text);
            command.Parameters.AddWithValue("@Surname", surname.Text);
            command.Parameters.AddWithValue("@Name", name.Text);
            command.Parameters.AddWithValue("@Patronymic", patronymic.Text);
            command.Parameters.AddWithValue("@Birthday", data.Value.ToString("yyyy-MM-dd"));
            command.Parameters.AddWithValue("@Gender", comboBoxGender.SelectedValue ?? (object)DBNull.Value);
            command.Parameters.AddWithValue("@Address", address.Text);
            command.Parameters.AddWithValue("@Phone", phone.Text.Replace(" ", "").Replace("-", ""));
            command.Parameters.AddWithValue("@Diagnosis", string.IsNullOrEmpty(diagnos.Text) ? (object)DBNull.Value : diagnos.Text);
            command.Parameters.AddWithValue("@RelativeFIO", string.IsNullOrEmpty(FIOrod.Text) ? (object)DBNull.Value : FIOrod.Text);
            command.Parameters.AddWithValue("@RelativePhone", string.IsNullOrEmpty(phoneRod.Text) ? (object)DBNull.Value : phoneRod.Text.Replace(" ", "").Replace("-", ""));
            command.Parameters.AddWithValue("@Workplace", string.IsNullOrEmpty(rab.Text) ? (object)DBNull.Value : rab.Text);
            command.Parameters.AddWithValue("@Benefit", comboBoxBenefit.SelectedValue ?? (object)DBNull.Value);
        }

        private bool ValidateInput()
        {
            // Проверка СНИЛС (11 цифр)
            if (!Regex.IsMatch(snils.Text, @"^\d{11}$"))
            {
                MessageBox.Show("СНИЛС должен содержать 11 цифр.", "Ошибка ввода", MessageBoxButtons.OK, MessageBoxIcon.Error);
                snils.Focus();
                return false;
            }

            // Проверка паспорта (4 цифры, пробел, 6 цифр)
            if (!Regex.IsMatch(passport.Text, @"^\d{4}\s\d{6}$"))
            {
                MessageBox.Show("Паспорт должен быть в формате: 1234 567890", "Ошибка ввода", MessageBoxButtons.OK, MessageBoxIcon.Error);
                passport.Focus();
                return false;
            }

            // Проверка полиса (11 цифр)
            if (!Regex.IsMatch(policy.Text, @"^\d{11}$"))
            {
                MessageBox.Show("Полис должен содержать 11 цифр.", "Ошибка ввода", MessageBoxButtons.OK, MessageBoxIcon.Error);
                policy.Focus();
                return false;
            }

            // Проверка фамилии (русские буквы, начинается с заглавной, допускается до 3 дефисов)
            if (!Regex.IsMatch(surname.Text, @"^[А-ЯЁ][а-яё]*(?:-[А-ЯЁ][а-яё]*){0,3}$"))
            {
                MessageBox.Show("Фамилия должна начинаться с заглавной русской буквы и может содержать дефисы (максимум 3).", "Ошибка ввода", MessageBoxButtons.OK, MessageBoxIcon.Error);
                surname.Focus();
                return false;
            }

            // Проверка имени (русские буквы, начинается с заглавной)
            if (!Regex.IsMatch(name.Text, @"^[А-ЯЁ][а-яё]*$"))
            {
                MessageBox.Show("Имя должно начинаться с заглавной русской буквы.", "Ошибка ввода", MessageBoxButtons.OK, MessageBoxIcon.Error);
                name.Focus();
                return false;
            }

            // Проверка отчества (русские буквы, начинается с заглавной)
            if (!Regex.IsMatch(patronymic.Text, @"^[А-ЯЁ][а-яё]*(?:-[А-ЯЁ][а-яё]*)?$"))
            {
                MessageBox.Show("Отчество должно начинаться с заглавной русской буквы.", "Ошибка ввода", MessageBoxButtons.OK, MessageBoxIcon.Error);
                patronymic.Focus();
                return false;
            }

            // Проверка даты рождения
            if (data.Value > DateTime.Now.Date)
            {
                MessageBox.Show("Дата рождения не может быть в будущем.", "Ошибка ввода", MessageBoxButtons.OK, MessageBoxIcon.Error);
                data.Focus();
                return false;
            }

            if (data.Value < DateTime.Now.AddYears(-120))
            {
                MessageBox.Show("Возраст должен быть до 120 лет.", "Ошибка ввода", MessageBoxButtons.OK, MessageBoxIcon.Error);
                data.Focus();
                return false;
            }

            // Проверка пола
            if (comboBoxGender.SelectedIndex == -1)
            {
                MessageBox.Show("Выберите пол.", "Ошибка ввода", MessageBoxButtons.OK, MessageBoxIcon.Error);
                comboBoxGender.Focus();
                return false;
            }

            // Проверка адреса
            if (string.IsNullOrWhiteSpace(address.Text))
            {
                MessageBox.Show("Введите адрес.", "Ошибка ввода", MessageBoxButtons.OK, MessageBoxIcon.Error);
                address.Focus();
                return false;
            }

            // Проверка телефона
            if (!phone.MaskCompleted)
            {
                MessageBox.Show("Введите корректный номер телефона.", "Ошибка ввода", MessageBoxButtons.OK, MessageBoxIcon.Error);
                phone.Focus();
                return false;
            }

            return true;
        }

        private void PatientsForm_FormClosed(object sender, FormClosedEventArgs e)
        {
            if (HospitalizationIteractionForm.isSwitch)
            {
                HospitalizationIteractionForm.isSwitch = false;
                if (Application.OpenForms["HospitalizationIteractionForm"] is HospitalizationIteractionForm mm)
                    mm.Show();
            }
            else
            {
                if (Application.OpenForms["MainForm"] is MainForm mn)
                    mn.Show();
            }
        }

        private void textBoxSurName_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)Keys.Back)
            {
                e.Handled = false;
                return;
            }

            // Русские буквы и дефис
            if (!Regex.IsMatch(e.KeyChar.ToString(), @"^[А-Яа-яЁё-]+$"))
            {
                e.Handled = true;
            }

            // Автоматическое преобразование первой буквы в заглавную
            if (surname.Text.Length == 0 && char.IsLetter(e.KeyChar))
            {
                e.KeyChar = char.ToUpper(e.KeyChar);
            }
        }

        private void textBoxName_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)Keys.Back)
            {
                e.Handled = false;
                return;
            }

            // Только русские буквы
            if (!Regex.IsMatch(e.KeyChar.ToString(), @"^[А-Яа-яЁё]+$"))
            {
                e.Handled = true;
            }

            // Автоматическое преобразование первой буквы в заглавную
            if (name.Text.Length == 0 && char.IsLetter(e.KeyChar))
            {
                e.KeyChar = char.ToUpper(e.KeyChar);
            }
        }

        private void textBoxPatronymic_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)Keys.Back)
            {
                e.Handled = false;
                return;
            }

            // Русские буквы и дефис
            if (!Regex.IsMatch(e.KeyChar.ToString(), @"^[А-Яа-яЁё-]+$"))
            {
                e.Handled = true;
            }

            // Автоматическое преобразование первой буквы в заглавную
            if (patronymic.Text.Length == 0 && char.IsLetter(e.KeyChar))
            {
                e.KeyChar = char.ToUpper(e.KeyChar);
            }
        }

        private void textBoxPassport_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)Keys.Back)
            {
                e.Handled = false;
                return;
            }

            // Только цифры
            if (!char.IsDigit(e.KeyChar))
            {
                e.Handled = true;
                return;
            }

            // Автоматическая вставка пробела после 4 цифр
            if (passport.Text.Length == 4 && !passport.Text.Contains(" "))
            {
                passport.Text += " ";
                passport.SelectionStart = passport.Text.Length;
            }
        }

        private void textBoxSnils_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)Keys.Back)
            {
                e.Handled = false;
                return;
            }

            // Только цифры
            if (!char.IsDigit(e.KeyChar))
            {
                e.Handled = true;
            }
        }

        private void textBoxPolicy_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)Keys.Back)
            {
                e.Handled = false;
                return;
            }

            // Только цифры
            if (!char.IsDigit(e.KeyChar))
            {
                e.Handled = true;
            }
        }

        private void textBoxDiagnos_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)Keys.Back)
            {
                e.Handled = false;
                return;
            }

            // Только цифры
            if (!char.IsDigit(e.KeyChar))
            {
                e.Handled = true;
            }
        }

        private void maskedTextBoxPhoneNumber_Leave(object sender, EventArgs e)
        {
            string phoneNumberText = phone.Text.Replace("(", "").Replace(")", "").Replace("-", "").Replace(" ", "");
            if (!Regex.IsMatch(phoneNumberText, @"^\+7\d{10}$"))
            {
                MessageBox.Show("Неверный формат номера телефона. Пример: +7 (9XX) XXX-XX-XX", "Ошибка ввода", MessageBoxButtons.OK, MessageBoxIcon.Error);
                phone.Focus();
            }
        }

        private void maskedTextBoxPhoneRod_Leave(object sender, EventArgs e)
        {
            string phoneNumberText = phoneRod.Text.Replace("(", "").Replace(")", "").Replace("-", "").Replace(" ", "");
            if (!string.IsNullOrEmpty(phoneNumberText) && !Regex.IsMatch(phoneNumberText, @"^\+7\d{10}$"))
            {
                MessageBox.Show("Неверный формат номера телефона. Пример: +7 (9XX) XXX-XX-XX", "Ошибка ввода", MessageBoxButtons.OK, MessageBoxIcon.Error);
                phoneRod.Focus();
            }
        }

        private void textBoxPolicy_Leave(object sender, EventArgs e)
        {
            if (!Regex.IsMatch(policy.Text, @"^\d{11}$"))
            {
                MessageBox.Show("Полис должен содержать 11 цифр.", "Ошибка ввода", MessageBoxButtons.OK, MessageBoxIcon.Error);
                policy.Focus();
            }
        }

        private void textBoxPassport_Leave(object sender, EventArgs e)
        {
            if (!Regex.IsMatch(passport.Text, @"^\d{4}\s\d{6}$"))
            {
                MessageBox.Show("Паспорт должен быть в формате: 1234 567890", "Ошибка ввода", MessageBoxButtons.OK, MessageBoxIcon.Error);
                passport.Focus();
            }
        }

        private void textBoxSnils_Leave(object sender, EventArgs e)
        {
            if (!Regex.IsMatch(snils.Text, @"^\d{11}$"))
            {
                MessageBox.Show("СНИЛС должен содержать 11 цифр.", "Ошибка ввода", MessageBoxButtons.OK, MessageBoxIcon.Error);
                snils.Focus();
            }
        }

        private void dataGridViewPatients_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                if (isRowSelected && selectedRowIndex == e.RowIndex)
                {
                    // Повторное нажатие на ту же строку - снимаем выделение
                    dataGridViewPatients.ClearSelection();
                    ClearInputFields();
                    isRowSelected = false;
                    selectedRowIndex = -1;
                }
                else
                {
                    // Выбираем новую строку
                    dataGridViewPatients.Rows[e.RowIndex].Selected = true;
                    selectedRowIndex = e.RowIndex;
                    isRowSelected = true;

                    // Заполняем поля данными из выбранной строки
                    FillFieldsFromSelectedRow();
                }

                UpdateButtonStates();

                if (HospitalizationIteractionForm.isSwitch)
                {
                    HospitalizationIteractionForm.patID = dataGridViewPatients.Rows[e.RowIndex].Cells[0].Value.ToString();
                    this.Close();
                }
            }
        }

        private void FillFieldsFromSelectedRow()
        {
            try
            {
                using (MySqlConnection connection = new MySqlConnection(GlobalValue.GetConnString()))
                {
                    connection.Open();

                    string snilsValue = dataGridViewPatients.Rows[selectedRowIndex].Cells["patientSnils"].Value.ToString();
                    string query = "SELECT * FROM Patient WHERE patientSnils = @Snils";

                    using (MySqlCommand command = new MySqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@Snils", snilsValue);

                        using (MySqlDataReader reader = command.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                snils.Text = reader["patientSnils"].ToString();
                                passport.Text = reader["passportNumber"].ToString();
                                policy.Text = reader["policyNumber"].ToString();

                                string[] fioParts = reader["patientFIO"].ToString().Split(' ');
                                if (fioParts.Length >= 1) surname.Text = fioParts[0];
                                if (fioParts.Length >= 2) name.Text = fioParts[1];
                                if (fioParts.Length >= 3) patronymic.Text = fioParts[2];

                                if (reader["patientBirthday"] != DBNull.Value)
                                {
                                    data.Value = Convert.ToDateTime(reader["patientBirthday"]);
                                }

                                comboBoxGender.SelectedValue = reader["genderID"];
                                address.Text = reader["patientAddress"].ToString();
                                phone.Text = reader["patientPhoneNumber"].ToString();
                                comboBoxBenefit.SelectedValue = reader["benefitID"];
                                diagnos.Text = reader["diagnosisID"].ToString();
                                FIOrod.Text = reader["relativeFIO"].ToString();
                                phoneRod.Text = reader["relativePhone"].ToString();
                                rab.Text = reader["workplace"].ToString();
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка при загрузке данных пациента: {ex.Message}", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void UpdateButtonStates()
        {
            buttonAdd.Enabled = !isRowSelected;
            buttonEdit.Enabled = isRowSelected;

            if (isRowSelected)
            {
                buttonAdd.BackColor = SystemColors.Control;
                buttonEdit.BackColor = Color.PowderBlue;
            }
            else
            {
                buttonAdd.BackColor = Color.PowderBlue;
                buttonEdit.BackColor = SystemColors.Control;
            }
        }

        private void buttonAdd_Click(object sender, EventArgs e)
        {
            isAdding = true;
            ClearInputFields();
            EnableInputFields(true);
            dataGridViewPatients.ClearSelection();
            isRowSelected = false;
            selectedRowIndex = -1;
            UpdateButtonStates();
        }

        private void buttonEdit_Click(object sender, EventArgs e)
        {
            isAdding = false;
            EnableInputFields(true);
        }

        private void buttonExit_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void UpdateRowCount()
        {
            labelRowCount.Text = $"Всего записей: {dataGridViewPatients.Rows.Count}";
        }

        private void PatientsForm_Load(object sender, EventArgs e)
        {
            // Загрузка данных для комбобоксов
            LoadComboBoxData();
        }

        private void LoadComboBoxData()
        {
            try
            {
                using (MySqlConnection connection = new MySqlConnection(GlobalValue.GetConnString()))
                {
                    connection.Open();

                    // Загрузка полов
                    using (MySqlCommand command = new MySqlCommand("SELECT genderID, genderName FROM Gender", connection))
                    {
                        DataTable genderTable = new DataTable();
                        MySqlDataAdapter adapter = new MySqlDataAdapter(command);
                        adapter.Fill(genderTable);

                        comboBoxGender.DataSource = genderTable;
                        comboBoxGender.DisplayMember = "genderName";
                        comboBoxGender.ValueMember = "genderID";
                    }

                    // Загрузка льгот
                    using (MySqlCommand command = new MySqlCommand("SELECT benefitID, benefitName FROM Benefits", connection))
                    {
                        DataTable benefitTable = new DataTable();
                        MySqlDataAdapter adapter = new MySqlDataAdapter(command);
                        adapter.Fill(benefitTable);

                        comboBoxBenefit.DataSource = benefitTable;
                        comboBoxBenefit.DisplayMember = "benefitName";
                        comboBoxBenefit.ValueMember = "benefitID";
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка при загрузке справочников: {ex.Message}", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}