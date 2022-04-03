using Plugin.LocalNotifications;
using SQLite;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TermTracker.Entities;
using Xamarin.Forms;
using TermTracker.HelperClasses;

namespace TermTracker
{
    public partial class MainPage : ContentPage
    {
        public MainPage()
        {
            InitializeComponent();
        }

        public void AddTermToolBarItem_Clicked(object sender, EventArgs e)
        {
            Navigation.PushAsync(new AddTerm());
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();

            NotificationHelpers.AddAllNotifications();

            using(SQLiteConnection con = new SQLiteConnection(App.FilePath))
            {
                con.CreateTable<Term_DB>();
               

                var terms = con.Table<Term_DB>().ToList();
               

                termListView.ItemsSource = terms;
            }
        }

        private void TermStackLayout_Tapped(object sender, EventArgs e)
        {
            var id = ((TappedEventArgs)e).Parameter;

            Navigation.PushAsync(new TermPage((int)id));
        }

    }
}
