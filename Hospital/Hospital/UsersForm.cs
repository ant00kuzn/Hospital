using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;
using MySql.Data.MySqlClient;

namespace Hospital
{
    public partial class UsersForm : Form
    {
        private bool isAddingNew = false;
        public static bool isSwitch = false;

        public static string empID = "";

        public UsersForm()
        {
            InitializeComponent();
        }

        private void LoadData()
        {
            try
            {
                MySqlConnection connection = new MySqlConnection(GlobalValue.GetConnString());
                connection.Open();
                MySqlCommand cmd = new MySqlCommand($"select user.employeeServiceNumber as 'Таб. номер', e.employeeFIO as 'ФИО', " +
                    $"r.roleName as 'Роль', login as 'Логин' " +
                    $"from user " +
                    $"inner join employee e on e.employeeServiceNumber = user.employeeServiceNumber " +
                    $"inner join role r on r.roleID = user.roleID " +
                    $"order by e.employeeFIO asc", connection);

                MySqlDataAdapter adapter = new MySqlDataAdapter(cmd);
                DataTable dt = new DataTable();

                adapter.Fill(dt);

                dataGridViewUsers.DataSource = dt;

                MySqlCommand cdm = new MySqlCommand("select count(*) from user", connection);
                labRowCount.Text = "Количество записей: " + cdm.ExecuteScalar();

                dataGridViewUsers.Columns[0].Width = 100;

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

        private void LoadRoles()
        {
            try
            {
                MySqlConnection connection = new MySqlConnection(GlobalValue.GetConnString());
                connection.Open();
                MySqlCommand cmd = new MySqlCommand($"select * from role", connection);

                MySqlDataAdapter adapter = new MySqlDataAdapter(cmd);
                DataTable dt = new DataTable();

                adapter.Fill(dt);

                role.DataSource = dt;
                role.DisplayMember = "roleName";
                role.ValueMember = "roleID";

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

        private void UsersForm_FormClosed(object sender, FormClosedEventArgs e)
        {
            if (Application.OpenForms["MainForm"] is MainForm mn)
                mn.Show();
        }

        private void back_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void ClearFields()
        {
            tabNumber.Text = "";
            fio.Text = "";
            login.Text = "";
            password.Text = "";
            role.SelectedIndex = -1;
        }

        private bool ValidateFields()
        {
            // Проверка табельного номера
            if (string.IsNullOrWhiteSpace(tabNumber.Text))
            {
                MessageBox.Show("Табельный номер не может быть пустым.", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                tabNumber.Focus();
                return false;
            }

            // Проверка ФИО
            if (string.IsNullOrWhiteSpace(fio.Text))
            {
                MessageBox.Show("ФИО не может быть пустым.", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                fio.Focus();
                return false;
            }

            // Проверка логина
            if (string.IsNullOrWhiteSpace(login.Text))
            {
                MessageBox.Show("Логин не может быть пустым.", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                login.Focus();
                return false;
            }

            if (!Regex.IsMatch(login.Text, @"^[a-zA-Z0-9_]+$"))
            {
                MessageBox.Show("Логин может содержать только латинские буквы, цифры и символ подчеркивания.", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                login.Focus();
                return false;
            }

            // Проверка пароля (только при добавлении)
            if (isAddingNew && string.IsNullOrWhiteSpace(password.Text))
            {
                MessageBox.Show("Пароль не может быть пустым.", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                password.Focus();
                return false;
            }

            // Проверка роли
            if (role.SelectedIndex == -1)
            {
                MessageBox.Show("Необходимо выбрать роль.", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                role.Focus();
                return false;
            }

            return true;
        }

        private void add_Click(object sender, EventArgs e)
        {
            try
            {
                isAddingNew = true;
                if (!ValidateFields())
                    return;

                using (MySqlConnection connection = new MySqlConnection(GlobalValue.GetConnString()))
                {
                    connection.Open();

                    // Проверка на дубликат табельного номера
                    MySqlCommand checkCmd = new MySqlCommand(
                        "SELECT COUNT(*) FROM user WHERE employeeServiceNumber = @tabNum", connection);
                    checkCmd.Parameters.AddWithValue("@tabNum", tabNumber.Text);
                    int count = Convert.ToInt32(checkCmd.ExecuteScalar());

                    if (count > 0)
                    {
                        MessageBox.Show("Пользователь с таким табельным номером уже существует.", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return;
                    }

                    // Проверка на дубликат логина
                    checkCmd.CommandText = "SELECT COUNT(*) FROM user WHERE login = @login";
                    checkCmd.Parameters.AddWithValue("@login", login.Text);
                    count = Convert.ToInt32(checkCmd.ExecuteScalar());

                    if (count > 0)
                    {
                        MessageBox.Show("Пользователь с таким логином уже существует.", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return;
                    }

                    // Добавление нового пользователя
                    MySqlCommand cmd = new MySqlCommand(
                        "INSERT INTO user (employeeServiceNumber, login, password, roleID) " +
                        "VALUES (@tabNum, @login, SHA2(@password, 256), @roleID)", connection);

                    cmd.Parameters.AddWithValue("@tabNum", tabNumber.Text);
                    cmd.Parameters.AddWithValue("@login", login.Text);
                    cmd.Parameters.AddWithValue("@password", password.Text);
                    cmd.Parameters.AddWithValue("@roleID", role.SelectedValue);

                    int rowsAffected = cmd.ExecuteNonQuery();

                    if (rowsAffected > 0)
                    {
                        MessageBox.Show("Пользователь успешно добавлен.", "Успех", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        LoadData();
                        ClearFields();
                    }
                    else
                    {
                        MessageBox.Show("Не удалось добавить пользователя.", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }

                    connection.Close();
                }
            }
            catch (MySqlException ex)
            {
                MessageBox.Show("Ошибка базы данных: " + ex.Message, "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Ошибка: " + ex.Message, "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
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
                    MessageBox.Show("Не выбран пользователь для редактирования.", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                using (MySqlConnection connection = new MySqlConnection(GlobalValue.GetConnString()))
                {
                    connection.Open();

                    // Проверка на дубликат логина (исключая текущего пользователя)
                    MySqlCommand checkCmd = new MySqlCommand(
                        "SELECT COUNT(*) FROM user WHERE login = @login AND employeeServiceNumber != @tabNum", connection);
                    checkCmd.Parameters.AddWithValue("@login", login.Text);
                    checkCmd.Parameters.AddWithValue("@tabNum", tabNumber.Text);
                    int count = Convert.ToInt32(checkCmd.ExecuteScalar());

                    if (count > 0)
                    {
                        MessageBox.Show("Пользователь с таким логином уже существует.", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return;
                    }

                    // Обновление данных пользователя
                    MySqlCommand cmd;
                    if (!string.IsNullOrWhiteSpace(password.Text))
                    {
                        // Если пароль изменен
                        cmd = new MySqlCommand(
                            "UPDATE user SET login = @login, password = SHA2(@password, 256), roleID = @roleID " +
                            "WHERE employeeServiceNumber = @tabNum", connection);
                        cmd.Parameters.AddWithValue("@password", password.Text);
                    }
                    else
                    {
                        // Если пароль не изменен
                        cmd = new MySqlCommand(
                            "UPDATE user SET login = @login, roleID = @roleID " +
                            "WHERE employeeServiceNumber = @tabNum", connection);
                    }

                    cmd.Parameters.AddWithValue("@login", login.Text);
                    cmd.Parameters.AddWithValue("@roleID", role.SelectedValue);
                    cmd.Parameters.AddWithValue("@tabNum", tabNumber.Text);

                    int rowsAffected = cmd.ExecuteNonQuery();

                    if (rowsAffected > 0)
                    {
                        MessageBox.Show("Данные пользователя успешно обновлены.", "Успех", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        LoadData();
                    }
                    else
                    {
                        MessageBox.Show("Не удалось обновить данные пользователя.", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }

                    connection.Close();
                }
            }
            catch (MySqlException ex)
            {
                MessageBox.Show("Ошибка базы данных: " + ex.Message, "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Ошибка: " + ex.Message, "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void delete_Click(object sender, EventArgs e)
        {
            try
            {
                if (string.IsNullOrEmpty(tabNumber.Text))
                {
                    MessageBox.Show("Не выбран пользователь для удаления.", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                // Подтверждение удаления
                DialogResult result = MessageBox.Show(
                    $"Вы уверены, что хотите удалить пользователя {fio.Text} (логин: {login.Text})?",
                    "Подтверждение удаления",
                    MessageBoxButtons.YesNo,
                    MessageBoxIcon.Question);

                if (result != DialogResult.Yes)
                    return;

                using (MySqlConnection connection = new MySqlConnection(GlobalValue.GetConnString()))
                {
                    connection.Open();

                    // Проверка на связанные записи (например, в таблице действий пользователя)
                    // Здесь нужно добавить проверки для конкретных таблиц вашей БД
                    MySqlCommand checkCmd = new MySqlCommand(
                        "SELECT COUNT(*) FROM user_actions WHERE employeeServiceNumber = @tabNum", connection);
                    checkCmd.Parameters.AddWithValue("@tabNum", tabNumber.Text);
                    int relatedRecords = Convert.ToInt32(checkCmd.ExecuteScalar());

                    if (relatedRecords > 0)
                    {
                        MessageBox.Show("Невозможно удалить пользователя, так как есть связанные записи.", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return;
                    }

                    // Удаление пользователя
                    MySqlCommand cmd = new MySqlCommand(
                        "DELETE FROM user WHERE employeeServiceNumber = @tabNum", connection);
                    cmd.Parameters.AddWithValue("@tabNum", tabNumber.Text);

                    int rowsAffected = cmd.ExecuteNonQuery();

                    if (rowsAffected > 0)
                    {
                        MessageBox.Show("Пользователь успешно удален.", "Успех", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        LoadData();
                        ClearFields();
                    }
                    else
                    {
                        MessageBox.Show("Не удалось удалить пользователя.", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }

                    connection.Close();
                }
            }
            catch (MySqlException ex)
            {
                MessageBox.Show("Ошибка базы данных: " + ex.Message, "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Ошибка: " + ex.Message, "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void dataGridViewUsers_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                dataGridViewUsers.Rows[e.RowIndex].Selected = true;
                string employeeID = dataGridViewUsers.Rows[e.RowIndex].Cells[0].Value.ToString();

                edit.Enabled = true;
                delete.Enabled = true;
                add.Enabled = false;
                isAddingNew = false;

                LoadUserData(employeeID);
            }
            else
            {
                dataGridViewUsers.ClearSelection();
                edit.Enabled = false;
                delete.Enabled = false;
                add.Enabled = true;
            }
        }

        private void LoadUserData(string empID)
        {
            try
            {
                MySqlConnection connection = new MySqlConnection(GlobalValue.GetConnString());
                connection.Open();
                MySqlCommand cmd = new MySqlCommand($"select * from user inner join employee on employee.employeeServiceNumber = user.employeeServiceNumber where user.employeeServiceNumber = '{empID}'", connection);
                MySqlDataReader reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    tabNumber.Text = reader["employeeServiceNumber"].ToString();
                    fio.Text = reader["employeeFIO"].ToString();
                    login.Text = reader["login"].ToString();
                    role.SelectedValue = Convert.ToInt32(reader["roleID"].ToString());
                    password.Text = ""; // Пароль не показываем
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

        private void UsersForm_Load(object sender, EventArgs e)
        {
            LoadData();
            LoadRoles();
            dataGridViewUsers.ClearSelection();
        }

        // Валидация ввода
        private void login_KeyPress(object sender, KeyPressEventArgs e)
        {
            // Разрешаем только латинские буквы, цифры и подчеркивание
            if (!char.IsLetterOrDigit(e.KeyChar) && e.KeyChar != '_' && !char.IsControl(e.KeyChar))
            {
                e.Handled = true;
            }
        }

        private void tabNumber_KeyPress(object sender, KeyPressEventArgs e)
        {
            // Разрешаем только цифры
            if (!char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar))
            {
                e.Handled = true;
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            isSwitch = true;
            EmployeesForm employees = new EmployeesForm();
            this.Hide();
            employees.ShowDialog();

            LoadDate();
        }

        private void LoadDate()
        {
            try
            {
                MySqlConnection con = new MySqlConnection(GlobalValue.GetConnString());
                con.Open();
                MySqlCommand cmd = new MySqlCommand($"select employeeServiceNumber, employeeFIO from employee where employeeServiceNumber = '{empID}'", con);
                MySqlDataReader reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    tabNumber.Text = empID;
                    fio.Text = reader[1].ToString();
                }

                con.Close();
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
    }
}