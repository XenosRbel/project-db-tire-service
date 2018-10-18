using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Preferences;
using Android.Runtime;
using Android.Views;
using Android.Widget;

namespace Project_DB_Tire_Service_Client_Part.Utils
{
    public class AppPreferences
    {
        private ISharedPreferences mSharedPrefs;
        private ISharedPreferencesEditor mPrefsEditor;
        private Context mContext;

        private String PREFERENCE_COUNTNTRY_DATA = "PREFERENCE_COUNTNTRY_DATA";

        public AppPreferences(Context context)
        {
            this.mContext = context;
            mSharedPrefs = PreferenceManager.GetDefaultSharedPreferences(mContext);
            mPrefsEditor = mSharedPrefs.Edit();
        }

        public void SaveAccessKey(PreferenceField field, string value)
        {
            switch (field)
            {
                case PreferenceField.PREFERENCE_COUNTNTRY_DATA:
                    mPrefsEditor.PutString(PREFERENCE_COUNTNTRY_DATA, value);
                    mPrefsEditor.Commit();
                    break;
                default:
                    break;
            }
        }

        public string GetAccessKey(PreferenceField field)
        {
            switch (field)
            {
                case PreferenceField.PREFERENCE_COUNTNTRY_DATA:
                    return mSharedPrefs.GetString(PREFERENCE_COUNTNTRY_DATA, "");
                default:
                    return null;
            }
        }

        public void ClearAccessKey()
        {
            mPrefsEditor.Clear().Commit();
        }
    }

    public enum PreferenceField
    {
        PREFERENCE_COUNTNTRY_DATA
    }
}