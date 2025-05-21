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

namespace Hospital
{
    public partial class WardsForm : Form
    {
        public static string depName = "";
        public static int genderID = -1;
        private int selectedWardId = -1;

        public WardsForm()
        {
            InitializeComponent();
            LoadDep();
            LoadSpec();
            LoadData();
        }

        private void LoadDep()
        {
            MySqlConnection connection = new MySqlConnection(GlobalValue.GetConnString());
            connection.Open();
            MySqlCommand cmd = new MySqlCommand("select * from department", connection);
            MySqlDataAdapter my = new MySqlDataAdapter(cmd);
            DataTable data = new DataTable();

            my.Fill(data);

            comboBox1.DataSource = data;
            comboBox1.DisplayMember = "departmentName";
            comboBox1.ValueMember = "departmentID";
            comboBox1.SelectedValue = -1;

            connection.Close();
        }

        private void LoadSpec()
        {
            MySqlConnection connection = new MySqlConnection(GlobalValue.GetConnString());
            connection.Open();
            MySqlCommand cmd = new MySqlCommand("select * from gender", connection);
            MySqlDataAdapter my = new MySqlDataAdapter(cmd);
            DataTable data = new DataTable();

            my.Fill(data);

            comboBox2.DataSource = data;
            comboBox2.DisplayMember = "genderName";
            comboBox2.ValueMember = "genderID";
            comboBox2.SelectedValue = -1;

            connection.Close();
        }

        private void LoadData()
        {
            MySqlConnection connection = new MySqlConnection(GlobalValue.GetConnString());
            connection.Open();
            string query = "";
            if (HospitalizationIteractionForm.isSwitch)
            {
                query = $"select dep.departmentID, dep.departmentName as 'Наименование отделения', wardNumber as 'Номер палаты', " +
                "totalNumberOfBeds as 'Всего коек', avaliableNumberOfBeds as 'Количество свободных коек', gen.genderID, gen.genderName as 'Пол / Специализация' " +
                "from ward" +
                " inner join department dep on dep.departmentID = ward.departmentID " +
                $"inner join gender gen on gen.genderID = ward.genderID where dep.departmentName = '{depName}' and avaliableNumberOfBeds > 0 and gen.genderID = '{genderID}' order by wardNumber ASC";
            }
            else
            {
                query = "select ward.wardNumber, dep.departmentID, dep.departmentName as 'Наименование отделения', wardNumber as 'Номер палаты', " +
                "totalNumberOfBeds as 'Всего коек', avaliableNumberOfBeds as 'Количество свободных коек', gen.genderID, gen.genderName as 'Пол / Специализация' " +
                "from ward" +
                " inner join department dep on dep.departmentID = ward.departmentID " +
                "inner join gender gen on gen.genderID = ward.genderID order by wardNumber ASC";
            }

            MySqlCommand cmd = new MySqlCommand(query, connection);
            MySqlDataAdapter mySqlDataAdapter = new MySqlDataAdapter(cmd);
            DataTable data = new DataTable();

            mySqlDataAdapter.Fill(data);

            dataGridViewWards.DataSource = data;

            // Скрываем ID-столбцы
            foreach (DataGridViewColumn column in dataGridViewWards.Columns)
            {
                if (column.Name.Contains("ID") || column.Index == 0)
                {
                    column.Visible = false;
                }
            }

            labelRowCount.Text = "Количество записей: " + data.Rows.Count;

            connection.Close();
        }

        private void dataGridViewWards_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                // Если строка уже выделена, снимаем выделение
                if (dataGridViewWards.Rows[e.RowIndex].Selected)
                {
                    dataGridViewWards.ClearSelection();
                    ClearFields();
                    button2.Enabled = true;
                    button1.Enabled = false;
                    selectedWardId = -1;
                    return;
                }

                dataGridViewWards.ClearSelection();
                dataGridViewWards.Rows[e.RowIndex].Selected = true;

                selectedWardId = Convert.ToInt32(dataGridViewWards.Rows[e.RowIndex].Cells["wardID"].Value ??
                                                 dataGridViewWards.Rows[e.RowIndex].Cells[0].Value);
                comboBox1.SelectedValue = Convert.ToInt32(dataGridViewWards.Rows[e.RowIndex].Cells["departmentID"].Value ??
                                                         dataGridViewWards.Rows[e.RowIndex].Cells[1].Value);
                comboBox2.SelectedValue = Convert.ToInt32(dataGridViewWards.Rows[e.RowIndex].Cells["genderID"].Value ??
                                         dataGridViewWards.Rows[e.RowIndex].Cells[6].Value);
                textBox1.Text = dataGridViewWards.Rows[e.RowIndex].Cells["Номер палаты"].Value?.ToString() ??
                               dataGridViewWards.Rows[e.RowIndex].Cells[3].Value.ToString();
                textBox2.Text = dataGridViewWards.Rows[e.RowIndex].Cells["Всего коек"].Value?.ToString() ??
                               dataGridViewWards.Rows[e.RowIndex].Cells[4].Value.ToString();
                textBox3.Text = dataGridViewWards.Rows[e.RowIndex].Cells["Количество свободных коек"].Value?.ToString() ??
                               dataGridViewWards.Rows[e.RowIndex].Cells[5].Value.ToString();

                button2.Enabled = false;
                button1.Enabled = true;

                if (HospitalizationIteractionForm.isSwitch)
                {
                    HospitalizationIteractionForm.wardNumbe = dataGridViewWards.Rows[e.RowIndex].Cells["Номер палаты"].Value?.ToString() ??
                                                             dataGridViewWards.Rows[e.RowIndex].Cells[3].Value.ToString();
                    this.Close();
                }
            }
        }

        private void ClearFields()
        {
            comboBox1.SelectedValue = -1;
            comboBox2.SelectedValue = -1;
            textBox1.Text = "";
            textBox2.Text = "";
            textBox3.Text = "";
        }

        private void WardsForm_Load(object sender, EventArgs e)
        {
            // Отключаем автоматическое выделение первой строки
            dataGridViewWards.ClearSelection();
            button1.Enabled = false;
        }

        private void WardsForm_FormClosed(object sender, FormClosedEventArgs e)
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

        private void button2_Click(object sender, EventArgs e)
        {
            // Добавление новой палаты
            if (string.IsNullOrWhiteSpace(textBox1.Text) || string.IsNullOrWhiteSpace(textBox2.Text) ||
                comboBox1.SelectedValue == null || comboBox2.SelectedValue == null)
            {
                MessageBox.Show("Заполните все поля!", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            try
            {
                MySqlConnection connection = new MySqlConnection(GlobalValue.GetConnString());
                connection.Open();

                // Проверка на дубликат номера палаты в отделении
                string checkQuery = "SELECT COUNT(*) FROM ward WHERE wardNumber = @wardNum AND departmentID = @depId";
                MySqlCommand checkCmd = new MySqlCommand(checkQuery, connection);
                checkCmd.Parameters.AddWithValue("@wardNum", textBox1.Text);
                checkCmd.Parameters.AddWithValue("@depId", comboBox1.SelectedValue);

                int existingCount = Convert.ToInt32(checkCmd.ExecuteScalar());
                if (existingCount > 0)
                {
                    MessageBox.Show("Палата с таким номером уже существует в этом отделении!", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    connection.Close();
                    return;
                }

                string query = "INSERT INTO ward (departmentID, wardNumber, totalNumberOfBeds, avaliableNumberOfBeds, genderID) " +
                              "VALUES (@depId, @wardNum, @totalBeds, @availBeds, @genderId)";

                MySqlCommand cmd = new MySqlCommand(query, connection);
                cmd.Parameters.AddWithValue("@depId", comboBox1.SelectedValue);
                cmd.Parameters.AddWithValue("@wardNum", textBox1.Text);
                cmd.Parameters.AddWithValue("@totalBeds", Convert.ToInt32(textBox2.Text));
                cmd.Parameters.AddWithValue("@availBeds", Convert.ToInt32(textBox2.Text));
                cmd.Parameters.AddWithValue("@genderId", comboBox2.SelectedValue);

                cmd.ExecuteNonQuery();
                connection.Close();

                MessageBox.Show("Палата успешно добавлена!", "Успех", MessageBoxButtons.OK, MessageBoxIcon.Information);
                LoadData();
                ClearFields();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка при добавлении палаты: {ex.Message}", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            // Редактирование палаты
            if (selectedWardId == -1 || string.IsNullOrWhiteSpace(textBox1.Text) || string.IsNullOrWhiteSpace(textBox2.Text) ||
                string.IsNullOrWhiteSpace(textBox3.Text) || comboBox1.SelectedValue == null || comboBox2.SelectedValue == null)
            {
                MessageBox.Show("Выберите палату для редактирования и заполните все поля!", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            try
            {
                MySqlConnection connection = new MySqlConnection(GlobalValue.GetConnString());
                connection.Open();

                // Проверка на дубликат номера палаты в отделении (исключая текущую палату)
                string checkQuery = "SELECT COUNT(*) FROM ward WHERE wardNumber = @wardNum AND departmentID = @depId AND wardID != @wardId";
                MySqlCommand checkCmd = new MySqlCommand(checkQuery, connection);
                checkCmd.Parameters.AddWithValue("@wardNum", textBox1.Text);
                checkCmd.Parameters.AddWithValue("@depId", comboBox1.SelectedValue);
                checkCmd.Parameters.AddWithValue("@wardId", selectedWardId);

                int existingCount = Convert.ToInt32(checkCmd.ExecuteScalar());
                if (existingCount > 0)
                {
                    MessageBox.Show("Палата с таким номером уже существует в этом отделении!", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    connection.Close();
                    return;
                }

                string query = "UPDATE ward SET departmentID = @depId, wardNumber = @wardNum, " +
                              "totalNumberOfBeds = @totalBeds, avaliableNumberOfBeds = @availBeds, " +
                              "genderID = @genderId WHERE wardID = @wardId";

                MySqlCommand cmd = new MySqlCommand(query, connection);
                cmd.Parameters.AddWithValue("@depId", comboBox1.SelectedValue);
                cmd.Parameters.AddWithValue("@wardNum", textBox1.Text);
                cmd.Parameters.AddWithValue("@totalBeds", Convert.ToInt32(textBox2.Text));
                cmd.Parameters.AddWithValue("@availBeds", Convert.ToInt32(textBox3.Text));
                cmd.Parameters.AddWithValue("@genderId", comboBox2.SelectedValue);
                cmd.Parameters.AddWithValue("@wardId", selectedWardId);

                cmd.ExecuteNonQuery();
                connection.Close();

                MessageBox.Show("Палата успешно обновлена!", "Успех", MessageBoxButtons.OK, MessageBoxIcon.Information);
                LoadData();
                ClearFields();
                button1.Enabled = false;
                button2.Enabled = true;
                selectedWardId = -1;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка при обновлении палаты: {ex.Message}", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}