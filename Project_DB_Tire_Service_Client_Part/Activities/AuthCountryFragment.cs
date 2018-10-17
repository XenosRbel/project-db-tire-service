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
using Project_DB_Tire_Service_Client_Part.Adapters;

namespace Project_DB_Tire_Service_Client_Part.Activities
{
    public class AuthCountryFragment : Android.Support.V4.App.Fragment
    {
        private View _view;

        public override void OnActivityCreated(Bundle savedInstanceState)
        {
            base.OnActivityCreated(savedInstanceState);
        }

        public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {
            _view = inflater.Inflate(Resource.Layout.auth_fragment_countyList, container, false);

            var toolbar = _view.FindViewById<Android.Widget.Toolbar>(Resource.Id.toolbar_country);
            this.Activity.SetActionBar(toolbar);
            this.Activity.ActionBar.SetDisplayHomeAsUpEnabled(true);
            //this.Activity.ActionBar.SetDisplayShowHomeEnabled(true);

            var listOfCountry = _view.FindViewById<ListViewCompat>(Resource.Id.list_view_country);
            var countryArray = Resources.GetStringArray(Resource.Array.countryCodes);
            //countryArray = countryArray.Select(x => x.Replace(',', ' ')).ToArray();

            listOfCountry.Adapter = new ArrayAdapter<string>(_view.Context, Resource.Layout.item_country, countryArray);

            return _view;
        }

        private static Dictionary<CountryData, CountryData> GetMapCountry(string[] countryArray)
        {
            var country = new Dictionary<CountryData, CountryData>();

            for (int i = 0; i < countryArray.Length; i++)
            {
                var item = countryArray[i].Split(',');
                country.Add(new CountryData(item[1]), new CountryData(item[0]));
            }

            return country;
        }
    }
}