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
        public string actualiteID { get; set; }
        public string photo { get; set; }
        public string titre { get; set; }
        public string date { get; set; }
        public string details { get; set; }

    }
}