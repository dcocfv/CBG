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
                //Create an intent to launch the Viewer Activity
                Intent viewerIntent = new Intent(this, typeof(ViewerActivity));

                //Add the necessary data to the intent
                viewerIntent.PutExtra("NewBoard", false);
                viewerIntent.PutExtra("Variance", 5);

                //Start the activity
                StartActivity(viewerIntent);
            };
            Stats.Click += (sender, e) =>
            {
                Console.WriteLine("Stats");
            };
            Save_Load.Click += (sender, e) =>
            {
                Console.WriteLine("Save_Load");
            };

            //Add functionality to Resource Options buton
            Button ResourceOptions = FindViewById<Button>(Resource.Id.ResourceOptions);
            ResourceOptions.Click += (sender, e) =>
            {
                View[] views = new View[10];

                views[0] = FindViewById<TextView>(Resource.Id.Brick);
                views[1] = FindViewById<SeekBar>(Resource.Id.BrickBar);

                views[2] = FindViewById<TextView>(Resource.Id.Ore);
                views[3] = FindViewById<SeekBar>(Resource.Id.OreBar);

                views[4] = FindViewById<TextView>(Resource.Id.Sheep);
                views[5] = FindViewById<SeekBar>(Resource.Id.SheepBar);

                views[6] = FindViewById<TextView>(Resource.Id.Wheat);
                views[7] = FindViewById<SeekBar>(Resource.Id.WheatBar);

                views[8] = FindViewById<TextView>(Resource.Id.Wood);
                views[9] = FindViewById<SeekBar>(Resource.Id.WoodBar);

                for (int i = 0; i < views.Length; i++)
                {
                    if(views[i].Visibility == ViewStates.Visible)
                    {
                        views[i].Visibility = ViewStates.Gone;
                    }
                    else
                    {
                        views[i].Visibility = ViewStates.Visible;
                    }
                }
            };

            //Add functionality to Other Options buton
            Button OtherOptions = FindViewById<Button>(Resource.Id.OtherOptions);
            OtherOptions.Click += (sender, e) =>
            {
                View[] views = new View[2];

                views[0] = FindViewById<TextView>(Resource.Id.Variance);
                views[1] = FindViewById<SeekBar>(Resource.Id.VarianceBar);

                for (int i = 0; i < views.Length; i++)
                {
                    if (views[i].Visibility == ViewStates.Visible)
                    {
                        views[i].Visibility = ViewStates.Gone;
                    }
                    else
                    {
                        views[i].Visibility = ViewStates.Visible;
                    }
                }
            };

            //Get the generate board button
            Button GenerateBoard = FindViewById<Button>(Resource.Id.GenerateBoard);

            //Add functionality to the generate board button
            GenerateBoard.Click += (sender, e) =>
            {
                //Get the values of the sliders
                SeekBar BrickBar = FindViewById<SeekBar>(Resource.Id.BrickBar);
                Console.WriteLine("Brick: " + BrickBar.Progress);

                SeekBar OreBar = FindViewById<SeekBar>(Resource.Id.OreBar);
                Console.WriteLine("Ore: " + OreBar.Progress);

                SeekBar SheepBar = FindViewById<SeekBar>(Resource.Id.SheepBar);
                Console.WriteLine("Sheep: " + SheepBar.Progress);

                SeekBar WheatBar = FindViewById<SeekBar>(Resource.Id.WheatBar);
                Console.WriteLine("Wheat: " + WheatBar.Progress);

                SeekBar WoodBar = FindViewById<SeekBar>(Resource.Id.WoodBar);
                Console.WriteLine("Wood: " + WoodBar.Progress);

                SeekBar VarianceBar = FindViewById<SeekBar>(Resource.Id.VarianceBar);
                Console.WriteLine("Variance: " + VarianceBar.Progress + 3);

                //Create an intent to launch the Viewer Activity
                Intent viewerIntent = new Intent(this, typeof(ViewerActivity));

                //Add the necessary data to the intent
                viewerIntent.PutExtra("NewBoard", true);
                viewerIntent.PutExtra("Variance", VarianceBar.Progress + 3);

                //Start the activity
                StartActivity(viewerIntent);
            };
        }
    }
}