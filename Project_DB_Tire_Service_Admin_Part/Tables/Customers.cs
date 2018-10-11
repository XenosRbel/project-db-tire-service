using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project_DB_Tire_Service_Admin_Part.Tables
{
    /// <summary>
    /// Класс "клиенты"
    /// </summary>
    class Customers
    {
        /// <summary>
        /// Конструктор объекта "клиент"
        /// </summary>
        /// <param name="iD">Код клиента</param>
        /// <param name="fIO">Фамилия Имя Отчество клиента</param>
        /// <param name="phoneNumber">Номер телефона</param>
        /// <param name="email">Личная почта</param>
        public Customers(int iD, string fIO, string phoneNumber, string email)
        {
            ID = iD;
            FIO = fIO;
            PhoneNumber = phoneNumber;
            Email = email;
        }
        
        public int ID { set; get; }
        public string FIO { set; get; }
        public string PhoneNumber { set; get; }
        public string Email { set; get; }        
    }
}
