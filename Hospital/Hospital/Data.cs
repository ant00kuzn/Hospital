using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
        public static string server = "localhost";
        public static string db = "hospital";
        public static string uid = "root";
        public static string pwd = "";
        //Метод для получения строки подключения
        public static string GetConnString()
        {
            return "server=" + server + ";uid=" + uid + ";pwd=" + pwd + ";database=" + db;
        }
    }
}
