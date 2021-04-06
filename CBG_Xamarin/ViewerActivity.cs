using Android.App;
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


            //Test code for map saving
            //Currently running the app twice from the same build sometimes crashes because of this saving twice
            //If you wish to generate multiple boards by restarting the app, build once, then comment out these two lines and build again
            BoardGenerationConfig foo = new BoardGenerationConfig("foo");
            foo.save_xml();

            int variance = 5;

            //Get data given from GeneratorActivity
            if(!(Intent.Extras is null))
            {
                variance = Intent.Extras.GetInt("Variance");
            }

            //TODO: move this to generator with board gen
            Board testBoard;
            do
            {
                //TODO: probably load the board in the generator, and send the actual board to here once generated
                //For now, create an abritrary board for testing
                testBoard = new Board("base_3-4");
            }
            while(!analyzer.acceptable_variance(testBoard, variance));

            //Get a variable for the main relative layout
            RelativeLayout r = FindViewById<RelativeLayout>(Resource.Id.board);

            //The size (height and width) in pixels of the images to be displayed on screen
            var dimensions = 200;

            //The "size" of the hexagons (used in positioning the hexes in the grid)
            var size = dimensions / 2;
            
            //Loop through all the tiles in the board
            foreach (KeyValuePair<HexPosition, Hex> currentTile in testBoard.tiles)
            {
                //Create a new image and add it to the layout
                ImageView currentHexImage = new ImageView(this);
                r.AddView(currentHexImage);

                //Set proper image file based on resource type
                var currentResource = currentTile.Value.type;
                switch (currentResource)
                {
                    case global::Resource.brick:
                        currentHexImage.SetImageResource(Resource.Drawable.BrickPiece);
                        break;
                    case global::Resource.ore:
                        currentHexImage.SetImageResource(Resource.Drawable.OrePiece);
                        break;
                    case global::Resource.sheep:
                        currentHexImage.SetImageResource(Resource.Drawable.WoolPiece);
                        break;
                    case global::Resource.wheat:
                        currentHexImage.SetImageResource(Resource.Drawable.GrainPiece);
                        break;
                    case global::Resource.wood:
                        currentHexImage.SetImageResource(Resource.Drawable.LumberPiece);
                        break;
                    case global::Resource.gold:
                        currentHexImage.SetImageResource(Resource.Drawable.test);
                        break;
                    case global::Resource.desert:
                        currentHexImage.SetImageResource(Resource.Drawable.DesertPiece);
                        break;
                    case global::Resource.harbor_brick:
                        currentHexImage.SetImageResource(Resource.Drawable.HarborPiece_Brick);
                        break;
                    case global::Resource.harbor_ore:
                        currentHexImage.SetImageResource(Resource.Drawable.HarborPiece_Ore);
                        break;
                    case global::Resource.harbor_sheep:
                        currentHexImage.SetImageResource(Resource.Drawable.HarborPiece_Wool);
                        break;
                    case global::Resource.harbor_wheat:
                        currentHexImage.SetImageResource(Resource.Drawable.HarborPiece_Grain);
                        break;
                    case global::Resource.harbor_wood:
                        currentHexImage.SetImageResource(Resource.Drawable.HarborPiece_Lumber);
                        break;
                    case global::Resource.harbor_any:
                        currentHexImage.SetImageResource(Resource.Drawable.HarborPiece_QuestionMark);
                        break;
                    case global::Resource.sea:
                        currentHexImage.SetImageResource(Resource.Drawable.HarborPiece);
                        break;
                    default:
                        currentHexImage.SetImageResource(Resource.Drawable.test);
                        break;
                }

                //Rotate harbor pieces
                currentHexImage.Rotation = currentTile.Key.direction * 60;

                //Do hex to pixel conversion
                //var xPos = size * (Math.Sqrt(3) * currentTile.Key.x_pos + (Math.Sqrt(3) / 2) * currentTile.Key.z_pos);
                //var yPos = size * ((3.0 / 2) * currentTile.Key.z_pos);
                var xPos = size * ((3.0 / 2) * currentTile.Key.x_pos);
                var yPos = size * ((Math.Sqrt(3) / 2) * currentTile.Key.x_pos + (Math.Sqrt(3)) * currentTile.Key.z_pos);

                //Move the hexagon into place
                currentHexImage.TranslationX = (int)xPos;
                currentHexImage.TranslationY = (int)yPos;

                //Adjust max size of the tiles
                currentHexImage.SetMaxHeight(dimensions);
                currentHexImage.SetMaxWidth(dimensions);
                currentHexImage.SetAdjustViewBounds(true);

                //If the tile produces anything
                if (currentTile.Value.number != 0)
                {

                    //Create a new textView and add it to the layout
                    TextView currentChit = new TextView(this);
                    r.AddView(currentChit);

                    //Set the number of the chit
                    currentChit.SetText(currentTile.Value.number.ToString().ToCharArray(), 0, currentTile.Value.number.ToString().Length);

                    //Set the location of the chit
                    currentChit.TranslationX = (int)xPos + (dimensions/3);
                    if (currentTile.Value.number.ToString().Length == 1)
                    {
                        currentChit.TranslationX += (dimensions/10);
                    }
                    currentChit.TranslationY = (int)yPos + (dimensions/5);

                    //Scale the chit
                    currentChit.SetTextSize(Android.Util.ComplexUnitType.Px, dimensions / (float)2.7);

                    //Set color
                    if (currentTile.Value.number == 8 || currentTile.Value.number == 6)
                    {
                        currentChit.SetTextColor(Android.Graphics.Color.Red);
                    }
                    else
                    {
                        currentChit.SetTextColor(Android.Graphics.Color.LightGray);
                    }
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