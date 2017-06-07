﻿using AppProtoype.Data;
using AppProtoype.Models;
using AppProtoype.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Xamarin.Forms;

namespace AppProtoype
{
    public class App : Application
    {

        public App()
        {
            // The root page of your application
            MainPage = new AppProtoype.MainPage ();
        }

        protected override void OnStart()
        {
            // Handle when your app starts
        }

        protected override void OnSleep()
        {
            // Handle when your app sleeps
        }

        protected override void OnResume()
        {
            // Handle when your app resumes
        }

        static UserDetailsDatabase database;
        public static UserDetailsDatabase Database
        {
            get
            {
                if (database == null)
                {
                    database = new UserDetailsDatabase();
                }
                return database;
            }
        }

    }
}
