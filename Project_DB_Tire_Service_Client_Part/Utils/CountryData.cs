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
            var data = new Dictionary<string, string>();
            var country = GetCountryData();

            for (int i = 0; i < country.Length; i++)
            {
                var item = country[i].Split('\t');
                data.Add(item[0], item[1]);
            }

            return data;
        }
    }
}