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

            //When the Seafarers button's check is changed
            RadioButton Seafarers = FindViewById<RadioButton>(Resource.Id.SeafarersButton);
            Seafarers.CheckedChange += (sender, e) =>
            {
                //Get the radio group with the scenario radio buttons in it
                RadioGroup Scenario = FindViewById<RadioGroup>(Resource.Id.Scenario);
                TextView ScenarioText = FindViewById<TextView>(Resource.Id.ScenarioText);

                //If seafarers button was just checked
                if(Seafarers.Checked)
                {
                    //Set the scenario text and radio buttons to be visible
                    ScenarioText.Visibility = ViewStates.Visible;
                    Scenario.Visibility = ViewStates.Visible;

                    //Set the specific scenario radio buttons to be clickable
                    for(int i = 0; i < Scenario.ChildCount; i++)
                    {
                        View v = Scenario.GetChildAt(i);
                        v.Clickable = true;
                    }
                }
                //If the seafarers button was just unchecked
                else
                {
                    //Set the scenario text and radio buttons to be visible
                    ScenarioText.Visibility = ViewStates.Gone;
                    Scenario.Visibility = ViewStates.Gone;

                    //Set the specific scenario radio buttons to be clickable
                    for (int i = 0; i < Scenario.ChildCount; i++)
                    {
                        View v = Scenario.GetChildAt(i);
                        v.Clickable = false;
                    }
                }
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
    }
}