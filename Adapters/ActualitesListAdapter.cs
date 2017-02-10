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
using Android.Graphics;
using System.Net;


namespace EcommerceFrench
{
    class ActualitesListAdapter : BaseAdapter<ActualitesModel>
    {
        Utility util = new Utility();
        private List<ActualitesModel> mItems;
        private Context mContext;
      

        public ActualitesListAdapter(Context  context, List<ActualitesModel> items)
        {
            mContext = context;
            mItems = items;
        }


        public override ActualitesModel this[int position]
        {
            get
            {
                return mItems[position];
            }
        }

        public override int Count
        {
            get
            {
                return mItems.Count();
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
                row = LayoutInflater.From(mContext).Inflate(Resource.Layout.activListRow, null);
            }
           
            ImageView image = row.FindViewById<ImageView>(Resource.Id.imgViewUrl);
            TextView textHead = row.FindViewById<TextView>(Resource.Id.txHeadings);
            TextView textDate = row.FindViewById<TextView>(Resource.Id.txDate);


           
            var imgBitmap = Utility.GetImageBitmapFromUrl(mItems[position].photo);
            image.SetImageBitmap(imgBitmap);
            textHead.Text = mItems[position].titre;
            textDate.Text = mItems[position].date;

            return row;
        }
    
    }
}