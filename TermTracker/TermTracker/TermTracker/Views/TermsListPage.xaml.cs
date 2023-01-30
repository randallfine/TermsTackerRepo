using SQLite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TermTracker.Entities;
using TermTracker.HelperClasses;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace TermTracker.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class TermsListPage : ContentPage
    {
        public TermsListPage()
        {
        
            InitializeComponent();
        }

        protected override void OnAppearing()
        {

            base.OnAppearing();

            NotificationHelpers.AddAllNotifications();

            using (SQLiteConnection con = new SQLiteConnection(App.FilePath))
            {
                con.CreateTable<Term_DB>();


                var terms = con.Table<Term_DB>().ToList();


                termListView.ItemsSource = terms;
            }

        }



        public void AddTermToolBarItem_Clicked(object sender, EventArgs e)
        {
            Navigation.PushAsync(new AddTerm());
        }

        

        private void TermStackLayout_Tapped(object sender, EventArgs e)
        {
            var id = ((TappedEventArgs)e).Parameter;

            Navigation.PushAsync(new TermPage((int)id));
        }

       
    }
}