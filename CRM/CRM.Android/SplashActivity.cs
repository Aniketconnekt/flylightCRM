using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Timers;
using Android.App;
using Android.Content;
using Android.Content.PM;
using Android.OS;
using Android.Runtime;
using Android.Support.V7.App;
using Android.Views;
using Android.Widget;
using Felipecsl.GifImageViewLibrary;

namespace CRM.Droid
{
    [Activity(Theme = "@style/splashTheme", MainLauncher = true, NoHistory = true, ScreenOrientation = ScreenOrientation.Portrait)]
    public class SplashActivity : AppCompatActivity
    {
        private GifImageView gifImageView;

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            // Create your application here
            //var mainIntent = new Intent(Application.Context, typeof(MainActivity));
            //if (Intent.Extras != null)
            //    mainIntent.PutExtras(Intent.Extras);
            //mainIntent.SetFlags(ActivityFlags.SingleTop);

            //StartActivity(mainIntent);

            SetContentView(Resource.Layout.SplashScreen);
            gifImageView = FindViewById<GifImageView>(Resource.Id.gifIamgeView);

            Stream input = Assets.Open("splash.gif");
            byte[] bytes = ConvertFileToByteArray(input);
            gifImageView.SetBytes(bytes);
            gifImageView.StartAnimation();

            Timer timer = new Timer();
            timer.Interval = 5000;
            timer.AutoReset = false;
            timer.Elapsed += Timer_Elapsed;
            timer.Start();
        }

        private void Timer_Elapsed(object sender, ElapsedEventArgs e)
        {
            StartActivity(new Intent(this, typeof(MainActivity)));
        }

        private byte[] ConvertFileToByteArray(Stream input)
        {
            byte[] buffer = new byte[16 * 1024];
            using(MemoryStream ms = new MemoryStream())
            {
                int read;
                while ((read = input.Read(buffer, 0, buffer.Length)) > 0)
                    ms.Write(buffer, 0, read);
                return ms.ToArray();
            }
        }

        protected override void OnResume()
        {
            base.OnResume();
        }
    }
}