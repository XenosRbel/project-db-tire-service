using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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
using Project_DB_Tire_Service_Client_Part.Adapters;

namespace Project_DB_Tire_Service_Client_Part.Activities
{
    public class ServicesFragment : Android.Support.V4.App.Fragment
    {
        private View _view;
        private ListView _listView;
        private List<Services> _services;

        public override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            // Create your fragment here
        }

        public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {
            _view = inflater.Inflate(Resource.Layout.app_fragment_services, container, false);

            _listView = _view.FindViewById<ListView>(Resource.Id.lst_services);

            SetDataToDapter();

            _listView.ItemClick += OnListItemClick;  // to be defined

            //var serviceImage= _view.FindViewById<ImageView>(Resource.Id.image_service_photo);

            
            //Drawable d = Drawable.CreateFromStream(new System.IO.MemoryStream(services.First().ImageBytes), null);
            //createFromStream(new ByteArrayInputStream(ARRAY_BYTES), null);
            //serviceImage.SetBackgroundDrawable(d);
            //serviceImage.SetImageDrawable(d);
            return _view;
        }

        private async void SetDataToDapter()
        {

            this.Activity.RunOnUiThread(() =>
            {
                _services = (List<Services>)new Services().Select();
                _listView.Adapter = new ServicesListAdapter(this.Activity, _services);

            });
        }

        void OnListItemClick(object sender, AdapterView.ItemClickEventArgs e)
        {
            var listView = sender as ListView;
            var t = _services[e.Position];
            
        }
    }
}