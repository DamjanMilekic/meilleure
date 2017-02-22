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
using Android.Webkit;
using Square.Picasso;

namespace EcommerceFrench.Fragments
{
    public class FrDetails : Fragment
    {

       
        public static string  photoUrl, strHead, strDate, strContent;

        private Context mContext;

        List<ActualitesModel> listModel = new List<ActualitesModel>();
 
        public override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

        }

        public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {
            View view = inflater.Inflate(Resource.Layout.fragDetails, container, false);
            WebView webWiew = view.FindViewById<WebView>(Resource.Id.webView1);
            ImageView image = view.FindViewById<ImageView>(Resource.Id.image1);
            TextView date = view.FindViewById<TextView>(Resource.Id.txdate);
            TextView head = view.FindViewById<TextView>(Resource.Id.txhead);
            webWiew.Settings.JavaScriptEnabled = true;

            string[] retrieve = new string[5];

            retrieve = Arguments.GetStringArray("detailsID");

            strDate = retrieve[1];
            strHead = retrieve[2];
            photoUrl = retrieve[3];
            strContent = retrieve[4];
            Picasso.With(mContext).Load(photoUrl).Into(image);
         
            head.Text = strHead;
            date.Text = strDate;

            webWiew.LoadDataWithBaseURL("https://www.meilleurescpi.com/actualite-liste-xml/", strContent, "text/html", "UTF-8", "about:blank");

            return view;
        }

        public override void OnDetach()
        {
            base.OnDetach();

            var fr = FragmentManager.BeginTransaction();
            fr.Hide(this);
            fr.Commit();

        }

    }
}