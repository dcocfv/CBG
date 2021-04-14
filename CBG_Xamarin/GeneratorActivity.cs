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

            //Get data input into this activity
            if (!(Intent.Extras is null))
            {
                //Get the VarianceBar Progress and update it accordingly
                int variance = Intent.Extras.GetInt("Variance");
                int brick = Intent.Extras.GetInt("Brick");
                int ore = Intent.Extras.GetInt("Ore");
                int wheat = Intent.Extras.GetInt("Wheat");
                int sheep = Intent.Extras.GetInt("Sheep");
                int wood = Intent.Extras.GetInt("Wood");
                if (variance != 0)
                {
                    Console.WriteLine("VARIANCE INPUT GENERATOR: " + variance);
                    SeekBar VarianceBar = FindViewById<SeekBar>(Resource.Id.VarianceBar);
                    VarianceBar.Progress = variance - 3;
                }
                if(brick != 0)
                {
                    brick -= 1;
                    Console.WriteLine("BRICK INPUT GENERATOR: " + brick);
                    SeekBar BrickBar = FindViewById<SeekBar>(Resource.Id.BrickBar);
                    BrickBar.Progress = brick;
                }
                if(ore != 0)
                {
                    ore -= 1;
                    Console.WriteLine("ORE INPUT GENERATOR: " + ore);
                    SeekBar OreBar = FindViewById<SeekBar>(Resource.Id.OreBar);
                    OreBar.Progress = ore;
                }
                if(wheat != 0)
                {
                    wheat -= 1;
                    Console.WriteLine("WHEAT INPUT GENERATOR: " + wheat);
                    SeekBar WheatBar = FindViewById<SeekBar>(Resource.Id.WheatBar);
                    WheatBar.Progress = wheat;
                }
                if(sheep != 0)
                {
                    sheep -= 1;
                    Console.WriteLine("SHEEP INPUT GENERATOR: " + sheep);
                    SeekBar SheepBar = FindViewById<SeekBar>(Resource.Id.SheepBar);
                    SheepBar.Progress = sheep;
                }
                if(wood != 0)
                {
                    wood -= 1;
                    Console.WriteLine("WOOD INPUT GENERATOR: " + wood);
                    SeekBar WoodBar = FindViewById<SeekBar>(Resource.Id.WoodBar);
                    WoodBar.Progress = wood;
                }
            }

            //Get the back button
            ImageButton backButton = FindViewById<ImageButton>(Resource.Id.backButton);

            //Setup functionality for back button
            backButton.Click += (sender, e) =>
            {
                //Make this button not clickable and gone
                backButton.Clickable = false;
                backButton.Visibility = ViewStates.Gone;
                //Make the prompt visible and clickable
                TextView ContinuePrompt = FindViewById<TextView>(Resource.Id.ContinuePrompt);
                ContinuePrompt.Visibility = ViewStates.Visible;
                Button Yes = FindViewById<Button>(Resource.Id.Yes);
                Yes.Visibility = ViewStates.Visible;
                Yes.Clickable = true;
                Button No = FindViewById<Button>(Resource.Id.No);
                No.Visibility = ViewStates.Visible;
                No.Clickable = true;
            };

            //Get the yes button
            Button Yes = FindViewById<Button>(Resource.Id.Yes);
            //Add functionlity to it
            Yes.Click += (sender, e) =>
            {
                //Create an intent to launch the SaveLoad Activity
                Intent saveLoadIntent = new Intent(this, typeof(SaveLoadActivity));

                //TODO: Add the necessary data to the intent

                //Start the activity
                StartActivity(saveLoadIntent);
            };

            //Get the No button
            Button No = FindViewById<Button>(Resource.Id.No);
            //Setup functionaliy of this button
            No.Click += (sender, e) =>
            {
                //reset the state of this activity
                ImageButton backButton = FindViewById<ImageButton>(Resource.Id.backButton);
                backButton.Clickable = true;
                backButton.Visibility = ViewStates.Visible;
                TextView ContinuePrompt = FindViewById<TextView>(Resource.Id.ContinuePrompt);
                ContinuePrompt.Visibility = ViewStates.Gone;
                Yes.Visibility = ViewStates.Gone;
                Yes.Clickable = false;
                Button No = FindViewById<Button>(Resource.Id.No);
                No.Visibility = ViewStates.Gone;
                No.Clickable = false;
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
                int varianceBarProgress = VarianceBar.Progress + 3;
                Console.WriteLine("Variance: " + varianceBarProgress);

                //Create an intent to launch the Viewer Activity
                Intent viewerIntent = new Intent(this, typeof(ViewerActivity));

                //Add the necessary data to the intent
                viewerIntent.PutExtra("Variance", varianceBarProgress);
                viewerIntent.PutExtra("Brick", BrickBar.Progress);
                viewerIntent.PutExtra("Ore", OreBar.Progress);
                viewerIntent.PutExtra("Sheep", SheepBar.Progress);
                viewerIntent.PutExtra("Wheat", WheatBar.Progress);
                viewerIntent.PutExtra("Wood", WoodBar.Progress);

                //Start the activity
                StartActivity(viewerIntent);
            };
        }
    }
}