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
using EcommerceFrench.Models;
using System.Xml;
using Android.Views.InputMethods;
using System.Threading;

namespace EcommerceFrench.Fragments
{
    public class FrQuickSearch : Fragment
    {

        private string valueForInput;
        private Context mContext;

        List<string> listModel = new List<string>();
        List<string> showingList = new List<string>();
        List<ActualitesModel> helpList = new List<ActualitesModel>();

        public override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

        
        }

        public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {
            View view = inflater.Inflate(Resource.Layout.fragQuickSearch, container, false);
            mContext = container.Context;
            EditText inputEditText = view.FindViewById<EditText>(Resource.Id.QuicksearchInput);
            ListView listView = view.FindViewById<ListView>(Resource.Id.Quicklistsearch);
            ProgressDialog progressDialog = new ProgressDialog(mContext);

            progressDialog.SetMessage("S'il vous plaît, attendez..");
            progressDialog.SetProgressStyle(ProgressDialogStyle.Spinner);
            progressDialog.Progress = 0;
            progressDialog.Max = 100;
            progressDialog.Show();
            int prg = 0;

            string retrive = Arguments.GetString("searchQuery");

            valueForInput = retrive;
                     
            new Thread(new ThreadStart(delegate
            {

                ShowXml(listModel);

                Activity.RunOnUiThread(() => 
                {
                    while (prg < 100)
                    {
                        prg += 50;
                        progressDialog.Progress = prg;

                    }
                    ArrayAdapter adapter = new ArrayAdapter(mContext, Android.Resource.Layout.SimpleListItem1, showingList);

                    listView.Adapter = adapter;
                    progressDialog.Dismiss();

                    InputMethodManager hideKeyboard = (InputMethodManager)mContext.GetSystemService(Context.InputMethodService);
                    hideKeyboard.HideSoftInputFromWindow(inputEditText.WindowToken, 0);

                
                });     

            })).Start();


            inputEditText.EditorAction += (object sender, EditText.EditorActionEventArgs e) =>
            {
                e.Handled = false;
                if (e.ActionId == ImeAction.Search)
                {
                    valueForInput = "";
                    valueForInput = inputEditText.Text;

                    listModel.Clear();
                    helpList.Clear();
                    listView.Adapter = null;
                    ShowXml(listModel);

                    ArrayAdapter adapter = new ArrayAdapter(mContext, Android.Resource.Layout.SimpleListItem1, showingList);
                    listView.Adapter = adapter;
                    InputMethodManager hideKeyboardInSearchMode = (InputMethodManager)mContext.GetSystemService(Context.InputMethodService);
                    hideKeyboardInSearchMode.HideSoftInputFromWindow(inputEditText.WindowToken, 0);
                    e.Handled = true;
                }

            };
            listView.ItemClick += (s, e) =>
            {
                FrDetails fragmentDetails = new FrDetails();
                string[] obj = new string[5];

                obj[0] = helpList[e.Position].ActualiteID;
                obj[1] = helpList[e.Position].Date;
                obj[2] = helpList[e.Position].Titre;
                obj[3] = helpList[e.Position].Photo;
                obj[4] = helpList[e.Position].Details;

                Bundle buns = new Bundle();
                buns.PutStringArray("detailsID", obj);

                fragmentDetails.Arguments = buns;
                var tran = FragmentManager.BeginTransaction();
                tran.Add(Resource.Id.content_frame, fragmentDetails, "fragmentDetails");
                tran.Show(fragmentDetails);
                tran.AddToBackStack(null);
               
                tran.Commit();
                tran.Hide(this);

            };
            return view;
        }
        private void ShowXml(List<string> inList)
        {
            SearchXml();
            XmlDocument xmlDocument = new XmlDocument();
            xmlDocument.Load("https://www.meilleurescpi.com/actualite-liste-xml/");
            XmlNodeList nodeList = xmlDocument.DocumentElement.SelectNodes("/actualites/actualite ");

            foreach (var it in inList)
            {
                foreach (XmlNode node in nodeList)
                {
                    string s = node.Attributes["id"].Value.ToString();

                    if (s == it)
                    {
                        ActualitesModel actuModel = new ActualitesModel();

                        actuModel.ActualiteID = node.Attributes["id"].Value;
                        actuModel.Photo = node.SelectSingleNode("photo").InnerText;
                        actuModel.Titre = node.SelectSingleNode("titre").InnerText;
                        actuModel.Date = node.SelectSingleNode("date").InnerText;
                        actuModel.Details = node.SelectSingleNode("contenu").InnerText;
                        string strTitleList = node.SelectSingleNode("titre").InnerText;
                        showingList.Add(strTitleList);
                        helpList.Add(actuModel);

                    }

                }

            }


        }
        private void SearchXml()
        {
            XmlDocument xmlDocument = new XmlDocument();
            xmlDocument.Load("http://www.meilleurescpi.com/actualite-listeRub-xml");
            XmlNodeList nodelist = xmlDocument.DocumentElement.SelectNodes("/motscles/motcle ");


            foreach (XmlNode node in nodelist)
            {
                string singleNode = node.SelectSingleNode("mot").InnerText;

                if (singleNode.Contains(valueForInput))
                {
                    string strList = node.Attributes["id"].Value.ToString();
                    listModel.Add(strList);

                }

            }

        }
    }
}