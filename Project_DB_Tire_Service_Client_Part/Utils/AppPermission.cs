using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Android;
using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;

namespace Project_DB_Tire_Service_Client_Part.Utils
{
    class AppPermission
    {
        private Activity _activity;
        private ContextWrapper _context;

        readonly string[] _permissionsArray =
        {
            Manifest.Permission.ReadExternalStorage,
            Manifest.Permission.WriteExternalStorage,
            Manifest.Permission.Internet,
            Manifest.Permission.AccessNetworkState,
            Manifest.Permission.AccessWifiState,
            Manifest.Permission.ReadPhoneState,
            Manifest.Permission.ReadPhoneNumbers,
            Manifest.Permission.ReceiveSms,
            Manifest.Permission.ReadSms
        };

        const int RequestCodeId = 0;

        public AppPermission(Activity activity, ContextWrapper context)
        {
            _activity = activity;
            _context = context;
        }

        private Task PermissionAsync()
        {
            //Finally request permissions with the list of permissions and Id
            return Task.Run(() => { _activity.RequestPermissions(_permissionsArray, RequestCodeId); });
        }

        public async void GetPermissionsAsync()
        {
            await PermissionAsync();
        }
    }
}