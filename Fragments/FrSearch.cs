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
using Android.Views.InputMethods;
using System.Threading;

namespace EcommerceFrench.Fragments
{
    public class FrSearch : Fragment
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
           
            View view = inflater.Inflate(Resource.Layout.fragSearch, container, false);
            mContext = container.Context;
            EditText inputTextView = view.FindViewById<EditText>(Resource.Id.searchInput);
            ListView listView = view.FindViewById<ListView>(Resource.Id.listsearch);


            InputMethodManager hideKeyboard = (InputMethodManager)mContext.GetSystemService(Context.InputMethodService);
            hideKeyboard.HideSoftInputFromWindow(inputTextView.WindowToken, 0);

            inputTextView.EditorAction += (object sender, EditText.EditorActionEventArgs e) =>
            {
                int prg;
                e.Handled = false;
                if (e.ActionId == ImeAction.Search)
                {
                    ProgressDialog progressDialog = new ProgressDialog(mContext);
                    progressDialog.SetMessage("please wait");
                    progressDialog.SetProgressStyle(ProgressDialogStyle.Spinner);
                    progressDialog.Progress = 0;
                    progressDialog.Max = 100;
                    progressDialog.Show();
                    prg = 0;
                    listModel.Clear();
                    helpList.Clear();
                    listView.Adapter = null;
                    valueForInput = inputTextView.Text;
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
                          InputMethodManager hideKeyboardInSearchMode = (InputMethodManager)mContext.GetSystemService(Context.InputMethodService);
                          hideKeyboardInSearchMode.HideSoftInputFromWindow(inputTextView.WindowToken, 0);
                         
                          e.Handled = true;
                        });

                    })).Start();
           
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

                    if (s==it)
                    {
                        ActualitesModel actuModels = new ActualitesModel();

                        actuModels.ActualiteID = node.Attributes["id"].Value;
                        actuModels.Photo = node.SelectSingleNode("photo").InnerText;
                        actuModels.Titre = node.SelectSingleNode("titre").InnerText;
                        actuModels.Date = node.SelectSingleNode("date").InnerText;
                        actuModels.Details = node.SelectSingleNode("contenu").InnerText;
                        string strTitle = node.SelectSingleNode("titre").InnerText;
                        showingList.Add(strTitle);
                        helpList.Add(actuModels);

                    }

                }

            }
           

        }
        private  void SearchXml()
        {
            XmlDocument xmlDocument = new XmlDocument();
            xmlDocument.Load("http://www.meilleurescpi.com/actualite-listeRub-xml");
            XmlNodeList nodelist = xmlDocument.DocumentElement.SelectNodes("/motscles/motcle ");

            
            foreach (XmlNode node in nodelist)
            {
                string singleNode = node.SelectSingleNode("mot").InnerText;
                
                if (singleNode.Contains(valueForInput))
                {
                    string strId = node.Attributes["id"].Value.ToString();
                    listModel.Add(strId);
                   
                }

            }
            
        }  
    }
}