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
using Java.IO;

namespace Project_DB_Tire_Service_Client_Part.Entity
{
    [Serializable]
    partial class Services : EntityAbstract
    {
        public int IdServices { set; get; }
        public string NameService { set; get; }
        public int Radius { set; get; }
        public float Price { set; get; }
        public byte[] ImageByte { set; get; }

        public Services() : base()
        {

        }

        public override bool Equals(object obj)
        {
            return obj is Services services &&
                   IdServices == services.IdServices &&
                   NameService == services.NameService &&
                   Radius == services.Radius &&
                   Price == services.Price &&
                   EqualityComparer<byte[]>.Default.Equals(ImageByte, services.ImageByte);
        }

        public override int GetHashCode()
        {
            var hashCode = 600516994;
            hashCode = hashCode * -1521134295 + IdServices.GetHashCode();
            hashCode = hashCode * -1521134295 + EqualityComparer<string>.Default.GetHashCode(NameService);
            hashCode = hashCode * -1521134295 + Radius.GetHashCode();
            hashCode = hashCode * -1521134295 + Price.GetHashCode();
            hashCode = hashCode * -1521134295 + EqualityComparer<byte[]>.Default.GetHashCode(ImageByte);
            return hashCode;
        }
    }
}