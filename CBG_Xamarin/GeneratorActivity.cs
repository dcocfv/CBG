using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CBG_Xamarin
{
    [Activity(Label = "GeneratorActivity")]
    public class GeneratorActivity : Activity
    {
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            Xamarin.Essentials.Platform.Init(this, savedInstanceState);
            // Set our view from the "main" layout resource
            SetContentView(Resource.Layout.activity_generator);

            //Get the navigation buttons
            Button Viewer = FindViewById<Button>(Resource.Id.Viewer);
            Button Generator = FindViewById<Button>(Resource.Id.Generator);
            Button Stats = FindViewById<Button>(Resource.Id.Stats);
            Button Save_Load = FindViewById<Button>(Resource.Id.Save_Load);

            //Set their initial colors
            Viewer.SetBackgroundColor(Android.Graphics.Color.Gray);
            Generator.SetBackgroundColor(Android.Graphics.Color.DarkGray);
            Stats.SetBackgroundColor(Android.Graphics.Color.Gray);
            Save_Load.SetBackgroundColor(Android.Graphics.Color.Gray);

            //Add functionality to all the navigation buttons
            Viewer.Click += (sender, e) =>
            {
                Console.WriteLine("Viewer");
                StartActivity(typeof(ViewerActivity));
            };
            Stats.Click += (sender, e) =>
            {
                Console.WriteLine("Stats");
            };
            Save_Load.Click += (sender, e) =>
            {
                Console.WriteLine("Save_Load");
            };
        }
    }
}