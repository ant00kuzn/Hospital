using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MySql.Data.MySqlClient;
using System.Text.RegularExpressions;

namespace Hospital.Справочники
{
    public partial class Department : Form
    {
        private string employeeServiceNumber = "";
        private int depID = -1;
        private int prevRowIndex = -1;
        private string prevDepartmentName = "";

        public Department()
        {
            InitializeComponent();
            LoadData();
            LoadCB();

            // Input validation setup
            textBox1.TextChanged += TextBox1_TextChanged;
            textBox1.KeyPress += TextBox1_KeyPress;
        }

        private void LoadCB()
        {
            try
            {
                MySqlConnection connection = new MySqlConnection(GlobalValue.GetConnString());
                connection.Open();
                MySqlCommand cmd = new MySqlCommand("SELECT employeeServiceNumber, employeeFIO FROM employee", connection);
                MySqlDataAdapter adapter = new MySqlDataAdapter(cmd);
                DataTable dt = new DataTable();

                adapter.Fill(dt);

                comboBox1.DataSource = dt;
                comboBox1.DisplayMember = "employeeFIO";
                comboBox1.ValueMember = "employeeServiceNumber";

                connection.Close();
            }
            catch (MySqlException ex)
            {
                MessageBox.Show("Ошибка получения данных: " + ex.Message, "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            catch (Exception e)
            {
                MessageBox.Show("Ошибка получения данных: " + e.Message, "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void LoadData()
        {
            try
            {
                MySqlConnection connection = new MySqlConnection(GlobalValue.GetConnString());
                connection.Open();
                MySqlCommand cmd = new MySqlCommand(@"SELECT d.departmentID, d.departmentName, 
                                                    e.employeeServiceNumber, e.employeeFIO 
                                                    FROM department d
                                                    LEFT JOIN employee e ON d.headEmployeeID = e.employeeServiceNumber
                                                    ORDER BY d.departmentID DESC", connection);
                MySqlDataAdapter adapter = new MySqlDataAdapter(cmd);
                DataTable dt = new DataTable();

                adapter.Fill(dt);

                dataGridViewDepartment.DataSource = dt;
                dataGridViewDepartment.Columns[0].Visible = false;
                dataGridViewDepartment.Columns[2].Visible = false;

                labelRecordCount.Text = $"Всего записей: {dt.Rows.Count}";

                connection.Close();
            }
            catch (MySqlException ex)
            {
                MessageBox.Show("Ошибка получения данных: " + ex.Message, "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            catch (Exception e)
            {
                MessageBox.Show("Ошибка получения данных: " + e.Message, "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void dataGridViewDepartment_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                if (prevRowIndex != e.RowIndex)
                {
                    dataGridViewDepartment.Rows[e.RowIndex].Selected = true;
                    depID = Convert.ToInt32(dataGridViewDepartment.Rows[e.RowIndex].Cells[0].Value);
                    textBox1.Text = dataGridViewDepartment.Rows[e.RowIndex].Cells[1].Value.ToString();
                    prevDepartmentName = textBox1.Text;

                    if (dataGridViewDepartment.Rows[e.RowIndex].Cells[2].Value != DBNull.Value)
                    {
                        employeeServiceNumber = dataGridViewDepartment.Rows[e.RowIndex].Cells[2].Value.ToString();
                        comboBox1.SelectedValue = employeeServiceNumber;
                    }
                    else
                    {
                        comboBox1.SelectedIndex = -1;
                    }

                    prevRowIndex = e.RowIndex;

                    // Enable appropriate buttons
                    button1.Enabled = true;
                    button2.Enabled = false;
                    button3.Enabled = true;
                }
                else
                {
                    dataGridViewDepartment.Rows[e.RowIndex].Selected = false;
                    depID = -1;
                    textBox1.Text = "";
                    comboBox1.SelectedIndex = -1;
                    prevRowIndex = -1;
                    dataGridViewDepartment.Focus();

                    // Enable appropriate buttons
                    button1.Enabled = false;
                    button2.Enabled = true;
                    button3.Enabled = false;
                }
            }
        }

        private void TextBox1_TextChanged(object sender, EventArgs e)
        {
            // Capitalize first letter
            if (textBox1.Text.Length > 0 && char.IsLetter(textBox1.Text[0]) && !char.IsUpper(textBox1.Text[0]))
            {
                textBox1.Text = char.ToUpper(textBox1.Text[0]) + textBox1.Text.Substring(1);
                textBox1.SelectionStart = textBox1.Text.Length;
            }
        }

        private void TextBox1_KeyPress(object sender, KeyPressEventArgs e)
        {
            // Allow only Russian letters and spaces
            if (!Regex.IsMatch(e.KeyChar.ToString(), @"^[а-яА-Я\s\b]+$") && e.KeyChar != (char)Keys.Back)
            {
                e.Handled = true;
                return;
            }
        }

        // Add button
        private void button2_Click(object sender, EventArgs e)
        {
            if (ValidateInput())
            {
                try
                {
                    string departmentName = textBox1.Text.Trim();
                    string headEmployeeID = comboBox1.SelectedValue?.ToString() ?? "NULL";

                    MySqlConnection connection = new MySqlConnection(GlobalValue.GetConnString());
                    connection.Open();

                    // Check for duplicate
                    MySqlCommand checkCmd = new MySqlCommand(
                        $"SELECT COUNT(*) FROM department WHERE departmentName = '{departmentName}'",
                        connection);

                    if ((long)checkCmd.ExecuteScalar() > 0)
                    {
                        MessageBox.Show("Отделение с таким названием уже существует.", "Ошибка",
                                      MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return;
                    }

                    // Insert new department
                    MySqlCommand cmd = new MySqlCommand(
                        $"INSERT INTO department (departmentName, headEmployeeID) " +
                        $"VALUES ('{departmentName}', {(headEmployeeID == "NULL" ? "NULL" : "'" + headEmployeeID + "'")})",
                        connection);

                    cmd.ExecuteNonQuery();
                    MessageBox.Show("Отделение успешно добавлено!", "Успех",
                                  MessageBoxButtons.OK, MessageBoxIcon.Information);

                    // Refresh data
                    LoadData();
                    textBox1.Text = "";
                    comboBox1.SelectedIndex = -1;
                    dataGridViewDepartment.Focus();

                    connection.Close();
                }
                catch (MySqlException ex)
                {
                    MessageBox.Show("Ошибка добавления данных: " + ex.Message, "Ошибка",
                                  MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Ошибка: " + ex.Message, "Ошибка",
                                  MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        // Edit button
        private void button1_Click(object sender, EventArgs e)
        {
            if (depID == -1)
            {
                MessageBox.Show("Не выбрано отделение для редактирования.", "Ошибка",
                              MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (ValidateInput())
            {
                try
                {
                    string departmentName = textBox1.Text.Trim();
                    string headEmployeeID = comboBox1.SelectedValue?.ToString() ?? "NULL";

                    // Check if name was actually changed
                    if (departmentName == prevDepartmentName &&
                        headEmployeeID == (employeeServiceNumber ?? "NULL"))
                    {
                        MessageBox.Show("Не было сделано изменений.", "Информация",
                                      MessageBoxButtons.OK, MessageBoxIcon.Information);
                        return;
                    }

                    MySqlConnection connection = new MySqlConnection(GlobalValue.GetConnString());
                    connection.Open();

                    // Check for duplicate (excluding current record)
                    MySqlCommand checkCmd = new MySqlCommand(
                        $"SELECT COUNT(*) FROM department WHERE departmentName = '{departmentName}' AND departmentID != {depID}",
                        connection);

                    if ((long)checkCmd.ExecuteScalar() > 0)
                    {
                        MessageBox.Show("Отделение с таким названием уже существует.", "Ошибка",
                                      MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return;
                    }

                    // Update department
                    MySqlCommand cmd = new MySqlCommand(
                        $"UPDATE department SET departmentName = '{departmentName}', " +
                        $"headEmployeeID = {(headEmployeeID == "NULL" ? "NULL" : "'" + headEmployeeID + "'")} " +
                        $"WHERE departmentID = {depID}",
                        connection);

                    cmd.ExecuteNonQuery();
                    MessageBox.Show("Отделение успешно обновлено!", "Успех",
                                  MessageBoxButtons.OK, MessageBoxIcon.Information);

                    // Refresh data
                    LoadData();
                    textBox1.Text = "";
                    comboBox1.SelectedIndex = -1;
                    dataGridViewDepartment.Focus();

                    connection.Close();
                }
                catch (MySqlException ex)
                {
                    MessageBox.Show("Ошибка обновления данных: " + ex.Message, "Ошибка",
                                  MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Ошибка: " + ex.Message, "Ошибка",
                                  MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        // Delete button
        private void button3_Click(object sender, EventArgs e)
        {
            if (depID == -1)
            {
                MessageBox.Show("Не выбрано отделение для удаления.", "Ошибка",
                              MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (DialogResult.Yes == MessageBox.Show("Вы уверены, что хотите удалить это отделение?", "Подтверждение",
                                                  MessageBoxButtons.YesNo, MessageBoxIcon.Question))
            {
                try
                {
                    MySqlConnection connection = new MySqlConnection(GlobalValue.GetConnString());
                    connection.Open();

                    // Check for related records
                    MySqlCommand checkCmd = new MySqlCommand(
                        $"SELECT COUNT(*) FROM employee WHERE departmentID = {depID}",
                        connection);

                    if ((long)checkCmd.ExecuteScalar() > 0)
                    {
                        MessageBox.Show("Невозможно удалить отделение, так как в нем есть сотрудники.", "Ошибка",
                                      MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }

                    // Delete department
                    MySqlCommand cmd = new MySqlCommand(
                        $"DELETE FROM department WHERE departmentID = {depID}",
                        connection);

                    int affectedRows = cmd.ExecuteNonQuery();

                    if (affectedRows > 0)
                    {
                        MessageBox.Show("Отделение успешно удалено!", "Успех",
                                      MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }

                    // Refresh data
                    LoadData();
                    textBox1.Text = "";
                    comboBox1.SelectedIndex = -1;
                    dataGridViewDepartment.Focus();

                    connection.Close();
                }
                catch (MySqlException ex)
                {
                    if (ex.Number == 1451) 
                    {
                        MessageBox.Show("Невозможно удалить отделение, так как оно связано с другими записями.", "Ошибка",
                                      MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }
                    else
                    {
                        MessageBox.Show("Ошибка удаления данных: " + ex.Message, "Ошибка",
                                      MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Ошибка: " + ex.Message, "Ошибка",
                                  MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
            }
        }

        private bool ValidateInput()
        {
            if (string.IsNullOrWhiteSpace(textBox1.Text))
            {
                MessageBox.Show("Название отделения не может быть пустым.", "Ошибка",
                              MessageBoxButtons.OK, MessageBoxIcon.Warning);
                textBox1.Focus();
                return false;
            }

            if (textBox1.Text.Length < 3)
            {
                MessageBox.Show("Название отделения должно содержать не менее 3 символов.", "Ошибка",
                              MessageBoxButtons.OK, MessageBoxIcon.Warning);
                textBox1.Focus();
                return false;
            }

            if (!Regex.IsMatch(textBox1.Text, @"^[А-Яа-яЁё\s]+$"))
            {
                MessageBox.Show("Название отделения должно содержать только русские буквы.", "Ошибка",
                              MessageBoxButtons.OK, MessageBoxIcon.Warning);
                textBox1.Focus();
                return false;
            }

            return true;
        }

        private void button4_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void Department_FormClosed(object sender, FormClosedEventArgs e)
        {
            if (Application.OpenForms["GuidesForm"] is GuidesForm guides)
                guides.Show();
        }
    }
}