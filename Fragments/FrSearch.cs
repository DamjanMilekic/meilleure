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

namespace EcommerceFrench.Fragments
{
    public class FrSearch : Fragment
    {

        private string value4Input;
        private Context mcontext;
        List<string> listModel = new List<string>();
        List<string> helpList = new List<string>();
        public override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            // Create your fragment here
        }

        public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {
            mcontext = container.Context;
            View view = inflater.Inflate(Resource.Layout.fragSearch, container, false);
            EditText input = view.FindViewById<EditText>(Resource.Id.searchInput);
            ListView list = view.FindViewById<ListView>(Resource.Id.listsearch);


            input.KeyPress += (object sender, View.KeyEventArgs e) => {
                e.Handled = false;
                if (e.Event.Action == KeyEventActions.Down && e.KeyCode == Keycode.Enter)
                {
                    value4Input = input.Text;

                    searchxml();
                    showXML(listModel);
                    ArrayAdapter adapter = new ArrayAdapter(mcontext, Android.Resource.Layout.SimpleListItem1, helpList);
                    list.Adapter = adapter;

                    e.Handled = true;
                }
            };

           

            return view;
        }
        private void showXML(List<string> inList)
        {
            XmlDocument xmldoc = new XmlDocument();
            xmldoc.Load("https://www.meilleurescpi.com/actualite-liste-xml/");
            XmlNodeList nodeList = xmldoc.DocumentElement.SelectNodes("/actualites/actualite ");

            foreach (var it in inList)
            {
                foreach (XmlNode node in nodeList)
                {
                    string s = node.Attributes["id"].Value.ToString();

                    if (s==it)
                    {
                        string m1 = node.SelectSingleNode("titre").InnerText;
                        helpList.Add(m1);

                    }

                }

            }
           

        }
        private  void searchxml()
        {
            XmlDocument xmldoc = new XmlDocument();
            xmldoc.Load("http://www.meilleurescpi.com/actualite-listeRub-xml");
            XmlNodeList nodelist = xmldoc.DocumentElement.SelectNodes("/motscles/motcle ");

            
            foreach (XmlNode node in nodelist)
            {
                string s = node.SelectSingleNode("mot").InnerText;
                
                if (s.Contains(value4Input))
                {
                    string m1 = node.Attributes["id"].Value.ToString();
                    listModel.Add(m1);
                   
                }

            }
            

        }  
    }
}