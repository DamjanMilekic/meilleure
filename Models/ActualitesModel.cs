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

namespace EcommerceFrench.Models
{
    class ActualitesModel
    {
        public string ActualiteID { get; set; }
        public string Photo { get; set; }
        public string Titre { get; set; }
        public string Date { get; set; }
        public string Details { get; set; }

    }
}