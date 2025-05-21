// Импорт необходимых пространств имен
using Microsoft.Office.Interop.Word;
using MySql.Data.MySqlClient;
using MySqlX.XDevAPI.Relational;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;
using Table = Microsoft.Office.Interop.Word.Table;
using word = Microsoft.Office.Interop.Word;

namespace Hospital
{
    // Класс формы для работы с госпитализациями
    public partial class HospitalizationsForm : Form
    {
        // Поля для хранения данных о госпитализациях
        private DataTable hospitalizationTable;
        private DataView hospitalizationView;

        // Флаги для отслеживания состояния формы
        private bool isAdding = false;
        private bool isDischarge = false;

        // Индекс выбранной строки
        private int selectedRowIndex = -1;

        private string patID = "";
        private string hospID = "";

        public static bool isSee = false;

        // Конструктор формы
        public HospitalizationsForm()
        {
            InitializeComponent();
            LoadHospitalizationData(); // Загрузка данных о госпитализациях
            ApplyUserRolePermissions(); // Применяем разрешения на основе роли пользователя 
        }

        // Загрузка данных о госпитализациях в DataGridView
        private void LoadHospitalizationData()
        {
            dataGridViewHospitalizations.DataSource = null;
            dataGridViewHospitalizations.Columns.Clear();
            dataGridViewHospitalizations.Rows.Clear();

            try
            {
                using (MySqlConnection connection = new MySqlConnection(GlobalValue.GetConnString()))
                {
                    connection.Open();

                    // SQL-запрос для получения данных о госпитализациях
                    string query = @"SELECT h.HospitalizationID, p.patientFIO, " +
                        "p.patientSnils, g.genderName, TIMESTAMPDIFF(YEAR, patientBirthday, CURDATE()) AS patientAge, d.departmentName, w.wardNumber, e.employeeFIO, " +
                        "h.dateOfReceipt, h.dateOfDischarge, s.statusName " +
                        "FROM hospitalization h " +
                        "INNER JOIN patient p ON h.patientSnils = p.patientSnils " +
                        "left JOIN ward w ON w.wardNumber = h.wardNumber " +
                        "inner JOIN department d ON h.departmentID = d.departmentID " +
                        "Inner join gender g on p.genderID = g.genderID " +
                        "Inner join patientstatus s on s.statusID = p.statusID " +
                        "Inner join employee e on e.employeeServiceNumber = h.employeeServiceNumber ";

                    // Добавляем условие для роли 3
                    if (User.Role == 3)
                    {
                        query += $" WHERE d.departmentName like '%{User.Post}%' "; // Предполагаем, что User.Post содержит название отделения
                    }


                    query += " ORDER BY s.statusName DESC";


                    using (MySqlCommand command = new MySqlCommand(query, connection))
                    {
                        MySqlDataAdapter adapter = new MySqlDataAdapter(command);
                        hospitalizationTable = new DataTable();
                        adapter.Fill(hospitalizationTable);

                        hospitalizationView = hospitalizationTable.DefaultView;

                        dataGridViewHospitalizations.AutoGenerateColumns = false;
                        dataGridViewHospitalizations.Columns.Clear();

                        // Добавление колонок в DataGridView
                        AddDataGridViewColumn("HospitalizationID", "Номер записи", "HospitalizationID");
                        AddDataGridViewColumn("PatientFIO", "ФИО пациента", "PatientFIO");
                        AddDataGridViewColumn("patientSnils", "СНИЛС", "patientSnils");
                        AddDataGridViewColumn("genderName", "Пол", "genderName");
                        AddDataGridViewColumn("patientAge", "Возраст", "patientAge");
                        AddDataGridViewColumn("departmentName", "Отделение", "DepartmentName");
                        AddDataGridViewColumn("wardNumber", "Номер палаты", "wardNumber");
                        AddDataGridViewColumn("employeeFIO", "ФИО врача", "EmployeeFIO");
                        AddDataGridViewColumn("dateOfReceipt", "Дата Поступления", "DateOfReceipt");
                        AddDataGridViewColumn("dateOfDischarge", "Дата Выписки", "DateOfDischarge");
                        AddDataGridViewColumn("statusName", "Статус", "statusName");

                        dataGridViewHospitalizations.DataSource = hospitalizationView;

                        dataGridViewHospitalizations.Columns[0].Width = 100;
                        dataGridViewHospitalizations.Columns[2].Width = 150;
                        dataGridViewHospitalizations.Columns[1].Width = 300;
                        dataGridViewHospitalizations.Columns[7].Width = 300;
                        dataGridViewHospitalizations.Columns[8].Width = 87;
                        dataGridViewHospitalizations.Columns[9].Width = 85;
                        dataGridViewHospitalizations.Columns[10].Width = 85;
                        dataGridViewHospitalizations.Columns[3].Width = 50;
                        dataGridViewHospitalizations.Columns[4].Width = 85;
                        dataGridViewHospitalizations.Columns[6].Width = 80;

                        labelRecordCount.Text = $"Количество записей: {hospitalizationTable.Rows.Count}";
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка при загрузке данных: {ex.Message}", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        // Метод для добавления колонки в DataGridView
        private void AddDataGridViewColumn(string name, string headerText, string dataPropertyName)
        {
            DataGridViewTextBoxColumn column = new DataGridViewTextBoxColumn();
            column.Name = name;
            column.HeaderText = headerText;
            column.DataPropertyName = dataPropertyName;
            dataGridViewHospitalizations.Columns.Add(column);
        }

        // Обработчик изменения текста в поле поиска
        private void textBoxSearch_TextChanged(object sender, EventArgs e)
        {

        }

        // Обработчик изменения выбранного отделения
        private void ComboBoxDepartment_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        // Обновление сортировки данных
        private void UpdateSorting()
        {
            string sortingString = comboBoxSorting.SelectedItem.ToString();

            string sortColumn;
            if (sortingString == "Дата поступления (по возрастанию)")
            {
                sortColumn = "DateOfReceipt ASC";
            }
            else if (sortingString == "Дата поступления (по убыванию)")
            {
                sortColumn = "DateOfReceipt DESC";
            }
            else if (sortingString == "Возраст (по возрастанию)")
            {
                sortColumn = "patientAge ASC";
            }
            else
            {
                sortColumn = "patientAge DESC";
            }

            hospitalizationView.Sort = sortColumn; // Применение сортировки
        }

        // Обработчик изменения способа сортировки
        private void ComboBoxSorting_SelectedIndexChanged(object sender, EventArgs e)
        {
            UpdateSorting(); // Обновление сортировки
        }

        // Генерация направления на госпитализацию в Word
        // Класс для хранения информации о направлении
        private class ReferralData
        {
            public string InsurancePolicy { get; set; } // Полис ОМС
            public string PatientFIO { get; set; } // ФИО пациента
            public DateTime PatientBirthday { get; set; } // Дата рождения
            public string PatientAddress { get; set; } // Адрес проживания
            public string EmployeeFIO { get; set; } // ФИО врача
            public string EmployeePost { get; set; } // Должность врача
            public string Diagnosis { get; set; } // Диагноз (код по МКБ-10 и название)
            public string DepartmentName { get; set; } // Название отделения
            public string PatientSnils { get; set; } // СНИЛС пациента
            public string PhoneNumber { get; set; } // Телефон пациента
        }

        // Получение данных для направления на госпитализацию
        private ReferralData GetReferralData(string hospitalizationId)
        {
            ReferralData data = new ReferralData();

            string connectionString = GlobalValue.GetConnString();
            // SQL-запрос для получения данных о госпитализации
            string query = @"SELECT
                    p.patientMedicalPolice,
                    p.patientFIO,
                    p.patientBirthday,
                    p.patientAddress,
                    p.patientPhoneNumber,
                    p.patientSnils,
                    e.employeeFIO,
                    e.employeePost,
                    d.departmentName,
                    diag.ICD10Code,
                    diag.diagnosisName
                FROM
                    hospitalization h
                INNER JOIN
                    patient p ON h.patientSnils = p.patientSnils
                INNER JOIN
                    employee e ON h.employeeServiceNumber = e.employeeServiceNumber
                INNER JOIN
                    department d ON h.departmentID = d.departmentID
                INNER JOIN
                    diagnosis diag ON diag.ICD10Code = h.diagnosisID
                WHERE h.hospitalizationID = @HospitalizationID;";

            try
            {
                using (MySqlConnection connection = new MySqlConnection(connectionString))
                {
                    connection.Open();
                    using (MySqlCommand command = new MySqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@HospitalizationID", hospitalizationId);

                        using (MySqlDataReader reader = command.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                // Заполнение объекта данными из БД
                                data.InsurancePolicy = reader["patientMedicalPolice"].ToString();
                                data.PatientFIO = reader["patientFIO"].ToString();
                                data.PatientBirthday = Convert.ToDateTime(reader["patientBirthday"]);
                                data.PatientAddress = reader["patientAddress"].ToString();
                                data.PhoneNumber = reader["patientPhoneNumber"].ToString();
                                data.PatientSnils = reader["patientSnils"].ToString();
                                data.DepartmentName = reader["departmentName"].ToString();
                                data.EmployeeFIO = reader["employeeFIO"].ToString();
                                data.EmployeePost = reader["employeePost"].ToString();
                                data.Diagnosis = $"{reader["ICD10Code"]} - {reader["diagnosisName"]}";

                                return data;
                            }
                            else
                            {
                                return null; // Данные для направления не найдены
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка при получении данных: {ex.Message}", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return null;
            }
        }

        // Генерация направления на госпитализацию в Word
        private void GenerateRefferalForHospitalization(string hospitalizationId)
        {
            try
            {
                // Создаем экземпляр приложения Word и документ
                word.Application app = new word.Application();
                word.Document doc = app.Documents.Add();

                // Получение данных для направления
                ReferralData referralData = GetReferralData(hospitalizationId);

                if (referralData == null)
                {
                    MessageBox.Show("Данные для направления не найдены.", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    app.Quit();
                    return;
                }

                // Форматирование документа
                object missing = Type.Missing;

                // 1. Шапка направления
                Paragraph ministryHeader = doc.Content.Paragraphs.Add(ref missing);
                ministryHeader.Range.Text = "МИНИСТЕРСТВО ЗДРАВООХРАНЕНИЯ РОССИЙСКОЙ ФЕДЕРАЦИИ";
                ministryHeader.Range.Font.Size = 10;
                ministryHeader.Alignment = WdParagraphAlignment.wdAlignParagraphCenter;
                ministryHeader.Range.InsertParagraphAfter();

                // 2. Название формы
                Paragraph formTitle = doc.Content.Paragraphs.Add(ref missing);
                formTitle.Range.Text = "Форма N 057/у-04";
                formTitle.Range.Font.Size = 10;
                formTitle.Alignment = WdParagraphAlignment.wdAlignParagraphRight;
                formTitle.Range.InsertParagraphAfter();

                Paragraph approvalInfo = doc.Content.Paragraphs.Add(ref missing);
                approvalInfo.Range.Text = "Утверждена приказом Минздравсоцразвития России\nот 22.11.2004 г. N 255";
                approvalInfo.Range.Font.Size = 8;
                approvalInfo.Alignment = WdParagraphAlignment.wdAlignParagraphRight;
                approvalInfo.Range.InsertParagraphAfter();

                // 3. Основной заголовок
                Paragraph mainTitle = doc.Content.Paragraphs.Add(ref missing);
                mainTitle.Range.Text = "НАПРАВЛЕНИЕ НА ГОСПИТАЛИЗАЦИЮ";
                mainTitle.Range.Font.Size = 14;
                mainTitle.Range.Font.Bold = 1;
                mainTitle.Alignment = WdParagraphAlignment.wdAlignParagraphCenter;
                mainTitle.Range.InsertParagraphAfter();

                // 5. Данные пациента
                Paragraph patientData = doc.Content.Paragraphs.Add(ref missing);
                patientData.Range.Text = $"1. Фамилия, имя, отчество: {referralData.PatientFIO}\n" +
                                        $"2. Полис ОМС: {referralData.InsurancePolicy}\n" +
                                        $"3. СНИЛС: {referralData.PatientSnils}\n" +
                                        $"4. Дата рождения: {referralData.PatientBirthday:dd.MM.yyyy}\n" +
                                        $"5. Адрес: {referralData.PatientAddress}\n" +
                                        $"6. Телефон: {referralData.PhoneNumber}";
                patientData.Range.Font.Size = 10;
                patientData.Alignment = WdParagraphAlignment.wdAlignParagraphLeft;
                patientData.Range.InsertParagraphAfter();

                // 6. Информация о направлении
                Paragraph directionInfo = doc.Content.Paragraphs.Add(ref missing);
                directionInfo.Range.Text = $"7. Направлен в: {referralData.DepartmentName}\n" +
                                          $"8. Диагноз: {referralData.Diagnosis}";
                directionInfo.Range.Font.Size = 10;
                directionInfo.Alignment = WdParagraphAlignment.wdAlignParagraphLeft;
                directionInfo.Range.InsertParagraphAfter();

                // 7. Подпись врача
                Paragraph doctorSignature = doc.Content.Paragraphs.Add(ref missing);
                doctorSignature.Range.Text = $"Врач: {referralData.EmployeePost} _________________ {referralData.EmployeeFIO}\n" +
                                            $"Дата: \"{DateTime.Now:dd}\" {DateTime.Now:MMMM} {DateTime.Now:yyyy} г.";
                doctorSignature.Range.Font.Size = 10;
                doctorSignature.Alignment = WdParagraphAlignment.wdAlignParagraphLeft;
                doctorSignature.Range.InsertParagraphAfter();

                // 8. Печать и отметки стационара
                Paragraph hospitalMarks = doc.Content.Paragraphs.Add(ref missing);
                hospitalMarks.Range.Text = "М.П.\n\nОтметки стационара:\nДата поступления: __________ Время: __________";
                hospitalMarks.Range.Font.Size = 10;
                hospitalMarks.Alignment = WdParagraphAlignment.wdAlignParagraphLeft;
                hospitalMarks.Range.InsertParagraphAfter();

                // Сохранение документа
                string fileName = $"Направление_{referralData.PatientFIO}_{DateTime.Now:yyyyMMdd_HHmmss}.docx";
                doc.SaveAs(FileName: fileName);

                app.Visible = true;

                MessageBox.Show("Направление успешно сформировано по пути: " + doc.FullName, "Информация", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка при формировании направления: {ex.Message}", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }


        // Обработчик закрытия формы
        private void HospitalizationsForm_FormClosed(object sender, FormClosedEventArgs e)
        {
            // Показываем главную форму при закрытии
            if (isSee)
            {
                isSee = false;
                if (System.Windows.Forms.Application.OpenForms["HospitalizationIteractionForm"] is HospitalizationIteractionForm mn)
                    mn.Show();
            }
            else 
            {
                if (System.Windows.Forms.Application.OpenForms["MainForm"] is MainForm mn)
                    mn.Show();
                else
                    new MainForm().Show(); 
            }
        }

        // Обработчик нажатия клавиш в поле поиска
        private void textBoxSearch_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)Keys.Back || e.KeyChar == (char)Keys.Space)
            {
                e.Handled = false;
                return;
            }

            // Разрешаем только русские буквы
            if (!Regex.IsMatch(e.KeyChar.ToString(), @"^[А-Яа-яЁё]+$"))
            {
                e.Handled = true;
            }
        }



        // Обработчик клика по ячейке DataGridView
        private void dataGridViewHospitalizations_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                dataGridViewHospitalizations.Rows[e.RowIndex].Selected = true;
                patID = dataGridViewHospitalizations.Rows[e.RowIndex].Cells[2].Value.ToString();
                hospID = dataGridViewHospitalizations.Rows[e.RowIndex].Cells[0].Value.ToString();
            }
        }

        // Обработчик события перед отрисовкой строки DataGridView
        private void dataGridViewHospitalizations_RowPrePaint(object sender, DataGridViewRowPrePaintEventArgs e)
        {
            // Подсветка строк в зависимости от дат и статуса
            if (e.RowIndex >= 0)
            {
                // Поле 9: Подсветка, когда значение равно или на день раньше текущей даты бледно-красным
                if (dataGridViewHospitalizations.Rows[e.RowIndex].Cells[9].Value != null && dataGridViewHospitalizations.Rows[e.RowIndex].Cells[9].Value != DBNull.Value)
                {
                    if (DateTime.TryParse(dataGridViewHospitalizations.Rows[e.RowIndex].Cells[9].Value.ToString(), out DateTime date9))
                    {
                        DateTime today = DateTime.Now.Date;
                        DateTime yesterday = today.AddDays(-1);

                        if (date9 == today || date9 == yesterday)
                        {
                            dataGridViewHospitalizations.Rows[e.RowIndex].Cells[9].Style.BackColor = Color.LightCoral; // Бледно-красный
                        }
                    }
                }

                // Поле 10: Подсветка в зависимости от статуса
                if (dataGridViewHospitalizations.Rows[e.RowIndex].Cells[10].Value != null)
                {
                    string status = dataGridViewHospitalizations.Rows[e.RowIndex].Cells[10].Value.ToString();

                    if (status == "Направлен")
                    {
                        dataGridViewHospitalizations.Rows[e.RowIndex].Cells[10].Style.BackColor = Color.Orange;
                    }
                    else if (status == "На лечении")
                    {
                        dataGridViewHospitalizations.Rows[e.RowIndex].Cells[10].Style.BackColor = Color.PowderBlue;
                    }
                }
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            HospitalizationIteractionForm.patID = patID;
            HospitalizationIteractionForm.hospID = hospID;
            HospitalizationIteractionForm iteractionForm = new HospitalizationIteractionForm();
            this.Hide();
            iteractionForm.ShowDialog();

            LoadHospitalizationData();

            if (!string.IsNullOrEmpty(hospID))
            {
                HighlightEditedRecord(hospID);
            }
        }
        private void ApplyUserRolePermissions()
        {
            if (User.Role == 4)
            {
                dataGridViewHospitalizations.Columns["dateOfDischarge"].Visible = false;
                dataGridViewHospitalizations.Columns["employeeFIO"].Visible = false;
                dataGridViewHospitalizations.Columns["wardNumber"].Visible = false;
            }else if (User.Role == 2)
            {
                button1.Enabled = false;
            }
        }

        public void HighlightNewRecord(string hospitalizationID)
        {
            // Ждем завершения загрузки данных
            this.Activated += (sender, e) =>
            {
                foreach (DataGridViewRow row in dataGridViewHospitalizations.Rows)
                {
                    if (row.Cells["HospitalizationID"].Value.ToString() == hospitalizationID)
                    {
                        row.DefaultCellStyle.BackColor = Color.LightGreen;
                        dataGridViewHospitalizations.FirstDisplayedScrollingRowIndex = row.Index;
                        break;
                    }
                }
            };
        }

        public void HighlightEditedRecord(string hospitalizationID)
        {
            foreach (DataGridViewRow row in dataGridViewHospitalizations.Rows)
            {
                if (row.Cells["HospitalizationID"].Value.ToString() == hospitalizationID)
                {
                    row.DefaultCellStyle.BackColor = Color.LightGreen;
                    dataGridViewHospitalizations.FirstDisplayedScrollingRowIndex = row.Index;
                    break;
                }
            }
        }

        private void HospitalizationsForm_Load(object sender, EventArgs e)
        {
            if (isSee)
            {
                button1.Visible = button2.Visible = false;
                button4.Visible = true;
                button3.Text = "Вернуться к записи";
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            GenerateRefferalForHospitalization(hospID);
        }
    }
}