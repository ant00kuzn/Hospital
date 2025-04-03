﻿using System;
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

namespace Hospital
{
    public partial class EmployeesForm : Form
    {
        private DataTable employeeTable; // Таблица для хранения данных о сотрудниках
        private DataView dt; // Представление данных для фильтрации
        private ContextMenuStrip employeeContextMenu; // Контекстное меню


        private int selectedRowIndex = -1;
        private int currentNumPage = 0;

        public EmployeesForm()
        {
            InitializeComponent(); // Инициализация компонентов
        }

        private void InitializeContextMenu()
        {
            employeeContextMenu = new ContextMenuStrip(); // Создание контекстного меню
            ToolStripMenuItem addEmployeeMenuItem = new ToolStripMenuItem("Добавить сотрудника"); // Пункт "Добавить"
            ToolStripMenuItem removeEmployeeMenuItem = new ToolStripMenuItem("Удалить сотрудника"); // Пункт "Добавить"
            employeeContextMenu.Items.Add(addEmployeeMenuItem);
            employeeContextMenu.Items.Add(removeEmployeeMenuItem);
            addEmployeeMenuItem.Click += AddEmployeeMenuItem_Click; // Обработчик добавления
            removeEmployeeMenuItem.Click += RemoveEmployeeMenuItem_Click;

            dataGridViewEmployees.ContextMenuStrip = employeeContextMenu;
        }

        private void RemoveEmployeeMenuItem_Click(object sender, EventArgs e)
        {
            if (selectedRowIndex != -1)
                DeleteEmployee(selectedRowIndex);
            else
            {
                MessageBox.Show("Не выбрана запись для удаления.", "Внимание!", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
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

                        DataGridViewTextBoxColumn Login = new DataGridViewTextBoxColumn();
                        Login.DataPropertyName = "Login";
                        Login.HeaderText = "Логин";
                        dataGridViewEmployees.Columns.Add(Login);

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
                return;
            }
        }

        void Pagination()
        {
            dataGridViewEmployees.CurrentCell = null;
            foreach (DataGridViewRow r in dataGridViewEmployees.Rows)
                r.Visible = false;

            // Узнаём сколько страниц будет
            int pageSize = 20; // Записей на странице
            int totalRows = dataGridViewEmployees.Rows.Count;
            int totalPages = (int)Math.Ceiling((double)totalRows / pageSize); // Используем Math.Ceiling для округления вверх

            // Обновляем UI (удаляем старые элементы пагинации)
            ClearPaginationUI();

            // Создаем LinkLabel для страниц
            LinkLabel[] ll = new LinkLabel[totalPages];
            int x = buttonPreviousPage.Location.X + 38;
            int y = 593, step = 15;

            for (int i = 0; i < totalPages; ++i)
            {
                ll[i] = new LinkLabel();
                ll[i].Text = Convert.ToString(i + 1);
                ll[i].Name = "page" + i;
                ll[i].ForeColor = Color.Black;
                ll[i].AutoSize = true;
                ll[i].Location = new Point(x, y);
                ll[i].Click += new EventHandler(LinkLabel_Click);
                this.Controls.Add(ll[i]);

                x += step;
            }

            // Устанавливаем активную страницу (подчеркивание)
            SetActivePage(currentNumPage, ll);

            labRowCount.Text = "Общее количество записей: " + totalRows;

            buttonNextPage.Location = totalPages > 0 ? new Point(ll[totalPages - 1].Location.X + 25, 593) : new Point(x, y); //Обработка если нет данных
            UpdateButtonState();
            DisplayData(currentNumPage, pageSize);
        }

        private void ClearPaginationUI()
        {
            // Удаляем LinkLabel служащие для пагинации
            List<Control> controlsToRemove = new List<Control>();
            foreach (Control ctrl in this.Controls)
            {
                if (ctrl is LinkLabel && ctrl.Name.StartsWith("page"))
                {
                    controlsToRemove.Add(ctrl);
                }
            }
            foreach (Control ctrl in controlsToRemove)
            {
                this.Controls.Remove(ctrl);
            }
        }

        private void SetActivePage(int pageNum, LinkLabel[] ll)
        {
            // Сбрасываем подчеркивание у всех LinkLabel
            foreach (var ctrl in this.Controls)
            {
                if (ctrl is LinkLabel)
                {
                    (ctrl as LinkLabel).LinkBehavior = LinkBehavior.AlwaysUnderline;
                }
            }

            // Убираем подчеркивание у активной страницы, если она существует
            if (ll != null && ll.Length > pageNum && pageNum >= 0)
            {
                ll[pageNum].LinkBehavior = LinkBehavior.NeverUnderline;
            }
        }

        // выбор страницы пагинации
        // те строки которые нам не нужны на выбраной странице - скрываем
        private void LinkLabel_Click(object sender, EventArgs e)
        {
            dataGridViewEmployees.ClearSelection();
            dataGridViewEmployees.CurrentCell = null;

            // узнаём какая страница выбрана
            LinkLabel l = sender as LinkLabel;
            if (l != null)
            {
                currentNumPage = Convert.ToInt32(l.Text) - 1;
                Pagination(); //Перерисовываем интерфейс
            }
        }

        private void buttonPreviousPage_Click(object sender, EventArgs e)
        {
            if (currentNumPage > 0)
            {
                currentNumPage--;
                Pagination();
            }
        }

        private void buttonNextPage_Click(object sender, EventArgs e)
        {
            int pageSize = 20;
            int totalRows = dataGridViewEmployees.Rows.Count;
            int totalPages = (int)Math.Ceiling((double)totalRows / pageSize);

            if (currentNumPage < totalPages - 1)
            {
                currentNumPage++;
                Pagination();
            }
        }

        private void DisplayData(int pageNum, int pageSize)
        {
            int start = pageNum * pageSize;
            int end = Math.Min(start + pageSize, dataGridViewEmployees.Rows.Count); //Используем Math.Min на случай, если записей меньше, чем pageSize

            // Сначала показываем все строки, чтобы избежать проблем при переключении страниц
            foreach (DataGridViewRow row in dataGridViewEmployees.Rows)
            {
                row.Visible = true;
            }

            // Скрываем строки, которые не входят в текущую страницу
            for (int i = 0; i < dataGridViewEmployees.Rows.Count; i++)
            {
                dataGridViewEmployees.Rows[i].Visible = (i >= start && i < end);
            }

            labRowCount.Text = "Показаны записи: " + (start + 1) + " - " + end + " из " + dataGridViewEmployees.Rows.Count; //Для красоты добавил +1 к start

            dataGridViewEmployees.Refresh();
            UpdateButtonState();
        }
        private void UpdateButtonState()
        {
            int pageSize = 20;
            int totalRows = dataGridViewEmployees.Rows.Count;
            int totalPages = (int)Math.Ceiling((double)totalRows / pageSize);

            buttonPreviousPage.Enabled = (currentNumPage > 0);
            buttonNextPage.Enabled = (currentNumPage < totalPages - 1);
        }

        private void LoadRoles()
        {
            roleFilterComboBox.DataSource = null;
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
                    allRolesRow["RoleName"] = "Фильтрация по роли сотрудника";
                    roleTable.Rows.InsertAt(allRolesRow, 0);

                    roleFilterComboBox.DataSource = roleTable; // Привязка данных к ComboBox
                    roleFilterComboBox.DisplayMember = "RoleName"; // Настройка отображаемого поля
                    roleFilterComboBox.ValueMember = "RoleName"; // Настройка поля значения
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Ошибка загрузки comboBox: " + ex.Message);
                    return;
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
            if (searchTextBox.Text == "Начните ввод для поиска по фио сотрудника...")
                return;

            if (searchTextBox.Text.Length == 0)
                return;
            // Сначала показываем все строки, чтобы избежать проблем при переключении страниц
            foreach (DataGridViewRow row in dataGridViewEmployees.Rows)
            {
                row.Visible = true;
            }

            string filter = $"EmployeeFIO LIKE '%{searchTextBox.Text}%'"; // Фильтр по ФИО
            if (roleFilterComboBox.SelectedValue != null && roleFilterComboBox.SelectedIndex != 0)
            {
                filter += $" AND RoleName = '{roleFilterComboBox.SelectedValue}'"; // Фильтр по роли
            }
            dt.RowFilter = filter; // Применение фильтра

            dataGridViewEmployees.Refresh(); // Обновление DataGridView

            Pagination();
            currentNumPage = 0;
        }

        private void RoleFilterComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            // Сначала показываем все строки, чтобы избежать проблем при переключении страниц
            foreach (DataGridViewRow row in dataGridViewEmployees.Rows)
            {
                row.Visible = true;
            }

            string filter = "";
            if (roleFilterComboBox.SelectedValue != null && roleFilterComboBox.SelectedIndex != 0)
            {
                filter = $"RoleName = '{roleFilterComboBox.SelectedValue}'"; // Фильтр по роли
            }
            if (!string.IsNullOrEmpty(searchTextBox.Text) && searchTextBox.Text != "Начните ввод для поиска по фио сотрудника...")
            {
                if (!string.IsNullOrEmpty(filter))
                {
                    filter += " AND ";
                }
                filter += $"EmployeeFIO LIKE '%{searchTextBox.Text}%'"; // Фильтр по фамилии
            }
            dt.RowFilter = filter; // Применение фильтра

            dataGridViewEmployees.Refresh(); // Обновление DataGridView

            Pagination();
            currentNumPage = 0;
        }

        private void DataGridViewEmployees_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                dataGridViewEmployees.Rows[e.RowIndex].Selected = true; // Выбор строки
                selectedRowIndex = e.RowIndex;
            }
        }

        private void DeleteEmployee(int rowIndex)
        {
            int employeeID = (int)dataGridViewEmployees.Rows[rowIndex].Cells[0].Value; // Получение ID сотрудника

            using (MySqlConnection connection = new MySqlConnection(GlobalValue.GetConnString()))
            {
                try
                {
                    connection.Open(); // Открытие соединения
                    string query = "DELETE FROM employee WHERE EmployeeID = @EmployeeID";
                    MySqlCommand command = new MySqlCommand(query, connection);
                    command.Parameters.AddWithValue("@EmployeeID", employeeID);
                    command.ExecuteNonQuery(); // Удаление сотрудника

                    MessageBox.Show("Учетная запись сотрудника удалена.", "Успешно", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    LoadEmployeeData();
                    Pagination();

                    return;
                }
                catch (MySqlException ex)
                {
                    if (ex.Number == 1451)
                    {
                        MessageBox.Show("Ошибка удаления:\n Данная учетная запись имеет связанные записи. Её удаление повлечет за собой утрату многих записей. Удаление невозможно.");
                        return;
                    }
                    else
                    {
                        MessageBox.Show("Ошибка удаления: " + ex.Message);
                        return;
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Ошибка удаления: " + ex.Message);
                    return;
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
            InitializeContextMenu(); // Настройка контекстного меню
            LoadEmployeeData(); // Загрузка данных при загрузке формы
            LoadRoles(); // Загрузка ролей
            Pagination();

            dataGridViewEmployees.Rows[0].Selected = false;

            searchTextBox.Text = "Начните ввод для поиска по фио сотрудника...";
            searchTextBox.ForeColor = Color.LightGray;
        }

        private void searchTextBox_Enter(object sender, EventArgs e)
        {
            if (searchTextBox.Text == "Начните ввод для поиска по фио сотрудника...")
            {
                searchTextBox.Text = "";
                searchTextBox.ForeColor = Color.Black;
            }
        }

        private void searchTextBox_Leave(object sender, EventArgs e)
        {
            if (searchTextBox.Text.Length == 0)
            {
                searchTextBox.Text = "Начните ввод для поиска по фио сотрудника...";
                searchTextBox.ForeColor = Color.LightGray;
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}