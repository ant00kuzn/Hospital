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

namespace Hospital
{
    public partial class EmployeesForm : Form
    {
        private DataTable employeeTable; // Таблица для хранения данных о сотрудниках
        private DataView dt; // Представление данных для фильтрации
        private ContextMenuStrip employeeContextMenu; // Контекстное меню

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
        }

        //метод обработки сброса таймера по действию пользователя
        private void ActivityOccured(object sender, EventArgs e)
        {
            ResetInactivityTimer();
        }

        //Тик таймера с проверкой на истечение таймера
        private void Inactive_Tick(object sender, EventArgs e)
        {
            if(DateTime.Now.Second - lastActivity.Second > Convert.ToInt32(ConfigurationManager.AppSettings["timerInactive"]) - 1)
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

        //Контекстное меню
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

        //Удаление пользователя
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

        /// <summary>
        /// Загрузка данных о сотрудниках
        /// </summary>
        private void LoadEmployeeData()
        {
            try
            {
                using (MySqlConnection con = new MySqlConnection(GlobalValue.GetConnString()))
                {
                    con.Open(); // Открытие соединения
                    using (MySqlCommand cmd = new MySqlCommand($"SELECT EmployeeID, EmployeeSurname, CONCAT(EmployeeSurname, ' ', EmployeeName, ' ', EmployeePatronymic) as EmployeeFIO, Post, Phone, Login, Password, role.RoleName, Photo FROM employee INNER JOIN role ON employee.Role = role.RoleID order by EmployeeSurname asc", con))
                    {
                        MySqlDataAdapter bedAdapter = new MySqlDataAdapter(cmd);
                        DataTable bedTable = new DataTable();
                        bedAdapter.Fill(bedTable); // Заполнение таблицы данными

                        dataGridViewEmployees.AutoGenerateColumns = false;
                        dataGridViewEmployees.Columns.Clear(); // Очистка колонок

                        // Добавление колонок в DataGridView
                        DataGridViewTextBoxColumn employeeID = new DataGridViewTextBoxColumn();
                        employeeID.DataPropertyName = "EmployeeID";
                        employeeID.DataPropertyName = "EmployeeID";
                        employeeID.HeaderText = "ID";
                        dataGridViewEmployees.Columns.Add(employeeID);

                        DataGridViewTextBoxColumn employeeSurname = new DataGridViewTextBoxColumn();
                        employeeSurname.DataPropertyName = "EmployeeSurname";
                        employeeSurname.DataPropertyName = "EmployeeSurname";
                        employeeSurname.HeaderText = "EmployeeSurname";
                        dataGridViewEmployees.Columns.Add(employeeSurname);

                        DataGridViewTextBoxColumn employeeFIo = new DataGridViewTextBoxColumn();
                        employeeFIo.DataPropertyName = "EmployeeFIO";
                        employeeFIo.Name = "EmployeeFIO";
                        employeeFIo.HeaderText = "ФИО Сотрудника";
                        dataGridViewEmployees.Columns.Add(employeeFIo);

                        DataGridViewTextBoxColumn post = new DataGridViewTextBoxColumn();
                        post.DataPropertyName = "Post";
                        post.Name = "Post";
                        post.HeaderText = "Должность";
                        dataGridViewEmployees.Columns.Add(post);

                        DataGridViewTextBoxColumn phone = new DataGridViewTextBoxColumn();
                        phone.DataPropertyName = "Phone";
                        phone.Name = "Phone";
                        phone.HeaderText = "Номер телефона";
                        dataGridViewEmployees.Columns.Add(phone);

                        DataGridViewTextBoxColumn Role = new DataGridViewTextBoxColumn();
                        Role.DataPropertyName = "RoleName";
                        Role.Name = "RoleName";
                        Role.HeaderText = "Роль";
                        dataGridViewEmployees.Columns.Add(Role);

                        DataGridViewTextBoxColumn Login = new DataGridViewTextBoxColumn();
                        Login.DataPropertyName = "Login";
                        Login.Name = "Login";
                        Login.HeaderText = "Логин";
                        dataGridViewEmployees.Columns.Add(Login);

                        dt = bedTable.DefaultView; // Установка представления данных
                        dataGridViewEmployees.DataSource = dt; // Привязка данных
                        dataGridViewEmployees.Columns[0].Visible = false; // Скрытие колонки ID
                        dataGridViewEmployees.Columns[1].Visible = false; // Скрытие колонки surname
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

        //Скрытие персональных данных
        private void dataGridViewEmployees_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            if (e.Value != null && e.Value != "")
            {
                string val = e.Value.ToString();

                switch (dataGridViewEmployees.Columns[e.ColumnIndex].Name)
                {
                    case "EmployeeFIO": //фамилия и. о.
                        e.Value = val.Split(' ')[0];
                        e.Value += " " + val.Split(' ')[1][0].ToString() + ".";
                        e.Value += " " + val.Split(' ')[2][0].ToString() + ".";
                        break;
                    case "Phone": //+7952763****
                        e.Value = val.Substring(0, 8) + "****";
                        break;
                    case "Login": //ku****
                        if (val.Length < 3)
                        {
                            e.Value = val.Substring(0, 2) + "****";
                            break;
                        }
                        e.Value = val.Substring(0, 3) + "****";
                        break;
                }
            }
        }

        #region pagination
        //Добавление пагинации
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

        //Очищение пагинации
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

        //Установка активной страницы пагинации
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

        //Предыдущая страница пагинации
        private void buttonPreviousPage_Click(object sender, EventArgs e)
        {
            if (currentNumPage > 0)
            {
                currentNumPage--;
                Pagination();
            }
        }

        //Слпедующая страница пагинации
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

        //Обновление данных, согласно странице пагинации
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

        //Обновление сосоятние кнопок вперед и назад
        private void UpdateButtonState()
        {
            int pageSize = 20;
            int totalRows = dataGridViewEmployees.Rows.Count;
            int totalPages = (int)Math.Ceiling((double)totalRows / pageSize);

            buttonPreviousPage.Enabled = (currentNumPage > 0);
            buttonNextPage.Enabled = (currentNumPage < totalPages - 1);
        }
#endregion

        //Загрузка ролей в comboBox
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

        //Подробный просмотр информации о пользователе / редактирование
        private void DataGridViewEmployees_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                DataRowView row = (DataRowView)dataGridViewEmployees.Rows[e.RowIndex].DataBoundItem;
                if (row != null)
                {
                    OpenEmployeeManagementForm((int)row["EmployeeID"]); // Открытие формы редактирования
                } 
            }
        }

        //Добавление  пользователя
        private void AddEmployeeMenuItem_Click(object sender, EventArgs e)
        {
            OpenEmployeeManagementForm(null); // Открытие формы добавления
        }

        private void OpenEmployeeManagementForm(int? employeeId)
        {
            EmployeeManagment employeeManagmentForm = new EmployeeManagment(employeeId); // Создание формы управления
            if (employeeManagmentForm.ShowDialog() == DialogResult.OK)
            {
                LoadEmployeeData();  // Обновление данных после закрытия формы
            }
        }

        //Строка поиска
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

            string filter = $"EmployeeSurname LIKE '%{searchTextBox.Text}%'"; // Фильтр по фамилии
            if (roleFilterComboBox.SelectedValue != null && roleFilterComboBox.SelectedIndex != 0)
            {
                filter += $" AND RoleName = '{roleFilterComboBox.SelectedValue}'"; // Фильтр по роли
            }
            dt.RowFilter = filter; // Применение фильтра

            dataGridViewEmployees.Refresh(); // Обновление DataGridView

            Pagination();
            currentNumPage = 0;
        }

        //Фильтр по роли
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
                filter += $"EmployeeSurname LIKE '%{searchTextBox.Text}%'"; // Фильтр по фамилии
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
            if (!fclosed)
            {
                inactive.Start();
            }

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

        //Остановка таймера
        private void EmployeesForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            inactive.Stop();
            inactive.Dispose();
        }
    }
}