using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Hospital
{
    public partial class HospitalizationIteractionForm : Form
    {
        public static string patID = "";
        public static string hospID = "";
        public static string wardNumbe = "";
        public static bool isSwitch = false;
        public HospitalizationIteractionForm()
        {
            InitializeComponent();
        }

        private void Form2_FormClosed(object sender, FormClosedEventArgs e)
        {
            if (User.Role == 4)
            {
                if (Application.OpenForms["MainForm"] is MainForm mn)
                    mn.Show();
            }
            else
            {
                if (Application.OpenForms["HospitalizationsForm"] is HospitalizationsForm hp)
                    hp.Show();
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void Form2_Load(object sender, EventArgs e)
        {
            LoadCB();

            data.Text = DateTime.Now.ToShortDateString();
            postFIO.Text = User.Post + ": " + User.Fio;

            if (User.Role == 3)
            {
                buttonWard.Visible = wardNumber.Visible = lechenie.Visible = true;
                buttonPatient.Enabled = false;
                status.Enabled = true;
                this.Size = new Size(955, 900);
                LoadDate();
                LoadData();
                hospNumber.Text = "Номер госпитализации " + hospID;

                // Загрузка текущего статуса пациента
                try
                {
                    MySqlConnection connection = new MySqlConnection(GlobalValue.GetConnString());
                    connection.Open();
                    MySqlCommand cmd = new MySqlCommand($"SELECT statusID FROM patient WHERE patientSnils = '{patID}'", connection);
                    object statusResult = cmd.ExecuteScalar();
                    if (statusResult != null)
                    {
                        status.SelectedValue = statusResult.ToString();

                        // Управление видимостью кнопки buttonVip
                        buttonVip.Visible = statusResult.ToString() == "3" && !string.IsNullOrEmpty(lech.Text);
                    }
                    connection.Close();
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Ошибка загрузки статуса пациента: " + ex.Message, "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            else if (User.Role == 4)
            {
                buttonWard.Visible = wardNumber.Visible = lechenie.Visible = buttonVip.Visible = false;
                buttonPatient.Enabled = true;
                status.SelectedIndex = 0;
                status.Enabled = false;
                this.Size = new Size(955, 645);
                hospNumber.Text = "Номер госпитализации " + (Convert.ToInt32(GetLastNumberHosp()) + 1).ToString("D6");
            }
        }

        private string GetLastNumberHosp()
        {
            try
            {
                MySqlConnection connection = new MySqlConnection(GlobalValue.GetConnString());
                connection.Open();
                MySqlCommand cmd = new MySqlCommand($"select hospitalizationID from hospitalization order by hospitalizationID desc limit 1", connection);
                object result = cmd.ExecuteScalar();
                string res = result != null ? result.ToString() : "0";
                connection.Close();

                return res;
            }
            catch (MySqlException ex)
            {
                MessageBox.Show("Ошибка получения данных: " + ex.Message, "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return "0";
            }
            catch (Exception e)
            {
                MessageBox.Show("Ошибка получения данных: " + e.Message, "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return "0";
            }
        }

        private void LoadCB()
        {
            try
            {
                MySqlConnection connection = new MySqlConnection(GlobalValue.GetConnString());
                connection.Open();
                MySqlCommand cmd = new MySqlCommand($"select departmentID, departmentName from department inner join ward on ward.departmentID = department.departmentID where ward.avaliableNumberOfBeds > 0", connection);
                MySqlDataAdapter adapter = new MySqlDataAdapter(cmd);
                DataTable dt = new DataTable();

                adapter.Fill(dt);

                department.DataSource = dt;
                department.DisplayMember = "departmentName";
                department.ValueMember = "departmentID";

                department.SelectedIndex = -1;

                MySqlCommand cmd1 = new MySqlCommand($"select * from patientstatus", connection);
                MySqlDataAdapter adapter1 = new MySqlDataAdapter(cmd1);
                DataTable dt1 = new DataTable();

                adapter1.Fill(dt1);

                status.DataSource = dt1;
                status.DisplayMember = "statusName";
                status.ValueMember = "statusID";

                status.SelectedIndex = -1;

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

        private void LoadDate()
        {
            try
            {
                MySqlConnection connection = new MySqlConnection(GlobalValue.GetConnString());
                connection.Open();
                MySqlCommand cmd = new MySqlCommand($"select * from hospitalization where hospitalizationID = '{hospID}'", connection);
                MySqlDataReader reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    if (reader[2].ToString() != "")
                        wardNumber.Text = reader[2].ToString();

                    dateOfReceipt.Value = DateTime.Parse(reader[3].ToString());
                    dateOfDischarge.Value = reader[4].ToString() == "" ? dateOfReceipt.Value.AddDays(3) : DateTime.Parse(reader[4].ToString());

                    if (reader[6].ToString() != "")
                        lech.Text = reader[6].ToString();
                    if (reader[7].ToString() != "")
                    {
                        MySqlConnection conn = new MySqlConnection(GlobalValue.GetConnString());
                        conn.Open();
                        MySqlCommand cdm = new MySqlCommand($"select diagnosisName from diagnosis where ICD10Code = '{reader[6].ToString()}'", conn);
                        diagnosis.Text = cdm.ExecuteScalar().ToString();
                        conn.Close();
                    }

                    department.SelectedValue = reader[8].ToString();
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

        private void buttonPatient_Click(object sender, EventArgs e)
        {
            isSwitch = true;
            PatientsForm patients = new PatientsForm();
            this.Hide();
            patients.ShowDialog();

            LoadData();
        }

        private void LoadData()
        {
            try
            {
                MySqlConnection con = new MySqlConnection(GlobalValue.GetConnString());
                con.Open();
                MySqlCommand cmd = new MySqlCommand($"select * from patient where patientSnils = '{patID}'", con);
                MySqlDataReader reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    snils.Text = reader[0].ToString();
                    fio.Text = reader[1].ToString();
                    dateOfBirthday.Value = reader.GetDateTime(4);
                    addres.Text = reader[5].ToString();

                    passportSeria.Text = reader[2].ToString().Split(' ')[0];
                    passportNumber.Text = reader[2].ToString().Split(' ')[1];
                    phone.Text = reader[6].ToString();
                    police.Text = reader[3].ToString();

                    if (User.Role == 3)
                    {
                        WardsForm.genderID = Convert.ToInt32(reader[10].ToString());
                    }
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

        private void buttonWard_Click(object sender, EventArgs e)
        {
            isSwitch = true;
            WardsForm.depName = department.Text;
            WardsForm wards = new WardsForm();
            this.Hide();
            wards.ShowDialog();

            wardNumber.Text = wardNumbe;
        }

        private void buttonOf_Click(object sender, EventArgs e)
        {
            try
            {
                if (string.IsNullOrEmpty(patID))
                {
                    MessageBox.Show("Не выбран пациент!", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                if (department.SelectedIndex == -1)
                {
                    MessageBox.Show("Не выбрано отделение!", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                if (status.SelectedIndex == -1)
                {
                    MessageBox.Show("Не выбран статус пациента!", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                string statusID = status.SelectedValue.ToString();
                string oldStatusID = "0"; // Значение по умолчанию

                // Проверка даты последней выписки для новой госпитализации
                if (User.Role == 4)
                {
                    MySqlConnection checkConnection = new MySqlConnection(GlobalValue.GetConnString());
                    checkConnection.Open();

                    // Получаем текущий статус пациента
                    MySqlCommand getStatusCmd = new MySqlCommand(
                        "SELECT statusID FROM patient WHERE patientSnils = @patientSnils",
                        checkConnection);
                    getStatusCmd.Parameters.AddWithValue("@patientSnils", patID);
                    object statusResult = getStatusCmd.ExecuteScalar();
                    if (statusResult != null)
                    {
                        oldStatusID = statusResult.ToString();
                    }

                    // Проверяем дату последней выписки
                    MySqlCommand checkCmd = new MySqlCommand(
                        "SELECT MAX(dateOfDischarge) FROM hospitalization WHERE patientSnils = @patientSnils",
                        checkConnection);
                    checkCmd.Parameters.AddWithValue("@patientSnils", patID);

                    object lastDischargeDateObj = checkCmd.ExecuteScalar();
                    if (lastDischargeDateObj != null && lastDischargeDateObj != DBNull.Value)
                    {
                        DateTime lastDischargeDate = Convert.ToDateTime(lastDischargeDateObj);
                        if (lastDischargeDate > dateOfReceipt.Value || lastDischargeDate == dateOfReceipt.Value)
                        {
                            MessageBox.Show($"Нельзя оформить госпитализацию: дата поступления ({dateOfReceipt.Value:dd.MM.yyyy}) " +
                                          $"раньше даты последней выписки ({lastDischargeDate:dd.MM.yyyy})",
                                          "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            checkConnection.Close();
                            return;
                        }
                    }
                    checkConnection.Close();
                }

                MySqlConnection connection = new MySqlConnection(GlobalValue.GetConnString());
                connection.Open();

                using (MySqlTransaction transaction = connection.BeginTransaction())
                {
                    try
                    {
                        int rowsAffected = 0;
                        string newHospID = "";

                        if (User.Role == 4) // Регистратор
                        {
                            newHospID = (Convert.ToInt32(GetLastNumberHosp()) + 1).ToString("D6");

                            string query = $@"INSERT INTO hospitalization 
                        (hospitalizationID, patientSnils, wardNumber, dateOfReceipt, dateOfDischarge, 
                         employeeServiceNumber, treatment, diagnosisID, departmentID) 
                        VALUES 
                        ('{newHospID}', '{patID}', NULL, '{dateOfReceipt.Value:yyyy-MM-dd}', NULL, 
                         '{User.Id}', NULL, NULL, '{department.SelectedValue}')";

                            MySqlCommand cmd = new MySqlCommand(query, connection, transaction);
                            rowsAffected = cmd.ExecuteNonQuery();

                            if (rowsAffected > 0)
                            {
                                // Обновляем статус пациента
                                string updatePatientQuery = $"UPDATE patient SET statusID = '{statusID}' WHERE patientSnils = '{patID}'";
                                MySqlCommand updateCmd = new MySqlCommand(updatePatientQuery, connection, transaction);
                                int patientRowsAffected = updateCmd.ExecuteNonQuery();

                                if (patientRowsAffected > 0)
                                {
                                    transaction.Commit();

                                    HospitalizationsForm.isSee = true;

                                    // Открываем только форму HospitalizationsForm
                                    HospitalizationsForm hospForm = new HospitalizationsForm();
                                    hospForm.HighlightNewRecord(newHospID);
                                    this.Hide();
                                    hospForm.ShowDialog();
                                    this.Show();
                                }
                                else
                                {
                                    transaction.Rollback();
                                    MessageBox.Show("Ошибка при обновлении статуса пациента", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                }
                            }
                        }
                        else if (User.Role == 3) // Врач
                        {
                            // Получаем текущий статус пациента для врача
                            MySqlCommand getOldStatusCmd = new MySqlCommand(
                                $"SELECT statusID FROM patient WHERE patientSnils = '{patID}'",
                                connection, transaction);
                            oldStatusID = getOldStatusCmd.ExecuteScalar()?.ToString() ?? "0";

                            string query = $@"UPDATE hospitalization SET
                        wardNumber = '{wardNumber.Text}',
                        dateOfDischarge = '{dateOfDischarge.Value:yyyy-MM-dd}',
                        employeeServiceNumber = '{User.Id}',
                        treatment = {(lech.Text != "" ? "'" + lech.Text + "'" : "null")}, 
                        diagnosisID = {(diagnosis.Text != "" ? "'" + diagnosis.Text + "'" : "null")},
                        departmentID = '{department.SelectedValue}'
                        WHERE hospitalizationID = '{hospID}'";

                            MySqlCommand cmd = new MySqlCommand(query, connection, transaction);
                            rowsAffected = cmd.ExecuteNonQuery();

                            if (rowsAffected > 0)
                            {
                                // Обновляем статус пациента
                                string updatePatientQuery = $"UPDATE patient SET statusID = '{statusID}' WHERE patientSnils = '{patID}'";
                                MySqlCommand updateCmd = new MySqlCommand(updatePatientQuery, connection, transaction);
                                int patientRowsAffected = updateCmd.ExecuteNonQuery();

                                if (patientRowsAffected > 0)
                                {
                                    // Обновляем количество свободных мест в палате, если статус изменился на/с "На лечении" (statusID = 2)
                                    if (!string.IsNullOrEmpty(wardNumber.Text))
                                    {
                                        if (oldStatusID == "2" && statusID != "2")
                                        {
                                            MySqlCommand updateWardCmd = new MySqlCommand(
                                                $"UPDATE ward SET avaliableNumberOfBeds = avaliableNumberOfBeds + 1 " +
                                                $"WHERE wardNumber = '{wardNumber.Text}'", connection, transaction);
                                            updateWardCmd.ExecuteNonQuery();
                                        }
                                        else if (statusID == "2" && oldStatusID != "2")
                                        {
                                            MySqlCommand updateWardCmd = new MySqlCommand(
                                                $"UPDATE ward SET avaliableNumberOfBeds = avaliableNumberOfBeds - 1 " +
                                                $"WHERE wardNumber = '{wardNumber.Text}' AND avaliableNumberOfBeds > 0",
                                                connection, transaction);
                                            updateWardCmd.ExecuteNonQuery();
                                        }
                                    }

                                    transaction.Commit();
                                    buttonVip.Visible = statusID == "3" && !string.IsNullOrEmpty(lech.Text);
                                    MessageBox.Show("Операция выполнена успешно!", "Успех", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                    if (statusID != "3")
                                    {
                                        this.Close();
                                    }
                                }
                                else
                                {
                                    transaction.Rollback();
                                    MessageBox.Show("Ошибка при обновлении статуса пациента", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                }
                            }
                            else
                            {
                                transaction.Rollback();
                                MessageBox.Show("Ошибка при выполнении операции", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        transaction.Rollback();
                        MessageBox.Show("Ошибка: " + ex.Message, "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                    finally
                    {
                        connection.Close();
                    }
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
    }
}