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
using Android.Support.V4.View;
using EcommerceFrench;

namespace SlidingTabLayoutTutorial
{
    public class SlidingTabsFragment : Fragment
    {
        private SlidingTabScrollView mSlidingTabScrollView;
        private ViewPager mViewPager;

        public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {
            return inflater.Inflate(Resource.Layout.fragment_sample, container, false);
        }

        public override void OnViewCreated(View view, Bundle savedInstanceState)
        {
            mSlidingTabScrollView = view.FindViewById<SlidingTabScrollView>(Resource.Id.sliding_tabs);
            mViewPager = view.FindViewById<ViewPager>(Resource.Id.viewpager);
            mViewPager.Adapter = new SamplePagerAdapter();

            mSlidingTabScrollView.ViewPager = mViewPager;
        }

        public class SamplePagerAdapter : PagerAdapter
        {
            List<string> items = new List<string>();

            public SamplePagerAdapter() : base()
            {
                items.Add("Les SCPI");
                items.Add("Publications");
                items.Add("Presse");
             
            }

            public override int Count
            {
                get { return items.Count; }
            }

            public override bool IsViewFromObject(View view, Java.Lang.Object obj)
            {
                return view == obj;
            }

            public override Java.Lang.Object InstantiateItem(ViewGroup container, int position)
            {
                View view = LayoutInflater.From(container.Context).Inflate(Resource.Layout.pager_item, container, false);
                container.AddView(view);

               
                ImageView img1 = view.FindViewById<ImageView>(Resource.Id.imageView1);
                ImageView img2 = view.FindViewById<ImageView>(Resource.Id.imageView2);
                ImageView img3 = view.FindViewById<ImageView>(Resource.Id.imageView3);
                ImageView img4 = view.FindViewById<ImageView>(Resource.Id.imageView4);

                var imm = Utility.GetImageBitmapFromUrl("https://www.meilleurescpi.com/uploads/posts/3292/2017/01/Viseo%20SCPI%20PFO2.001.jpeg");
                img1.SetImageBitmap(imm);
                var imm2 = Utility.GetImageBitmapFromUrl("https://www.meilleurescpi.com/fichiers/actu/La_Francaise_acquisition_deux_immeubles_Issy_les_Moulineaux_8_SCPI.001.jpg");
                img2.SetImageBitmap(imm2);
                var imm3 = Utility.GetImageBitmapFromUrl("https://www.meilleurescpi.com/uploads/posts/3329/2017/02/boutiques.001.jpeg");
                img3.SetImageBitmap(imm3);
                var imm4 = Utility.GetImageBitmapFromUrl("https://www.meilleurescpi.com/uploads/posts/3327/2017/02/Lyon.001.jpeg");
                img4.SetImageBitmap(imm4);

                return view;
            }

            public string GetHeaderTitle(int position)
            {
                return items[position];
            }

            public override void DestroyItem(ViewGroup container, int position, Java.Lang.Object obj)
            {
                container.RemoveView((View)obj);
            }
        }
    }
}