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
        public Orders(int iD, int idMaster, int idServices, int idCustomer, DateTime orderDate, int countO)
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
        public int IdMaster { set; get; }
        public int IdServices { set; get; }
        public int IdCustomer { set; get; }
        public DateTime OrderDate { set; get; }
        public int CountO { set; get; }

        public override bool Equals(object obj)
        {
            return obj is Orders order &&
                   ID == order.ID &&
                   IdMaster == order.IdMaster &&
                   IdServices == order.IdServices &&
                   IdCustomer == order.IdCustomer &&
                   OrderDate == order.OrderDate &&
                   CountO == order.CountO;
        }

        public override int GetHashCode()
        {
            var hashCode = 950079018;
            hashCode = hashCode * -1521134295 + ID.GetHashCode();
            hashCode = hashCode * -1521134295 + IdMaster.GetHashCode();
            hashCode = hashCode * -1521134295 + IdServices.GetHashCode();
            hashCode = hashCode * -1521134295 + IdCustomer.GetHashCode();
            hashCode = hashCode * -1521134295 + OrderDate.GetHashCode();
            hashCode = hashCode * -1521134295 + CountO.GetHashCode();
            return hashCode;
        }
    }
}
