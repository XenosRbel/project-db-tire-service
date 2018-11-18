using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
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
    }
}