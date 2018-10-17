using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Telephony;
using Android.Util;
using Android.Views;
using Android.Widget;
using Project_DB_Tire_Service_Client_Part.PhoneAuth;

namespace Project_DB_Tire_Service_Client_Part.Activities
{
    public class AuthFragment : Android.Support.V4.App.Fragment
    {
        View _view;

        public override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            // Create your fragment here
         }

        private void CountryEdit_Click(object sender, System.EventArgs e)
        {
            var transaction = FragmentManager.BeginTransaction();
            transaction.Replace(Resource.Id.parent_fragment_container, new AuthCountryFragment()).AddToBackStack("fragment_country").Commit();
        }

        public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {
            _view = inflater.Inflate(Resource.Layout.AuthFragment, container, false);


            var countryEdit = _view.FindViewById<EditText>(Resource.Id.edit_auth_contry);
            countryEdit.Click += CountryEdit_Click;

            var btnContinue = _view.FindViewById<Button>(Resource.Id.button_auth_continue);
            btnContinue.Click += BtnContinue_Click;

            var editCountry = _view.FindViewById<EditText>(Resource.Id.edit_auth_contry);

            TelephonyManager manager = (TelephonyManager)this.Activity.GetSystemService(Context.TelephonyService);
            var countryCode = manager.SimCountryIso;

            return _view;
        }

        private void BtnContinue_Click(object sender, EventArgs e)
        {
            var phoneAuth = new FireBasePhoneAuth();
            phoneAuth.Activity = this.Activity;
            phoneAuth.InitFirebaseAuth();

            var editPhone = _view.FindViewById<EditText>(Resource.Id.edit_auth_phone);
            var editCountry = _view.FindViewById<EditText>(Resource.Id.edit_auth_contry);

            phoneAuth.StartPhoneNumberVerification(editCountry.Text + editPhone.Text);
        }

        public override void OnViewCreated(View view, Bundle savedInstanceState)
        {
            base.OnViewCreated(view, savedInstanceState);
        }
    }
}