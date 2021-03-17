﻿using Android.App;
using Android.OS;
using Android.Support.V7.App;
using Android.Runtime;
using Android.Widget;
using System;
using System.Collections.Generic;
using Android.Views;

namespace CBG_Xamarin
{
    [Activity(Label = "ViewerActivity", MainLauncher = true)]
    public class ViewerActivity : Activity
    {
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            Xamarin.Essentials.Platform.Init(this, savedInstanceState);
            // Set our view from the "main" layout resource
            SetContentView(Resource.Layout.activity_viewer);

            //Get the navigation buttons
            Button Viewer = FindViewById<Button>(Resource.Id.Viewer);
            Button Generator = FindViewById<Button>(Resource.Id.Generator);
            Button Stats = FindViewById<Button>(Resource.Id.Stats);
            Button Save_Load = FindViewById<Button>(Resource.Id.Save_Load);

            //Set their initial colors
            Viewer.SetBackgroundColor(Android.Graphics.Color.DarkGray);
            Generator.SetBackgroundColor(Android.Graphics.Color.Gray);
            Stats.SetBackgroundColor(Android.Graphics.Color.Gray);
            Save_Load.SetBackgroundColor(Android.Graphics.Color.Gray);

            //Add functionality to all the navigation buttons
            Generator.Click += (sender, e) =>
            {
                Console.WriteLine("Generator");
                StartActivity(typeof(GeneratorActivity));
            };
            Stats.Click += (sender, e) =>
            {
                Console.WriteLine("Stats");
            };
            Save_Load.Click += (sender, e) =>
            {
                Console.WriteLine("Save_Load");
            };


            //Test code for map saving and loading
            /*BoardGenerationConfig foo = new BoardGenerationConfig();
            foo.name = "TEST";
            foo.save_xml();
            BoardGenerationConfig bar = new BoardGenerationConfig();
            bar = BoardGenerationConfig.load_xml("TEST");
            Console.WriteLine("AAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAA");
            Console.WriteLine("Bar's name: " + bar.name);*/

            //TODO: get the actual board from the generator
            //Create an abritrary board for testing
            Board testBoard = new Board(-10, 10, -10, 10, -10, 10);

            //Get a variable for the main relative layout
            RelativeLayout r = FindViewById<RelativeLayout>(Resource.Id.board);

            //The "size" of the hexagons
            var size = 30;
            
            //Loop through all the tiles in the board
            foreach (KeyValuePair<HexPosition, Hex> currentTile in testBoard.tiles)
            {
                //Create a new image and add it to the layout
                ImageView currentHexImage = new ImageView(this);
                r.AddView(currentHexImage);

                //TODO: set proper image file based on resource type
                currentHexImage.SetImageResource(Resource.Drawable.test);

                //Do hex to pixel conversion
                var xPos = size * (Math.Sqrt(3) * currentTile.Key.x_pos + (Math.Sqrt(3) / 2) * currentTile.Key.z_pos);
                var yPos = size * ((3.0 / 2) * currentTile.Key.z_pos);

                //Move the hexagon into place
                currentHexImage.TranslationX = (int)xPos;
                currentHexImage.TranslationY = (int)yPos;

                //Create a new image and add it to the layout
                TextView currentChit = new TextView(this);
                r.AddView(currentChit);

                //Set the number of the chit
                currentChit.SetText(currentTile.Value.number.ToString().ToCharArray(),0,1);

                //Set the location of the chit
                currentChit.TranslationX = (int)xPos + 12;
                currentChit.TranslationY = (int)yPos;

                //Set color
                if(currentTile.Value.number == 8 || currentTile.Value.number == 6)
                {
                    currentChit.SetTextColor(Android.Graphics.Color.Red);
                }
            }
        }
        public override void OnRequestPermissionsResult(int requestCode, string[] permissions, [GeneratedEnum] Android.Content.PM.Permission[] grantResults)
        {
            Xamarin.Essentials.Platform.OnRequestPermissionsResult(requestCode, permissions, grantResults);

            base.OnRequestPermissionsResult(requestCode, permissions, grantResults);
        }
    }
}