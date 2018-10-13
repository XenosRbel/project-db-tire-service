using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project_DB_Tire_Service_Admin_Part.Tables
{
    [Serializable]
    partial class Armor
    {
        public Armor(int iD, int idCustomers, int iDService, DateTime arrivalDate, DateTime dateExecution, ArmorStatus statusA)
        {
            ID = iD;
            IdCustomers = idCustomers;
            IDService = iDService;
            ArrivalDate = arrivalDate;
            DateExecution = dateExecution;
            StatusA = statusA;

            connection = new MySqlConnection(new Properties.Settings().dbConnectionS);
        }

        public int ID { set; get; }
        public int IdCustomers { set; get; }
        public int IDService { set; get; }
        public DateTime ArrivalDate { set; get; }
        public DateTime DateExecution { set; get; }
        public ArmorStatus StatusA { set; get; }

        public override bool Equals(object obj)
        {
            return obj is Armor armor &&
                   ID == armor.ID &&
                   IdCustomers == armor.IdCustomers &&
                   IDService == armor.IDService &&
                   ArrivalDate == armor.ArrivalDate &&
                   DateExecution == armor.DateExecution &&
                   StatusA == armor.StatusA;
        }

        public override int GetHashCode()
        {
            var hashCode = -161968347;
            hashCode = hashCode * -1521134295 + ID.GetHashCode();
            hashCode = hashCode * -1521134295 + IdCustomers.GetHashCode();
            hashCode = hashCode * -1521134295 + IDService.GetHashCode();
            hashCode = hashCode * -1521134295 + ArrivalDate.GetHashCode();
            hashCode = hashCode * -1521134295 + DateExecution.GetHashCode();
            hashCode = hashCode * -1521134295 + StatusA.GetHashCode();
            return hashCode;
        }
    }
}
