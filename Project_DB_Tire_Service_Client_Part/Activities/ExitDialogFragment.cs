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

namespace Project_DB_Tire_Service_Client_Part.Activities
{
    public class ExitDialogFragment : Android.Support.V4.App.DialogFragment
    {
        private View _view;

        public override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
        }

        public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {
            _view = inflater.Inflate(Resource.Layout.dialog_fragment_exit_app, container, false);

            var btnExit = _view.FindViewById<Button>(Resource.Id.btn_exit_app);
            btnExit.Click += BtnExit_Click;

            var btnComplite = _view.FindViewById<Button>(Resource.Id.btn_complete);
            btnComplite.Click += BtnComplite_Click; ;

            return _view;
        }

        private void BtnComplite_Click(object sender, EventArgs e)
        {
            this.Dismiss();
        }

        private void BtnExit_Click(object sender, EventArgs e)
        {
            this.Activity.Finish();
        
        }
    }
}