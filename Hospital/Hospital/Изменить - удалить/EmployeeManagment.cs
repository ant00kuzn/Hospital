﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Text.RegularExpressions;
using MySql.Data.MySqlClient;
using System.Security.Cryptography;
using System.IO;

namespace Hospital
{
    public partial class EmployeeManagment : Form
    {
        private int? employeeId; // ID сотрудника (null для нового)
        private string password; // Пароль (хранится в хэшированном виде)

        public EmployeeManagment(int? employeeId)
        {
            InitializeComponent(); // Инициализация компонентов
            this.employeeId = employeeId; // Установка ID сотрудника
            LoadData(); // Загрузка данных

            // Настройка текста кнопки в зависимости от режима
            if (employeeId != null)
            {
                this.button2.Text = "Изменить"; // Режим редактирования
                this.Text = "Изменение сотрудника";
                this.labelPassword.Text = "Пароль";
                toolTip1.SetToolTip(labelPassword, "Если поле оставить пустым, то пароль не изменится.");
            }
            else
            {
                this.button2.Text = "Добавить"; // Режим добавления
                this.Text = "Добавление сотрудника";

                toolTip1.SetToolTip(labelPassword, "Является обязательным для заполнения полем");
                toolTip1.SetToolTip(labelLogin, "Является обязательным для заполнения полем");
                toolTip1.SetToolTip(label1, "Является обязательным для заполнения полем");
                toolTip1.SetToolTip(label3, "Является обязательным для заполнения полем");

                label1.Text = "*" + label1.Text;
                label3.Text = "*" + label3.Text;
                labelLogin.Text = "*" + labelLogin.Text;
                labelPassword.Text = "*" + labelPassword.Text;
            }
        }

        /// <summary>
        /// Загрузка данных о пользователе
        /// </summary>
        private void LoadData()
        {
            LoadRoles(); // Загрузка списка ролей

            if (employeeId.HasValue) // Если передан ID (режим редактирования)
            {
                using (MySqlConnection connection = new MySqlConnection(GlobalValue.GetConnString()))
                {
                    try
                    {
                        connection.Open();
                        // Запрос данных сотрудника
                        string query = "SELECT CONCAT(EmployeeSurname, ' ', EmployeeName, ' ', EmployeePatronymic) as EmployeeFIO, Post, PassportDetails, Phone, Login, Password, Role, Photo FROM employee WHERE EmployeeID = @EmployeeID";
                        MySqlCommand command = new MySqlCommand(query, connection);
                        command.Parameters.AddWithValue("@EmployeeID", employeeId.Value);

                        using (MySqlDataReader reader = command.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                // Заполнение полей данными
                                textBox3.Text = reader["EmployeeFIO"].ToString();
                                textBox1.Text = reader["Post"].ToString();
                                textBoxLogin.Text = reader["Login"].ToString();
                                password = reader["Password"].ToString(); // Сохранение хэша пароля
                                comboBoxRole.SelectedValue = Convert.ToInt32(reader["Role"]);
                                maskedTextBox2.Text = reader["PassportDetails"].ToString();
                                maskedTextBox1.Text = reader["Phone"].ToString();

                                // Загрузка фото, если есть
                                if (reader["Photo"] != DBNull.Value)
                                {
                                    byte[] imgData = (byte[])reader["Photo"];
                                    using (System.IO.MemoryStream ms = new System.IO.MemoryStream(imgData))
                                    {
                                        Image img = Image.FromStream(ms);
                                        photoPictureBox.Image = img;
                                    }
                                }
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Ошибка загрузки сотрудника: " + ex.Message);
                    }
                }
            }
        }


        /// <summary>
        /// Загрузка ролей в comboBox
        /// </summary>
        private void LoadRoles()
        {
            using (MySqlConnection connection = new MySqlConnection(GlobalValue.GetConnString()))
            {
                try
                {
                    connection.Open();
                    // Запрос списка ролей
                    string query = "SELECT RoleID, RoleName FROM role";
                    MySqlDataAdapter adapter = new MySqlDataAdapter(query, connection);
                    DataTable roleTable = new DataTable();
                    adapter.Fill(roleTable);

                    // Привязка данных к ComboBox
                    comboBoxRole.DataSource = roleTable;
                    comboBoxRole.DisplayMember = "RoleName";
                    comboBoxRole.ValueMember = "RoleID";

                    comboBoxRole.SelectedIndex = 0; // Выбор первого элемента
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Ошибка загрузки роли: " + ex.Message);
                }
            }
        }


        //Сохоранение изменений
        private void SaveButton_Click(object sender, EventArgs e)
        {
            using (MySqlConnection connection = new MySqlConnection(GlobalValue.GetConnString()))
            {
                try
                {
                    connection.Open();
                    string query;

                    // Обработка пароля (хэширование, если он был изменен)
                    if (textBoxPassword.Text != "" && password != null)
                    {
                        using (var sh2 = SHA256.Create())
                        {
                            var sh2byte = sh2.ComputeHash(Encoding.UTF8.GetBytes(textBoxPassword.Text));
                            password = BitConverter.ToString(sh2byte).Replace("-", "").ToLower();
                        }
                    }
                    else if (password == null && textBoxPassword.Text != "")
                    {
                        using (var sh2 = SHA256.Create())
                        {
                            var sh2byte = sh2.ComputeHash(Encoding.UTF8.GetBytes(textBoxPassword.Text));
                            password = BitConverter.ToString(sh2byte).Replace("-", "").ToLower();
                        }
                    }
                    else if (password == null && textBoxPassword.Text == "")
                    {
                        MessageBox.Show("Пароль не может быть пустым.", "Ошибка.", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }

                    if (employeeId.HasValue) // Режим обновления
                    {
                        query = @"UPDATE employee SET EmployeeSurname = @EmployeeSurname, EmployeeName = @EmployeeName, EmployeePatronymic = @EmployeePatronymic,
                                Post = @Post, PassportDetails = @Passport, Phone = @Phone, Address = @Address, Login = @Login, Password = @Password, Role = @Role, Photo = @Photo
                                WHERE EmployeeID = @EmployeeID";
                    }
                    else // Режим добавления
                    {
                        // Проверка на дубликат логина
                        string checkQuery = "SELECT COUNT(*) FROM employee WHERE Login = @Login";
                        using (MySqlCommand checkCmd = new MySqlCommand(checkQuery, connection))
                        {
                            checkCmd.Parameters.AddWithValue("@Login", textBoxLogin.Text);
                            int duplicateCount = Convert.ToInt32(checkCmd.ExecuteScalar());

                            if (duplicateCount > 0)
                            {
                                MessageBox.Show("Сотрудник с таким логином уже существует.", "Предупреждение", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                                return;
                            }
                        }

                        query = @"INSERT INTO employee (EmployeeSurname, EmployeeName, EmployeePatronymic, Post, PassportDetails, Phone, Address Login, Password, Role, Photo) 
                                VALUES (@EmployeeSurname, @EmployeeName, @EmployeePatronymic, @Post, @Passport, @Phone, @Address, @Login, @Password, @Role, @Photo)";
                    }

                    MySqlCommand command = new MySqlCommand(query, connection);
                    // Разбиение ФИО на отдельные компоненты
                    command.Parameters.AddWithValue("@EmployeeSurname", textBox3.Text.Split(' ')[0]);
                    command.Parameters.AddWithValue("@EmployeeName", textBox3.Text.Split(' ')[1]);
                    command.Parameters.AddWithValue("@EmployeePatronymic", textBox3.Text.Split(' ')[2]);
                    command.Parameters.AddWithValue("@Post", textBox1.Text);
                    command.Parameters.AddWithValue("@Passport", maskedTextBox2.Text);
                    command.Parameters.AddWithValue("@Phone", maskedTextBox1.Text);
                    command.Parameters.AddWithValue("@Address", textBoxAddress.Text);
                    command.Parameters.AddWithValue("@Login", textBoxLogin.Text);
                    command.Parameters.AddWithValue("@Password", password);
                    command.Parameters.AddWithValue("@Role", comboBoxRole.SelectedValue);
                    command.Parameters.AddWithValue("@Photo", ImageToByteArray(photoPictureBox.Image)); // Конвертация изображения

                    if (employeeId.HasValue)
                    {
                        command.Parameters.AddWithValue("@EmployeeID", employeeId.Value);
                    }

                    command.ExecuteNonQuery(); // Выполнение запроса

                    // Сообщение об успехе
                    if (employeeId.HasValue)
                    {
                        MessageBox.Show("Изменения сохранены.", "Успешно", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    else
                    {
                        MessageBox.Show("Учетная запись добавлена.", "Успешно", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    this.DialogResult = DialogResult.OK; // Установка результата
                    this.Close(); // Закрытие формы
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Ошибка: " + ex.Message);
                }
            }
        }

        // Конвертация изображения в массив байтов
        private byte[] ImageToByteArray(System.Drawing.Image imageIn)
        {
            if (imageIn == null)
                return null;
            using (System.IO.MemoryStream ms = new System.IO.MemoryStream())
            {
                imageIn.Save(ms, System.Drawing.Imaging.ImageFormat.Jpeg);
                return ms.ToArray();
            }
        }

        // Обработчики событий и валидации
        private void CancelButton_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }

        private void BrowseButton_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "Image Files|*.jpg;*.jpeg;*.png;*.gif;*.bmp";
            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                FileInfo fi = new FileInfo(openFileDialog.FileName);
                if (fi.Length > 16777214)
                {
                    MessageBox.Show("Размер картинки превышает максимально допустимый.", "Ошибка выбора картинки.", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
                Image img = Image.FromFile(openFileDialog.FileName);
                photoPictureBox.Image = img;
            }
        }

        // Валидация ввода
        private void textBox3_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)Keys.Back)
            {
                e.Handled = false;
                return;
            }

            if (!Regex.IsMatch(e.KeyChar.ToString(), @"^[А-Яа-яЁё\s-]+$") && e.KeyChar == (char)Keys.Delete)
            {
                e.Handled = true;
            }
        }

        private void textBox2_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)Keys.Back)
            {
                e.Handled = false;
                return;
            }

            if (!Regex.IsMatch(e.KeyChar.ToString(), @"^[А-Яа-яЁё\s-]+$") && e.KeyChar == (char)Keys.Delete)
            {
                e.Handled = true;
            }
        }

        private void textBox1_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)Keys.Back)
            {
                e.Handled = false;
                return;
            }

            if (!Regex.IsMatch(e.KeyChar.ToString(), @"^[А-Яа-яЁё\s-]+$") && e.KeyChar == (char)Keys.Delete)
            {
                e.Handled = true;
            }
        }

        private void textBoxLogin_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)Keys.Back)
            {
                e.Handled = false;
                return;
            }

            if (!Regex.IsMatch(e.KeyChar.ToString(), @"^[a-zA-Z0-9_]+$") && e.KeyChar == (char)Keys.Delete)
            {
                e.Handled = true;
            }
        }

        private void textBoxPassword_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)Keys.Back)
            {
                e.Handled = false;
                return;
            }

            if (!Regex.IsMatch(e.KeyChar.ToString(), @"^[a-zA-Z0-9_]+$") && e.KeyChar == (char)Keys.Delete)
            {
                e.Handled = true;
            }
        }

        private void maskedTextBox1_Leave(object sender, EventArgs e)
        {
            string phoneNumberText = maskedTextBox1.Text.Replace("(", "").Replace(")", "").Replace("-", "").Replace(" ", "");
            if (!Regex.IsMatch(phoneNumberText, @"^\+7\d{10}$"))
            {
                MessageBox.Show("Неверный формат номера телефона. Пример: +7 (9XX) XXX-XX-XX", "Ошибка ввода", MessageBoxButtons.OK, MessageBoxIcon.Error);
                maskedTextBox1.Focus();
            }
        }

        private void textBoxAddress_KeyPress(object sender, KeyPressEventArgs e)
        {
            //kladr
        }

        private void maskedTextBox2_Leave(object sender, EventArgs e)
        {
            string passport = maskedTextBox2.Text.Replace(" ", "");
            if (!Regex.IsMatch(passport, @"^\d{10}$"))
            {
                MessageBox.Show("Неверный формат паспорта (серия номер). Пример: **** ******", "Ошибка ввода", MessageBoxButtons.OK, MessageBoxIcon.Error);
                maskedTextBox2.Focus();
            }
        }
    }
}