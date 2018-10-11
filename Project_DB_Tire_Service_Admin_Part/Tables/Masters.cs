using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project_DB_Tire_Service_Admin_Part.Tables
{
    class Masters
    {
        public Masters(int iD, string fIO, string specialization, string phone)
        {
            ID = iD;
            FIO = fIO;
            Specialization = specialization;
            Phone = phone;
        }

        public int ID { set; get; }
        public string FIO { set; get; }
        public string Specialization { set; get; }
        public string Phone { set; get; }
    }
}
