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

namespace EcommerceFrench.Fragments
{
    class DialogFragmentt : DialogFragment
    {
        public static DialogFragmentt NewInstance(string Title,
       string Message, bool Indeterminate, bool Cancelable)
        {
            DialogFragmentt progress = new DialogFragmentt();

            Bundle args = new Bundle();
            args.PutString("Title", Title);
            args.PutString("Message", Message);
            args.PutBoolean("Indeterminate", Indeterminate);

            progress.Arguments = args;
            progress.Cancelable = Cancelable;
            return progress;
        }

        public override Dialog OnCreateDialog(Bundle savedInstanceState)
        {
            ProgressDialog dialog = new ProgressDialog(Activity);
            dialog.SetTitle(Arguments.GetString("Title"));
            dialog.SetMessage(Arguments.GetString("Message"));
            dialog.Indeterminate = Arguments.GetBoolean("Indeterminate");
            return dialog;
        }
    }
}