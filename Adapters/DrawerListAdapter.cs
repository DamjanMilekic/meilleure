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
using EcommerceFrench.Models;
using System.Threading.Tasks;
using Android.Graphics;

namespace EcommerceFrench
{
  
    class DrawerListAdapter :BaseAdapter<MenuModel>
    {
        private List<MenuModel> mItems;
        private Context mContext;
       

        public DrawerListAdapter(Context context,List<MenuModel> items)
        {
            mItems = items;
            mContext = context;
           
        }
        public override int Count
        {
            get
            {
                return mItems.Count();
            }
        }

        public override MenuModel this[int position]
        {
            get
            {
                return mItems[position];
            }
        }
  
        public override long GetItemId(int position)
        {
            return position;
        }
       

        public override View GetView(int position, View convertView, ViewGroup parent)
        {
            View row = convertView;
            var item = mItems[position];

            if (row==null)
            {
                row = LayoutInflater.From(mContext).Inflate(Resource.Layout.drawableListRow, null, false);
            }

            ImageView image = row.FindViewById<ImageView>(Resource.Id.imageView1);
            TextView text = row.FindViewById<TextView>(Resource.Id.textView1);
            
                 text.Text = mItems[position].menuText;

                 image.SetImageResource(mItems[position].menuImage);

            return row;
        }
    }
}