using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Util;
using Android.Views;
using Android.Widget;
using Project_DB_Tire_Service_Client_Part.PhoneAuth;
using Project_DB_Tire_Service_Client_Part.Utils;

namespace Project_DB_Tire_Service_Client_Part.Activities
{
    public class AuthConfirmCodeFragment : Android.Support.V4.App.Fragment
    {
        View _view;
        private FireBasePhoneAuth phoneAuth;
        public override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            InitFireBaseAuth();

            Bundle bundle = Arguments;
            if (bundle != null)
            {
                string phoneNumber = bundle.GetString("phoneNumber");

                phoneAuth.StartPhoneNumberVerification(phoneNumber);

                phoneAuth.OnAuthSuccessful += () => AuthSuccessHandler();
            }
        }

        private void AuthSuccessHandler()
        {
            new AppPreferences(this._view.Context).SaveAccessKey(PreferenceField.PREFERENCE_AUTH_SSUCCESS, "true");

            Intent intent = new Intent(this.Activity, typeof(MainAppActivity));
            StartActivity(intent);
        }
        private void InitFireBaseAuth()
        {
            phoneAuth = new FireBasePhoneAuth();
            phoneAuth.Activity = this.Activity;
            phoneAuth.InitFirebaseAuth();
        }

        public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {
            _view = inflater.Inflate(Resource.Layout.auth_fragment_confirm_code, container, false);

            var btnContinue = _view.FindViewById<Button>(Resource.Id.button_auth_codecontinue);
            btnContinue.Click += BtnContinue_Click;

            return _view;
        }

        private void BtnContinue_Click(object sender, EventArgs e)
        {
            var codeEdit = _view.FindViewById<EditText>(Resource.Id.edit_auth_code_secure);

            phoneAuth
                .VerifyPhoneNumberWithCode(phoneAuth.MCallbacks.mVerificationId, codeEdit.Text);
        }
    }
}