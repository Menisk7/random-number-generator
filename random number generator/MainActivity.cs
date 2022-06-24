using System;
using System.ComponentModel;
using Android.Animation;
using Android.App;
using Android.Content.PM;
using Android.Graphics.Drawables;
using Android.OS;
using Android.Runtime;
using Android.Support.V7.App;
using Android.Views;
using Android.Widget;
using Android.Gms.Ads;
using Android.Content.Res;
using GR.Net.Maroulis.Library;
using Android.Graphics;
using System.Drawing;
using Xamarin.Forms;

namespace random_number_generator
{
    [DesignTimeVisible(true)]
    [Activity(Theme = "@style/AppTheme.NoActionBar", Icon = "@drawable/icon", MainLauncher = true, ScreenOrientation = ScreenOrientation.Portrait)]

    public class MainActivity : AppCompatActivity
    {
        TextView FromText;
        TextView ToText;
        TextView ResultText;
        AdView mAdView;
        

        


        protected override void OnCreate(Bundle savedInstanceState)
        {

            base.OnCreate(savedInstanceState);



            var metrics = Resources.DisplayMetrics;
            int ScreenWidth = ConvertPixelsToDp(metrics.WidthPixels);
            int ScreenHeight = ConvertPixelsToDp(metrics.HeightPixels);



            if (ScreenWidth > 720)
            {
                //SetContentView(Resource.Layout.content_main);
                SetContentView(Resource.Layout.content_main_tablet);
            }
            if (ScreenWidth <= 400)
            {
                SetContentView(Resource.Layout.content_main_small);
            }
            if (ScreenWidth > 400 && ScreenWidth <= 720)
            {
                //SetContentView(Resource.Layout.content_main_tablet);
                SetContentView(Resource.Layout.content_main);
            }
            int ConvertPixelsToDp(float pixelValue)
            {
                var dp = (int)((pixelValue) / Resources.DisplayMetrics.Density);
                return dp;
            }






            
            LinearLayout root_layout = (LinearLayout)FindViewById<LinearLayout>(Resource.Id.root_layout);
            var animDrawable = root_layout.Background as AnimationDrawable;
            animDrawable.SetEnterFadeDuration(10);
            animDrawable.SetExitFadeDuration(5000);
            animDrawable.Start();
            


            mAdView = FindViewById<AdView>(Resource.Id.adView);

            var adRequest = new AdRequest.Builder().Build();

            mAdView.LoadAd(adRequest);



            
            

            FromText = FindViewById<TextView>(Resource.Id.FromText);
            ToText = FindViewById<TextView>(Resource.Id.ToText);
            ResultText = FindViewById<TextView>(Resource.Id.ResultText);

            Android.Widget.Button GenBtn = FindViewById<Android.Widget.Button>(Resource.Id.GenBtn);
            GenBtn.Click += GenBtn_Click;

        }

            void GenBtn_Click(object sender, EventArgs e)
            {
                Random ran = new Random();
                try
                {
                


                    ResultText.TextSize = 60;

                    int low = 0;
                    int high = 0;
                    int x = 0;

                    low = Convert.ToInt32(FromText.Text);
                    high = Convert.ToInt32(ToText.Text);
                    if (low >= high)
                    {
                        x = low;
                        low = high;
                        high = x;

                    }


                    genAnim(low, Convert.ToInt32(ran.Next(low, high + 1)));


                }
                catch (Exception ex)
                {
                    ResultText.TextSize = 24;
                    ResultText.Text = ex.Message;


                }

            }

            void genAnim(int fromText, int result)
            {


                var valueAnimator = ValueAnimator.OfInt(fromText, result);
                valueAnimator.SetDuration(300); // 300 miliseconds
                valueAnimator.Start();
                int newValue = fromText;
                valueAnimator.Update += (object sender, ValueAnimator.AnimatorUpdateEventArgs e) =>
                {
                    newValue = (int)e.Animation.AnimatedValue;
                    ResultText.Text = Convert.ToString(newValue);
                };






            }



            public override void OnRequestPermissionsResult(int requestCode, string[] permissions, [GeneratedEnum] Android.Content.PM.Permission[] grantResults)
            {
                Xamarin.Essentials.Platform.OnRequestPermissionsResult(requestCode, permissions, grantResults);

                base.OnRequestPermissionsResult(requestCode, permissions, grantResults);
            }
    }
}

