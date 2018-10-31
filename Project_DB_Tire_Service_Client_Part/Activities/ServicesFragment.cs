using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.Graphics.Drawables;
using Android.OS;
using Android.Runtime;
using Android.Util;
using Android.Views;
using Android.Widget;
using Java.IO;
using Project_DB_Tire_Service_Client_Part.Entity;

namespace Project_DB_Tire_Service_Client_Part.Activities
{
    public class ServicesFragment : Android.Support.V4.App.Fragment
    {
        private View _view;

        public override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            // Create your fragment here
        }

        public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {
            _view = inflater.Inflate(Resource.Layout.AuthFragment, container, false);

            var serviceImage= _view.FindViewById<EditText>(Resource.Id.image_service_photo);
            var services = (List<Services>)new Services().Select();
            Drawable d = Drawable.CreateFromStream(new System.IO.MemoryStream(services.First().ImageByte), null);
            //createFromStream(new ByteArrayInputStream(ARRAY_BYTES), null);
            serviceImage.SetBackgroundDrawable(d);
            return _view;
        }
    }
}