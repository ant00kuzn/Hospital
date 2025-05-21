using MySql.Data.MySqlClient;
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

namespace Hospital.Справочники
{
    public partial class Benefit : Form
    {
        private int id = -1;

        private int prevRowIndex = -1;

        private string prevBenefitName = "";
        private int spaceCount = 0; // Счетчик пробелов
        private bool lastCharWasSpace = false; // Флаг последнего символа - пробел

        public Benefit()
        {
            InitializeComponent();
            LoadData("asc");

            textBox1.LostFocus += textBox1_LostFocus;
            textBox1.GotFocus += textBox1_GotFocus;
        }

        private void LoadData(string a)
        {
            try
            {
                MySqlConnection connection = new MySqlConnection(GlobalValue.GetConnString());
                connection.Open();
                MySqlCommand cmd = new MySqlCommand($"select benefitID, benefitName as 'Льгота' from benefit order by benefitName {a}", connection);
                MySqlDataAdapter adapter = new MySqlDataAdapter(cmd);
                DataTable table = new DataTable();

                adapter.Fill(table);

                dataGridViewBenefit.DataSource = table;
                dataGridViewBenefit.Columns[0].Visible = false;

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

        private void dataGridViewBenefit_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                if (prevRowIndex != e.RowIndex)
                {
                    dataGridViewBenefit.Rows[e.RowIndex].Selected = true;
                    id = Convert.ToInt32(dataGridViewBenefit.Rows[e.RowIndex].Cells[0].Value.ToString());
                    textBox1.Text = dataGridViewBenefit.Rows[e.RowIndex].Cells[1].Value.ToString();
                    prevBenefitName = textBox1.Text;
                    prevRowIndex = e.RowIndex;
                    textBox1.Focus();
                }
                else
                {
                    dataGridViewBenefit.Rows[e.RowIndex].Selected = false;
                    id = -1;
                    textBox1.Text = "";
                    prevRowIndex = -1;
                    dataGridViewBenefit.Focus();
                }
            }
            else
            {
                id = -1;
            }
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            // Считаем пробелы заново при изменении текста (вставка, удаление)
            spaceCount = textBox1.Text.Count(c => c == ' ');

            // 4. Начало с заглавной буквы
            if (textBox1.Text.Length > 0)
            {
                if (!char.IsUpper(textBox1.Text[0]) && char.IsLetter(textBox1.Text[0]))
                {
                    textBox1.Text = char.ToUpper(textBox1.Text[0]) + textBox1.Text.Substring(1);
                    textBox1.SelectionStart = textBox1.Text.Length; // Возвращаем курсор в конец
                }
            }

            //Проверка на пробелы подряд, если вставили текст
            if (textBox1.Text.Contains("  "))
            {
                textBox1.Text = textBox1.Text.Replace("  ", " ");
                textBox1.SelectionStart = textBox1.Text.Length; // Возвращаем курсор в конец
            }

            if (id != -1)
            {
                button1.Enabled = true;
                button2.Enabled = false;
                button3.Enabled = true;
            }
            else
            {
                button1.Enabled = false;
                button2.Enabled = true;
                button3.Enabled = false;
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void benefit_FormClosed(object sender, FormClosedEventArgs e)
        {
            if (Application.OpenForms["GuidesForm"] is GuidesForm guides)
                guides.Show();
        }

        //add
        private void button2_Click(object sender, EventArgs e)
        {
            if (textBox1.Text != "")
            {
                if (textBox1.Text.Length > 4)
                {
                    try
                    {
                        string benefitName = textBox1.Text.Trim();

                        MySqlConnection connection = new MySqlConnection(GlobalValue.GetConnString());
                        connection.Open();

                        MySqlCommand checkCmd = new MySqlCommand($"SELECT COUNT(*) FROM benefit WHERE benefitName = '{benefitName}'", connection);

                        if ((long)checkCmd.ExecuteScalar() > 0)
                        {
                            MessageBox.Show("Льгота с таким наименованием уже существует.", "Предупреждение", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                            textBox1.Focus();
                            connection.Close();
                            return;
                        }

                        MySqlCommand cmd = new MySqlCommand($"insert into benefit (benefitName) value ('{benefitName}')", connection);
                        cmd.ExecuteNonQuery();
                        MessageBox.Show("Запись добавлена!", "Успех", MessageBoxButtons.OK, MessageBoxIcon.Information);

                        LoadData("desc");
                        textBox1.Text = "";
                        dataGridViewBenefit.Focus();
                        connection.Close();
                    }
                    catch (MySqlException ex)
                    {
                        MessageBox.Show("Ошибка добавления данных: " + ex.Message, "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }
                    catch (Exception ee)
                    {
                        MessageBox.Show("Ошибка добавления данных: " + ee.Message, "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }
                }
                else
                {
                    MessageBox.Show("Наименование льготы ыдолжно быть не короче 5 символов.", "Предупреждение", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    textBox1.Focus();
                    return;
                }
            }
            else
            {
                MessageBox.Show("Заполните обязательное поле с наименованием льготы.", "Предупреждение", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                textBox1.Focus();
                return;
            }
        }
        //change
        private void button1_Click(object sender, EventArgs e)
        {
            if (textBox1.Text != "" && id != -1)
            {
                if (textBox1.Text.Length > 4 && prevBenefitName != textBox1.Text)
                {
                    try
                    {
                        string benefitName = textBox1.Text.Trim();

                        MySqlConnection connection = new MySqlConnection(GlobalValue.GetConnString());
                        connection.Open();

                        MySqlCommand checkCmd = new MySqlCommand($"SELECT COUNT(*) FROM benefit WHERE benefitName = '{benefitName}'", connection);

                        if ((long)checkCmd.ExecuteScalar() > 0)
                        {
                            MessageBox.Show("Льгота с таким наименованием уже существует.", "Предупреждение", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                            textBox1.Focus();
                            connection.Close();
                            return;
                        }

                        MySqlCommand cmd = new MySqlCommand($"update benefit set benefitName = '{benefitName}' where benefitID = {id}", connection);
                        cmd.ExecuteNonQuery();
                        MessageBox.Show("Запись изменена!", "Успех", MessageBoxButtons.OK, MessageBoxIcon.Information);

                        LoadData("asc");
                        textBox1.Text = "";
                        dataGridViewBenefit.Focus();
                        dataGridViewBenefit.Rows[prevRowIndex].Selected = true;
                        connection.Close();
                    }
                    catch (MySqlException ex)
                    {
                        MessageBox.Show("Ошибка изменения данных:" + ex.Message, "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }
                    catch (Exception ee)
                    {
                        MessageBox.Show("Ошибка изменения данных:" + ee.Message, "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }
                }
                else
                {
                    MessageBox.Show("Наименование льготы должно быть изменено и не короче 5 символов.", "Предупреждение", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    textBox1.Focus();
                    return;
                }
            }
            else
            {
                MessageBox.Show("Заполните обязательное поле с наименованием льготы.", "Предупреждение", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                textBox1.Focus();
                return;
            }
        }
        //del
        private void button3_Click(object sender, EventArgs e)
        {
            if (id != -1)
            {
                if (DialogResult.Yes == MessageBox.Show("Вы уверены, что хотите удалить запись?", "Подтверждение", MessageBoxButtons.YesNo, MessageBoxIcon.Stop))
                {
                    try
                    {
                        MySqlConnection connection = new MySqlConnection(GlobalValue.GetConnString());
                        connection.Open();
                        MySqlCommand cmd = new MySqlCommand($"delete from benefit where benefitID = {id}", connection);
                        cmd.ExecuteNonQuery();
                        MessageBox.Show("Запись удалена!", "Успех", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        LoadData("asc");
                        textBox1.Text = "";
                        dataGridViewBenefit.Focus();
                        connection.Close();
                    }
                    catch (MySqlException ex)
                    {
                        if (ex.Number == 1451)
                        {
                            MessageBox.Show("Удаление невозможно. Данная запись учавствует в других записях и её удаление может привести к потере данных.", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            return;
                        }
                        else
                        {
                            MessageBox.Show("Ошибка удаления данных:" + ex.Message, "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            return;
                        }
                    }
                    catch (Exception ee)
                    {
                        MessageBox.Show("Ошибка удаления данных:" + ee.Message, "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }
                }
            }
        }

        private void textBox1_KeyPress(object sender, KeyPressEventArgs e)
        {
            // 1. Только русские буквы и пробелы
            if (!Regex.IsMatch(e.KeyChar.ToString(), @"^[а-яА-Я\s\b]+$")) // \b для Backspace
            {
                e.Handled = true; // Отмена ввода
                return;
            }

            // 2. Не более 3 пробелов (с учетом уже введенных)
            if (e.KeyChar == ' ')
            {
                if (spaceCount >= 3)
                {
                    e.Handled = true;
                    return;
                }

                // 3. Нельзя вводить пробелы подряд
                if (lastCharWasSpace)
                {
                    e.Handled = true;
                    return;
                }

                spaceCount++; // Увеличиваем счетчик пробелов
                lastCharWasSpace = true;
            }
            else
            {
                lastCharWasSpace = false;
            }
        }

        private void textBox1_LostFocus(object sender, EventArgs e)
        {
            // Удаляем пробелы в начале и конце при потере фокуса
            textBox1.Text = textBox1.Text.Trim();

            // Пересчитываем пробелы еще раз, после Trim()
            spaceCount = textBox1.Text.Count(c => c == ' ');
        }

        private void textBox1_GotFocus(object sender, EventArgs e)
        {
            // Сбрасываем счетчики при получении фокуса
            spaceCount = textBox1.Text.Count(c => c == ' ');
            lastCharWasSpace = (textBox1.Text.Length > 0 && textBox1.Text[textBox1.Text.Length - 1] == ' ');
        }
    }
}
