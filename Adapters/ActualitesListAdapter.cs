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
using Square.Picasso;

namespace EcommerceFrench
{
    class ActualitesListAdapter : BaseAdapter<ActualitesModel>
    {
       
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
            MyViewHolder holder = null;
            var view = convertView; 
            var item = mItems[position];
          
            if (view!=null)
            
                holder = view.Tag as MyViewHolder;
               
            
            if (holder==null)
            {
                holder = new MyViewHolder();
                view = LayoutInflater.From(mContext).Inflate(Resource.Layout.activListRow, null);
                holder.img = view.FindViewById<ImageView>(Resource.Id.imgViewUrl);
                holder.txHead= view.FindViewById<TextView>(Resource.Id.txHeadings);
                holder.txDate = view.FindViewById<TextView>(Resource.Id.txDate);
                view.Tag = holder;
            }



            Picasso.With(mContext).Load(mItems[position].Photo).Into(holder.img);
           


            holder.txHead.Text = mItems[position].Titre;
            holder.txDate.Text = mItems[position].Date;

            return view;
        }

        public class MyViewHolder : Java.Lang.Object
        {
            public ImageView img { get; set; }
            public TextView txHead { get; set; }
            public TextView txDate { get; set; }
        }

    }
 
}