using System;
using TermTracker.Views;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace TermTracker
{
    public partial class App : Application
    {
        public static string FilePath { get; set; }
        public static bool IsUserLoggedIn { get; set; }
        //public App()
        //{
        //    Device.SetFlags(new[] { "RadioButton_Experimental" });
        //    InitializeComponent();

        //    MainPage = new NavigationPage(new LoginPage());
        //}

        public App(string filePath)
        {
            Device.SetFlags(new[] { "RadioButton_Experimental" });
            FilePath = filePath;    
            InitializeComponent();

            if (!IsUserLoggedIn)
            {
                MainPage = new NavigationPage(new LoginPage());
            }
            else
            {
                MainPage = new NavigationPage(new MenuPage());
            }
        }

        protected override void OnStart()
        {
        }

        protected override void OnSleep()
        {
        }

        protected override void OnResume()
        {
        }
    }
}
