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
using Android.Views.InputMethods;
using Android.Content.PM;

namespace EcommerceFrench
{
    [Activity( MainLauncher = true, Icon = "@drawable/icon",Theme="@style/CustomActionBarTheme", ScreenOrientation = ScreenOrientation.Portrait)]
    public class MainActivity : Activity
    {
        private List<MenuModel> leftItem = new List<MenuModel>();
        private ListView leftList;
        private DrawerLayout mDrawerLayout;
        private ActionBarDrawerToggle drawerToggle;
        private EditText searchText;
        private SearchView searchView;

        private Android.App.Fragment currentFragment;
        private FrSCPI fragmentSCPI;
        private FrSearch fragmentSearch;
        private FrActualites fragmentActualites;
      
       
       // public MainActivity() {}

        protected override void OnCreate(Bundle bundle)
        {
           
            base.OnCreate(bundle);
            SetContentView(Resource.Layout.Main);
            Utility.SetActionBarText(this, "meilleure");
            mDrawerLayout = FindViewById<DrawerLayout>(Resource.Id.myDrawer);
            leftList = FindViewById<ListView>(Resource.Id.leftListView);
            searchText = FindViewById<EditText>(Resource.Id.txSearch);

            
           

            leftList.Tag = 0;
            searchText.Tag = 1;
           
            fragmentActualites = new FrActualites();
            fragmentSCPI = new FrSCPI();
            fragmentSearch = new FrSearch();
          
        
           
            var tran = FragmentManager.BeginTransaction();
            tran.Add(Resource.Id.content_frame, fragmentSearch, "fragmentSearch");
            tran.Add(Resource.Id.content_frame, fragmentActualites, "fragmentActualites");
            tran.Add(Resource.Id.content_frame, fragmentSCPI, "FragmentSCPI");     
            tran.Show(fragmentSCPI);
            tran.Commit();

            var tranDialog = FragmentManager.BeginTransaction();
            BanerDialog dialfrag = new BanerDialog();
            dialfrag.Show(tranDialog, "dialog"); ;

            currentFragment = fragmentSCPI;

            AddDrawer();
           
            ActionBar.SetDisplayHomeAsUpEnabled(true);
            ActionBar.SetHomeButtonEnabled(true);
            ActionBar.SetDisplayShowTitleEnabled(true);

            Listeners();
        }

        private void Listeners()
        {
            
            mDrawerLayout.SetDrawerListener(drawerToggle);
            leftList.ItemClick += LeftList_ItemClick;

        }

        public override bool OnKeyDown(Keycode keyCode, KeyEvent e)
        {
            if (e.KeyCode == Keycode.Back)
            {
                var tr1 = FragmentManager.BeginTransaction();
                tr1.Show(currentFragment);               
                tr1.Commit();
            }

            return base.OnKeyDown(keyCode, e);
        }
     
        private void LeftList_ItemClick(object sender, AdapterView.ItemClickEventArgs e)
        {
            if (leftItem[e.Position].MenuText == "SCPI")
            {
   
                Android.App.Fragment fragmentDetails = FragmentManager.FindFragmentByTag("fragmentDetails");
                Android.App.Fragment fragmentQuick = FragmentManager.FindFragmentByTag("fragmentQuick");
                if (fragmentDetails != null)
                {
                    if (fragmentDetails is FrDetails)
                    {
                        var tr = FragmentManager.BeginTransaction();
                        FragmentManager.PopBackStack();
                        ShowFragment(fragmentSCPI);
                        tr.Commit();
                        var tran = FragmentManager.BeginTransaction();
                        BanerDialog banerDialog = new BanerDialog();
                        banerDialog.Show(tran, "dialog");
                        if (fragmentQuick != null)
                        {
                            if (fragmentQuick is FrQuickSearch)
                            {
                                var tr1 = FragmentManager.BeginTransaction();
                                tr1.Hide(fragmentQuick);
                                tr1.Commit();
                            }
                        }
                        mDrawerLayout.CloseDrawer(leftList);
                       

                    }
                }
               
              
                else
                {
                    ShowFragment(fragmentSCPI);
                    mDrawerLayout.CloseDrawer(leftList);
                    var tran = FragmentManager.BeginTransaction();
                    BanerDialog banerDial = new BanerDialog();
                    banerDial.Show(tran, "dialog");
                   
                }

              
            }
            if (leftItem[e.Position].MenuText == "ACTUALITES")
            {

                Android.App.Fragment fragmentDetails = FragmentManager.FindFragmentByTag("fragmentDetails");
                Android.App.Fragment fragmentQuick = FragmentManager.FindFragmentByTag("fragmentQuick");
                if (fragmentDetails != null)
                {
                    if (fragmentDetails is FrDetails)
                    {
                        var tr = FragmentManager.BeginTransaction();
                        FragmentManager.PopBackStack();
                        ShowFragment(fragmentActualites);
                        tr.Commit();
                        if (fragmentQuick != null)
                        {
                            if (fragmentQuick is FrQuickSearch)
                            {
                                var tr1 = FragmentManager.BeginTransaction();
                                tr1.Hide(fragmentQuick);
                                tr1.Commit();
                            }
                        }
                        mDrawerLayout.CloseDrawer(leftList);
                       
                    }
                }
                else
                {
                    ShowFragment(fragmentActualites);
                    mDrawerLayout.CloseDrawer(leftList);

                }

            }
            if (leftItem[e.Position].MenuText == "RECHERCHER")
            {

                Android.App.Fragment fragmentDetails = FragmentManager.FindFragmentByTag("fragmentDetails");
                Android.App.Fragment fragmentQuick = FragmentManager.FindFragmentByTag("fragmentQuick");
                if (fragmentDetails != null)
                {
                    if (fragmentDetails is FrDetails)
                    {
                        var tr = FragmentManager.BeginTransaction();
                        FragmentManager.PopBackStack();
                        ShowFragment(fragmentSearch);
                        tr.Commit();
                        if (fragmentQuick != null)
                        {
                            if (fragmentQuick is FrQuickSearch)
                            {
                                var tr1 = FragmentManager.BeginTransaction();
                                tr1.Hide(fragmentQuick);
                                tr1.Commit();

                            }
                        }
                        mDrawerLayout.CloseDrawer(leftList);
                    
                    }
                }
                else
                {
                    ShowFragment(fragmentSearch);
                    var tran = FragmentManager.BeginTransaction();
                
                    mDrawerLayout.CloseDrawer(leftList);
                }

            }

       }
       
        #region fragment

        public void ShowFragment(Android.App.Fragment fraggy)
        {
          
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
         
        public override bool OnCreateOptionsMenu(IMenu Imenu)
        {
            MenuInflater.Inflate(Resource.Menu.action_bar, Imenu);
            var item = Imenu.FindItem(Resource.Id.searchActionBar);

           
            var searchViewHelp = MenuItemCompat.GetActionView(item);
            
            searchView = searchViewHelp.JavaCast<SearchView>();
            
                 
             
            searchView.QueryTextSubmit += (s, e) =>
            {
                FrQuickSearch fragmentQuick = new FrQuickSearch();
                
                Bundle bun = new Bundle();
                bun.PutString("searchQuery",e.Query);

                fragmentQuick.Arguments = bun;

                var tran = FragmentManager.BeginTransaction();
                tran.Add(Resource.Id.content_frame, fragmentQuick, "fragmentQuick");

                tran.Show(fragmentQuick);
                tran.AddToBackStack(null);
                tran.Commit();
                tran.Hide(currentFragment);

                InputMethodManager hideKeyboard = (InputMethodManager)GetSystemService(Context.InputMethodService);
                hideKeyboard.HideSoftInputFromWindow(searchView.WindowToken, 0);

                e.Handled = true;
            };

            return true;
        }

       

        public override bool OnOptionsItemSelected(IMenuItem IMenuItem)
        {


            if (drawerToggle.OnOptionsItemSelected(IMenuItem))
            {
                if (mDrawerLayout.IsDrawerOpen(searchText))
                {
                    mDrawerLayout.CloseDrawer(searchText);
                }
                return true;
            }

            switch (IMenuItem.ItemId)
            {
                case Resource.Id.searchInput:
                    if (mDrawerLayout.IsDrawerOpen(searchText))
                    {
                        mDrawerLayout.CloseDrawer(searchText);
                    }
                    else
                    {
                        mDrawerLayout.CloseDrawer(leftList);
                        mDrawerLayout.OpenDrawer(searchText);
                    }
                    return true;



                default:
                    return base.OnOptionsItemSelected(IMenuItem);
            }
        }
        private void AddDrawer()
        {
            drawerToggle = new MyActionBarDrawerToggle(this, mDrawerLayout, Resource.Drawable.drawerbutton, Resource.String.open_drawer, Resource.String.close_drawer);
            AddDrawerMenu();
            DrawerListAdapter myDrawerAdapter = new DrawerListAdapter(this, leftItem);
            leftList.Adapter = myDrawerAdapter;
        }
        public void AddDrawerMenu()
        {
            MenuModel mod = new MenuModel();
            mod.MenuImage = Resource.Drawable.first;
            mod.MenuText = "SCPI";
            leftItem.Add(mod);
            MenuModel mod2 = new MenuModel();
            mod2.MenuImage = Resource.Drawable.second;
            mod2.MenuText = "OUTILS";
            leftItem.Add(mod2);
            MenuModel mod3 = new MenuModel();
            mod3.MenuImage = Resource.Drawable.third;
            mod3.MenuText = "ACTUALITES";
            leftItem.Add(mod3);
            MenuModel mod4 = new MenuModel();
            mod4.MenuImage = Resource.Drawable.fourth;
            mod4.MenuText = "RECHERCHER";
            leftItem.Add(mod4);
            MenuModel mod5 = new MenuModel();
            mod5.MenuImage = Resource.Drawable.fifth;
            mod5.MenuText = "SOCIETE";
            leftItem.Add(mod5);
            MenuModel mod6 = new MenuModel();
            mod6.MenuImage = Resource.Drawable.sixth;
            mod6.MenuText = "CONTACTEZ-NOUZ";
            leftItem.Add(mod6);

        }
        #endregion

      
    }
  
}

