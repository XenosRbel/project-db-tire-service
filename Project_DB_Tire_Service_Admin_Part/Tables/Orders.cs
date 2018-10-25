using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project_DB_Tire_Service_Admin_Part.Tables
{
    [Serializable]
    partial class Orders
    {
        public Orders(int iD, string idMaster, string idServices, string idCustomer, DateTime orderDate, int countO)
        {
            ID = iD;
            IdMaster = idMaster;
            IdServices = idServices;
            IdCustomer = idCustomer;
            OrderDate = orderDate;
            CountO = countO;

            connection = new MySqlConnection(new Properties.Settings().dbConnectionS);
        }

        public int ID { set; get; }
        public string IdMaster { set; get; }
        public string IdServices { set; get; }
        public string IdCustomer { set; get; }
        public DateTime OrderDate { set; get; }
        public int CountO { set; get; }

    }
}
