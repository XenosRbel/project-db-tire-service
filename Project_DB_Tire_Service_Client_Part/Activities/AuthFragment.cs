using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Telephony;
using Android.Util;
using Android.Views;
using Android.Widget;
using Java.Util;
using Project_DB_Tire_Service_Client_Part.PhoneAuth;
using Project_DB_Tire_Service_Client_Part.Utils;

namespace Project_DB_Tire_Service_Client_Part.Activities
{
    class AuthFragment : Android.Support.V4.App.Fragment
    {
        View _view;

        public override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
        }

        private void CountryEdit_Click(object sender, System.EventArgs e)
        {
            new FragmentUtil(this.Activity, this.FragmentManager)
                .CreateLoadView(Resource.Id.parent_fragment_container, new AuthCountryFragment());
        }

        public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {
            _view = inflater.Inflate(Resource.Layout.AuthFragment, container, false);

            var countryEdit = _view.FindViewById<EditText>(Resource.Id.edit_auth_contry);
            countryEdit.Click += CountryEdit_Click;

            var btnContinue = _view.FindViewById<Button>(Resource.Id.button_auth_continue);
            btnContinue.Click += BtnContinue_Click;

            var editCountry = _view.FindViewById<EditText>(Resource.Id.edit_auth_contry);

            GetCurrentCountryCode(out var currentCountry, out var countryCode);

            editCountry.Text = $"{currentCountry}\t(+{countryCode})";
            
            return _view;
        }

        public override void OnResume()
        {
            base.OnResume();

            var preferences = new AppPreferences(this._view.Context);
            var data = Convert.ToString(preferences.GetAccessKey(PreferenceField.PREFERENCE_COUNTNTRY_DATA)).Split('\t');

            if (!string.IsNullOrEmpty(data[0]))
            {
                var editCountry = _view.FindViewById<EditText>(Resource.Id.edit_auth_contry);
                editCountry.Text = $"{data[0]}\t(+{data[1]})";
            }
        }

        private void GetCurrentCountryCode(out string currentCountry, out string countryCode)
        {
            TelephonyManager manager = (TelephonyManager)this.Activity.GetSystemService(Context.TelephonyService);
            currentCountry = new Locale("", manager.SimCountryIso)
                .DisplayCountry;
            countryCode = new CountryData(this.Activity)
                .GetCountryDictonary()[currentCountry];
        }

        private void BtnContinue_Click(object sender, EventArgs e)
        {            
            var editPhone = _view.FindViewById<EditText>(Resource.Id.edit_auth_phone);
            var editCountry = _view.FindViewById<EditText>(Resource.Id.edit_auth_contry);

            Regex regex = new Regex(@"\+\d*");

            string phoneNumber = regex.Match(editCountry.Text).Value + editPhone.Text;
                     
            new AppPreferences(this._view.Context).ClearAccessKey();

            var fragment = new AuthConfirmCodeFragment();
            Bundle bundle = new Bundle();
            bundle.PutString("phoneNumber", phoneNumber);

            fragment.Arguments = bundle;

            new FragmentUtil(this.Activity, this.FragmentManager)
                .CreateLoadView(Resource.Id.parent_fragment_container, fragment);
        }
    }
}