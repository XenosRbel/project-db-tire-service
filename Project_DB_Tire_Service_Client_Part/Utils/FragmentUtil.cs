using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Support.V4.App;
using Android.Views;
using Android.Widget;

namespace Project_DB_Tire_Service_Client_Part.Utils
{
    class FragmentUtil
    {
        public FragmentUtil(Activity activity, Android.Support.V4.App.FragmentManager fragmentManager)
        {
            Activity = activity;
            FragmentManager = fragmentManager;
        }

        public Activity Activity { set; get; }
        public Android.Support.V4.App.FragmentManager FragmentManager { set; get; }

        public void CreateLoadView(int containerViewId, Android.Support.V4.App.Fragment fragmentView)
        {
            var transaction = this.FragmentManager.BeginTransaction();

            transaction.Replace(containerViewId, fragmentView)
               .AddToBackStack($"{fragmentView.GetType().Name}")
               .Commit();
        }
    }
}