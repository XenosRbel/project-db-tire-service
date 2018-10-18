﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Preferences;
using Android.Runtime;
using Android.Support.V7.Widget;
using Android.Util;
using Android.Views;
using Android.Widget;
using Java.Util;
using Project_DB_Tire_Service_Client_Part.Utils;

namespace Project_DB_Tire_Service_Client_Part.Activities
{
    public class AuthCountryFragment : Android.Support.V4.App.Fragment
    {
        private View _view;

        private ISharedPreferences mSharedPrefs;
        private ISharedPreferencesEditor mPrefsEditor;
        private String PREFERENCE_COUNTNTRY_DATA = "PREFERENCE_COUNTNTRY_DATA";
        private string[] countryArray;

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

            countryArray = new CountryData(this.Activity).GetCountryData();

            listOfCountry.Adapter = new ArrayAdapter<string>(_view.Context, Resource.Layout.item_country, countryArray);
            listOfCountry.ItemClick += (sender, e) => PushCountryData(e.Position);

            return _view;
        }

        private void PushCountryData(int position)
        {
            var preferences = new AppPreferences(this._view.Context);

            preferences.SaveAccessKey(PreferenceField.PREFERENCE_COUNTNTRY_DATA, countryArray[position]);
        }

        private void Toolbar_NavigationOnClick(object sender, EventArgs e)
        {
            this.FragmentManager.PopBackStack();
        }
    }
}