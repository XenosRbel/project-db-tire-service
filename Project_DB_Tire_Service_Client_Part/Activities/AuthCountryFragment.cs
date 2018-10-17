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
using Java.Util;

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
            this.Activity.ActionBar.SetDisplayShowHomeEnabled(true);
            toolbar.NavigationOnClick += Toolbar_NavigationOnClick;

            var listOfCountry = _view.FindViewById<ListViewCompat>(Resource.Id.list_view_country);
            string[] countryArray = GetCountryData();

            listOfCountry.Adapter = new ArrayAdapter<string>(_view.Context, Resource.Layout.item_country, countryArray);

            return _view;
        }

        private void Toolbar_NavigationOnClick(object sender, EventArgs e)
        {

                this.FragmentManager.PopBackStack();

        }

        private string[] GetCountryData()
        {
            var countryMatrix = Resources.GetStringArray(Resource.Array.countryCodes).Select(x => x.Split(' ')).ToArray();
            var countryArray = new string[countryMatrix.Length];

            for (int i = 0; i < countryMatrix.Length; i++)
            {
                countryArray[i] = ($"{new Locale("", countryMatrix[i][0]).DisplayCountry}\t{countryMatrix[i][1]}");
            }

            return countryArray;
        }
    }
}