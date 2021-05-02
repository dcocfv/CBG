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
using System.Threading.Tasks;

namespace CBG_Xamarin
{
    [Activity(Label = "ViewerActivity")]
    public class ViewerActivity : Activity
    {
        //This tells the board generation thread to stop if the user uses the back button
        private bool stop = false;

        //The data that we will get in from the generator acrivity
        private float variance = 5;
        private float brick = 50;
        private float ore = 50;
        private float sheep = 50;
        private float wheat = 50;
        private float wood = 50;
        private string boardConfig = "base_3";

        async protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            Xamarin.Essentials.Platform.Init(this, savedInstanceState);
            // Set our view from the "main" layout resource
            SetContentView(Resource.Layout.activity_viewer);

            

            if(!(Intent.Extras is null))
            {
                variance = (float)Intent.Extras.GetInt("Variance");
                brick = (float)Intent.Extras.GetInt("Brick");
                ore = (float)Intent.Extras.GetInt("Ore");
                sheep = (float)Intent.Extras.GetInt("Sheep");
                wheat = (float)Intent.Extras.GetInt("Wheat");
                wood = (float)Intent.Extras.GetInt("Wood");
                boardConfig = Intent.Extras.GetString("BoardConfig");
                
                System.Diagnostics.Debug.WriteLine("brick: " + brick);
                System.Diagnostics.Debug.WriteLine("ore: " + ore);
                System.Diagnostics.Debug.WriteLine("sheep: " + sheep);
                System.Diagnostics.Debug.WriteLine("wheat: " + wheat);
                System.Diagnostics.Debug.WriteLine("wood: " + wood);
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
                //Go back to the previous activity
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

            VideoView LoadingIcon = FindViewById<VideoView>(Resource.Id.LoadingIcon);
            Android.Net.Uri uri = Android.Net.Uri.Parse("android.resource://"+PackageName+"/"+Resource.Raw.LoadingIcon2);
            LoadingIcon.SetVideoURI(uri);
            LoadingIcon.Start();
            string[] possibleTips =
            {
                "Raising Mountains",
                "Carving Canyons",
                "Shifting Tectonic Plates",
                "Adding Volcanos",
                "Removing Volcanos",
                "Filling Ocean",
                "Too Much Water, Draining Ocean",
                "Building Harbors",
                "Prospecting for Ore",
                "Plowing Fields",
                "Planting Wheat",
                "Watering Wheat",
                "Waiting for Wheat to Grow",
                "Still Waiting for Wheat to Grow",
                "Wheat Died; Planting more",
                "Breeding Sheep",
                "Shearing Sheep",
                "Feeding Wheat to Sheep",
                "Digging up Clay",
                "Turning Clay into Bricks",
                "Commiting Deforestation",
                "Planting New Trees"
            };

            Random randomNumberGenerator = new Random();
            TextView tips = FindViewById<TextView>(Resource.Id.Tips);
            LoadingIcon.Completion += async (sender, e) =>
            {
                LoadingIcon.Start();
                tips.Visibility = ViewStates.Gone;
                await Task.Delay(500);
                tips.Text = "..." + possibleTips[randomNumberGenerator.Next(0, possibleTips.Length)] + "...";
                await Task.Delay(500);
                tips.Visibility = ViewStates.Visible;
                await Task.Delay(1500);
                tips.Visibility = ViewStates.Gone;
                await Task.Delay(500);
                tips.Text = "..." + possibleTips[randomNumberGenerator.Next(0, possibleTips.Length)] + "...";
                await Task.Delay(500);
                tips.Visibility = ViewStates.Visible;
            };
            tips.Text = "...Generating Map...";

            RelativeLayout menu = FindViewById<RelativeLayout>(Resource.Id.menuButtons);
            menu.SetBackgroundColor(Android.Graphics.Color.Black);









            //Wait for the board to be generated
            Board testBoard = await Task.Run(() => generateBoard(variance, brick, ore, sheep, wheat, wood, boardConfig));

            //Intentional delay to make the transition from the Generator Activity to the loading screen smoother
            await Task.Delay(800);

            //Once the board is generated, get rid of the loading icon
            tips.Visibility = ViewStates.Gone;
            //Intential delay to make the transition from the loading screen to the board display smoother
            await Task.Delay(200);
            LoadingIcon.Visibility = ViewStates.Gone;
            LoadingIcon.StopPlayback();
            menu.SetBackgroundColor(Android.Graphics.Color.White);

            //Get a variable for the main relative layout
            RelativeLayout r = FindViewById<RelativeLayout>(Resource.Id.board);

            if (stop)
            {
                Console.WriteLine("STOPPED BECAUSE USER HIT BACK BUTTON");
            }
            else
            {
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
                    if (first || tile.Key.x_pos > max)
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
                for (int i = 1; i < max; i++)
                {
                    if (i % 2 == 0)
                    {
                        boardWidth += 1;
                    }

                    if (i % 2 == 1)
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
                int dimensions = (int)Math.Ceiling(width / boardWidth);

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
                        currentChit.TranslationX = (int)xPos + (dimensions / 3);
                        if (currentTile.Value.number.ToString().Length == 1)
                        {
                            currentChit.TranslationX += (dimensions / 10);
                        }
                        currentChit.TranslationY = (int)yPos + (dimensions / 5);

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

                    //If the tile is covered by fog (seafarers scenario 3)
                    if (currentTile.Key.fog)
                    {
                        //Create a new image and add it to the layout
                        ImageView fogImage = new ImageView(this);
                        r.AddView(fogImage);

                        //Move the hexagon into place
                        fogImage.TranslationX = (int)xPos;
                        fogImage.TranslationY = (int)yPos;

                        //Adjust max size of the tiles
                        fogImage.SetMaxHeight(dimensions);
                        fogImage.SetMaxWidth(dimensions);
                        fogImage.SetAdjustViewBounds(true);

                        //Set fog image
                        fogImage.SetImageResource(Resource.Drawable.FogPiece);

                        //Setup functionality for clicking fog tile
                        fogImage.Click += (sender, e) => { fogImage.Visibility = ViewStates.Gone; };
                    }
                }
            }
        }
        public override void OnRequestPermissionsResult(int requestCode, string[] permissions, [GeneratedEnum] Android.Content.PM.Permission[] grantResults)
        {
            Xamarin.Essentials.Platform.OnRequestPermissionsResult(requestCode, permissions, grantResults);

            base.OnRequestPermissionsResult(requestCode, permissions, grantResults);
        }

        //This function generates the board. It happens asynchronously
        //Input all necessary stuff to make board generation happen.
        //Outputs the board
        public Board generateBoard(float variance, float brick, float ore, float sheep, float wheat, float wood, string boardConfig)
        {
            // Initialize stopwatch to measure elapsed time in board generation
            System.Diagnostics.Stopwatch stopwatch = new System.Diagnostics.Stopwatch();
            stopwatch.Start();
            
            Board board;
            do
            {
                if (brick == 0 && ore == 0 && sheep == 0 && wheat == 0 && wood == 0)
                    brick = ore = sheep = wheat = wood = 1;
                    
                // if time elapses, loosen requirements slightly and continue 
                if(stopwatch.ElapsedMilliseconds > 5000)
                {
                    System.Diagnostics.Debug.WriteLine("5s hit! Loosing requirements...");
                    System.Diagnostics.Debug.WriteLine("old: " + brick + " " + ore + " " + sheep + " " + wheat + " " + wood);

                    variance += 1;

                    float avg = (brick + ore + sheep + wheat + wood) / 5;
                    brick += (float)(brick > avg ? (-1 * (brick - avg) * 0.25) : ((avg - brick) * 0.25));
                    ore += (float)(ore > avg ? (-1 * (ore - avg) * 0.25) : ((avg - ore) * 0.25));
                    sheep += (float)(sheep > avg ? (-1 * (sheep - avg) * 0.25) : ((avg - sheep) * 0.25));
                    wheat += (float)(wheat > avg ? (-1 * (wheat - avg) * 0.25) : ((avg - wheat) * 0.25));
                    wood += (float)(wood > avg ? (-1 * (wood - avg) * 0.25) : ((avg - wood) * 0.25));
                    System.Diagnostics.Debug.WriteLine("new: " + brick + " " + ore + " " + sheep + " " + wheat + " " + wood);
                    stopwatch.Restart();
                }
                
                //TODO: probably load the board in the generator, and send the actual board to here once generated
                //For now, create an abritrary board for testing
                board = new Board(boardConfig);
            }
            while (!stop &&
                  (!analyzer.acceptable_variance(board, variance) ||
                  !analyzer.acceptable_distribution_tile(board, brick, ore, sheep, wheat, wood) ||
                  !analyzer.no_6_8_adjacent(board)));

            return board;
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
            //Stop the board generation
            stop = true;

            //Create an intent to launch the Generator Activity
            Intent generatorIntent = new Intent(this, typeof(GeneratorActivity));

            //Add the necessary data to the intent
            generatorIntent.PutExtra("Variance", variance);
            generatorIntent.PutExtra("Brick", brick + 1);
            generatorIntent.PutExtra("Ore", ore + 1);
            generatorIntent.PutExtra("Sheep", sheep + 1);
            generatorIntent.PutExtra("Wheat", wheat + 1);
            generatorIntent.PutExtra("Wood", wood + 1);
            generatorIntent.PutExtra("BoardConfig", boardConfig);

            //Start the activity
            StartActivity(generatorIntent);
        }
    }
}