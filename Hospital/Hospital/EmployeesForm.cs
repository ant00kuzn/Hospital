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

namespace Hospital
{
    public partial class EmployeesForm : Form
    {
        private DataTable employeeTable; // Таблица для хранения данных о сотрудниках
        private DataView dt; // Представление данных для фильтрации
        private ContextMenuStrip employeeContextMenu; // Контекстное меню

        public EmployeesForm()
        {
            InitializeComponent(); // Инициализация компонентов
            InitializeContextMenu(); // Настройка контекстного меню
            LoadEmployeeData(); // Загрузка данных о сотрудниках
            LoadRoles(); // Загрузка ролей
        }

        private void InitializeContextMenu()
        {
            employeeContextMenu = new ContextMenuStrip(); // Создание контекстного меню
            ToolStripMenuItem addEmployeeMenuItem = new ToolStripMenuItem("Добавить сотрудника"); // Пункт "Добавить"
            employeeContextMenu.Items.Add(addEmployeeMenuItem);
            addEmployeeMenuItem.Click += AddEmployeeMenuItem_Click; // Обработчик добавления

            dataGridViewEmployees.ContextMenuStrip = employeeContextMenu;
        }

        private void LoadEmployeeData()
        {
            try
            {
                using (MySqlConnection con = new MySqlConnection(GlobalValue.GetConnString()))
                {
                    con.Open(); // Открытие соединения
                    using (MySqlCommand cmd = new MySqlCommand($"SELECT EmployeeID, CONCAT(EmployeeSurname, ' ', EmployeeName, ' ', EmployeePatronymic) as EmployeeFIO, Post, Login, Password, role.RoleName, Photo FROM employee INNER JOIN role ON employee.Role = role.RoleID order by EmployeeSurname asc", con))
                    {
                        MySqlDataAdapter bedAdapter = new MySqlDataAdapter(cmd);
                        DataTable bedTable = new DataTable();
                        bedAdapter.Fill(bedTable); // Заполнение таблицы данными

                        dataGridViewEmployees.AutoGenerateColumns = false;
                        dataGridViewEmployees.Columns.Clear(); // Очистка колонок

                        // Добавление колонок в DataGridView
                        DataGridViewTextBoxColumn employeeID = new DataGridViewTextBoxColumn();
                        employeeID.DataPropertyName = "EmployeeID";
                        employeeID.HeaderText = "ID";
                        dataGridViewEmployees.Columns.Add(employeeID);

                        DataGridViewTextBoxColumn employeeFIo = new DataGridViewTextBoxColumn();
                        employeeFIo.DataPropertyName = "EmployeeFIO";
                        employeeFIo.HeaderText = "ФИО Сотрудника";
                        dataGridViewEmployees.Columns.Add(employeeFIo);

                        DataGridViewTextBoxColumn post = new DataGridViewTextBoxColumn();
                        post.DataPropertyName = "Post";
                        post.HeaderText = "Должность";
                        dataGridViewEmployees.Columns.Add(post);

                        DataGridViewTextBoxColumn Role = new DataGridViewTextBoxColumn();
                        Role.DataPropertyName = "RoleName";
                        Role.HeaderText = "Роль";
                        dataGridViewEmployees.Columns.Add(Role);

                        DataGridViewButtonColumn deleteButtonColumn = new DataGridViewButtonColumn(); // Колонка удаления
                        deleteButtonColumn.Name = "DeleteColumn";
                        deleteButtonColumn.Text = "Удалить";
                        deleteButtonColumn.HeaderText = "Удаление";
                        deleteButtonColumn.UseColumnTextForButtonValue = true;
                        dataGridViewEmployees.Columns.Add(deleteButtonColumn);

                        dt = bedTable.DefaultView; // Установка представления данных
                        dataGridViewEmployees.DataSource = dt; // Привязка данных
                        dataGridViewEmployees.Columns[0].Visible = false; // Скрытие колонки ID
                        dataGridViewEmployees.Refresh(); // Обновление DataGridView
                    }
                    con.Close(); // Закрытие соединения
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"{ex.Message}", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void LoadRoles()
        {
            using (MySqlConnection connection = new MySqlConnection(GlobalValue.GetConnString()))
            {
                try
                {
                    connection.Open(); // Открытие соединения
                    string query = "SELECT RoleID, RoleName FROM role";
                    MySqlDataAdapter adapter = new MySqlDataAdapter(query, connection);
                    DataTable roleTable = new DataTable();
                    adapter.Fill(roleTable); // Заполнение таблицы ролей

                    DataRow allRolesRow = roleTable.NewRow(); // Добавление строки "Все роли"
                    allRolesRow["RoleID"] = 0;
                    allRolesRow["RoleName"] = "Все роли";
                    roleTable.Rows.InsertAt(allRolesRow, 0);

                    roleFilterComboBox.DataSource = roleTable; // Привязка данных к ComboBox
                    roleFilterComboBox.DisplayMember = "RoleName"; // Настройка отображаемого поля
                    roleFilterComboBox.ValueMember = "RoleName"; // Настройка поля значения
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Ошибка загрузки comboBox: " + ex.Message);
                }
            }
        }

        private void DataGridViewEmployees_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                EditEmployee(e.RowIndex); // Редактирование сотрудника при двойном клике
            }
        }

        private void AddEmployeeMenuItem_Click(object sender, EventArgs e)
        {
            OpenEmployeeManagementForm(null); // Открытие формы добавления
        }

        private void EditEmployee(int rowIndex)
        {
            DataRowView row = (DataRowView)dataGridViewEmployees.Rows[rowIndex].DataBoundItem;
            if (row != null)
            {
                OpenEmployeeManagementForm((int)row["EmployeeID"]); // Открытие формы редактирования
            }
        }

        private void OpenEmployeeManagementForm(int? employeeId)
        {
            EmployeeManagment employeeManagmentForm = new EmployeeManagment(employeeId); // Создание формы управления
            if (employeeManagmentForm.ShowDialog() == DialogResult.OK)
            {
                LoadEmployeeData();  // Обновление данных после закрытия формы
            }
        }

        private void SearchTextBox_TextChanged(object sender, EventArgs e)
        {
            string filter = $"EmployeeFIO LIKE '%{searchTextBox.Text}%'"; // Фильтр по ФИО
            if (roleFilterComboBox.SelectedValue != null && roleFilterComboBox.SelectedIndex != 0)
            {
                filter += $" AND RoleName = '{roleFilterComboBox.SelectedValue}'"; // Фильтр по роли
            }
            dt.RowFilter = filter; // Применение фильтра
        }

        private void RoleFilterComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            string filter = "";
            if (roleFilterComboBox.SelectedValue != null && roleFilterComboBox.SelectedIndex != 0)
            {
                filter = $"RoleName = '{roleFilterComboBox.SelectedValue}'"; // Фильтр по роли
            }
            if (!string.IsNullOrEmpty(searchTextBox.Text))
            {
                if (!string.IsNullOrEmpty(filter))
                {
                    filter += " AND ";
                }
                filter += $"EmployeeSurname LIKE '%{searchTextBox.Text}%'"; // Фильтр по фамилии
            }
            dt.RowFilter = filter; // Применение фильтра
        }

        private void DataGridViewEmployees_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                dataGridViewEmployees.Rows[e.RowIndex].Selected = true; // Выбор строки
            }

            if (e.ColumnIndex == dataGridViewEmployees.Columns["DeleteColumn"].Index && e.RowIndex >= 0)
            {
                if (MessageBox.Show("Вы уверены, что хотите удалить выбранную учетную запись?", "Подтверждение удаления", MessageBoxButtons.YesNo) == DialogResult.Yes)
                {
                    DeleteEmployee(e.RowIndex); // Удаление сотрудника
                }
            }
        }

        private void DeleteEmployee(int rowIndex)
        {
            DataRowView rowToDelete = (DataRowView)dataGridViewEmployees.Rows[rowIndex].DataBoundItem;
            if (rowToDelete != null)
            {
                int employeeID = (int)rowToDelete["EmployeeID"]; // Получение ID сотрудника

                using (MySqlConnection connection = new MySqlConnection(GlobalValue.GetConnString()))
                {
                    try
                    {
                        connection.Open(); // Открытие соединения
                        string query = "DELETE FROM employee WHERE EmployeeID = @EmployeeID";
                        MySqlCommand command = new MySqlCommand(query, connection);
                        command.Parameters.AddWithValue("@EmployeeID", employeeID);
                        command.ExecuteNonQuery(); // Удаление сотрудника

                        employeeTable.Rows.Remove(rowToDelete.Row); // Удаление строки из таблицы
                        employeeTable.AcceptChanges();
                        MessageBox.Show("Учетная запись сотрудника удалена.", "Успешно", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Ошибка удаления: " + ex.Message);
                    }
                }
            }
        }

        private void EmployeesForm_FormClosed(object sender, FormClosedEventArgs e)
        {
            if (Application.OpenForms["MainForm"] is MainForm mn)
                mn.Show(); // Показ главной формы при закрытии
        }

        private void EmployeesForm_Load(object sender, EventArgs e)
        {
            LoadEmployeeData(); // Загрузка данных при загрузке формы
        }
    }
}