using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Autoservice_Core.Entity
{
    [Serializable]
    public partial class Orders : EntityAbstract
    {
        public Orders(int iD, string idMaster, string idServices, string idCustomer, DateTime orderDate, int countO) : base()
        {
            ID = iD;
            IdMaster = idMaster;
            IdServices = idServices;
            IdCustomer = idCustomer;
            OrderDate = orderDate;
            CountO = countO;
        }

        public int ID { set; get; }
        public string IdMaster { set; get; }
        public string IdServices { set; get; }
        public string IdCustomer { set; get; }
        public DateTime OrderDate { set; get; }
        public int CountO { set; get; }

        public override bool Equals(object obj)
        {
            var orders = obj as Orders;
            return orders != null &&
                   ID == orders.ID &&
                   IdMaster == orders.IdMaster &&
                   IdServices == orders.IdServices &&
                   IdCustomer == orders.IdCustomer &&
                   OrderDate == orders.OrderDate &&
                   CountO == orders.CountO;
        }

        public override int GetHashCode()
        {
            var hashCode = 950079018;
            hashCode = hashCode * -1521134295 + ID.GetHashCode();
            hashCode = hashCode * -1521134295 + EqualityComparer<string>.Default.GetHashCode(IdMaster);
            hashCode = hashCode * -1521134295 + EqualityComparer<string>.Default.GetHashCode(IdServices);
            hashCode = hashCode * -1521134295 + EqualityComparer<string>.Default.GetHashCode(IdCustomer);
            hashCode = hashCode * -1521134295 + OrderDate.GetHashCode();
            hashCode = hashCode * -1521134295 + CountO.GetHashCode();
            return hashCode;
        }

        public static bool operator ==(Orders orders1, Orders orders2)
        {
            return EqualityComparer<Orders>.Default.Equals(orders1, orders2);
        }

        public static bool operator !=(Orders orders1, Orders orders2)
        {
            return !(orders1 == orders2);
        }
    }
}
