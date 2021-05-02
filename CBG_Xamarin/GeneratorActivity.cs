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
        //We only want to start generating the board once, so we will use this variable to ensure that happens only once
        bool generating = false;

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            Xamarin.Essentials.Platform.Init(this, savedInstanceState);
            // Set our view from the "main" layout resource
            SetContentView(Resource.Layout.activity_generator);

            //the board config string
            string boardConfig = "base_3";

            //Get data input into this activity
            if (!(Intent.Extras is null))
            {
                //Get the VarianceBar Progress and update it accordingly
                int variance = Intent.Extras.GetInt("Variance");
                int brick = (int)Intent.Extras.GetFloat("Brick");
                int ore = (int)Intent.Extras.GetFloat("Ore");
                int wheat = (int)Intent.Extras.GetFloat("Wheat");
                int sheep = (int)Intent.Extras.GetFloat("Sheep");
                int wood = (int)Intent.Extras.GetFloat("Wood");
                int gold = (int)Intent.Extras.GetFloat("Gold");
                int numThreads = Intent.Extras.GetInt("NumThreads");
                if (variance != 0)
                {
                    Console.WriteLine("VARIANCE INPUT GENERATOR: " + variance);
                    SeekBar VarianceBar = FindViewById<SeekBar>(Resource.Id.VarianceBar);
                    VarianceBar.Progress = variance - 5;
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
                if (gold != 0)
                {
                    gold -= 1;
                    Console.WriteLine("GOLD INPUT GENERATOR: " + gold);
                    SeekBar GoldBar = FindViewById<SeekBar>(Resource.Id.GoldBar);
                    GoldBar.Progress = gold;
                }

                //Get the board config string
                string s = Intent.Extras.GetString("BoardConfig");
                if(s != null)
                {
                    boardConfig = s;
                }

                if (numThreads != 0)
                {
                    TextView NumThreadsText = FindViewById<TextView>(Resource.Id.NumThreads);
                    NumThreadsText.Text = "Num Threads: " + numThreads;

                    Console.WriteLine("NUMTHREADS INPUT GENERATOR: " + numThreads);
                    SeekBar NumThreadsBar = FindViewById<SeekBar>(Resource.Id.NumThreadsBar);
                    NumThreadsBar.Progress = numThreads - 1;
                }
                else
                {
                    if (System.Environment.ProcessorCount >= 1 && System.Environment.ProcessorCount <= 10)
                    {
                        TextView NumThreadsText = FindViewById<TextView>(Resource.Id.NumThreads);
                        NumThreadsText.Text = "Num Threads: " + System.Environment.ProcessorCount;

                        SeekBar NumThreadsBar = FindViewById<SeekBar>(Resource.Id.NumThreadsBar);
                        NumThreadsBar.Progress = System.Environment.ProcessorCount - 1;
                    }
                }
            }

            //Get the back button
            ImageButton backButton = FindViewById<ImageButton>(Resource.Id.backButton);
            backButton.SetImageResource(Resource.Drawable.BackButton);

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
                //Go back to the generator activity
                loadPreviousActivity();
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
                View[] views = new View[12];

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

                views[10] = FindViewById<TextView>(Resource.Id.Gold);
                views[11] = FindViewById<SeekBar>(Resource.Id.GoldBar);

                for (int i = 0; i < views.Length; i++)
                {
                    if(i >= 10 && (boardConfig[0] == 'b' || (boardConfig[0] == 's' && boardConfig[boardConfig.Length - 1] == '2')))
                    {
                        break;
                    }
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

            //Update the Number of threads whenever it is changed
            SeekBar NumThreadsBar2 = FindViewById<SeekBar>(Resource.Id.NumThreadsBar);
            NumThreadsBar2.ProgressChanged += (sender, e) =>
            {
                TextView NumThreadsText2 = FindViewById<TextView>(Resource.Id.NumThreads);
                int temp = NumThreadsBar2.Progress + 1;
                NumThreadsText2.Text = "Num Threads: " + temp;
            };


            //Get the generate board button
            Button GenerateBoard = FindViewById<Button>(Resource.Id.GenerateBoard);

            //Add functionality to the generate board button
            GenerateBoard.Click += (sender, e) =>
            {
                if (!generating)
                {
                    //Make sure we don't load the viwer activity and generate the board twice
                    generating = true;

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

                    SeekBar GoldBar = FindViewById<SeekBar>(Resource.Id.GoldBar);
                    Console.WriteLine("Gold: " + GoldBar.Progress);

                    SeekBar VarianceBar = FindViewById<SeekBar>(Resource.Id.VarianceBar);
                    int varianceBarProgress = VarianceBar.Progress + 5;
                    Console.WriteLine("Variance: " + varianceBarProgress);

                    SeekBar NumThreadsBar = FindViewById<SeekBar>(Resource.Id.NumThreadsBar);
                    int numThreadsBarProgress = NumThreadsBar.Progress + 1;
                    Console.WriteLine("NumTHREADS: " + numThreadsBarProgress);

                    //Create an intent to launch the Viewer Activity
                    Intent viewerIntent = new Intent(this, typeof(ViewerActivity));

                    //Add the necessary data to the intent
                    viewerIntent.PutExtra("Variance", varianceBarProgress);
                    viewerIntent.PutExtra("Brick", BrickBar.Progress);
                    viewerIntent.PutExtra("Ore", OreBar.Progress);
                    viewerIntent.PutExtra("Sheep", SheepBar.Progress);
                    viewerIntent.PutExtra("Wheat", WheatBar.Progress);
                    viewerIntent.PutExtra("Wood", WoodBar.Progress);
                    viewerIntent.PutExtra("Gold", GoldBar.Progress);
                    viewerIntent.PutExtra("BoardConfig", boardConfig);
                    viewerIntent.PutExtra("NumThreads", numThreadsBarProgress);

                    //Start the activity
                    StartActivity(viewerIntent);
                }
            };
        }

        //This function overrides the behavoir for when the native Android back button is pressed
        public override void OnBackPressed()
        {
            //Go back to the generator activity
            loadPreviousActivity();
        }

        //This function goes back to the previous activity
        //It should be called when either the Android back button or the in app back button is pressed
        public void loadPreviousActivity()
        {
            //Create an intent to launch the SaveLoad Activity
            Intent saveLoadIntent = new Intent(this, typeof(SaveLoadActivity));

            //Start the activity
            StartActivity(saveLoadIntent);
        }
    }
}