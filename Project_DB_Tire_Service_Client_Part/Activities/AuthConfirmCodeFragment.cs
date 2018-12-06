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
using Java.Security;
using Java.Util;
using Project_DB_Tire_Service_Client_Part.PhoneAuth;
using Project_DB_Tire_Service_Client_Part.Utils;

namespace Project_DB_Tire_Service_Client_Part.Activities
{
    public class AuthConfirmCodeFragment : Android.Support.V4.App.Fragment
    {
        View _view;
        private FireBasePhoneAuth _phoneAuth;
        private System.Timers.Timer _timer;
        private DateTime _endTime;

        public override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            InitFireBaseAuth();

            Bundle bundle = Arguments;
            if (bundle == null) return;

            string phoneNumber = bundle.GetString("phoneNumber");

            _phoneAuth.StartPhoneNumberVerification(phoneNumber);

            _timer = new System.Timers.Timer(1000);

            StartTimerAuth();

            _phoneAuth.OnAuthSuccessful += AuthSuccessHandler;
        }

        private void StartTimerAuth()
        {
            _endTime = DateTime.Now.AddSeconds(60);
            _timer.Elapsed += (sender, e) => PhoneAuthTimeDisplay();
            _timer.Start();
        }

        private TimeSpan TimeLeft()
        {
            return _endTime - DateTime.Now;
        }

        private void PhoneAuthTimeDisplay()
        {
            var textTimeLeft = _view.FindViewById<TextView>(Resource.Id.text_time_left);

            if (TimeLeft().Seconds >= 0)
            {
                this.Activity.RunOnUiThread(() => { textTimeLeft.Text = TimeLeft().Seconds.ToString(); });
            }
            else
            {
                _timer.Stop();
                _phoneAuth.ResendVerificationCode(this.Arguments.GetString("phoneNumber"), _phoneAuth.MCallbacks.mResendToken);
            }     
        }

        private void AuthSuccessHandler()
        {
            new AppPreferences(this._view.Context).SaveAccessKey(PreferenceField.PREFERENCE_AUTH_SSUCCESS, true);
            _timer.Stop();
            Intent intent = new Intent(this.Activity, typeof(MainAppActivity));
                StartActivity(intent);
        }
        private void InitFireBaseAuth()
        {
            _phoneAuth = new FireBasePhoneAuth();
            _phoneAuth.Activity = this.Activity;
            _phoneAuth.InitFirebaseAuth();
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

            _phoneAuth
                .VerifyPhoneNumberWithCode(_phoneAuth.MCallbacks.mVerificationId, codeEdit.Text);
        }
    }
}