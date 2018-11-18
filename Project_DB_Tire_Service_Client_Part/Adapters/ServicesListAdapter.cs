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
using Autoservice_Core.Entity;

namespace Project_DB_Tire_Service_Client_Part.Adapters
{
    class ServicesListAdapter : BaseAdapter<Services>
    {
        private List<Services> _items;
        private Activity _context;

        public ServicesListAdapter(Activity context)
        {
            this.Context = context;
        }

        public ServicesListAdapter(Activity context, List<Services> items)
            : base()
        {
            this._context = context;
            this._items = items;
        }

        public override Java.Lang.Object GetItem(int position)
        {
            return position;
        }

        public override Services this[int position] => Items[position];

        public override long GetItemId(int position)
        {
            return position;
        }

        public override View GetView(int position, View convertView, ViewGroup parent)
        {
            var item = Items[position];

            var view = convertView ?? Context.LayoutInflater.Inflate(Resource.Layout.item_service, null);

            view.FindViewById<TextView>(Resource.Id.text_name_service).Text = item.NameService;

            //var view = convertView;
            //ServicesListAdapterViewHolder holder = null;

            //if (view != null)
            //    holder = view.Tag as ServicesListAdapterViewHolder;

            //if (holder == null)
            //{
            //    holder = new ServicesListAdapterViewHolder();
            //    var inflater = context.GetSystemService(Context.LayoutInflaterService).JavaCast<LayoutInflater>();
            //    //replace with your item and your holder items
            //    //comment back in
            //    //view = inflater.Inflate(Resource.Layout.item, parent, false);
            //    //holder.Title = view.FindViewById<TextView>(Resource.Id.text);
            //    view.Tag = holder;
            //}


            ////fill in your items
            ////holder.Title.Text = "new text here";

            return view;
        }
        
        public override int Count => Items.Count;

        public List<Services> Items { get => _items; set => _items = value; }
        public Activity Context { get => _context; set => _context = value; }
    }

    class ServicesListAdapterViewHolder : Java.Lang.Object
    {
        //Your adapter views to re-use
        //public TextView Title { get; set; }
    }
}