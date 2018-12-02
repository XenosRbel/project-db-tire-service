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
using Project_DB_Tire_Service_Client_Part.Utils;
using System;

namespace Project_DB_Tire_Service_Client_Part
{
    [Activity(Label = "@string/app_name", Theme = "@style/AppTheme")]
    class MainActivity : AppCompatActivity
    {
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            new AppPermission(this, this).GetPermissionsAsync();

            var preferences = new AppPreferences(this.BaseContext);

            var data = Convert.ToBoolean(preferences.GetAccessKey(PreferenceField.PREFERENCE_AUTH_SSUCCESS));
            if (data)
            {
                Intent intent = new Intent(this, typeof(MainAppActivity));
                StartActivity(intent);
                this.Finish();
            }

            SetContentView(Resource.Layout.activity_main);

            new FragmentUtil(this, this.SupportFragmentManager)
                .CreateLoadView(Resource.Id.parent_fragment_container, new AuthFragment());
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
    }
}

