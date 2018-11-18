using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Autoservice_Core.Entity.Utils;

namespace Autoservice_Core.Entity
{
    [Serializable]
    public partial class Armor : EntityAbstract
    {
        public int IdArmor { set; get; }
        public DateTime ArrivalDate { set; get; }
        public DateTime DateExecution { set; get; }
        public ArmorStatus StatusA { set; get; }
        public string Customer { set; get; }
        public string NameService { set; get; }

        public override bool Equals(object obj)
        {
            return obj is Armor armor &&
                   IdArmor == armor.IdArmor &&
                   ArrivalDate == armor.ArrivalDate &&
                   DateExecution == armor.DateExecution &&
                   StatusA == armor.StatusA &&
                   Customer == armor.Customer &&
                   NameService == armor.NameService;
        }

        public override int GetHashCode()
        {
            var hashCode = 1339834214;
            hashCode = hashCode * -1521134295 + IdArmor.GetHashCode();
            hashCode = hashCode * -1521134295 + ArrivalDate.GetHashCode();
            hashCode = hashCode * -1521134295 + DateExecution.GetHashCode();
            hashCode = hashCode * -1521134295 + StatusA.GetHashCode();
            hashCode = hashCode * -1521134295 + EqualityComparer<string>.Default.GetHashCode(Customer);
            hashCode = hashCode * -1521134295 + EqualityComparer<string>.Default.GetHashCode(NameService);
            return hashCode;
        }
    }
}
