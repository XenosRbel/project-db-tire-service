using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Support.Design.Widget;
using Android.Support.V4.App;
using Android.Support.V7.App;
using Android.Views;
using Android.Widget;
using Project_DB_Tire_Service_Client_Part.Utils;

namespace Project_DB_Tire_Service_Client_Part.Activities
{
    [Activity(Label = "@string/app_name", Theme = "@style/AppTheme")]
    public class MainAppActivity : AppCompatActivity, BottomNavigationView.IOnNavigationItemSelectedListener
    {
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.activity_main_app);

            new FragmentUtil(this, this.SupportFragmentManager)
                .CreateLoadView(Resource.Id.fragment_main_container, new AuthCountryFragment());

            BottomNavigationView navigation = FindViewById<BottomNavigationView>(Resource.Id.navigation_menu);
            navigation.SetOnNavigationItemSelectedListener(this);
        }

        public bool OnNavigationItemSelected(IMenuItem item)
        {
            switch (item.ItemId)
            {
                case Resource.Id.navigation_home:
                {
                    return true;
                }
                case Resource.Id.navigation_services:
                {
                    new FragmentUtil(this, this.SupportFragmentManager)
                        .CreateLoadView(Resource.Id.fragment_main_container, new ServicesFragment());
                    return true;
                }
                case Resource.Id.navigation_notifications:
                {
                    return true;
                }
            }
            return false;
        }

        //public override void OnBackPressed() { }
    }
}