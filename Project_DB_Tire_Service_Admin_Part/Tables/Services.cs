﻿using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media.Imaging;

namespace Project_DB_Tire_Service_Admin_Part.Tables
{
    [Serializable]
    partial class Services : EntityAbstract
    {
        public Services(int idServices, string nameService, int radius, float price, BitmapImage photoDetails) : base()
        {
            IdServices = idServices;
            NameService = nameService;
            Radius = radius;
            Price = price;
            PhotoDetails = photoDetails;
        }

        public int IdServices { set; get; }
        public string NameService { set; get; }
        public int Radius { set; get; }
        public float Price { set; get; }
        public BitmapImage PhotoDetails { set; get; }
        public Image SImage { set; get; }

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