using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project_DB_Tire_Service_Admin_Part.Tables
{
    [Serializable]
    partial class Masters : EntityAbstract
    {
        public Masters(int iD, string fIO, string specialization, string phone)
        {
            ID = iD;
            FIO = fIO;
            Specialization = specialization;
            Phone = phone;

            connection = new MySqlConnection(new Properties.Settings().dbConnectionS);
        }

        public int ID { set; get; }
        public string FIO { set; get; }
        public string Specialization { set; get; }
        public string Phone { set; get; }

        public override bool Equals(object obj)
        {
            return obj is Masters masters &&
                   ID == masters.ID &&
                   FIO == masters.FIO &&
                   Specialization == masters.Specialization &&
                   Phone == masters.Phone;
        }

        public override int GetHashCode()
        {
            var hashCode = -1956427436;
            hashCode = hashCode * -1521134295 + ID.GetHashCode();
            hashCode = hashCode * -1521134295 + EqualityComparer<string>.Default.GetHashCode(FIO);
            hashCode = hashCode * -1521134295 + EqualityComparer<string>.Default.GetHashCode(Specialization);
            hashCode = hashCode * -1521134295 + EqualityComparer<string>.Default.GetHashCode(Phone);
            return hashCode;
        }
    }
}
