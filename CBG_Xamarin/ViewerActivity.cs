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
        private bool stopAll = false;
        private bool stopSome = false;

        //The data that we will get in from the generator acrivity
        private int originalInputVariance = 5;
        private float variance = 5;
        private float brick = 50;
        private float ore = 50;
        private float sheep = 50;
        private float wheat = 50;
        private float wood = 50;
        private float gold = 50;
        private string boardConfig = "base_3";
        private int numThreads = 1;

        async protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            Xamarin.Essentials.Platform.Init(this, savedInstanceState);
            // Set our view from the "main" layout resource
            SetContentView(Resource.Layout.activity_viewer);

            

            if(!(Intent.Extras is null))
            {
                variance = (float)Intent.Extras.GetInt("Variance");
                originalInputVariance = (int)variance;
                brick = (float)Intent.Extras.GetInt("Brick");
                ore = (float)Intent.Extras.GetInt("Ore");
                sheep = (float)Intent.Extras.GetInt("Sheep");
                wheat = (float)Intent.Extras.GetInt("Wheat");
                wood = (float)Intent.Extras.GetInt("Wood");
                gold = (float)Intent.Extras.GetInt("Gold");
                boardConfig = Intent.Extras.GetString("BoardConfig");
                numThreads = Intent.Extras.GetInt("NumThreads");
                
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
            tips.Visibility = ViewStates.Visible;
            tips.Text = "...Generating Map...";

            RelativeLayout menu = FindViewById<RelativeLayout>(Resource.Id.menuButtons);
            menu.SetBackgroundColor(Android.Graphics.Color.Black);









            //Wait for the board to be generated
            //Board testBoard = await Task.Run(() => generateBoard(variance, brick, ore, sheep, wheat, wood, gold, boardConfig));
            Board testBoard = await Task.Run(() => genMultipleBoards(variance, brick, ore, sheep, wheat, wood, gold, boardConfig, numThreads));

            //Intentional delay to make the transition from the Generator Activity to the loading screen smoother
            await Task.Delay(1000);

            //Once the board is generated, get rid of the loading icon
            LoadingIcon.Visibility = ViewStates.Gone;
            LoadingIcon.StopPlayback();
            menu.SetBackgroundColor(Android.Graphics.Color.White);
            tips.Text = "";
            tips.Visibility = ViewStates.Gone;

            //Get a variable for the main relative layout
            RelativeLayout r = FindViewById<RelativeLayout>(Resource.Id.board);

            if (stopAll)
            {
                Console.WriteLine("STOPPED BECAUSE USER HIT BACK BUTTON");
            }
            else
            {
                Console.WriteLine("YES!!!!!!!!!!!!");
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
                }
            }
        }
        public override void OnRequestPermissionsResult(int requestCode, string[] permissions, [GeneratedEnum] Android.Content.PM.Permission[] grantResults)
        {
            Xamarin.Essentials.Platform.OnRequestPermissionsResult(requestCode, permissions, grantResults);

            base.OnRequestPermissionsResult(requestCode, permissions, grantResults);
        }

        public Board genMultipleBoards(float variance, float brick, float ore, float sheep, float wheat, float wood, float gold, string boardConfig, int numBoards)
        {
            //Create an array of tasks that is numBoards long. Each task should run the generateBoard function
            Task<Board>[] boards = new Task<Board>[numBoards];
            for(int i = 0; i < numBoards; i++)
            {
                int id = i;
                boards[id] = Task.Run(() => generateBoard(variance, brick, ore, sheep, wheat, wood, gold, boardConfig, id));
            }

            //Wait for a task to be completed
            while(true)
            {
                //Loop through all the tasks
                for (int i = 0; i < numBoards; i++)
                {
                    //If a task is completed
                    if(boards[i].IsCompleted)
                    {
                        //Stop other tasks
                        stopSome = true;
                        Console.WriteLine("BOARD " + i + " COMPLETED!");
                        //Return the output from that task
                        return boards[i].Result;
                    }
                }
            }
        }


        //This function generates the board. It happens asynchronously
        //Input all necessary stuff to make board generation happen.
        //Outputs the board
        public Board generateBoard(float variance, float brick, float ore, float sheep, float wheat, float wood, float gold, string boardConfig, int my_id)
        {
            Console.WriteLine("STARTING BOARD GENERATION: " + my_id + ";");
            // Initialize stopwatch to measure elapsed time in board generation
            System.Diagnostics.Stopwatch stopwatch = new System.Diagnostics.Stopwatch();
            stopwatch.Start();

            /*float init_brick = brick;
            float init_ore = ore;
            float init_sheep = sheep;
            float init_wheat = wheat;
            float init_wood = wood;
            float init_gold = gold;*/

            //Ignore gold
            if (boardConfig[0] == 'b' || (boardConfig[0] == 's' && boardConfig[boardConfig.Length - 1] == '2'))
            {
                if (brick == 0 && ore == 0 && sheep == 0 && wheat == 0 && wood == 0)
                {
                    brick = ore = sheep = wheat = wood = 1;
                }
                else
                {
                    float total = (brick + ore + sheep + wheat + wood);
                    brick = brick / total;
                    ore = ore / total;
                    sheep = sheep / total;
                    wheat = wheat / total;
                    wood = wood / total;
                }
            }
            else//dont ignore gold
            {
                if (brick == 0 && ore == 0 && sheep == 0 && wheat == 0 && wood == 0 && gold == 0)
                {
                    brick = ore = sheep = wheat = wood = gold = 1;

                }
                else
                {
                    float total = (brick + ore + sheep + wheat + wood + gold);
                    brick = brick / total;
                    ore = ore / total;
                    sheep = sheep / total;
                    wheat = wheat / total;
                    wood = wood / total;
                    gold = gold / total;
                }
            }

            Console.WriteLine("REQUIREMENTS!!!!!! " + brick + " " + ore + " " + sheep + " " + wheat + " " + wood + " " + gold);

            Board board;
            do
            {
                // if time elapses, loosen requirements slightly and continue 
                if(stopwatch.ElapsedMilliseconds > 1000)
                {
                    System.Diagnostics.Debug.WriteLine("1s hit! Loosing requirements...");
                    System.Diagnostics.Debug.WriteLine("old: " + brick + " " + ore + " " + sheep + " " + wheat + " " + wood + " " + gold);

                    variance += 0.05F;


                    float avg = 0;
                    //Ignore gold
                    if (boardConfig[0] == 'b' || (boardConfig[0] == 's' && boardConfig[boardConfig.Length - 1] == '2'))
                    {
                        avg = (brick + ore + sheep + wheat + wood) / 5;
                    }
                    else//dont ignore gold
                    {
                        avg = (brick + ore + sheep + wheat + wood + gold) / 6;
                    }
                    
                    brick += (float)(brick > avg ? (-1 * (brick - avg) * 0.05) : ((avg - brick) * 0.05));
                    ore += (float)(ore > avg ? (-1 * (ore - avg) * 0.05) : ((avg - ore) * 0.05));
                    sheep += (float)(sheep > avg ? (-1 * (sheep - avg) * 0.05) : ((avg - sheep) * 0.05));
                    wheat += (float)(wheat > avg ? (-1 * (wheat - avg) * 0.05) : ((avg - wheat) * 0.05));
                    wood += (float)(wood > avg ? (-1 * (wood - avg) * 0.05) : ((avg - wood) * 0.05));
                    gold += (float)(gold > avg ? (-1 * (gold - avg) * 0.05) : ((avg - gold) * 0.05));
                    System.Diagnostics.Debug.WriteLine("new: " + brick + " " + ore + " " + sheep + " " + wheat + " " + wood + " " + gold);
                    stopwatch.Restart();
                }
                
                //TODO: probably load the board in the generator, and send the actual board to here once generated
                //For now, create an abritrary board for testing
                board = new Board(boardConfig);
            }
            while ((!stopAll && !stopSome) &&
                  (!analyzer.acceptable_variance(board, variance) ||
                  !analyzer.acceptable_distribution_tile(board, brick, ore, sheep, wheat, wood, gold) ||
                  !analyzer.no_6_8_adjacent(board)));

            /*float total_init_req = init_brick + init_ore + init_sheep + init_wheat + init_wood + init_gold;
            float total_end_req = brick + ore + sheep + wheat + wood + gold;
            float error = Math.Abs(init_brick / total_init_req - brick / total_end_req) + Math.Abs(init_ore / total_init_req - ore / total_end_req) +
                Math.Abs(init_sheep / total_init_req - sheep / total_end_req) + Math.Abs(init_wheat / total_init_req - wheat / total_end_req) +
                Math.Abs(init_wood / total_init_req - wood / total_end_req) + Math.Abs(init_gold / total_init_req - gold / total_end_req);
            System.Diagnostics.Debug.WriteLine("init req: " + init_brick / total_init_req + " " + init_ore / total_init_req + " " +
                init_sheep / total_init_req + " " + init_wheat / total_init_req + " " + init_wood / total_init_req);
            System.Diagnostics.Debug.WriteLine("end req: " + brick / total_end_req + " " + ore / total_end_req + " " +
                sheep / total_end_req + " " + wheat / total_end_req + " " + wood / total_end_req);
            System.Diagnostics.Debug.WriteLine("total error (% off desired model): " + error);*/

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
            stopAll = true;

            //Create an intent to launch the Generator Activity
            Intent generatorIntent = new Intent(this, typeof(GeneratorActivity));

            //Add the necessary data to the intent
            generatorIntent.PutExtra("Variance", originalInputVariance);
            generatorIntent.PutExtra("Brick", brick + 1);
            generatorIntent.PutExtra("Ore", ore + 1);
            generatorIntent.PutExtra("Sheep", sheep + 1);
            generatorIntent.PutExtra("Wheat", wheat + 1);
            generatorIntent.PutExtra("Wood", wood + 1);
            generatorIntent.PutExtra("Gold", gold + 1);
            generatorIntent.PutExtra("BoardConfig", boardConfig);
            generatorIntent.PutExtra("NumThreads", numThreads);

            //Start the activity
            StartActivity(generatorIntent);
        }
    }
}