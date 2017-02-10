using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


using Android.OS;
using Android.Runtime;
using Android.Util;
using Android.Views;
using Android.Widget;
using EcommerceFrench.Models;
using Android.Content;
using System.Xml;
using Android.App;
using System.Threading.Tasks;
using System.Threading;

namespace EcommerceFrench.Fragments
{
    public class FrActualites : Fragment
    {
       List<ActualitesModel> listModel = new List<ActualitesModel>();
        private Context mcontext;

       

        public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {

            mcontext = container.Context;
            View view = inflater.Inflate(Resource.Layout.fragactivities, container, false);
            
            ListView list = view.FindViewById<ListView>(Resource.Id.listView1);

            //DialogFragmentt progress = DialogFragmentt.NewInstance("Page de chargement",
            //"S'il vous plaît, attendez", true, false);
            //progress.Show(this.FragmentManager.BeginTransaction(), "PROGRESS");

            //System.Threading.ThreadPool.QueueUserWorkItem(o =>
            //{
            //    System.Threading.Thread.Sleep(3000);
            //    RunOnUiThread(() => { progress.Dismiss(); });
            //});

            ActualitesListAdapter adapter = new ActualitesListAdapter(mcontext, listModel);
            list.Adapter = adapter;

            LoadXML();
            list.ItemClick += List_ItemClick;

            return view;

        }

        
         private void List_ItemClick(object sender, AdapterView.ItemClickEventArgs e)
        {
            FrDetails details = new FrDetails();
            string id = listModel[e.Position].actualiteID;
            string date = listModel[e.Position].date;
            string title = listModel[e.Position].titre;
            string photo = listModel[e.Position].photo;
            string content = listModel[e.Position].details;

            string[] obj = new string[5];
            obj[0] = id;
            obj[1] = date;
            obj[2] = title;
            obj[3] = photo;
            obj[4] = content;

            Bundle buns = new Bundle();
            buns.PutStringArray("detailsID", obj);

            details.Arguments = buns;
           
            var tran = FragmentManager.BeginTransaction();
            tran.Replace(Resource.Id.content_frame, details);
            tran.Hide(new FrActualites());
            tran.AddToBackStack(null);
            tran.Commit();
        }
     

        private void LoadXML()
        {
          
            XmlDocument xmlDoc = new XmlDocument();
            xmlDoc.Load("https://www.meilleurescpi.com/actualite-liste-xml/");
            XmlNodeList nodeList = xmlDoc.DocumentElement.SelectNodes("/actualites/actualite ");
            foreach (XmlNode node in nodeList)
            {
                ActualitesModel actumod = new ActualitesModel();

                actumod.actualiteID = node.Attributes["id"].Value;
                actumod.photo = node.SelectSingleNode("photo").InnerText;
                actumod.titre = node.SelectSingleNode("titre").InnerText;
                actumod.date = node.SelectSingleNode("date").InnerText;
                actumod.details = node.SelectSingleNode("contenu").InnerText;
                listModel.Add(actumod);
             
               
            }
         
        }
    }
}