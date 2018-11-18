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

    }
}
