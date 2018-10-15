using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Support.V7.Widget;
using Android.Util;
using Android.Views;
using Android.Widget;

namespace Project_DB_Tire_Service_Client_Part.Activities
{
    public class AuthCountryFragment : Android.Support.V4.App.Fragment
    {
        public override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            // Create your fragment here
        }

        public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {
            // Use this to return your custom view for this Fragment
            // return inflater.Inflate(Resource.Layout.YourFragment, container, false);
            View view = inflater.Inflate(Resource.Layout.auth_fragment_countyList, container, false);

            var listView = view.FindViewById<ListViewCompat>(Resource.Id.list_view_country);
            var country = Resources.GetStringArray(Resource.Array.countryCodes);

            listView.Adapter = new ArrayAdapter<string>(view.Context, Resource.Layout.item_country, country);

            var toolbar = view.FindViewById<Android.Widget.Toolbar>(Resource.Id.toolbar_country);
            this.Activity.SetActionBar(toolbar);
            this.Activity.ActionBar.SetDisplayHomeAsUpEnabled(true);
            this.Activity.ActionBar.SetDisplayShowHomeEnabled(true);

            return view;
        }
    }
}