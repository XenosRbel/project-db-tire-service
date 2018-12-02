using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Autoservice_Core.Entity
{
    [Serializable]
    public partial class Services : EntityAbstract
    {
        public Services(int idServices, string nameService, int radius, float price, byte[] imageBytes) : base()
        {
            IdServices = idServices;
            NameService = nameService;
            Radius = radius;
            Price = price;
            ImageBytes = imageBytes;
        }

        public int IdServices { set; get; }
        public string NameService { set; get; }
        public int Radius { set; get; }
        public float Price { set; get; }
        public byte[] ImageBytes { set; get; }

        public override bool Equals(object obj)
        {
            var services = obj as Services;
            return services != null &&
                   IdServices == services.IdServices &&
                   NameService == services.NameService &&
                   Radius == services.Radius &&
                   Price == services.Price &&
                   EqualityComparer<byte[]>.Default.Equals(ImageBytes, services.ImageBytes);
        }

        public override int GetHashCode()
        {
            var hashCode = -727309877;
            hashCode = hashCode * -1521134295 + IdServices.GetHashCode();
            hashCode = hashCode * -1521134295 + EqualityComparer<string>.Default.GetHashCode(NameService);
            hashCode = hashCode * -1521134295 + Radius.GetHashCode();
            hashCode = hashCode * -1521134295 + Price.GetHashCode();
            hashCode = hashCode * -1521134295 + EqualityComparer<byte[]>.Default.GetHashCode(ImageBytes);
            return hashCode;
        }
    }
}