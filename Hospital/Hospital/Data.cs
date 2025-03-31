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

        static string server = ConfigurationManager.AppSettings["host"];
        static string uid = ConfigurationManager.AppSettings["uid"];
        static string pwd = ConfigurationManager.AppSettings["password"];
        static string db = ConfigurationManager.AppSettings["db"];
        //Метод для получения строки подключения
        public static string GetConnString()
        {
            return "server=" + server + ";uid=" + uid + ";pwd=" + pwd + ";database=" + db;
        }

    }
}
