using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media.Imaging;
using Autoservice_Core.Entity;

namespace Project_DB_Tire_Service_Admin_Part.Entity
{
    public class ServicesWrapper : Services
    {
        public BitmapImage ServicesImage { get; set; }

        public ServicesWrapper()
        {
        }

        public ServicesWrapper(int idServices, string nameService, int radius, float price, byte[] imageBytes) : base(idServices, nameService, radius, price, imageBytes)
        {
            this.IdServices = idServices;
            this.NameService = nameService;
            this.Radius = radius;
            this.Price = price;
            this.ServicesImage = ConvertBinToImage(imageBytes);
        }

        private static BitmapImage ConvertBinToImage(byte[] data)
        {
            if (data == null || data.Length == 0)
                return null;

            var image = new BitmapImage();

            using (var mem = new MemoryStream(data))
            {
                mem.Position = 0;
                image.BeginInit();
                image.CreateOptions = BitmapCreateOptions.PreservePixelFormat;
                image.CacheOption = BitmapCacheOption.OnLoad;
                image.UriSource = null;
                image.StreamSource = mem;
                image.EndInit();
            }

            image.Freeze();

            return image;
        }

        public List<ServicesWrapper> Wrapper()
        {
            var data = new List<ServicesWrapper>();

            var originalData = (List<Services>)this.Select();

            foreach (var item in originalData)
            {
                data.Add(
                    new ServicesWrapper(
                        item.IdServices,
                        item.NameService,
                        item.Radius,
                        item.Price,
                        item.ImageBytes
                    )
                );
            }

            return data;
        }
    }
}
