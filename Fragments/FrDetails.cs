using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Util;
using Android.Views;
using Android.Widget;
using System.Xml;
using EcommerceFrench.Models;
using Android.Text;

namespace EcommerceFrench.Fragments
{
    public class FrDetails : Fragment
    {
        List<ActualitesModel> listModel = new List<ActualitesModel>();
        public static string data;
        public static string id,photourl,shead,sdate,scontent;
        public override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

        }

        public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {
            View view = inflater.Inflate(Resource.Layout.fragDetails, container, false);
            ImageView image = view.FindViewById<ImageView>(Resource.Id.imgDetails);
            TextView date = view.FindViewById<TextView>(Resource.Id.txtDate);
            TextView head = view.FindViewById<TextView>(Resource.Id.txtHeading);
            TextView content = view.FindViewById<TextView>(Resource.Id.txtContent);

            string[] retrive = Arguments.GetStringArray("detailsID");

            sdate = retrive[1];
            shead = retrive[2];
            photourl = retrive[3];
            scontent = retrive[4];
         
            var imgBitmap = Utility.GetImageBitmapFromUrl(photourl);
            image.SetImageBitmap(imgBitmap);
            head.Text = shead;
            date.Text = sdate;
            content.TextFormatted = Html.FromHtml(scontent);

            return view;
        }

       

    }
}