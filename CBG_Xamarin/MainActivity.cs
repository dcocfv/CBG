using Android.App;
using Android.OS;
using Android.Support.V7.App;
using Android.Runtime;
using Android.Widget;
using System;
using System.Collections.Generic;

namespace CBG_Xamarin
{
    [Activity(Label = "@string/app_name", Theme = "@style/AppTheme", MainLauncher = true)]
    public class MainActivity : AppCompatActivity
    {
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            Xamarin.Essentials.Platform.Init(this, savedInstanceState);
            // Set our view from the "main" layout resource
            SetContentView(Resource.Layout.activity_main);

            //Test code for map saving and loading
            BoardGenerationConfig foo = new BoardGenerationConfig();
            foo.name = "TEST";
            foo.save_xml();
            BoardGenerationConfig bar = new BoardGenerationConfig();
            bar = BoardGenerationConfig.load_xml("TEST");
            Console.WriteLine("AAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAA");
            Console.WriteLine("Bar's name: " + bar.name);

            //TODO: get the actual board from the generator
            //Create an abritrary board for testing
            Board testBoard = new Board(-10, 10, -10, 10, -10, 10);

            //Get a variable for the main relative layout
            RelativeLayout r = FindViewById<RelativeLayout>(Resource.Id.main);

            //The "size" of the hexagons
            var size = 50;
            
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