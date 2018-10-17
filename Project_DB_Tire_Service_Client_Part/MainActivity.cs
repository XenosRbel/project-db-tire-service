using Android.App;
using Android.OS;
using Android.Runtime;
using Android.Support.Design.Widget;
using Android.Support.V7.App;
using Android.Views;
using Android.Widget;
using Android.Gms.Common;
using Firebase.Auth;
using Firebase.Iid;
using Android.Util;
using Firebase;
using Java.Util.Concurrent;
using Project_DB_Tire_Service_Client_Part.PhoneAuth;
using Android.Gms.Tasks;
using Project_DB_Tire_Service_Client_Part.Activities;
using Android.Telephony;
using Android.Content;
using Java.Util;

namespace Project_DB_Tire_Service_Client_Part
{
    [Activity(Label = "@string/app_name", Theme = "@style/AppTheme", MainLauncher = true)]
    public class MainActivity : AppCompatActivity/*, BottomNavigationView.IOnNavigationItemSelectedListener*/
    {
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.activity_main);

            var ft = this.SupportFragmentManager.BeginTransaction();
            ft.Replace(Resource.Id.parent_fragment_container, new AuthFragment()).AddToBackStack("fragment_auth");
            ft.Commit();

            //var authFragment = new AuthFragment();
            //SupportFragmentManager.BeginTransaction().Replace(Resource.Id.app_reg_frame, authFragment).Commit();

            //BottomNavigationView navigation = FindViewById<BottomNavigationView>(Resource.Id.navigation_menu);
            //navigation.SetOnNavigationItemSelectedListener(this);

            TelephonyManager tm = (TelephonyManager)GetSystemService(Context.TelephonyService);
            var ss = tm.SimCountryIso;
            Locale loc = new Locale("", ss);
            var a = loc.DisplayCountry;
            string countryCode = tm.GetSimState(1).ToString();

            //phoneAuth = new FireBasePhoneAuth();
            //phoneAuth.Activity = this;
            //phoneAuth.InitFirebaseAuth();

            //var countryEdit = FindViewById<EditText>(Resource.Id.edit_auth_contry);
            //countryEdit.Click += CountryEdit_Click;
        }
        public override void OnBackPressed()
        {
            if (this.SupportFragmentManager.BackStackEntryCount == 0)
            {
                base.OnBackPressed();
            }
            else if (this.SupportFragmentManager.BackStackEntryCount == 1)
            {
                var manager = this.SupportFragmentManager;
                var dialog = new ExitDialogFragment();
                dialog.Show(manager, "dialog_exit");
            }
            else if (this.SupportFragmentManager.BackStackEntryCount > 1)
            {
                this.SupportFragmentManager.PopBackStack();
            }
        }

        //public bool OnNavigationItemSelected(IMenuItem item)
        //{
        //    switch (item.ItemId)
        //    {
        //        case Resource.Id.navigation_home:

        //            phoneAuth.IsPlayServicesAvailable();
        //            phoneAuth.VerifyPhoneNumberWithCode(phoneAuth.MCallbacks.mVerificationId, "123456");
        //            return true;
        //        case Resource.Id.navigation_dashboard:

        //            phoneAuth.StartPhoneNumberVerification("+375293452225");
        //            return true;
        //        case Resource.Id.navigation_notifications:

        //            phoneAuth.ResendVerificationCode("+375293452225", phoneAuth.MCallbacks.mResendToken);
        //            return true;
        //    }
        //    return false;
        //}
    }
}

