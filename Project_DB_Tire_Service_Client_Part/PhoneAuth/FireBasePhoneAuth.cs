using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.Gms.Common;
using Android.Gms.Tasks;
using Android.OS;
using Android.Runtime;
using Android.Support.V7.App;
using Android.Views;
using Android.Widget;
using Firebase;
using Firebase.Auth;
using Java.Util.Concurrent;

namespace Project_DB_Tire_Service_Client_Part.PhoneAuth
{
    [Serializable]
    class FireBasePhoneAuth : AppCompatActivity, IOnCompleteListener
    {
        public delegate void MethodContainer();
        public event MethodContainer OnAuthSuccessful;
        public event MethodContainer OnAuthFailed;

        public FireBasePhoneAuth()
        {
            MCallbacks = new PhoneAuthCallbacks();
        }

        public Activity Activity { set; get; }
        public string ResultInfo { get; private set; }
        public static FirebaseApp App { set; get; }
        public FirebaseAuth Auth { set; get; }
        public PhoneAuthCallbacks MCallbacks { set; get; }
        public PhoneAuthProvider.ForceResendingToken MResendToken { set; get; }

        public void InitFirebaseAuth()
        {
            var options = new FirebaseOptions.Builder()
               .SetApplicationId("bitcoin-simulator-175509")
               .SetApiKey("AIzaSyAVW03GBXJ489Hb1pKFOR73b0JhbfDWN8M")
               .Build();

            if (App == null)
            {
                App = FirebaseApp.InitializeApp(Activity, options);
            }

            Auth = FirebaseAuth.GetInstance(App);
        }

        public void StartPhoneNumberVerification(string phoneNumber)
        {
            PhoneAuthProvider.GetInstance(Auth).VerifyPhoneNumber(
                    phoneNumber,     
                    120,           
                    TimeUnit.Seconds,
                    Activity,            
                    MCallbacks);    
        }

        public void ResendVerificationCode(string phoneNumber, PhoneAuthProvider.ForceResendingToken token)
        {
            PhoneAuthProvider.GetInstance(Auth).VerifyPhoneNumber(
                    phoneNumber,    
                    60,            
                    TimeUnit.Seconds, 
                    Activity,             
                    MCallbacks,         
                    token);          
        }

        private void SignInWithPhoneAuthCredential(PhoneAuthCredential credential)
        {
            Auth.SignInWithCredential(credential).AddOnCompleteListener(Activity, this);
        }

        public void VerifyPhoneNumberWithCode(string verificationId, string code)
        {
            PhoneAuthCredential credential = PhoneAuthProvider.GetCredential(verificationId, code);

            SignInWithPhoneAuthCredential(credential);
        }

        void IOnCompleteListener.OnComplete(Task task)
        {
            if (task.IsSuccessful)
            {
                FirebaseUser user = Auth.CurrentUser;

                OnAuthSuccessful?.Invoke();
                Toast.MakeText(Activity, "Authentication Successful.", ToastLength.Short).Show();
            }
            else
            {
                OnAuthFailed?.Invoke();
                Toast.MakeText(Activity, "Authentication failed.", ToastLength.Short).Show();
            }
        }

        public bool IsPlayServicesAvailable()
        {
            int resultCode = GoogleApiAvailability.Instance.IsGooglePlayServicesAvailable(this);
            if (resultCode != ConnectionResult.Success)
            {
                if (GoogleApiAvailability.Instance.IsUserResolvableError(resultCode))
                    ResultInfo = GoogleApiAvailability.Instance.GetErrorString(resultCode);
                else
                {
                    ResultInfo = "This device is not supported";
                    Finish();
                }
                return false;
            }
            else
            {
                ResultInfo = "Google Play Services is available.";
                return true;
            }
        }
    }
}