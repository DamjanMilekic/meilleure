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
using Android.Graphics;
using System.Net;
using System.Xml;
using EcommerceFrench.Models;

namespace EcommerceFrench
{
    class Utility
    {

        

        public static Bitmap GetImageBitmapFromUrl(string url)
        {
            Bitmap imageBitmap = null;

            using (var webClient = new WebClient())
            {
                var imageBytes = webClient.DownloadData(url);
                if (imageBytes != null && imageBytes.Length > 0)
                {
                    imageBitmap = BitmapFactory.DecodeByteArray(imageBytes, 0, imageBytes.Length);
                }
            }

            return imageBitmap;
        }

        public static void SetActionbarText(Activity activity, string text)
        {
           
            activity.ActionBar.SetHomeButtonEnabled(false);
            activity.ActionBar.SetIcon(Android.Resource.Color.Transparent);
            activity.ActionBar.SetDisplayShowCustomEnabled(true);
            activity.ActionBar.Title = "";

            LinearLayout linearLayout = new LinearLayout(activity);
            linearLayout.SetGravity(GravityFlags.CenterVertical);

            LinearLayout.LayoutParams textViewParameters =
                new LinearLayout.LayoutParams(LinearLayout.LayoutParams.MatchParent, LinearLayout.LayoutParams.MatchParent);
            textViewParameters.RightMargin = (int)(40 * activity.Resources.DisplayMetrics.Density);
            TextView modelTitle = new TextView(activity);
            modelTitle.Text = text;
            modelTitle.Gravity = GravityFlags.Center;
            modelTitle.SetTextColor(Android.Graphics.Color.White);
            modelTitle.SetTextSize(Android.Util.ComplexUnitType.Dip, 50);
            linearLayout.AddView(modelTitle, textViewParameters);
            ActionBar.LayoutParams actionbarParams =
                new ActionBar.LayoutParams(ActionBar.LayoutParams.MatchParent, ActionBar.LayoutParams.MatchParent);
            activity.ActionBar.SetCustomView(linearLayout, actionbarParams);
        }


       

    }
}