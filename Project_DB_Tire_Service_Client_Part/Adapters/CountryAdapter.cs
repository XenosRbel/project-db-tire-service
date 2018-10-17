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
using Java.Lang;

namespace Project_DB_Tire_Service_Client_Part.Adapters
{
    [Serializable]
    public class CountryData : Java.Lang.Object {
        private string _value;

        public CountryData(string value)
        {
            _value = value;
        }

        public string Value { get => _value; set => _value = value; }
    }

    class CountryAdapter : BaseAdapter
    {
        public override int Count => _data.Count;
        public Activity Activity { set; get; }
        private Dictionary<CountryData, CountryData> _data;
        private LayoutInflater _inflater;
        private View _view;

        public CountryAdapter(Activity context, Dictionary<CountryData, CountryData> data)
        {
            Activity = context;
            _data = data;

            if (_inflater == null)
            {
                _inflater = (LayoutInflater)Activity.GetSystemService(Context.LayoutInflaterService);
            }
        }

        public override Java.Lang.Object GetItem(int position)
        {
            return _data.ElementAt(position).Key;
        }

        public override long GetItemId(int position)
        {
            return position;
        }

        public override View GetView(int position, View convertView, ViewGroup parent)
        {
           var _view = convertView;

            if (_view == null)
            {
                _view = _inflater.Inflate(Resource.Layout.auth_fragment_countyList, parent, false);
            }

            TextView textCountry = _view.FindViewById<TextView>(Resource.Id.textCountryName);
            TextView textCountryCode = _view.FindViewById<TextView>(Resource.Id.textCountryCode);

            var item = _data.ElementAt(position);
            textCountry.Text = _data.ElementAt(position).Key.Value;
            textCountryCode.Text = _data.ElementAt(position).Value.Value;

            return _view;
        }
    }
}