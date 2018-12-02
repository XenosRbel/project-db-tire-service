using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using Android.App;
using Android.Content;
using Android.Graphics.Drawables;
using Android.OS;
using Android.Runtime;
using Android.Util;
using Android.Views;
using Android.Widget;
using Autoservice_Core.Entity;
using Java.IO;

namespace Project_DB_Tire_Service_Client_Part.Activities
{
    public class ServiceItemFragment : Android.Support.V4.App.Fragment
    {
        private View _view;
        private Bundle _bundle;
        private readonly string TAG = "SELECTED_ITEM";
        private readonly string SELECTED_ITEM_IMAGE = "SELECTED_ITEM_IMAGE";

        public override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            
            _bundle = this.Arguments;
        }

        public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {
            _view = inflater.Inflate(Resource.Layout.item_info_services, container, false);

            var itemServices = new Services();

            if (_bundle != null)
            {
                var bundleBytes = _bundle.GetByteArray(TAG);
                itemServices = Services.Desserialize(bundleBytes);
                itemServices.ImageBytes = _bundle.GetByteArray(SELECTED_ITEM_IMAGE);
            }

            var serviceImage = _view.FindViewById<ImageView>(Resource.Id.image_service_photo);

            var bytes1 = itemServices.ImageBytes;

            Drawable drawable = Drawable.CreateFromStream(new System.IO.MemoryStream(bytes1), null);
            serviceImage.SetImageDrawable(drawable);

            var serviceName = _view.FindViewById<TextView>(Resource.Id.text_service_descript);
            serviceName.Text = itemServices.NameService;

            var radius = _view.FindViewById<TextView>(Resource.Id.text_service_radius);
            radius.Text = Convert.ToString(itemServices.Radius);

            var price = _view.FindViewById<TextView>(Resource.Id.text_service_price);
            price.Text = PriceFormatter(itemServices.Price);

            return _view;
        }

        private string PriceFormatter(float value)
        {
            Regex regex = new Regex(@"\d*.{3}");
            return regex.Match(Convert.ToString(value, CultureInfo.CurrentCulture)).Value + " BYN";
        }
    }
}