using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media.Imaging;

namespace Project_DB_Tire_Service_Admin_Part.Tables
{
    [Serializable]
    partial class Services
    {
        public Services(int idServices, string nameService, byte radius, float price, BitmapImage photoDetails)
        {
            IdServices = idServices;
            NameService = nameService;
            Radius = radius;
            Price = price;
            PhotoDetails = photoDetails;

            connection = new MySqlConnection(new Properties.Settings().dbConnectionS);
        }

        public int IdServices { set; get; }
        public string NameService { set; get; }
        public byte Radius { set; get; }
        public float Price { set; get; }
        public BitmapImage PhotoDetails { set; get; }

        public override bool Equals(object obj)
        {
            return obj is Services services &&
                   IdServices == services.IdServices &&
                   NameService == services.NameService &&
                   Radius == services.Radius &&
                   Price == services.Price &&
                   EqualityComparer<BitmapImage>.Default.Equals(PhotoDetails, services.PhotoDetails);
        }

        public override int GetHashCode()
        {
            var hashCode = 238356869;
            hashCode = hashCode * -1521134295 + IdServices.GetHashCode();
            hashCode = hashCode * -1521134295 + EqualityComparer<string>.Default.GetHashCode(NameService);
            hashCode = hashCode * -1521134295 + Radius.GetHashCode();
            hashCode = hashCode * -1521134295 + Price.GetHashCode();
            hashCode = hashCode * -1521134295 + EqualityComparer<BitmapImage>.Default.GetHashCode(PhotoDetails);
            return hashCode;
        }
    }
}