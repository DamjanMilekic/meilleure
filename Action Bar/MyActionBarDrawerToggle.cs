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
using Android.Support.V4.Widget;
using Android.Support.V4.App;



namespace EcommerceFrench
{
    class MyActionBarDrawerToggle: ActionBarDrawerToggle
    {

        public MyActionBarDrawerToggle(Activity activity, DrawerLayout drawerLayout, int imageResource, int openDrawerDesc, int closeDrawerDesc)
        : base(activity, drawerLayout, imageResource, openDrawerDesc, closeDrawerDesc)
        {

        }
        public override void OnDrawerOpened(View drawerView)
        {
            int drawerType = (int)drawerView.Tag;

            if (drawerType == 0)
            {
                base.OnDrawerOpened(drawerView);
            }
            
        }
       
        public override void OnDrawerClosed(View drawerView)
        {
            int drawerType = (int)drawerView.Tag;
            if (drawerType == 0)
            {
                base.OnDrawerClosed(drawerView);
            }
           
        }
        public override void OnDrawerSlide(View drawerView, float slideOffset)
        {
            int drawerType = (int)drawerView.Tag;
            if (drawerType == 0)
            {
                base.OnDrawerSlide(drawerView, slideOffset);
            }
           
        }

    }
}