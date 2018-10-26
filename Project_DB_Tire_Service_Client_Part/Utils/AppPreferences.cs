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

        private readonly String PREFERENCE_COUNTNTRY_DATA = "PREFERENCE_COUNTNTRY_DATA";
        private readonly String PREFERENCE_AUTH_SSUCCESS = "PREFERENCE_AUTH_SSUCCESS";

        public AppPreferences(Context context)
        {
            this.mContext = context;
            mSharedPrefs = PreferenceManager.GetDefaultSharedPreferences(mContext);
            mPrefsEditor = mSharedPrefs.Edit();
        }

        public void SaveAccessKey(PreferenceField field, object value)
        {
            switch (field)
            {
                case PreferenceField.PREFERENCE_COUNTNTRY_DATA:
                    {
                        mPrefsEditor.PutString(PREFERENCE_COUNTNTRY_DATA, Convert.ToString(value));
                        mPrefsEditor.Commit();
                        break;
                    }
                case PreferenceField.PREFERENCE_AUTH_SSUCCESS:
                    {
                        mPrefsEditor.PutBoolean(PREFERENCE_AUTH_SSUCCESS, Convert.ToBoolean(value));
                        mPrefsEditor.Commit();
                        break;
                    }
                default:
                    break;
            }
        }

        public object GetAccessKey(PreferenceField field)
        {
            try
            {
                switch (field)
                {
                    case PreferenceField.PREFERENCE_COUNTNTRY_DATA:
                    {
                        return mSharedPrefs.GetString(PREFERENCE_COUNTNTRY_DATA, "");
                    }
                    case PreferenceField.PREFERENCE_AUTH_SSUCCESS:
                    {
                        return mSharedPrefs.GetBoolean(PREFERENCE_AUTH_SSUCCESS, false);
                    }
                    default:
                        return null;
                }
            }
            catch (Exception e)
            {
                ClearAccessKey();
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
        PREFERENCE_COUNTNTRY_DATA,
        PREFERENCE_AUTH_SSUCCESS
    }
}