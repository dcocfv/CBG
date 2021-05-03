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
    [Activity(Label = "Catan Board Generator", MainLauncher = true)]
    public class SaveLoadActivity : Activity
    {
        //We only want to start loading the config once, so we will use this variable to ensure that happens only once
        bool loading = false;

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            Xamarin.Essentials.Platform.Init(this, savedInstanceState);
            // Set our view from the "main" layout resource
            SetContentView(Resource.Layout.activity_saveload);

            //When the NumberPlayers radio group is changed
            RadioGroup NumberPlayers = FindViewById<RadioGroup>(Resource.Id.NumberPlayers);
            NumberPlayers.CheckedChange += (sender, e) =>
            {
                //Make those radio buttons invisible
                NumberPlayers.Visibility = ViewStates.Gone;

                //Display the chosen option
                TextView NumberPlayersText = FindViewById<TextView>(Resource.Id.NumberPlayersText);
                for(int i = 0; i < NumberPlayers.ChildCount; i++)
                {
                    RadioButton currentRadioButton = (RadioButton)NumberPlayers.GetChildAt(i);
                    if(currentRadioButton.Checked)
                    {
                        int n = i + 3;
                        NumberPlayersText.Text = "Number of Players: " + n;
                    }
                }

                //Show the next prompt
                TextView GameTypeText = FindViewById<TextView>(Resource.Id.GameTypeText);
                GameTypeText.Visibility = ViewStates.Visible;

                RadioGroup GameType = FindViewById<RadioGroup>(Resource.Id.GameType);
                GameType.Visibility = ViewStates.Visible;
            };


            //When the GameType radio group is changed
            RadioGroup GameType = FindViewById<RadioGroup>(Resource.Id.GameType);
            GameType.CheckedChange += (sender, e) =>
            {
                //Make those radio buttons invisible
                GameType.Visibility = ViewStates.Gone;

                //Display the chosen option
                TextView GameTypeText = FindViewById<TextView>(Resource.Id.GameTypeText);
                for (int i = 0; i < GameType.ChildCount; i++)
                {
                    RadioButton currentRadioButton = (RadioButton)GameType.GetChildAt(i);
                    if (currentRadioButton.Checked && i == 0)
                    {
                        GameTypeText.Text = "Game Type: Base";

                        //Display the confirm board config button
                        Button LoadConfig = FindViewById<Button>(Resource.Id.LoadConfig);
                        LoadConfig.Visibility = ViewStates.Visible;
                    }
                    else if (currentRadioButton.Checked && i == 1)
                    {
                        GameTypeText.Text = "Game Type: Seafarers";

                        //Get the radio group with the scenario radio buttons in it
                        RadioGroup Scenario = FindViewById<RadioGroup>(Resource.Id.Scenario);
                        TextView ScenarioText = FindViewById<TextView>(Resource.Id.ScenarioText);
                        
                        //Set the scenario text and radio buttons to be visible
                        ScenarioText.Visibility = ViewStates.Visible;
                        Scenario.Visibility = ViewStates.Visible;

                        //Set the specific scenario radio buttons to be clickable
                        for (int j = 0; j < Scenario.ChildCount; j++)
                        {
                            View v = Scenario.GetChildAt(j);
                            v.Clickable = true;
                        }
                    }
                }
            };







            //When the Scenario radio group is changed
            RadioGroup Scenario = FindViewById<RadioGroup>(Resource.Id.Scenario);
            Scenario.CheckedChange += (sender, e) =>
            {
                //Make those radio buttons invisible
                Scenario.Visibility = ViewStates.Gone;

                //Display the chosen option
                TextView ScenarioText = FindViewById<TextView>(Resource.Id.ScenarioText);
                for (int i = 0; i < Scenario.ChildCount; i++)
                {
                    RadioButton currentRadioButton = (RadioButton)Scenario.GetChildAt(i);
                    if (currentRadioButton.Checked)
                    {
                        switch (i)
                        {
                            case (0):
                                ScenarioText.Text = "Scenario 1: Heading to New Shores";
                                break;
                            case (1):
                                ScenarioText.Text = "Scenario 2: The Four Islands";
                                break;
                            case (2):
                                ScenarioText.Text = "Scenario 3: The Fog Islands";
                                break;
                            case (3):
                                ScenarioText.Text = "Scenario 4: Through the Desert";
                                break;
                            case (4):
                                ScenarioText.Text = "Scenario 5: The Forgotten Tribe";
                                break;
                            case (5):
                                ScenarioText.Text = "Scenario 6: Cloth for Catan";
                                break;
                            case (6):
                                ScenarioText.Text = "Scenario 7: The Pirate Islands";
                                break;
                            case (7):
                                ScenarioText.Text = "Scenario 8: The Wonders of Catan";
                                break;
                            case (8):
                                ScenarioText.Text = "Scenario 9: New World";
                                break;
                        }
                    }
                }

                //Display the confirm board config button
                Button LoadConfig = FindViewById<Button>(Resource.Id.LoadConfig);
                LoadConfig.Visibility = ViewStates.Visible;
            };



            //Do reset button things
            ImageButton resetButton = FindViewById<ImageButton>(Resource.Id.resetButton);
            resetButton.SetImageResource(Resource.Drawable.ResetButton);
            resetButton.Click += (sender, e) =>
            {
                Finish();
                OverridePendingTransition(0, 0);
                StartActivity(Intent);
                OverridePendingTransition(0, 0);
            };


            //Get the LoadConfig button
            Button LoadConfig = FindViewById<Button>(Resource.Id.LoadConfig);
            //When we click this button
            LoadConfig.Click += (sender, e) =>
            {
                if (!loading)
                {
                    //Make sure we don't load the config and start the generator activity twice
                    loading = true;

                    //Create an intent to launch the Generator Activity
                    Intent generatorIntent = new Intent(this, typeof(GeneratorActivity));

                    //Add the necessary data to the intent
                    generatorIntent.PutExtra("BoardConfig", createBoardConfig());

                    //Start the activity
                    StartActivity(generatorIntent);

                    //close this activity
                    Finish();
                }
            };
        }

        private string createBoardConfig()
        {
            //Get the player count
            int playerCount = 3;
            RadioGroup NumberPlayers = FindViewById<RadioGroup>(Resource.Id.NumberPlayers);
            for(int i = 0; i < NumberPlayers.ChildCount; i++)
            {
                RadioButton v = (RadioButton)NumberPlayers.GetChildAt(i);
                if(v.Checked)
                {
                    playerCount = i + 3;
                }
            }

            //Get the game type
            string[] gameTypes = {"base", "seafarers"};
            string gameType = "base";
            RadioGroup GameType = FindViewById<RadioGroup>(Resource.Id.GameType);
            for (int i = 0; i < GameType.ChildCount; i++)
            {
                RadioButton v = (RadioButton)GameType.GetChildAt(i);
                if (v.Checked)
                {
                    gameType = gameTypes[i];
                }
            }

            //Get the Scenario Number
            int scenarioNumber = 1;
            RadioGroup Scenario = FindViewById<RadioGroup>(Resource.Id.Scenario);
            for (int i = 0; i < Scenario.ChildCount; i++)
            {
                RadioButton v = (RadioButton)Scenario.GetChildAt(i);
                if (v.Checked)
                {
                    scenarioNumber = i + 1;
                }
            }

            if(gameType == "base")
            {
                return gameType + "_" + playerCount;
            }

            return gameType + "_" + playerCount + "_" + scenarioNumber;
        }

        //This function overrides the behavoir for when the native Android back button is pressed
        public override void OnBackPressed()
        {
            //close the activity
            Finish();
        }
    }
}