using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Java.Util;

namespace Project_DB_Tire_Service_Client_Part.Utils
{
    class CountryData
    {
        public CountryData(Activity activity)
        {
            Activity = activity;
        }

        public Activity Activity { set; get; }

        public string[] GetCountryData()
        {
            var countryMatrix = this.Activity.Resources.GetStringArray(Resource.Array.countryCodes).Select(x => x.Split(' ')).ToArray();
            var countryArray = new string[countryMatrix.Length];

            for (int i = 0; i < countryMatrix.Length; i++)
            {
                countryArray[i] = ($"{new Locale("", countryMatrix[i][0]).DisplayCountry}\t{countryMatrix[i][1]}");
            }

            return countryArray;
        }

        public Dictionary<string, string> GetCountryDictonary()
        {
            var country = GetCountryData();

            return country.Select(t => t.Split('\t')).ToDictionary(item => item[0], item => item[1]);
        }
    }
}