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
using CRM.Droid.DI;
using CRM.Interfaces;

[assembly: Xamarin.Forms.Dependency(typeof(AppCloseMethod))]
namespace CRM.Droid.DI
{
    public class AppCloseMethod : IAppClosed
    {
        public void CloseApp()
        {
            Process.KillProcess(Process.MyPid());
        }
    }
}