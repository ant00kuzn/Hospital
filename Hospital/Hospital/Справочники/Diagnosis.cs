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
    public partial class Diagnosis : Form
    {
        private string diagnosisCodes = "";

        private int prevRowIndex = -1;

        private string prevDiagnosisName = "";
        public Diagnosis()
        {
            InitializeComponent();
            LoadData();

            this.ControlBox = false;
        }

        private void LoadData()
        {
            try
            {
                MySqlConnection connection = new MySqlConnection(GlobalValue.GetConnString());
                connection.Open();
                MySqlCommand cmd = new MySqlCommand(@"select ICD10Code as 'Код диагноза', diagnosisDescription as 'Диагноз' from diagnosis order by ICD10Code desc", connection);
                MySqlDataAdapter adapter = new MySqlDataAdapter(cmd);
                DataTable table = new DataTable();

                adapter.Fill(table);

                dataGridViewDiagnosis.DataSource = table;

                labelRecordCount.Text = $"Всего записей: {table.Rows.Count}";

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

        private void dataGridViewDiagnosis_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                if (prevRowIndex != e.RowIndex)
                {
                    dataGridViewDiagnosis.Rows[e.RowIndex].Selected = true;
                    diagnosisCodes = dataGridViewDiagnosis.Rows[e.RowIndex].Cells[0].Value.ToString();
                    textBox1.Text = diagnosisCodes;
                    richTextBox1.Text = dataGridViewDiagnosis.Rows[e.RowIndex].Cells[1].Value.ToString();
                    prevDiagnosisName = textBox1.Text;
                    prevRowIndex = e.RowIndex;
                    textBox1.Focus();
                }
                else
                {
                    dataGridViewDiagnosis.Rows[e.RowIndex].Selected = false;
                    diagnosisCodes = "";
                    textBox1.Text = "";
                    prevRowIndex = -1;
                    dataGridViewDiagnosis.Focus();
                }
            }
            else
            {
                diagnosisCodes = "";
            }
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            /*if (textBox1.Text.Length > 0)
            {
                if (!char.IsUpper(textBox1.Text[0]) && char.IsLetter(textBox1.Text[0]))
                {
                    textBox1.Text = char.ToUpper(textBox1.Text[0]) + textBox1.Text.Substring(1);
                    textBox1.SelectionStart = textBox1.Text.Length; // Возвращаем курсор в конец
                }
            }*/

            if (diagnosisCodes != "")
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

        private void diagnosis_FormClosed(object sender, FormClosedEventArgs e)
        {
            if (Application.OpenForms["GuidesForm"] is GuidesForm guides)
                guides.Show();
        }

        //add
        private void button2_Click(object sender, EventArgs e)
        {
            if (textBox1.Text != "" && richTextBox1.Text != "")
            {
                if (textBox1.Text.Length >= 3 && textBox1.Text.Length <= 6 && richTextBox1.Text.Length >= 5)
                {
                    try
                    {
                        string diagnosisCode = textBox1.Text.Trim();
                        string diagnosisName = richTextBox1.Text.Trim();

                        MySqlConnection connection = new MySqlConnection(GlobalValue.GetConnString());
                        connection.Open();

                        MySqlCommand checkCmd = new MySqlCommand($"SELECT COUNT(*) FROM diagnosis WHERE ICD10Code = '{diagnosisCode}' OR diagnosisDescription= '{diagnosisName}'", connection);

                        if ((long)checkCmd.ExecuteScalar() > 0)
                        {
                            MessageBox.Show("Диагноз с таким наименованием или кодом уже существует.", "Предупреждение", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                            textBox1.Focus();
                            connection.Close();
                            return;
                        }

                        MySqlCommand cmd = new MySqlCommand($"insert into diagnosis (ICD10Code, diagnosisDescription) value ('{diagnosisCode}', '{diagnosisName}')", connection);
                        cmd.ExecuteNonQuery();
                        MessageBox.Show("Запись добавлена!", "Успех", MessageBoxButtons.OK, MessageBoxIcon.Information);

                        LoadData();
                        textBox1.Text = "";
                        dataGridViewDiagnosis.Focus();
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
                    MessageBox.Show("Ошибка добавления: код диагноза должен быть от трех до 6 символов, а наименование от 5 символов.", "Предупреждение", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    textBox1.Focus();
                    return;
                }
            }
            else
            {
                MessageBox.Show("Пожалуйста, заполните оба текстовых поля для добавления.", "Предупреждение", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                textBox1.Focus();
                return;
            }
        }
        //change
        private void button1_Click(object sender, EventArgs e)
        {
            if (textBox1.Text != "" && diagnosisCodes != "" && richTextBox1.Text != "")
            {
                if (textBox1.Text.Length > 4 && prevDiagnosisName != textBox1.Text)
                {
                    try
                    {
                     /*   string benefitName = textBox1.Text.Trim();

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

                        MySqlCommand cmd = new MySqlCommand($"update benefit set benefitName = '{benefitName}' where benefitID = {diagnosisCodes}", connection);
                        cmd.ExecuteNonQuery();
                        MessageBox.Show("Запись изменена!", "Успех", MessageBoxButtons.OK, MessageBoxIcon.Information);

                        LoadData();
                        textBox1.Text = "";
                        dataGridViewDiagnosis.Focus();
                        connection.Close();*/
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
            if (diagnosisCodes != "")
            {
                if (DialogResult.Yes == MessageBox.Show("Вы уверены, что хотите удалить запись?", "Подтверждение", MessageBoxButtons.YesNo, MessageBoxIcon.Stop))
                {
                    try
                    {
                        MySqlConnection connection = new MySqlConnection(GlobalValue.GetConnString());
                        connection.Open();
                        MySqlCommand cmd = new MySqlCommand($"delete from diagnosis where ICD10Code = {diagnosisCodes}", connection);
                        cmd.ExecuteNonQuery();
                        MessageBox.Show("Запись удалена!", "Успех", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        LoadData();
                        textBox1.Text = "";
                        dataGridViewDiagnosis.Focus();
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
            if (!Regex.IsMatch(e.KeyChar.ToString(), @"^[А-Я\s\b]+$")) // \b для Backspace
            {
                e.Handled = true; // Отмена ввода
                return;
            }
        }

        private void richTextBox1_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!Regex.IsMatch(e.KeyChar.ToString(), @"^[а-яА-Я\s\b]+$")) // \b для Backspace
            {
                e.Handled = true; // Отмена ввода
                return;
            }
        }

        private void Diagnosis_Load(object sender, EventArgs e)
        {

        }
    }
}
