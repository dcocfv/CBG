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
    [Activity(Label = "SaveLoadActivity", MainLauncher = true)]
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

            //TODO: loop through all the saved configurations and create a button for each

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

                    //TODO: Add the necessary data to the intent

                    //Start the activity
                    StartActivity(generatorIntent);
                }
            };
        }
    }
}