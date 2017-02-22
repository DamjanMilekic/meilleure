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

namespace EcommerceFrench
{
    class BanerDialog : DialogFragment
    {
        private ImageButton icon;
        private ImageView image;

        public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {

            View view = inflater.Inflate(Resource.Layout.dialog,container,false);
            image = view.FindViewById<ImageView>(Resource.Id.imgBaner);
            icon = view.FindViewById<ImageButton>(Resource.Id.xicon);

            icon.Click += (s, e) =>
            {
                Dismiss();

            };

            image.Click += (s, e) =>
             {
                 var uri = Android.Net.Uri.Parse("https://nest.com/thermostat/meet-nest-thermostat/");
                 var intent = new Intent(Intent.ActionView, uri);
                 StartActivity(intent);
             };

            return view;
        }


        public override void OnActivityCreated(Bundle savedInstanceState)
        {
            Dialog.Window.RequestFeature(WindowFeatures.NoTitle);
            base.OnActivityCreated(savedInstanceState);
        }

        
    }
}