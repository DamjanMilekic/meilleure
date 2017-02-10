using System;
using Android.App;
using Android.Content;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.OS;
using Android.Support.V4.App;
using System.Collections.Generic;
using Android.Support.V4.Widget;
using EcommerceFrench.Models;
using EcommerceFrench.Fragments;
using Android.Support.V4.View;
using SlidingTabLayoutTutorial;
using Java.Lang;
using System.Threading;
using System.Xml;

namespace EcommerceFrench
{
    [Activity( MainLauncher = true, Icon = "@drawable/icon",Theme="@style/CustomActionBarTheme")]
    public class MainActivity : Activity
    {
        private   List<MenuModel> leftItem = new List<MenuModel>();
        private ListView leftList;
        private DrawerLayout mdrawerLayout;
        private ActionBarDrawerToggle drawerToggle;
        private  EditText search;
        private Android.App.Fragment currentFragment;
        private FrSCPI fragmentSCPI;
        private FrSearch fragmentSearch;
        private FrActualites fragmentActualites;

        public MainActivity() {}

        protected override void OnCreate(Bundle bundle)
        {
           
            base.OnCreate(bundle);
            SetContentView(Resource.Layout.Main);
            Utility.SetActionbarText(this, "meilleure");
            mdrawerLayout = FindViewById<DrawerLayout>(Resource.Id.myDrawer);
            leftList = FindViewById<ListView>(Resource.Id.leftListView);
            search = FindViewById<EditText>(Resource.Id.txsearch);

            leftList.Tag = 0;
            search.Tag = 1;
           
            fragmentActualites = new FrActualites();
            fragmentSCPI = new FrSCPI();
            fragmentSearch = new FrSearch();
           
             var tran = FragmentManager.BeginTransaction();
             tran.Add(Resource.Id.content_frame, fragmentSCPI, "FragmentSCPI");
             tran.Commit();


            var tranz = FragmentManager.BeginTransaction();

            BanerDialog dialfrag = new BanerDialog();
            dialfrag.Show(tranz, "dialog"); ;

            currentFragment = fragmentSCPI;

            addDrawer();
           
            ActionBar.SetDisplayHomeAsUpEnabled(true);
            ActionBar.SetHomeButtonEnabled(true);
            ActionBar.SetDisplayShowTitleEnabled(true);

            listeners();
        }
     
        private void listeners()
        {
            mdrawerLayout.SetDrawerListener(drawerToggle);
            leftList.ItemClick += LeftList_ItemClick;

        }

        private void LeftList_ItemClick(object sender, AdapterView.ItemClickEventArgs e)
        {
            if (leftItem[e.Position].menuText == "SCPI")
            {
               
                if (currentFragment == fragmentSCPI)
                {
                    mdrawerLayout.CloseDrawer(leftList);
                    return; 
                }
                replaceFrag(fragmentSCPI);
                var tran = FragmentManager.BeginTransaction();
              
                BanerDialog dialfrag = new BanerDialog();
                dialfrag.Show(tran, "dialog");

                mdrawerLayout.CloseDrawer(leftList);
            }
            if (leftItem[e.Position].menuText == "ACTUALITES")
            {
                mdrawerLayout.CloseDrawer(leftList);
            
                replaceFrag(fragmentActualites);
             
            }
            if (leftItem[e.Position].menuText == "RECHERCHER")
            {
                replaceFrag(fragmentSearch);

                mdrawerLayout.CloseDrawer(leftList);

            }
            }

        #region fragment
        public void replaceFrag(Android.App.Fragment replacable)
        {

            var trans = FragmentManager.BeginTransaction();
            trans.Replace(Resource.Id.content_frame, replacable);
            trans.AddToBackStack(null);
            trans.Commit();
            currentFragment = replacable;

        }
        public void showFrag(Android.App.Fragment fraggy)
        {
            //unused
            var trans = FragmentManager.BeginTransaction();
            if (currentFragment != null)
            {
                trans.Hide(currentFragment);
            }
            trans.Show(fraggy);
            trans.AddToBackStack(null);
            trans.Commit();

            currentFragment = fraggy;

        }
        #endregion fragment
        #region drawer

        protected override void OnPostCreate(Bundle savedInstanceState)
        {
            base.OnPostCreate(savedInstanceState);

            drawerToggle.SyncState();
        }
        public override void OnConfigurationChanged(Android.Content.Res.Configuration newConfig)
        {
            base.OnConfigurationChanged(newConfig);
            drawerToggle.OnConfigurationChanged(newConfig);

        }
        public override bool OnCreateOptionsMenu(IMenu menu)
        {
            MenuInflater.Inflate(Resource.Menu.action_bar, menu);
            return base.OnCreateOptionsMenu(menu);
        }
        public override bool OnOptionsItemSelected(IMenuItem item)
        {


            if (drawerToggle.OnOptionsItemSelected(item))
            {
                if (mdrawerLayout.IsDrawerOpen(search))
                {
                    mdrawerLayout.CloseDrawer(search);
                }
                return true;
            }

            switch (item.ItemId)
            {
                case Resource.Id.search:
                    if (mdrawerLayout.IsDrawerOpen(search))
                    {
                        mdrawerLayout.CloseDrawer(search);
                    }
                    else
                    {
                        mdrawerLayout.CloseDrawer(leftList);
                        mdrawerLayout.OpenDrawer(search);
                    }
                    return true;



                default:
                    return base.OnOptionsItemSelected(item);
            }
        }
        private void addDrawer()
        {
            drawerToggle = new MyActionBarDrawerToggle(this, mdrawerLayout, Resource.Drawable.drawerbutton, Resource.String.open_drawer, Resource.String.close_drawer);
            addDrawerMenu();
            DrawerListAdapter myDrawerAdapter = new DrawerListAdapter(this, leftItem);
            leftList.Adapter = myDrawerAdapter;
        }
        public void addDrawerMenu()
        {
            MenuModel mod = new MenuModel();
            mod.menuImage = Resource.Drawable.first;
            mod.menuText = "SCPI";
            leftItem.Add(mod);
            MenuModel mod2 = new MenuModel();
            mod2.menuImage = Resource.Drawable.second;
            mod2.menuText = "OUTILS";
            leftItem.Add(mod2);
            MenuModel mod3 = new MenuModel();
            mod3.menuImage = Resource.Drawable.third;
            mod3.menuText = "ACTUALITES";
            leftItem.Add(mod3);
            MenuModel mod4 = new MenuModel();
            mod4.menuImage = Resource.Drawable.fourth;
            mod4.menuText = "RECHERCHER";
            leftItem.Add(mod4);
            MenuModel mod5 = new MenuModel();
            mod5.menuImage = Resource.Drawable.fifth;
            mod5.menuText = "SOCIETE";
            leftItem.Add(mod5);
            MenuModel mod6 = new MenuModel();
            mod6.menuImage = Resource.Drawable.sixth;
            mod6.menuText = "CONTACTEZ-NOUZ";
            leftItem.Add(mod6);

        }
#endregion
    }
}

