using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;

namespace Project_DB_Tire_Service_Client_Part.Entity
{
    [Serializable]
    partial class Armor : EntityAbstract
    {
        public Armor() : base()
        {
            
        }

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