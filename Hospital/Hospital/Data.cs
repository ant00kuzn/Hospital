using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Hospital
{
    //Объявление класса для хранения информации о пользователе
    public static class User
    {
        public static int Id { get; set; }
        public static int Role { get; set; }
        public static string Name { get; set; }
        public static string SurName { get; set; }
        public static string Patronymic { get; set; }
    }

    public static class GlobalValue
    {
        public static bool dbIsntExist = false;

        public static string server = ConfigurationManager.AppSettings["host"];
        public static string uid = ConfigurationManager.AppSettings["uid"];
        public static string pwd = ConfigurationManager.AppSettings["password"];
        public static string db = ConfigurationManager.AppSettings["db"];
        //Метод для получения строки подключения
        public static string GetConnString()
        {
            return "server=" + server + ";uid=" + uid + ";pwd=" + pwd + ";database=" + db;
        }

        public static bool DatabaseIsValid()
        {
            string host = ConfigurationManager.AppSettings["host"];
            string uid = ConfigurationManager.AppSettings["uid"];
            string pass = ConfigurationManager.AppSettings["password"];
            string db = ConfigurationManager.AppSettings["db"];

            string connectionString = "server=" + host + ";uid=" + uid + ";pwd=" + pass + ";database=" + db;

            try
            {
                using (MySqlConnection connection = new MySqlConnection(connectionString))
                {
                    connection.Open();
                    connection.Close();
                    return true;
                }
            }
            catch (MySqlException ex)
            {
                if (ex.Number == 1049) // Error 1049 - Unknown database
                {
                    MessageBox.Show("База данных не существует.  Создайте базу данных.", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    GlobalValue.dbIsntExist = true;
                    return true;
                }
                else
                {
                    MessageBox.Show($"Ошибка подключения к базе данных: {ex.Message}", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                return false;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Произошла ошибка: {ex.Message}", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
        }

        public static bool DatabaseIsNotNull()
        {
            string host = ConfigurationManager.AppSettings["host"];
            string uid = ConfigurationManager.AppSettings["uid"];
            string pass = ConfigurationManager.AppSettings["password"];
            string db = ConfigurationManager.AppSettings["db"];

            string connectionString = "server=" + host + ";uid=" + uid + ";pwd=" + pass + ";database=" + db;

            try
            {
                using (MySqlConnection connection = new MySqlConnection(connectionString))
                {
                    connection.Open();

                    using (MySqlCommand command = new MySqlCommand("SHOW TABLES", connection))
                    {
                        using (MySqlDataReader reader = command.ExecuteReader())
                        {
                            if (!reader.HasRows)
                            {
                                MessageBox.Show("База данных пуста.  Произведите восстановление структуры базы данных.", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                return true;
                            }
                        }
                    }
                    return true;
                }
            }
            catch (MySqlException ex)
            {
                if (ex.Number == 1049) // Error 1049 - Unknown database
                {
                    MessageBox.Show("База данных не существует.  Создайте базу данных.", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                else
                {
                    MessageBox.Show($"Ошибка подключения к базе данных: {ex.Message}", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                return false;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Произошла ошибка: {ex.Message}", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
        }

        public static string GetConnStringWithoutDB()
        {
            string host = ConfigurationManager.AppSettings["host"];
            string uid = ConfigurationManager.AppSettings["uid"];
            string pass = ConfigurationManager.AppSettings["password"];

            return "server=" + host + ";uid=" + uid + ";pwd=" + pass + ";";
        }
    }
}
