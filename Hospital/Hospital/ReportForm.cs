using word = Microsoft.Office.Interop.Word;
using Microsoft.Office.Interop.Word;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices.ComTypes;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml.Linq;

namespace Hospital
{
    public partial class ReportForm : Form
    {
        public ReportForm()
        {
            InitializeComponent(); // Инициализация компонентов формы
        }

        private void buttonGenerateReport_Click(object sender, EventArgs e)
        {
            // Проверка валидности дат: начало не позже конца, разница больше 7 дней
            if (DateIsValid(dateTimePickerEndDate) && DateIsValid(dateTimePickerStartDate) && dateTimePickerStartDate.Value < dateTimePickerEndDate.Value && (dateTimePickerEndDate.Value - dateTimePickerStartDate.Value).Days > 7)
            {
                DateTime startDate = dateTimePickerStartDate.Value.Date; // Получение даты начала
                DateTime endDate = dateTimePickerEndDate.Value.Date; // Получение даты окончания

                string startDatee = startDate.ToString("yyyy-MM-dd"); // Форматирование даты начала
                string endDatee = endDate.ToString("yyyy-MM-dd"); // Форматирование даты окончания

                try
                {
                    GenerateBedUsageReport(startDatee, endDatee); // Генерация отчета
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Ошибка при формировании отчета: {ex.Message}", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error); // Обработка ошибок
                    return;
                }
            }
            else
            {
                return; // Выход при невалидных данных
            }
        }

        private void GenerateBedUsageReport(string startDatee, string endDatee)
        {
            try
            {
                // Создание приложения Word и документа
                word.Application app = new word.Application();
                Document doc = app.Documents.Add();

                // Добавление заголовка отчета
                Paragraph titleParagraph = doc.Content.Paragraphs.Add();
                titleParagraph.Range.Text = "Отчет об использовании коечных мест";
                titleParagraph.Range.Font.Size = 16;
                titleParagraph.Range.Bold = 1;
                titleParagraph.Alignment = WdParagraphAlignment.wdAlignParagraphCenter;
                titleParagraph.Range.InsertParagraphAfter();

                // Добавление деталей отчета (период и дата создания)
                Paragraph reportDetailsParagraph = doc.Content.Paragraphs.Add();
                reportDetailsParagraph.Range.Text = $"Период: {Convert.ToDateTime(startDatee):d} - {Convert.ToDateTime(endDatee):d}\nДата создания: {DateTime.Now:d}";
                reportDetailsParagraph.Range.Font.Size = 12;
                reportDetailsParagraph.Range.Bold = 0;
                reportDetailsParagraph.Range.InsertParagraphAfter();

                // Получение данных об использовании коек
                DataTable reportData = GetBedUsageData(startDatee, endDatee);

                // Добавление таблицы с данными, если они есть
                if (reportData != null && reportData.Rows.Count > 0)
                {
                    // Заголовок таблицы
                    Paragraph tableParagraph = doc.Content.Paragraphs.Add();
                    tableParagraph.Range.Text = "Данные об использовании коечных мест:";
                    tableParagraph.Range.Font.Size = 12;
                    tableParagraph.Range.InsertParagraphAfter();

                    // Создание таблицы в документе
                    Table bedUsageTable = doc.Tables.Add(reportDetailsParagraph.Range, reportData.Rows.Count + 1, reportData.Columns.Count, Type.Missing, Type.Missing);
                    bedUsageTable.Borders.Enable = 1; // Включение границ таблицы

                    // Заполнение заголовков таблицы
                    for (int i = 0; i < reportData.Columns.Count; i++)
                    {
                        bedUsageTable.Cell(1, i + 1).Range.Text = reportData.Columns[i].ColumnName;
                        bedUsageTable.Cell(1, i + 1).Range.Font.Bold = 1;
                    }

                    // Заполнение данных таблицы
                    for (int i = 0; i < reportData.Rows.Count; i++)
                    {
                        for (int j = 0; j < reportData.Columns.Count; j++)
                        {
                            bedUsageTable.Cell(i + 2, j + 1).Range.Text = reportData.Rows[i][j].ToString();
                        }
                    }
                    bedUsageTable.Range.Font.Size = 10; // Установка размера шрифта
                }
                else
                {
                    // Сообщение, если данных нет
                    Paragraph noDataParagraph = doc.Content.Paragraphs.Add();
                    noDataParagraph.Range.Text = "Нет данных для указанного периода.";
                    noDataParagraph.Range.Font.Size = 12;
                    noDataParagraph.Range.InsertParagraphAfter();
                }

                // Сохранение и отображение документа
                string fileName = $"BedUsageReport_{DateTime.Now:yyyyMMdd_HHmmss}.docx";
                doc.SaveAs(FileName: fileName);

                app.Visible = true; // Показ Word

                MessageBox.Show($"Отчет успешно создан по пути: {doc.Path}", "Информация", MessageBoxButtons.OK, MessageBoxIcon.Information);

                // Сброс значений дат
                dateTimePickerEndDate.Value = DateTime.Now.Date;
                dateTimePickerStartDate.Value = DateTime.Now.Date;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка при формировании отчета: {ex.Message}", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
        }

        private DataTable GetBedUsageData(string startDate, string endDate)
        {
            DataTable dataTable = new DataTable(); // Создание таблицы для данных

            // Добавление колонок
            dataTable.Columns.Add("Показатель", typeof(string));
            dataTable.Columns.Add("Значение", typeof(string));

            MySqlConnection connection = new MySqlConnection(GlobalValue.GetConnString());
            try
            {
                connection.Open(); // Открытие соединения с БД

                // Запрос общего количества коек
                MySqlCommand totalBedsQuery = new MySqlCommand("SELECT COUNT(*) FROM Bed", connection);
                int totalBeds = Convert.ToInt32(totalBedsQuery.ExecuteScalar());
                dataTable.Rows.Add("Общее количество коек", totalBeds.ToString());
                connection.Close();

                // Запрос количества функционирующих коек
                MySqlConnection con = new MySqlConnection(GlobalValue.GetConnString());
                con.Open();
                MySqlCommand functioningBedsQuery = new MySqlCommand("SELECT COUNT(*) FROM Bed WHERE BedStatus != \"Санитарная обработка\"", con);
                int functioningBeds = Convert.ToInt32(functioningBedsQuery.ExecuteScalar());
                dataTable.Rows.Add("Количество функционирующих коек", functioningBeds.ToString());
                con.Close();

                // Запрос количества занятых койко-дней
                MySqlConnection con1 = new MySqlConnection(GlobalValue.GetConnString());
                con1.Open();
                MySqlCommand occupiedBedDaysQuery = new MySqlCommand(@"SELECT SUM(DATEDIFF(LEAST(h.DateOfDischarge, @EndDate), h.DateOfReceipt) + 1)
                            FROM Hospitalization h
                            WHERE h.DateOfReceipt <= @EndDate AND (h.DateOfDischarge >= @StartDate OR h.DateOfDischarge IS NULL)", con1);

                occupiedBedDaysQuery.Parameters.AddWithValue("@StartDate", startDate.Split(' ')[0]);
                occupiedBedDaysQuery.Parameters.AddWithValue("@EndDate", endDate.Split(' ')[0]);

                object occupiedBedDaysResult = occupiedBedDaysQuery.ExecuteScalar();
                int occupiedBedDays = Convert.ToInt32(occupiedBedDaysResult);
                con1.Close();

                dataTable.Rows.Add("Количество занятых койко-дней", occupiedBedDays.ToString());

                // Расчет процента занятости коечного фонда
                int daysInPeriod = (Convert.ToDateTime(endDate) - Convert.ToDateTime(startDate)).Days + 1;
                double occupancyRate = ((double)occupiedBedDays / daysInPeriod) * 100;
                dataTable.Rows.Add("Процент занятости коечного фонда", occupancyRate.ToString("00.0") + "%");

                // Расчет средней длительности пребывания
                MySqlConnection con2 = new MySqlConnection(GlobalValue.GetConnString());
                con2.Open();
                MySqlCommand avgStayQuery = new MySqlCommand(@"SELECT AVG(DATEDIFF(DateOfDischarge, DateOfReceipt)) FROM Hospitalization WHERE DateOfDischarge BETWEEN @StartDate AND @EndDate", con2);
                avgStayQuery.Parameters.AddWithValue("@StartDate", startDate.Split(' ')[0]);
                avgStayQuery.Parameters.AddWithValue("@EndDate", endDate.Split(' ')[0]);
                object avgStayResult = avgStayQuery.ExecuteScalar();
                con2.Close();

                double averageStay = (avgStayResult != DBNull.Value && avgStayResult != null) ? Convert.ToDouble(avgStayResult) : 0;
                dataTable.Rows.Add("Средняя длительность пребывания", averageStay.ToString("0.0") + " дней");
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка: {ex.Message}", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return null;
            }

            return dataTable; // Возврат таблицы с данными
        }

        private void buttonCancel_Click(object sender, EventArgs e)
        {
            this.Close(); // Закрытие формы
        }

        private bool DateIsValid(DateTimePicker dt)
        {
            if (dt.Value > DateTime.Now) // Проверка, что дата не в будущем
            {
                MessageBox.Show("Дата не может быть в будущем.", "Ошибка ввода", MessageBoxButtons.OK, MessageBoxIcon.Error);
                dt.Focus();
                return false;
            }

            return true;
        }

        //Закрытие формы
        private void ReportForm_FormClosed(object sender, FormClosedEventArgs e)
        {
            if (System.Windows.Forms.Application.OpenForms["BedsForm"] is BedsForm mn)
                mn.Show(); // Показ формы BedsForm при закрытии
            else
                _ = new BedsForm().ShowDialog();
        }
    }
}