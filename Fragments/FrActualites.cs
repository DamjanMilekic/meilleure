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
      
        private Context mContext;

        List<ActualitesModel> listModel = new List<ActualitesModel>();

        public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {

            mContext = container.Context;
            View view = inflater.Inflate(Resource.Layout.fragactivities, container, false);

            ListView listViewActualites = view.FindViewById<ListView>(Resource.Id.listView1);

            ActualitesListAdapter listAdapter = new ActualitesListAdapter(mContext, listModel);
            listViewActualites.Adapter = listAdapter;

            LoadXml();
            listViewActualites.ItemClick += List_ItemClick;

            return view;

        }

    
        private void List_ItemClick(object sender, AdapterView.ItemClickEventArgs e)
        {
           FrDetails details = new FrDetails();
            string[] obj = new string[5];

            obj[0] = listModel[e.Position].ActualiteID;
            obj[1] = listModel[e.Position].Date;
            obj[2] = listModel[e.Position].Titre;
            obj[3] = listModel[e.Position].Photo;
            obj[4] = listModel[e.Position].Details;


             Bundle buns = new Bundle();
             buns.PutStringArray("detailsID", obj);

            details.Arguments = buns; 
            var tran = FragmentManager.BeginTransaction();
            tran.Add(Resource.Id.content_frame, details, "fragmentDetails");
            tran.Show(details);
            tran.AddToBackStack(null);
            tran.Commit();
            tran.Hide(this);


        }
     

        private void LoadXml()
        {

            try
            {
                XmlDocument xmlDoc = new XmlDocument();
                xmlDoc.Load("https://www.meilleurescpi.com/actualite-liste-xml/");
                XmlNodeList nodeList = xmlDoc.DocumentElement.SelectNodes("/actualites/actualite ");
                foreach (XmlNode node in nodeList)
                {
                    ActualitesModel actuModel = new ActualitesModel();

                    actuModel.ActualiteID = node.Attributes["id"].Value;
                    actuModel.Photo = node.SelectSingleNode("photo").InnerText;
                    actuModel.Titre = node.SelectSingleNode("titre").InnerText;
                    actuModel.Date = node.SelectSingleNode("date").InnerText;
                    actuModel.Details = node.SelectSingleNode("contenu").InnerText;
                    listModel.Add(actuModel);
                }
            }
            catch (Exception e)
            {

                throw new Exception(e.Message);
            }
      
        }
         
        
    }
}