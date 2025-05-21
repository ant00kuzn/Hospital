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
using ToolTip = System.Windows.Forms.ToolTip;
using System.Configuration;
using System.IO;

namespace Hospital
{
    public partial class EmployeesForm : Form
    {
        private bool isAddingNew = false;

        private DateTime lastActivity = DateTime.Now; //Последняя активность пользователя
        private Timer inactive; //Таймер
        private bool fclosed = false; //Активна ли щас эта форма

        private int selectedRowIndex = -1;
        private int currentNumPage = 0;

        public EmployeesForm()
        {
            InitializeComponent(); // Инициализация компонентов

            //Создаем новый таймер
            inactive = new Timer();
            inactive.Interval = 1000;
            inactive.Tick += Inactive_Tick;

            //Если пользователь что-то сделал сбрасываем таймер
            this.MouseMove += ActivityOccured;
            this.KeyDown += ActivityOccured;
            this.MouseClick += ActivityOccured;
            this.MouseWheel += ActivityOccured;
            this.Move += ActivityOccured;
            this.MouseCaptureChanged += ActivityOccured;
            this.LostFocus += LostFocu;
            this.GotFocus += ActivityOccured;
        }

        //метод обработки сброса таймера по действию пользователя
        private void ActivityOccured(object sender, EventArgs e)
        {
            ResetInactivityTimer();
        }

        private void LostFocu(object sender, EventArgs e)
        {
            fclosed = true;
            inactive.Stop();
        }

        //Тик таймера с проверкой на истечение таймера
        private void Inactive_Tick(object sender, EventArgs e)
        {
            if((DateTime.Now - lastActivity).TotalSeconds > Convert.ToInt32(ConfigurationManager.AppSettings["timerInactive"]))
            {
                LockSystem();
            }
        }

        //Блокировка системы (переброс на форму авторизации)
        private void LockSystem()
        {
            if (!fclosed)
            {
                fclosed = true;
                inactive.Stop();

                LoginForm ll = new LoginForm(true);
                this.Hide();
                
                ll.ShowDialog();

                this.Show();

                fclosed = false;
                ResetInactivityTimer();
                inactive.Start();
            }
        }

        //Сброс таймера
        private void ResetInactivityTimer()
        {
            lastActivity = DateTime.Now;
        }

        private void LoadEmployeeData()
        {
            try
            {
                using (MySqlConnection con = new MySqlConnection(GlobalValue.GetConnString()))
                {
                    con.Open();
                    using (MySqlCommand cmd = new MySqlCommand($"SELECT employeeServiceNumber as 'Таб. номер', employeeFIO as 'ФИО', " +
                        $"employeePost as 'Должность', employeePhoneNumber as 'Телефон' " +
                        $"FROM employee " +
                        $"ORDER BY employeeServiceNumber asc", con)) // Сортировка по табельному номеру
                    {
                        MySqlDataAdapter bedAdapter = new MySqlDataAdapter(cmd);
                        DataTable bedTable = new DataTable();
                        bedAdapter.Fill(bedTable);

                        dataGridViewEmployees.DataSource = bedTable;
                    }

                    MySqlCommand cdm = new MySqlCommand("select count(*) from employee", con);
                    labRowCount.Text = "Количество записей: " + cdm.ExecuteScalar();

                    dataGridViewEmployees.Columns[0].Width = 100;

                    con.Close();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"{ex.Message}", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
        }

        private void DataGridViewEmployees_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                dataGridViewEmployees.Rows[e.RowIndex].Selected = true;
                string employeeID = dataGridViewEmployees.Rows[e.RowIndex].Cells[0].Value.ToString();

                if (UsersForm.isSwitch)
                {
                    UsersForm.empID = employeeID;
                    this.Close();
                }
                else
                {
                    changePhoto.Text = "Изменить фото";
                    edit.Enabled = true;
                    delete.Enabled = true;
                    add.Enabled = false;
                    isAddingNew = false;

                    LoadEmployee(employeeID);
                }
            }
            else
            {
                dataGridViewEmployees.ClearSelection();

                changePhoto.Text = "Добавить фото";
                edit.Enabled = false;
                delete.Enabled = false;
                add.Enabled = true;
            }
        }

        private void LoadEmployee(string empID)
        {
            try
            {
                MySqlConnection connection = new MySqlConnection(GlobalValue.GetConnString());
                connection.Open();
                MySqlCommand cmd = new MySqlCommand($"select * from employee where employeeServiceNumber = '{empID}'", connection);
                MySqlDataReader reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    tabNumber.Text = reader.GetString(0);
                    fio.Text = reader.GetString(1);
                    phone.Text = reader.GetString(2);
                    post.Text = reader.GetString(3);
                    dbrd.Value = DateTime.Parse(reader.GetString(4));

                    if (reader[5] != DBNull.Value)
                    {
                        byte[] imgData = (byte[])reader[6];
                        using (System.IO.MemoryStream ms = new System.IO.MemoryStream(imgData))
                        {
                            Image img = Image.FromStream(ms);
                            photo.Image = img;
                        }
                    }
                    else
                    {
                        photo.Image = Properties.Resources.doctor_linear_icon_vector_285420161;
                    }
                }

                connection.Close();
            }
            catch (MySqlException ex)
            {
                MessageBox.Show("Ошибка получения данных: " + ex.Message, "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            catch (Exception e)
            {
                MessageBox.Show("Ошибка получения данных: " + e.Message, "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
        }

        private void EmployeesForm_FormClosed(object sender, FormClosedEventArgs e)
        {
            if (UsersForm.isSwitch)
            {
                UsersForm.isSwitch = false;
                if (Application.OpenForms["UsersForm"] is UsersForm mn)
                    mn.Show();
            }
            else
            {
                if (Application.OpenForms["MainForm"] is MainForm mn)
                    mn.Show();
            }
        }

        private void EmployeesForm_Load(object sender, EventArgs e)
        {
            changePhoto.Text = "Добавить фото";
            LoadEmployeeData();
            dbrd.MinDate = DateTime.Now.AddYears(-65);
            dbrd.MaxDate = DateTime.Now.AddYears(-18).AddMinutes(1);
            dataGridViewEmployees.ClearSelection();
            ClearFields();

            photo.Image = Properties.Resources.doctor_linear_icon_vector_285420161;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void changePhoto_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "Image Files|*.jpg;*.jpeg;*.png;*.gif;*.bmp";
            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                FileInfo fi = new FileInfo(openFileDialog.FileName);
                if (fi.Length > 16777214)
                {
                    MessageBox.Show("Размер картинки превышает максимально допустимый.", "Ошибка выбора картинки.", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
                Image img = Image.FromFile(openFileDialog.FileName);
                photo.Image = img;
            }
        }

        private byte[] ImageToByteArray(System.Drawing.Image imageIn)
        {
            if (imageIn == null)
                return null;
            using (System.IO.MemoryStream ms = new System.IO.MemoryStream())
            {
                imageIn.Save(ms, System.Drawing.Imaging.ImageFormat.Jpeg);
                return ms.ToArray();
            }
        }

        private void ClearFields()
        {
            tabNumber.Text = "";
            fio.Text = "";
            phone.Text = "";
            post.Text = "";
            dbrd.Value = DateTime.Now.AddYears(-18);
            photo.Image = null;
        }

        private bool ValidateFields()
        {
            // Проверка табельного номера (только цифры)
            if (!Regex.IsMatch(tabNumber.Text, @"^\d+$"))
            {
                MessageBox.Show("Табельный номер должен содержать только цифры.", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                tabNumber.Focus();
                return false;
            }

            // Проверка ФИО (только буквы, пробелы и дефисы)
            if (!Regex.IsMatch(fio.Text, @"^[А-ЯЁ][а-яё]+\s[А-ЯЁ][а-яё]+\s[А-ЯЁ][а-яё]+$"))
            {
                MessageBox.Show("ФИО должно быть в формате 'Фамилия Имя Отчество' с заглавной буквы.", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                fio.Focus();
                return false;
            }

            // Проверка телефона
            if (!Regex.IsMatch(phone.Text.Replace(" ", "").Replace("-", ""), @"^\+?\d{10,15}$"))
            {
                MessageBox.Show("Телефон должен содержать от 10 до 15 цифр.", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                phone.Focus();
                return false;
            }

            // Проверка должности
            if (string.IsNullOrWhiteSpace(post.Text))
            {
                MessageBox.Show("Должность не может быть пустой.", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                post.Focus();
                return false;
            }

            // Проверка даты рождения (не может быть в будущем)
            if (dbrd.Value > DateTime.Now)
            {
                MessageBox.Show("Дата рождения не может быть в будущем.", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                dbrd.Focus();
                return false;
            }

            return true;
        }
        #endregion

        private void add_Click(object sender, EventArgs e)
        {
            try
            {
                if (!ValidateFields())
                    return;

                // Проверка на дубликат табельного номера
                using (MySqlConnection con = new MySqlConnection(GlobalValue.GetConnString()))
                {
                    con.Open();
                    MySqlCommand checkCmd = new MySqlCommand(
                        "SELECT COUNT(*) FROM employee WHERE employeeServiceNumber = @tabNum", con);
                    checkCmd.Parameters.AddWithValue("@tabNum", tabNumber.Text);
                    int count = Convert.ToInt32(checkCmd.ExecuteScalar());

                    if (count > 0)
                    {
                        MessageBox.Show("Сотрудник с таким табельным номером уже существует.", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return;
                    }

                    // Добавление нового сотрудника
                    MySqlCommand cmd = new MySqlCommand(
                        "INSERT INTO employee (employeeServiceNumber, employeeFIO, employeePhoneNumber, employeePost, employeeBirthDate, employeePhoto) " +
                        "VALUES (@tabNum, @fio, @phone, @post, @birthDate, @photo)", con);

                    cmd.Parameters.AddWithValue("@tabNum", tabNumber.Text);
                    cmd.Parameters.AddWithValue("@fio", fio.Text);
                    cmd.Parameters.AddWithValue("@phone", phone.Text.Replace(" ", "").Replace("-", ""));
                    cmd.Parameters.AddWithValue("@post", post.Text);
                    cmd.Parameters.AddWithValue("@birthDate", dbrd.Value.ToString("yyyy-MM-dd"));
                    cmd.Parameters.AddWithValue("@photo", photo.Image != Properties.Resources.doctor_linear_icon_vector_285420161 ? ImageToByteArray(photo.Image) : null);

                    int rowsAffected = cmd.ExecuteNonQuery();

                    if (rowsAffected > 0)
                    {
                        MessageBox.Show("Сотрудник успешно добавлен.", "Успех", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        isAddingNew = false;
                        LoadEmployeeData();
                        ClearFields();
                    }
                    else
                    {
                        MessageBox.Show("Не удалось добавить сотрудника.", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }

                    con.Close();
                }
            }
            catch (MySqlException ex)
            {
                MessageBox.Show("Ошибка базы данных: " + ex.Message, "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            catch (Exception se)
            {
                MessageBox.Show("Ошибка: " + se.Message, "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void edit_Click(object sender, EventArgs e)
        {
            try
            {
                if (!ValidateFields())
                    return;

                if (string.IsNullOrEmpty(tabNumber.Text))
                {
                    MessageBox.Show("Не выбран сотрудник для редактирования.", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                using (MySqlConnection con = new MySqlConnection(GlobalValue.GetConnString()))
                {
                    con.Open();

                    // Обновление данных сотрудника
                    MySqlCommand cmd = new MySqlCommand(
                        "UPDATE employee SET employeeFIO = @fio, employeePhoneNumber = @phone, employeePost = @post, " +
                        "employeeBirthDate = @birthDate, employeePhoto = @photo " +
                        "WHERE employeeServiceNumber = @tabNum", con);

                    cmd.Parameters.AddWithValue("@fio", fio.Text);
                    cmd.Parameters.AddWithValue("@phone", phone.Text);
                    cmd.Parameters.AddWithValue("@post", post.Text);
                    cmd.Parameters.AddWithValue("@birthDate", dbrd.Value.ToString("yyyy-MM-dd"));
                    cmd.Parameters.AddWithValue("@photo", photo.Image != Properties.Resources.doctor_linear_icon_vector_285420161 ? ImageToByteArray(photo.Image) : null);
                    cmd.Parameters.AddWithValue("@tabNum", tabNumber.Text);

                    int rowsAffected = cmd.ExecuteNonQuery();

                    if (rowsAffected > 0)
                    {
                        MessageBox.Show("Данные сотрудника успешно обновлены.", "Успех", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        LoadEmployeeData();
                    }
                    else
                    {
                        MessageBox.Show("Не удалось обновить данные сотрудника.", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }

                    con.Close();
                }
            }
            catch (MySqlException ex)
            {
                MessageBox.Show("Ошибка базы данных: " + ex.Message, "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            catch (Exception ee)
            {
                MessageBox.Show("Ошибка: " + ee.Message, "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void delete_Click(object sender, EventArgs e)
        {
            try
            {
                if (string.IsNullOrEmpty(tabNumber.Text))
                {
                    MessageBox.Show("Не выбран сотрудник для удаления.", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                // Подтверждение удаления
                DialogResult result = MessageBox.Show(
                    $"Вы уверены, что хотите удалить сотрудника {fio.Text} (таб. № {tabNumber.Text})?",
                    "Подтверждение удаления",
                    MessageBoxButtons.YesNo,
                    MessageBoxIcon.Question);

                if (result != DialogResult.Yes)
                    return;

                using (MySqlConnection con = new MySqlConnection(GlobalValue.GetConnString()))
                {
                    con.Open();

                    // Проверка на связанные записи (например, в таблице назначений)
                    MySqlCommand checkCmd = new MySqlCommand(
                        "SELECT COUNT(*) FROM appointments WHERE employeeServiceNumber = @tabNum", con);
                    checkCmd.Parameters.AddWithValue("@tabNum", tabNumber.Text);
                    int relatedRecords = Convert.ToInt32(checkCmd.ExecuteScalar());

                    if (relatedRecords > 0)
                    {
                        MessageBox.Show("Невозможно удалить сотрудника, так как есть связанные записи (назначения).", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return;
                    }

                    // Удаление сотрудника
                    MySqlCommand cmd = new MySqlCommand(
                        "DELETE FROM employee WHERE employeeServiceNumber = @tabNum", con);
                    cmd.Parameters.AddWithValue("@tabNum", tabNumber.Text);

                    int rowsAffected = cmd.ExecuteNonQuery();

                    if (rowsAffected > 0)
                    {
                        MessageBox.Show("Сотрудник успешно удален.", "Успех", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        LoadEmployeeData();
                        ClearFields();
                    }
                    else
                    {
                        MessageBox.Show("Не удалось удалить сотрудника.", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }

                    con.Close();
                }
            }
            catch (MySqlException ex)
            {
                MessageBox.Show("Ошибка базы данных: " + ex.Message, "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            catch (Exception de)
            {
                MessageBox.Show("Ошибка: " + de.Message, "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        // Методы для форматирования ввода
        private void fio_Leave(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(fio.Text))
            {
                // Приводим ФИО к правильному регистру
                fio.Text = System.Threading.Thread.CurrentThread.CurrentCulture.TextInfo.ToTitleCase(fio.Text.ToLower());
            }
        }

        private void tabNumber_KeyPress(object sender, KeyPressEventArgs e)
        {
            // Разрешаем только цифры в табельном номере
            if (!char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar))
            {
                e.Handled = true;
            }
        }

        private void phone_KeyPress(object sender, KeyPressEventArgs e)
        {
            // Разрешаем только цифры и + в телефоне
            if (!char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar) && e.KeyChar != '+')
            {
                e.Handled = true;
            }
        }

        private void phone_Enter(object sender, EventArgs e)
        {
            // При получении фокуса перемещаем курсор в начало
            if (phone.Text.Length == 0)
                phone.SelectionStart = 0;
        }

        //Остановка таймера
        private void EmployeesForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            inactive.Stop();
            inactive.Dispose();
        }
    }
}