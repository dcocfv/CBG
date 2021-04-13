using Android.App;
using Android.OS;
using Android.Support.V7.App;
using Android.Runtime;
using Android.Widget;
using System;
using System.Collections.Generic;
using Android.Views;
using Android.Content.Res;
using Android.Content;

namespace CBG_Xamarin
{
    [Activity(Label = "ViewerActivity")]
    public class ViewerActivity : Activity
    {
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            Xamarin.Essentials.Platform.Init(this, savedInstanceState);
            // Set our view from the "main" layout resource
            SetContentView(Resource.Layout.activity_viewer);

            //Test code for map saving
            //Currently running the app twice from the same build sometimes crashes because of this saving twice
            //If you wish to generate multiple boards by restarting the app, build once, then comment out these two lines and build again
            BoardGenerationConfig foo = new BoardGenerationConfig("foo");
            foo.save_xml();

            //Get data given from GeneratorActivity
            int variance = 5;
            int brick = 50;
            int ore = 50;
            int sheep = 50;
            int wheat = 50;
            int wood = 50;
            if(!(Intent.Extras is null))
            {
                variance = Intent.Extras.GetInt("Variance");
                brick = Intent.Extras.GetInt("Brick");
                ore = Intent.Extras.GetInt("Ore");
                sheep = Intent.Extras.GetInt("Sheep");
                wheat = Intent.Extras.GetInt("Wheat");
                wood = Intent.Extras.GetInt("Wood");
            }

            //Get the back button
            ImageButton backButton = FindViewById<ImageButton>(Resource.Id.backButton);

            //Setup functionality for back button
            backButton.Click += (sender, e) =>
            {
                //Create an intent to launch the Generator Activity
                Intent generatorIntent = new Intent(this, typeof(GeneratorActivity));

                //Add the necessary data to the intent
                generatorIntent.PutExtra("Variance", variance);
                generatorIntent.PutExtra("Brick", brick+1);
                generatorIntent.PutExtra("Ore", ore+1);
                generatorIntent.PutExtra("Sheep", sheep+1);
                generatorIntent.PutExtra("Wheat", wheat+1);
                generatorIntent.PutExtra("Wood", wood+1);

                //Start the activity
                StartActivity(generatorIntent);
            };

            //TODO: move this to generator with board gen
            Board testBoard;
            do
            {
                //TODO: probably load the board in the generator, and send the actual board to here once generated
                //For now, create an abritrary board for testing
                testBoard = new Board("base_3-4");
            }
            while(!analyzer.acceptable_variance(testBoard, variance) || 
                  !analyzer.acceptable_distribution_tile(testBoard, 5));

            //Get a variable for the main relative layout
            RelativeLayout r = FindViewById<RelativeLayout>(Resource.Id.board);

            //Get the width and height of our screen (in pixels)
            int width = Resources.DisplayMetrics.WidthPixels;
            int height = Resources.DisplayMetrics.HeightPixels;

            Console.WriteLine("WIDTH: " + width);
            Console.WriteLine("HEIGHT: " + height);

            //Find the max and min values in the x direction on the board
            int max = 0;
            int min = 0;
            bool first = true;

            foreach (KeyValuePair<HexPosition, Hex> tile in testBoard.tiles)
            {
                if(first || tile.Key.x_pos > max)
                {
                    first = false;
                    max = tile.Key.x_pos;
                }
                if (first || tile.Key.x_pos < min)
                {
                    first = false;
                    min = tile.Key.x_pos;
                }
            }

            //Calculate the board width (its complicated because of flat tops)
            double boardWidth = 1;
            for(int i = 1; i < max; i++)
            {
                if(i%2 == 0)
                {
                    boardWidth += 1;
                }

                if(i%2 == 1)
                {
                    boardWidth += 0.5;
                }
            }
            boardWidth += 0.75;
            for (int i = -1; i > min; i--)
            {
                if (((-1) * i) % 2 == 0)
                {
                    boardWidth += 1;
                }

                if (((-1) * i) % 2 == 1)
                {
                    boardWidth += 0.5;
                }
            }
            boardWidth += 0.75;

            Console.WriteLine("BOARDWIDTH: " + boardWidth);

            //The size (height and width) in pixels of the images to be displayed on screen (we celiing it so it doesn't go 1 pixel off screen)
            int dimensions = (int)Math.Ceiling(width/boardWidth);

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